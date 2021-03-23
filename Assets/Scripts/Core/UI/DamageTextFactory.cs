using System.Linq;
using RogueParty.Core.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core {
    public class DamageTextFactory : MonoBehaviour {
        private Camera _camera;
        [SerializeField] private GameObject damageNumberPrefab;
        private void Awake() => _camera = Camera.main;
        private void OnEnable() {
            FindObjectsOfType<ActorController>().ToList().ForEach(a => a.OnDamageText += CreateDamageNumber);
        }

        private void CreateDamageNumber(object sender, ActorController.DamageTextArgs e) {
            var senderGameObject = (ActorController) sender;
            var damagePosition = _camera.WorldToScreenPoint(senderGameObject.gameObject.transform.position);
            damagePosition.y += 20F;
            var damageNumber = Instantiate(damageNumberPrefab, damagePosition, Quaternion.identity, transform);
            damageNumber.GetComponent<Text>().text = e.DamageNumber;
        }
    }
}