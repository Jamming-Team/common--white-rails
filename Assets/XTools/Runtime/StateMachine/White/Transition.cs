using System;

namespace XTools.SM.White {
    [Serializable]
    public class Transition {
        public IState from;
        public IState to;
        
        public IPredicate condition;
        
        IState _parentState;
        
        internal void SetParent(IState parent) {
            _parentState = parent;
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