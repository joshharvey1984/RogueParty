using System;
using System.Collections;
using RogueParty.Data;
using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core.UI {
    public class SkillButton : MonoBehaviour {
        private SpriteRenderer _renderer;
        [SerializeField] private Text keyButton;
        [SerializeField] private Text coolDownTime;
        private float coolDownLeft;

        [SerializeField] private Canvas canvas;

        public Skill Skill { get; private set; }

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            canvas = transform.parent.parent.parent.parent.GetComponentInChildren<Canvas>();
        }

        private void Update() {
            if (coolDownLeft > 0) {
                coolDownLeft -= Time.deltaTime;
                CoolDownText(coolDownLeft);
            }
            else coolDownLeft = 0;
        }

        public void SetSkill(Skill newSkill) {
            Skill = newSkill;
            _renderer.sprite = Resources.Load<Sprite>($"Sprites/UI/SkillIcons/{Skill.GetType().Name}");
            Skill.OnSkillUse.AddListener(CoolDownStart);
            var keyPos = gameObject.transform.position;
            keyPos.x += 0.45F;
            keyPos.y -= 0.35F;
            keyButton = canvas.GetComponent<CreateText>().CreateUIText(keyPos);
            keyButton.GetComponent<Text>().text = "Q";
            SkillActive(true);
        }

        public event EventHandler<SkillClickArgs> OnMouseClick;
        public class SkillClickArgs { public Skill SkillClicked { get; set; } }
        
        private void OnMouseUp() => OnMouseClick?.Invoke(this, new SkillClickArgs { SkillClicked = Skill });

        private void CoolDownStart(float coolDown) {
            StartCoroutine(Cooldown());
            IEnumerator Cooldown() {
                SkillActive(false);
                coolDownLeft = coolDown;
                yield return new WaitForSeconds(coolDown);
                SkillActive(true);
            }
        }

        private void SkillActive(bool active) {
            Skill.CanUse = active;
            KeyButton(active);
            Icon(active);
        }
        
        private void KeyButton(bool on) => keyButton.color = on ? Color.white : Color.gray;
        private void Icon(bool on) => _renderer.color = on ? Color.white : new Color(0.25F, 0.25F, 0.25F);
        private void CoolDownText(float coolDown) {
            if (coolDownTime) Destroy(coolDownTime.gameObject);
            coolDownTime = canvas.GetComponent<CreateText>().CreateUIText(gameObject.transform.position);
            var cd = (int) coolDown;
            if (coolDown > 0) coolDownTime.text = cd.ToString();
            else Destroy(coolDownTime.gameObject);
        }
    }
}