using System;
using XTools.SM.White;
using Object = UnityEngine.Object;

namespace White.Gameplay.SM {
    public class GF_Paused : State<GameflowManager, GF_Gameplay> {
        Action _resumeButtonPressed;
        
        public override void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            base.SetupRecursively(machine, context, parent);
            
            var resumeCondition = new ActionPredicate(ref _resumeButtonPressed);
            this.parent.AddTransition(this, typeof(GF_Gameplay), resumeCondition);
        }
        
        // Lifecycle hooks
        protected override void OnEnter() {
            
        }

        protected override void OnExit() {
        }

        protected override void OnUpdate(float deltaTime) {
        }
    }
}