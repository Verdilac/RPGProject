using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FallowCamera : MonoBehaviour
    {

        [SerializeField] Transform _target;


        //this gets rid of the null ref warning on the fallow camera target
        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }





        //the reason for setting to late update is sometimes camera starts moving before the charactor so this is just to 
        //controll the execution flow
        void LateUpdate()
        {
            transform.position = _target.position;


        }
    }
}
