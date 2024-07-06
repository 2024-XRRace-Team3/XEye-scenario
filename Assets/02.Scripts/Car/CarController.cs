using System;
using UnityEngine;

namespace _02.Scripts.Car
{
    public class CarController : MonoBehaviour
    {
        public float speed = 1f;
        public bool crashed = false;
        
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
        }

        private void FixedUpdate() {
            if(!crashed) 
                rigidbody.velocity = transform.forward * speed;
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
        
            if (other.gameObject.CompareTag(nameof(Car)))
            {
                crashed = true;
                rigidbody.velocity = Vector3.zero;
                speed = 0;
            }
        }
    }
}
