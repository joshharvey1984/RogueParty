using System.Collections.Generic;
using System.Linq;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public static class ActorsInAreaOfEffect {
        public static List<ActorController> GetTargets(AreaOfEffectTypes areaOfEffectTypes, GameObject actorController, float range = 0) {
            if (areaOfEffectTypes == AreaOfEffectTypes.EnemiesInRange) return EnemiesInRange(actorController, range);
            if (areaOfEffectTypes == AreaOfEffectTypes.AlliesInRange) return AlliesInRange(actorController, range);
            return Self(actorController);
        }

        private static List<ActorController> Self(GameObject actorController) => 
            new List<ActorController> {actorController.GetComponent<ActorController>()};

        private static List<ActorController> EnemiesInRange(GameObject actorController, float range) {
            return (from enemy in GameObject.FindGameObjectsWithTag("Enemy") 
                where Vector2.Distance(enemy.transform.position, actorController.gameObject.transform.position) <= range 
                select enemy.GetComponent<ActorController>()).ToList();
        }
        
        private static List<ActorController> AlliesInRange(GameObject actorController, float range) {
            return (from ally in GameObject.FindGameObjectsWithTag("Player") 
                where Vector2.Distance(ally.transform.position, actorController.gameObject.transform.position) <= range 
                select ally.GetComponent<ActorController>()).ToList();
        }
    }
}