using System;
using System.Collections;
using Commons.Enums;
using Manager;
using UnityEngine;
using EventBus;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        public bool isDummy = false; // 더미로 bot Car controller에서는 작동 안하게 처리
        [SerializeField] private float speed = 1f;
        [SerializeField] private bool crashed = false;
        [SerializeField] private AudioClip impactAudio;
        
        private Rigidbody rigidbody;
        private AudioSource audioSource;

        private float crashedTime;
        private Vector3 crashedVec;
        private Quaternion crashedRot;
        
        private Vector3 startVec;
        private Quaternion startRot;
        private Vector3 originVelocity;
        private float originSpeed;
        

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            startVec = transform.position;
            startRot = transform.rotation;

            originSpeed = speed;
            originVelocity = rigidbody.velocity;
        }

        private void OnEnable()
        {
            EventBus<ScenarioEvent>.Subscribe(ScenarioEvent.Crashed,Crash);
            EventBus<ScenarioEvent>.Subscribe(ScenarioEvent.Replay, RePlay);
        }

        private void OnDisable()
        {
            EventBus<ScenarioEvent>.Unsubscribe(ScenarioEvent.Crashed,Crash);
            EventBus<ScenarioEvent>.Unsubscribe(ScenarioEvent.Replay, RePlay);
        }

        private void FixedUpdate() 
        {
            if (!crashed)
            {
                crashedTime += Time.fixedDeltaTime;
                rigidbody.velocity = transform.forward * speed;
            }
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
        
            if (other.gameObject.CompareTag(nameof(Car)) && !crashed)
            {
                ContactPoint contact = other.contacts[0]; // 첫 번째 충돌 지점 가져오기
                GameObject crashPointObject = new GameObject("Crash_Point");
                crashPointObject.transform.position = contact.point;
                GameManager.Instance.crashPoint = crashPointObject.transform;
                GameManager.Instance.stage = ScenarioStage.Crash;
                
            }
        }
        
        private void Crash()
        {
            crashedVec = transform.position;
            crashedRot = transform.rotation;
            Debug.Log("Car Crash");
            audioSource.clip = impactAudio;
            audioSource.loop = false;
            audioSource.Play();
            crashed = true;
            rigidbody.velocity = Vector3.zero;
            speed = 0;
            GameManager.Instance.crashCars.Add(this.gameObject);
            
            
        }

        private void RePlay()
        {
            this.gameObject.SetActive(true);
            GetComponent<MeshesDeformation>().RestoreMesh();
            StartCoroutine(MoveToTarget(startVec, crashedVec,crashedTime));
        }

        IEnumerator MoveToTarget(Vector3 startPosition, Vector3 targetPosition, float duration )
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.fixedDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            // Ensure the final position is set exactly to the target position
            transform.position = targetPosition;
        }
        
    }
    
}
