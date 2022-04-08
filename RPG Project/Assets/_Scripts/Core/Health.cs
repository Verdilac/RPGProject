using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
public class Health : MonoBehaviour
{
     [SerializeField]   float healthPoints = 100f;
        private bool isAlive = true;  

        public bool GetisAlive()
        {
            return isAlive;
        }

     public void TakeDamage(float damage)
     {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0 )
            {
                Die();
            }
            Debug.Log(healthPoints);
     }

        private void Die()
        {
            if(isAlive == true)
            {
            GetComponent<Animator>().SetTrigger("die");
            isAlive = false;
                GetComponent<ActionScheduler>().CancelCurrentAction(); 

            }
            
        }
    }
}

