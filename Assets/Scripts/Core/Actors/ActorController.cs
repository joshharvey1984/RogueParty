using System;
using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.Actors {
    public class ActorController : MonoBehaviour {
        private Renderer _renderer;
        public Actor actor;
        private ActorMove actorMove;
        public ActorStatus ActorStatus { get; private set; }
        public SelectionCircle SelectionCircle { get; private set; }
        private static readonly int Outline = Shader.PropertyToID("_OutlineAlpha");
        private static readonly int HitEffect = Shader.PropertyToID("_HitEffectBlend");
        protected GameObject EnemyTarget { get; set; }
        private float attackTimer;
        [SerializeField] private float attackRange = 0.8F;
        [SerializeField] private GameObject attackProjectile;
        private float refreshMoveTime = 0.1F;
        private GameObject navMarker;
        
        public event EventHandler<DamageTextArgs> OnDamageText;
        public event EventHandler OnScreenShake;
        public event EventHandler<AudioClipEventArgs> OnPlayAudioClip;

        public class DamageTextArgs { public string DamageNumber { get; set; } }
        public class AudioClipEventArgs { public string AudioClip { get; set; } }
        
        protected void Awake() {
            _renderer = GetComponent<Renderer>();
            actorMove = gameObject.GetComponent<ActorMove>();
            ActorStatus = gameObject.GetComponent<ActorStatus>();
            SelectionCircle = transform.GetChild(0).gameObject.GetComponent<SelectionCircle>();
        }

        protected void OnEnable() {
            ActorStatus.SetActor(actor);
        }

        protected void Update() {
            UpdateAttackTime();
            if (EnemyTarget) CheckEngagement();
        }
        
        private void UpdateAttackTime() {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) attackTimer = 0;
        }

        private void CheckEngagement() {
            if (Vector2.Distance(transform.position, EnemyTarget.transform.position) <= attackRange + 0.15F) {
                if (navMarker) Destroy(navMarker);
                AttackEnemy();
                return;
            }
            
            RefreshMove();
        }

        private void RefreshMove() {
            refreshMoveTime -= Time.deltaTime;
            if (refreshMoveTime > 0) return;
            refreshMoveTime = gameObject.CompareTag("Player") ? 0.1F : 0.5F;
            BeginMove(CalculateEnemyNavMarker(EnemyTarget));
        }
        
        protected void BeginMove(Vector2 position) {
            actorMove.Move(CreateNavMarker(position));
        }
        
        private void AttackEnemy() {
            actorMove.FaceTarget(EnemyTarget);
            if (attackTimer > 0F) return;
            if (!GetToggle(AbilityToggleName.CanAttack)) return;
            actorMove.AttackAnimation();
        }
        
        private void OnMouseEnter() => _renderer.material.SetFloat(Outline, 1F);
        private void OnMouseExit() => _renderer.material.SetFloat(Outline, 0F);
        
        public void AttackHit() {
            if (!EnemyTarget) return;
            attackTimer = 3.0F;
            EnemyTarget.GetComponent<ActorController>().TakeDamage(GetAttribute(AttributeName.Damage));
        }

        public void FireProjectile() {
            if (!EnemyTarget) return;
            attackTimer = 3.0F;
            var projectile = Instantiate(attackProjectile, transform.position, Quaternion.identity);
            var script = projectile.GetComponent<Projectile>();
            script.Target = EnemyTarget;
        }

        public void TakeDamage(int? damage = 0) {
            PlayAudioClip("weapon_blow");
            HitDamageFlash(0.1F);
            var damageTaken = damage - GetAttribute(AttributeName.DamageReduction);
            ActorStatus.TakeDamage(damageTaken);
            OnDamageText?.Invoke(this, new DamageTextArgs { DamageNumber = damageTaken.ToString() });
            OnScreenShake?.Invoke(this, EventArgs.Empty);
            actorMove.HitAnimation();
        }

        private void HitDamageFlash(float seconds) {
            _renderer.material.SetFloat(HitEffect, 1F);
            Invoke(nameof(StopDamageFlash), seconds);
        }

        public void ExecuteSkill(Skill skill) {
            skill.Trigger();
        }

        public int? GetAttribute(AttributeName attribute) => ActorStatus.ActorAttributes.Get(attribute);
        public bool GetToggle(AbilityToggleName toggle) => (bool)ActorStatus.AbilityToggles.GetToggle(toggle);

        private void StopDamageFlash() => _renderer.material.SetFloat(HitEffect, 0F);

        private void PlayAudioClip(string audioClip) {
            OnPlayAudioClip?.Invoke(this, new AudioClipEventArgs { AudioClip = audioClip });
        }
        
        private GameObject CreateNavMarker(Vector2 position) {
            navMarker = new GameObject { name = gameObject.name + " Nav", tag = "Nav" };
            navMarker.transform.position = position;
            if (!EnemyTarget) {
                var sr = navMarker.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>($"Sprites/NavMarker");
                sr.sortingLayerName = "Prop";
                sr.color = Color.green;
            }
            return navMarker;
        }
        
        private Vector2 CalculateEnemyNavMarker(GameObject enemy) {
            var enemyPosition = enemy.transform.position;
            var heroPosition = transform.position;
            var dist = Vector2.Distance(heroPosition, enemyPosition) - attackRange;

            var diff = enemyPosition - heroPosition;
            var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            
            var x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
            var y = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2 { x = heroPosition.x + x, y = heroPosition.y + y };
        }
    }
}