using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control 

{
    public class AIController : MonoBehaviour
    {

        //Object References 
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
       [SerializeField] PatrolPath patrolPath;
        NavMeshAgent navMeshAgent;
        


        //Complex Vars
        Vector3 guardPosition;


        //Primitives 
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float waypointDwellTime = 3f;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField]float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        [SerializeField] float waypointTolerance = 1f;
        private int currentWaypointIndex =  0;
       
        
        

     

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            guardPosition = transform.position;
        }

        public void Update()
        {

            if (!health.GetisAlive()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                

                AttackBehaviour();
            }

            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }

            else
            {
                PatrolBehaviour();
            }

            //becuase we are setting the timers to 0 at one point in code it doesnt really matter if we increment these time variables
            UpdateTimers();

        }










        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            navMeshAgent.speed = 2;
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if (AtWaypoint() ) 
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
            
        }


        private bool AtWaypoint()
        {
            
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;

        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
       
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);

        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            navMeshAgent.speed = 3;
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

