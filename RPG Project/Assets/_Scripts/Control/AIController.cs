using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control 

{
    public class AIController : MonoBehaviour
    {

        GameObject player;
        Fighter fighter;
       
        [SerializeField] float chaseDistance = 5f;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();

        }

        public void Update()
        {

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                Debug.Log(transform.name + "Need to Attack");
                fighter.Attack(player);
            }
            
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;

        }

    }
}

