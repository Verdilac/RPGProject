using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control 

{
    public class AIController : MonoBehaviour
    {

        GameObject player;
        Fighter fighter;
        Health health;
        Vector3 guardPosition;
        [SerializeField] float suspicionTime = 5f;
       
        [SerializeField] float chaseDistance = 5f; 
        float timeSinceLastSawPlayer = Mathf.Infinity;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

            guardPosition = transform.position;

           

        }

        public void Update()
        {

            if (!health.GetisAlive()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;

                AttackBehaviour();
            }

            else if (timeSinceLastSawPlayer<suspicionTime)
            {
                SuspicionBehaviour();
            }

            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            
        }

        private void GuardBehaviour()
        {
            GetComponent<Mover>().StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;

        }

        //Called by Unity

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }


    }
}

