using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        Transform target;
        [SerializeField] float weaponRange = 2f;
        private void Update()
        {

            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
                
            }
            else
            {
                //maybe try to find a way to check if the movement is already cancelled so we are not calling this func repetetivly 
               
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();

            }

        }
        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Debug.Log("Be Gone Thot");
            target = combatTarget.transform;
            
        }

        public void Cancel()
        {
            Debug.Log("Cancelling Combat");
            target = null;

        }
        
        //Animation Hit Event
        void Hit()
        {

        }

    }

}
