using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XTools.SM.White {
    public interface IState {
        StateMachine machine { get; set; }
        IState parent { get; set; }
        IState activeChild { get; set; }

        void Enter();
        void Exit();
        void UpdateState(float deltaTime);
        List<IState> GetChildren();
        void SetupRecursively(StateMachine machine, Object context, IState parent = null);
        void AddTransition(IState from, Type to, IPredicate condition);
        IEnumerable<IState> PathToRoot();
        IState Leaf();
        string GetName();
    }

    [Serializable]
    public class Test {
        public int a;
    }
    
    public abstract class State<TContext, TTarget> : MonoBehaviour, IState where TTarget : IState where TContext : Object {
        public StateMachine machine { get; set; }
        public IState parent { get; set; }
        public IState activeChild { get; set; }

        [SerializeField] State<TContext, TTarget> _initialState;

        public List<Transition> _transitions = new();

        [SerializeField] List<TTarget> _children = new();

        TContext _context;

        protected virtual IState GetInitialState() {
            return _initialState;
            // Initial child to enter when this state starts (null = this is the leaf)
        }

        protected Transition GetTransition() {
            // foreach (var transition in anyTransitions)
            //     if (transition.Evaluate())
            //         return transition;

            foreach (var transition in _transitions)
                if (transition.Evaluate())
                    return transition;

            return null;
        } // Target state to switch to this frame (null = stay in the current state)

        public List<IState> GetChildren() {
            List<IState> trueChildren = new();
            foreach (var child in _children)
                if (child != null)
                    trueChildren.Add(child);
            return trueChildren;
        }

        public virtual void SetupRecursively(StateMachine machine, Object context, IState parent = null) {
            this.machine = machine;
            this.parent = parent;
            InitTransitions();

            if (context != null && context is TContext ctx) _context = ctx;

            foreach (var child in _children) child.SetupRecursively(machine, _context, this);
        }

        public void AddTransition(IState from, Type to, IPredicate condition) {
            var targetState = GetChildren().Find(x => x.GetType() == to);
            var newTransition = new Transition(false);
            newTransition.SetFields(this, targetState, condition);
            newTransition.Init(this);
            _transitions.Add(newTransition);
        }

        // Lifecycle hooks
        protected virtual void OnEnter() {
        }

        protected virtual void OnExit() {
        }

        protected virtual void OnUpdate(float deltaTime) {
        }

        public void Enter() {
            if (parent != null) parent.activeChild = this;
            foreach (var transition in _transitions) transition.Enable();
            OnEnter();
            var init = GetInitialState();
            if (init != null) init.Enter();
        }

        public void Exit() {
            if (activeChild != null) activeChild.Exit();
            activeChild = null;
            foreach (var transition in _transitions) transition.Disable();
            OnExit();
        }

        public void UpdateState(float deltaTime) {
            var t = GetTransition();
            if (t != null) {
                machine.sequencer.RequestTransition(t.from, t.to);
                return;
            }

            if (activeChild != null) activeChild.UpdateState(deltaTime);
            OnUpdate(deltaTime);
        }

        // Returns the deepest currently-active descendant state (the leaf of the active path)
        public IState Leaf() {
            IState s = this;
            while (s.activeChild != null) s = s.activeChild;
            return s;
        }

        // Yields this state and then each ancestor up to the root (self -> parent -> ... -> root)
        public IEnumerable<IState> PathToRoot() {
            for (var s = this as IState; s != null; s = s.parent) yield return s;
        }

        void InitTransitions() {
            // Debug.Log(GetChildren());
            foreach (var transition in _transitions) transition.Init(this);
        }

        public string GetName() {
            return name;
        }
    }
}