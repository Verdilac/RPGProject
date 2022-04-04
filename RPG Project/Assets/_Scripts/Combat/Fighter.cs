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
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will Trigger the Hit() event
              GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;

                //Health healthComponent = target.GetComponent<Health>();
                //if (healthComponent == null)
                //{
                //    Debug.Log("Failed To Get Reference:healthComponent");
                //    healthComponent.TakeDamage(weaponDamage);
                //}

            } 
        }

        //Animation Hit Event
        void Hit()
        {

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
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;

        }
        
        
     

    }

}
