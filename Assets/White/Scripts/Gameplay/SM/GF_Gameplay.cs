using UnityEngine;
using XTools.SM.White;

namespace White.Gameplay.SM {
    public class GF_Gameplay : State<GameflowManager, GF_Gameplay> {
        
        public override void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            base.SetupRecursively(machine, context, parent);

            var gameFinishedCondition = new ActionPredicate(ref GameEvents.LevelLoaded);
            this.parent.AddTransition(this, typeof(GF_Stopped), gameFinishedCondition);
        }
        
    }
}