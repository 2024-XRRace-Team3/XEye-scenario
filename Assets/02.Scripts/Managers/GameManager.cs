using System;
using Commons.Enums;
using UnityEngine;
using EventBus;

namespace Manager {
    public class GameManager : GenericSingleton<GameManager>
    {
        public GameObject player;
        
        public Transform playerReportPoint;
        private void Start()
        {
            // Intro Scenario 
            // EventBus<ScenarioEvent>.Subscribe(ScenarioEvent.Crashed,InitReport);
        }
    }
}