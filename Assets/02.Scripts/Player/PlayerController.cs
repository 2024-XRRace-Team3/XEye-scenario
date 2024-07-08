using System;
using System.Collections;
using Commons.Enums;
using Manager;
using UnityEngine;
using EventBus;
namespace Car
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject phoneObj;
        public bool phoneActive = false; 
        
        public CameraFade cameraFade;
        void Start()
        {
            GameManager.Instance.player = this.gameObject;
            cameraFade = GetComponentInChildren<CameraFade>();
        }

        private void OnEnable()
        {
            EventBus<ScenarioEvent>.Subscribe(ScenarioEvent.Crashed,() => StartCoroutine(Crash()));
            EventBus<ScenarioStage>.Subscribe(ScenarioStage.Analysis,() => StartCoroutine(Analysis()));
        }

        void Update()
        {
#if UNITY_EDITOR
            this.phoneObj.SetActive(phoneActive);
#endif
        }

        IEnumerator Crash()
        {
            cameraFade.FadeOut(2f);
            yield return new WaitUntil(() => !cameraFade.IsFading);
            transform.position = GameManager.Instance.playerReportPoint.position;
            cameraFade.FadeIn(2f);
            Debug.Log($"fade IN");
            phoneActive = true;
            transform.parent = null;
            GameManager.Instance.stage = ScenarioStage.Report;

        }
        IEnumerator Analysis()
        {
            cameraFade.FadeOut(2f);
            yield return new WaitUntil(() => !cameraFade.IsFading);
            transform.position = GameManager.Instance.playerAnalysticPoint.position;
            cameraFade.FadeIn(2f);

        }
    }
}
