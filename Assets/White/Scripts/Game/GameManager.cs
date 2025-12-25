using System;
using UnityEngine;
using XTools.SM.White;

namespace White {
    public class GameManager : MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;
        
        void Start() {
            _stateMachine.StartSM(this);
        }

        void Update() {
            _stateMachine.Tick(Time.deltaTime);
        }
        
        public void SetTimeScale(float scale) {
            Time.timeScale = scale;
        }
    }
}