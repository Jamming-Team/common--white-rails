using System;
using XTools.SM.White;
using Object = UnityEngine.Object;

namespace White.Gameplay.SM {
    public class GF_Init : State<GameflowManager, GF_Stopped> {

        public override void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            base.SetupRecursively(machine, context, parent);

            var levelLoadedCondition = new ActionPredicate(ref GameEvents.LevelLoaded);
            this.parent.AddTransition(this, typeof(GF_Gameplay), levelLoadedCondition);
        }
        
    }
}