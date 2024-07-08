using System;
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
                        break;
                };
                _stage = value;
            }
        }

        [Header("Analysis Resources")] 
        public Transform playerAnalysticPoint;
        public GameObject crashModel;

        private void Start()
        {
            crashModel.SetActive(false);
        }
    }
}