using System;
using System.Collections;
using Commons.Enums;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using Manager;
using Scripts.Phone;

namespace Phone
{
    public class PhoneController : MonoBehaviour
    {
        public GameObject cameraModel;
        //[field: SerializeField] public bool isLookStraight { get; private set; } = false;
        private TargetTracker targetTracker;
        public bool virtualCapturing = false; // true일 경우 해당 폰의 위치를 기준으로 카메라가 생성됨.
        public float virtualCapturingStamp = 3f;

        
        public GameObject introUi;
        public GameObject reportUi;
        public GameObject reportSubmitUi;
        public GameObject completeUi;

        [Header("Report UI")]
        [SerializeField] private Text trackingStateText;
        [SerializeField] private Text guideText;
        
        
        private Camera camera;
        private float time = 0f;

 
        private void Awake()
        {

            camera = GetComponentInChildren<Camera>();
            targetTracker = GetComponent<TargetTracker>();
            targetTracker.camera = camera;
        }

        private void Start()
        {
            targetTracker.target = GameManager.Instance.crashPoint;
            targetTracker.FinishCallback= () => StartCoroutine(SubmitProcess());
        }
        
        private void OnEnable()
        {
            introUi.SetActive(true);
            reportUi.SetActive(false);
            reportSubmitUi.SetActive(false);
            completeUi.SetActive(false);
        }

        private void Update()
        {
            time += Time.deltaTime;
            if (time > virtualCapturingStamp)
            {
                if (targetTracker.isLookStraight && virtualCapturing)
                {
                    CreateWayPoint();
                }
                time = 0f;
            }

            targetTracker.enabled = reportUi.activeSelf;
            if (targetTracker.isLookStraight)
            {
                trackingStateText.text = "Recording tracking";
                trackingStateText.color = Color.green;
                guideText.text = $"Tracking progress : {GetPercentageFromAngle(targetTracker.accumulatedRotation)}%";
            }
            else
            {
                trackingStateText.text = "Tracking Failure";
                trackingStateText.color = Color.red;
                guideText.text = "Please turn 360 degrees for field records.";
            }
        }
        
        private int GetPercentageFromAngle(float angle)
        {
            // 각도를 0부터 360 사이의 값으로 정규화
            angle = (angle + 360f) % 360f;

            // 0부터 360 사이의 각도를 0부터 100으로 변환하여 정수형으로 반환
            int percentage = Mathf.RoundToInt((angle / 360f) * 100f);

            return percentage;
        }

        
        /* TODO
         - 360 트래킹 함수 구현
         - 360도 트래킹 성공 후 전송 씬 
         - 시간 지연후 전송 성공 화면 출력 후 화면 전환
         */
        

        public void CreateWayPoint()
        {
            Instantiate(cameraModel, transform.position, transform.rotation);
        }

        IEnumerator SubmitProcess()
        {
            // yield return new WaitForSeconds(2f);
            reportUi.SetActive(false);
            reportSubmitUi.SetActive(true);
            
            yield return new WaitForSeconds(5f);
            reportSubmitUi.SetActive(false);
            completeUi.SetActive(true);
            yield return new WaitForSeconds(3f);
            GameManager.Instance.stage = ScenarioStage.Analysis;
        }
        
        // Phone UI 내에서 사용
        public void SetVirtualCapturing(bool enable) => virtualCapturing = enable;
    }
}