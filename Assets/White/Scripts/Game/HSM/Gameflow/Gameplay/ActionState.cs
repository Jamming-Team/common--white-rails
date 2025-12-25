using UnityEngine;
using XTools.SM.White;

namespace White {
    public class ActionState : GameflowStateBase {
        public override void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            base.SetupRecursively(machine, context, parent);

            var gameFinishedCondition = new ActionPredicate(ref GameEvents.LevelLoaded);
            this.parent.AddTransition(this, typeof(ResultsState), gameFinishedCondition);
        }
    }
}