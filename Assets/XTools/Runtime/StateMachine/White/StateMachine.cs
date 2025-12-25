using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XTools.SM.White {
    public class StateMachine : MonoBehaviour {
        [SerializeField] bool _debug;

        public TransitionSequencer sequencer { get; private set; }
        [SerializeField] Object _context;
        [SerializeField] IState _root;

        bool _started;

        void StartSM() {
            if (_started) return;

            sequencer = new TransitionSequencer(this);
            // Wire(_root, this, new HashSet<IState>());
            _root.SetupRecursively(this, _context);
            _started = true;
            _root.Enter();
        }

        // Separate those for introducing sequencing
        public void Tick(float deltaTime) {
            if (!_started)
                Debug.LogError("You should start SM first!");

            sequencer.Tick(deltaTime);
        }

        internal void InternalTick(float deltaTime) {
            _root.UpdateState(deltaTime);
        }

        // Perform the actual switch from 'from' to 'to' by exiting up to the shared ancestor, then entering down to the target
        public void ChangeState(Silver.IState from, Silver.IState to) {
            if (from == to || from == null || to == null) return;

            var lca = TransitionSequencer.Lca(from, to);

            // Exit current branch up to (but not including) LCA
            for (var s = from; s != lca; s = s.parent) s.Exit();

            // Enter target branch from LCA down to target
            var stack = new Stack<Silver.IState>();
            for (var s = to; s != lca; s = s.parent) stack.Push(s);
            while (stack.Count > 0) stack.Pop().Enter();

            if (_debug)
                Debug.Log($"State path: {StatePath(_root.Leaf())}");
        }

        static string StatePath(IState state) {
            return string.Join(" > ", state.PathToRoot().Reverse().Select(x => x.GetType().Name));
        }
    }
}