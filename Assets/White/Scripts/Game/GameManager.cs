using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using XTools.SM.White;

namespace White {
    public class GameManager : MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;

        short a = 1;
        
        void Start() {
            Debug.Log(a);
            _stateMachine.StartSM(this);
        }

        void Update() {
            _stateMachine.Tick(Time.deltaTime);
        }
        
        public void SetTimeScale(float scale) {
            Time.timeScale = scale;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        readonly struct MyStruct {
            readonly short health;   
            readonly byte level;     
            readonly byte stamina;   
        }
        
            
        public void Increment() {
            Interlocked.Increment(ref count);  // ATOMIC
            // Guaranteed to complete without interruption
        }
    }
}