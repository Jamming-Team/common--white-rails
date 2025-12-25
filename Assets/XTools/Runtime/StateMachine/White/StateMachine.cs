using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace XTools.SM.White {
    public class StateMachine : MonoBehaviour {
        [SerializeField] bool _debug;
        [SerializeField] string _currentState;
        
        public IState lastState { get; private set; }
        public TransitionSequencer sequencer { get; private set; }
        [SerializeField] MonoBehaviour _root;
        public IState root => _root as IState;

        bool _started;

        public void StartSM(Object context) {
            if (_started) return;
            if (!(_root is IState))
                Debug.LogError("Root state must implement IState!");
            
            sequencer = new TransitionSequencer(this);
            // Wire(_root, this, new HashSet<IState>());
            root.SetupRecursively(this, context);
            _started = true;
            root.Enter();
        }

        // Separate those for introducing sequencing
        public void Tick(float deltaTime) {
            if (!_started)
                Debug.LogError("You should start SM first!");

            sequencer.Tick(deltaTime);
        }

        internal void InternalTick(float deltaTime) {
            root.UpdateState(deltaTime);
        }

        // Perform the actual switch from 'from' to 'to' by exiting up to the shared ancestor, then entering down to the target
        public void ChangeState(IState from, IState to) {
            if (from == to || from == null || to == null) return;
            lastState = from;

            var lca = TransitionSequencer.Lca(from, to);

            // Exit current branch up to (but not including) LCA
            for (var s = from; s != lca; s = s.parent) s.Exit();

            // Enter target branch from LCA down to target
            var stack = new Stack<IState>();
            for (var s = to; s != lca; s = s.parent) stack.Push(s);
            while (stack.Count > 0) stack.Pop().Enter();

            _currentState = to.GetName();
            
            if (_debug)
                Debug.Log($"State path: {StatePath(root.Leaf())}");
        }

        static string StatePath(IState state) {
            return string.Join(" > ", state.PathToRoot().Reverse().Select(x => x.GetType().Name));
        }
    }
}