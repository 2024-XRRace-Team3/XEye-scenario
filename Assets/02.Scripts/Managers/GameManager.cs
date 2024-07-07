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
        
        public Transform playerReportPoint;
        [SerializeField]
        private ScenarioStage _stage = ScenarioStage.Intro;

        public ScenarioStage stage
        {
            get => _stage;
            set
            {
                switch(value) {
                    case ScenarioStage.Crash:
                        EventBus<ScenarioEvent>.Publish(ScenarioEvent.Crashed);
                        break;
                };
                _stage = value;
            }
        }

        private void Start()
        {
            // Intro Scenario 
        }
    }
}