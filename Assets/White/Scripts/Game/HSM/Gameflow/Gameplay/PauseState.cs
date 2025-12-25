using XTools.SM.White;

namespace White {
    public class PauseState : GameflowStateBase {
        protected override void OnEnter() {
            _context.SetTimeScale(GameConstants.TimeScales.ZERO);
        }

        protected override void OnExit() {
            _context.SetTimeScale(GameConstants.TimeScales.NORMAL);
        }
    }
}