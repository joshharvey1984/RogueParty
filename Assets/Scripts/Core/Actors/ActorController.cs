using System;
using System.Collections.Generic;
using RogueParty.Core.UI;
using RogueParty.Data;
using UnityEngine;
using static RogueParty.Core.Actors.AttributeName;

namespace RogueParty.Core.Actors {
    public class ActorController : MonoBehaviour {
        private Renderer _renderer;
        private Transform _transform;
        public Actor actor;
        private ActorMove actorMove;
        public ActorStatus actorStatus;
        public SelectionCircle SelectionCircle { get; private set; }
        private static readonly int Outline = Shader.PropertyToID("_OutlineAlpha");
        private static readonly int HitEffect = Shader.PropertyToID("_HitEffectBlend");
        private static readonly int MotionBlurEffect = Shader.PropertyToID("_MotionBlurDist");
        public GameObject EnemyTarget { get; set; }
        private float attackTimer;
        private float refreshMoveTime = 0.1F;
        private GameObject navMarker;
        private GameObject skillPopup;

        public event EventHandler<DamageTextArgs> OnDamageText;
        public event EventHandler OnScreenShake;
        public event EventHandler<AudioClipEventArgs> OnPlayAudioClip;

        public class DamageTextArgs { public string DamageNumber { get; set; } }
        public class AudioClipEventArgs { public string AudioClip { get; set; } }
        
        protected void Awake() {
            _renderer = GetComponent<Renderer>();
            _transform = transform;
            actor = GetComponent<Actor>();
            actorMove = gameObject.GetComponent<ActorMove>();
            actorStatus = gameObject.GetComponent<ActorStatus>();
            SelectionCircle = transform.GetChild(0).gameObject.GetComponent<SelectionCircle>();
            skillPopup = Resources.Load<GameObject>("Prefabs/UI/SkillPopup");
        }

        protected void OnEnable() {
            actorStatus.SetActor(actor);
        }

        protected void Update() {
            UpdateAttackTime();
            if (EnemyTarget) CheckEngagement();
        }

        public static int GetShaderPropertyID(string property) => Shader.PropertyToID(property);

        private void UpdateAttackTime() {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) attackTimer = 0;
        }

        private void CheckEngagement() {
            if (Vector2.Distance(transform.position, EnemyTarget.transform.position) <= GetAttackRange + 0.15F) {
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

        public void DirectMoveToObject(GameObject position, int speed) {
            if (navMarker) Destroy(navMarker);
            actorMove.DirectMove(position, speed);
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
            attackTimer = GetAttackTimer;
            EnemyTarget.GetComponent<ActorController>().TakeDamage(GetAttribute(Damage));
        }

        public void ProjectileHit(GameObject target) {
            target.GetComponent<ActorController>().TakeDamage(GetAttribute(Damage));
        }

        private float GetAttackTimer => 100 / Convert.ToSingle(GetAttribute(AttackSpeed));
        private float GetAttackRange => Convert.ToSingle(GetAttribute(AttackRange)) / 100;
        
        public void FireProjectile() {
            if (!EnemyTarget) return;
            attackTimer = GetAttackTimer;
            var projectile = Instantiate(actor.actorProjectile, transform.position, Quaternion.identity);
            var script = projectile.GetComponent<Projectile>();
            script.firedBy = this;
            script.Target = EnemyTarget;
        }

        public void FireSpecialProjectile(GameObject target, GameObject projectile, List<SkillBehaviour> projectileBehaviours) {
            var specialProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            var script = specialProjectile.GetComponent<Projectile>();
            script.Target = target;
            script.projectileBehaviours = projectileBehaviours;
        }

        private void TakeDamage(int? damage = 0) {
            PlayAudioClip("weapon_blow");
            HitDamageFlash(0.1F);
            var damageTaken = damage - GetAttribute(DamageReduction);
            actorStatus.TakeDamage(damageTaken);
            OnDamageText?.Invoke(this, new DamageTextArgs { DamageNumber = damageTaken.ToString() });
            OnScreenShake?.Invoke(this, EventArgs.Empty);
            actorMove.HitAnimation();
        }

        private void HitDamageFlash(float seconds) {
            _renderer.material.SetFloat(HitEffect, 1F);
            Invoke(nameof(StopDamageFlash), seconds);
        }

        public void ExecuteSkill(Skill skill) {
            if (skill.Trigger(this)) SkillPopup(skill);
        }

        private int? GetAttribute(AttributeName attribute) => actorStatus.ActorAttributes.Get(attribute);
        private bool GetToggle(AbilityToggleName toggle) => (bool)actorStatus.AbilityToggles.GetToggle(toggle);
        private void StopDamageFlash() => _renderer.material.SetFloat(HitEffect, 0F);

        private void SkillPopup(Skill skill) {
            var popup = Instantiate(skillPopup, _transform.position, Quaternion.identity, _transform);
            popup.GetComponent<SkillPopup>().SetSprite(skill.Icon);
        }

        private void PlayAudioClip(string audioClip) {
            OnPlayAudioClip?.Invoke(this, new AudioClipEventArgs { AudioClip = audioClip });
        }

        public void SetDeterioratingEffect(int effect, float amount) {
            _renderer.material.SetFloat(effect, amount);
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
            var dist = Vector2.Distance(heroPosition, enemyPosition) - (actor.attackRange / 100);

            var diff = enemyPosition - heroPosition;
            var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            
            var x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
            var y = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2 { x = heroPosition.x + x, y = heroPosition.y + y };
        }
    }
}
