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
        Health target;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float  timeBetweenAttacks = 1f;
        float timeSinceLastAttack;
        float weaponDamage = 20;
        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!target.GetisAlive()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
                
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

            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will Trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;



            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Hit Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
          
            Debug.Log("Be Gone Thot");
            target = combatTarget.GetComponent<Health>();
            
        }

        public void Cancel()
        {
            Debug.Log("Cancelling Combat");
            StopAttack();
            target = null;

        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(CombatTarget combatTarget )
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && targetToTest.GetisAlive();     
        }

        
     

    }

}
