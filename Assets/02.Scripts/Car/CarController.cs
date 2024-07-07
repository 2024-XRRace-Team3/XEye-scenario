using System;
using Commons.Enums;
using Manager;
using UnityEngine;
using EventBus;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private bool crashed = false;
        [SerializeField] private AudioClip impactAudio;
        
        private Rigidbody rigidbody;
        private AudioSource audioSource;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventBus<ScenarioEvent>.Subscribe(ScenarioEvent.Crashed,Crash);
        }

        private void OnDisable()
        {
            EventBus<ScenarioEvent>.Unsubscribe(ScenarioEvent.Crashed,Crash);
        }

        private void FixedUpdate() 
        {
            if(!crashed) 
                rigidbody.velocity = transform.forward * speed;
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
        
            if (other.gameObject.CompareTag(nameof(Car)) && !crashed)
            {
                GameManager.Instance.stage = ScenarioStage.Crash;
                EventBus<ScenarioEvent>.Publish(ScenarioEvent.Crashed);
            }
        }
        
        private void Crash()
        {
            Debug.Log("Car Crash");
            audioSource.clip = impactAudio;
            audioSource.loop = false;
            audioSource.Play();
            crashed = true;
            rigidbody.velocity = Vector3.zero;
            speed = 0;
            GameManager.Instance.crashCars.Add(this.gameObject);
        }
    }
}
