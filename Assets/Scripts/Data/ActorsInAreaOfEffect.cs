using System.Collections.Generic;
using System.Linq;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public static class ActorsInAreaOfEffect {
        public static List<ActorController> GetTargets(AreaOfEffectTypes areaOfEffectTypes, ActorController actorController, int range = 0) {
            if (areaOfEffectTypes == AreaOfEffectTypes.EnemiesInRange) return EnemiesInRange(actorController, range);
            return Self(actorController);
        }

        private static List<ActorController> Self(ActorController actorController) => 
            new List<ActorController> {actorController};

        private static List<ActorController> EnemiesInRange(ActorController actorController, int range) {
            return (from enemy in GameObject.FindGameObjectsWithTag("Enemy") 
                where Vector2.Distance(enemy.transform.position, actorController.gameObject.transform.position) <= range 
                select enemy.GetComponent<ActorController>()).ToList();
        }
    }
}