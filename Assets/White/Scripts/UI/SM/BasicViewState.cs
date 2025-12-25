using System.Collections.Generic;
using UnityEngine;
using XTools.SM.White;

namespace White.UI.SM {
    public class BasicViewState : State<UIManager, BasicViewState> {
        [SerializeField] List<GameObject> _views;

        public override void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            base.SetupRecursively(machine, context, parent);
            foreach (var view in _views) {
                view.SetActive(false);
            }
        }
        
        protected override void OnEnter() {
            foreach (var view in _views) view.SetActive(true);
        }

        protected override void OnExit() {
            foreach (var view in _views) view.SetActive(false);
        }
    }
}