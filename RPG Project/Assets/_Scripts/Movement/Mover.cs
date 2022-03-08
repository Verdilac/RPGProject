﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
   


        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
       
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

            //Gets the Velocity of world space which is quite complex for animator system and break it down to basic instructions digestable by Animator.
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {    
                navMeshAgent.isStopped = true;
          
        }

      
    }


}
