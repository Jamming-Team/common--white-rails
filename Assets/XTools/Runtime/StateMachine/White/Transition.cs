using System;
using UnityEngine;

namespace XTools.SM.White {
    [Serializable]
    public class Transition {
        [SerializeField]
        MonoBehaviour _from;
        public IState from => _from as IState;
        [SerializeField]
        MonoBehaviour _to;
        public IState to => _to as IState;
        [SerializeField]
        MonoBehaviour _condition;
        public IPredicate condition => _condition as IPredicate;
        
        IState _parentState;
        

        internal void Init(IState parent) {
            _parentState = parent;

            var fieldsAreValid = _from is IState && _to is IState && _condition is IPredicate;
            
            if (!fieldsAreValid)
                Debug.LogError("Transition fields are not valid!");
        }
        
        public void Enable() {
            condition.Enable();
        }
        
        public void Disable() {
            condition.Disable();
        }
        
        public bool Evaluate() {
            // Check if the condition variable is a Func<bool> and call the Invoke method if it is not null
            // var result = (condition as Func<bool>)?.Invoke();
            // if (result.HasValue) {
            //     return result.Value;
            // }

            // // Check if the condition variable is an ActionPredicate and call the Evaluate method if it is not null
            // result = (condition as ActionPredicate)?.Evaluate();
            // if (result.HasValue) {
            //     return result.Value;
            // }

            // Check if the condition variable is an IPredicate and call the Evaluate method if it is not null
            if (_parentState.activeChild != from) return false;
            
            var result = condition?.Evaluate();
            if (result.HasValue) return result.Value;

            // If the condition variable is not a Func<bool>, an ActionPredicate, or an IPredicate, return false
            return false;
        }
    }
}