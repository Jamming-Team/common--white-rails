using System;
using UnityEngine;
using XTools.SM.White;

namespace White.UI {
    public class UIManager : MonoBehaviour {
        
        [SerializeField] StateMachine _stateMachine;
        
        void Start() {
            _stateMachine.StartSM(this);
        }

        void Update() {
            _stateMachine.Tick(Time.deltaTime);
        }
    }
}