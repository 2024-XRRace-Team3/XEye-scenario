using System;
using System.Collections;
using System.Collections.Generic;
using Common.State;
using Commons.Enums;
using UnityEngine;
using EventBus;

namespace Manager {
    public class GameManager : GenericSingleton<GameManager>
    {
        public GameObject player;

        public new List<GameObject> crashCars;
        public Transform crashPoint;
        public Transform playerReportPoint;

        public GameObject realEnv;
        
        [SerializeField]
        private ScenarioStage _stage = ScenarioStage.Intro;

        public ScenarioStage stage
        {
            get => _stage;
            set
            {
                if (value == _stage) return; // 변경이 없을떄는 호출 되지 않음.
                switch(value) {
                    case ScenarioStage.Crash:
                        EventBus<ScenarioEvent>.Publish(ScenarioEvent.Crashed);
                        break;
                    case ScenarioStage.Analysis:
                        EventBus<ScenarioStage>.Publish(ScenarioStage.Analysis);
                        StartCoroutine(SetAnalysis());
                        break;
                };
                _stage = value;
            }
        }

        [Header("Analysis Resources")] 
        public Transform playerAnalysticPoint;
        public GameObject crashModel;
        public GameObject crashEnvModel;
        public GameObject analysisEnv;
        public Material analysisSkybox;
        private void Start()
        {
            crashModel.SetActive(false);
            crashEnvModel.SetActive(false);
            
            realEnv.SetActive(true);
            analysisEnv.SetActive(false);
        }

        private IEnumerator SetAnalysis()
        {
            // BAD: 페이드 처리를 위한 대기 코드
            yield return new WaitForSeconds(4f);
            //
            // RenderSettings.fog = true;
            // RenderSettings.fogColor = Color.black;
            // RenderSettings.fogMode = FogMode.Exponential;
            // RenderSettings.fogDensity = 0.23f;
            RenderSettings.skybox = analysisSkybox;

            realEnv.SetActive(false);
            analysisEnv.SetActive(true);

            crashModel.SetActive(true);
            crashEnvModel.SetActive(true);
            
        }
    }
}