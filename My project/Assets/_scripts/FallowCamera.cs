using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCamera : MonoBehaviour
{

    [SerializeField] Transform _target;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = _target.position;
        
        
    }
}
