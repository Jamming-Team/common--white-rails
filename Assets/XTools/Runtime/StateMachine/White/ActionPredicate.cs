using System;

namespace XTools.SM.White {
    /// <summary>
    ///     Represents a predicate that encapsulates an action and evaluates to true once the action has been invoked.
    /// </summary>
    public class ActionPredicate : IPredicate {
        public bool flag;
         
        Action _eventReaction;
     
        public ActionPredicate(ref Action eventReaction) {
            _eventReaction = eventReaction;
        }
     
        public bool Evaluate() {
            var result = flag;
            flag = false;
            return result;
        }
         
        public void Enable() {
            _eventReaction += SetFlag;
        }
         
        public void Disable() {
            _eventReaction -= SetFlag;
        }

        protected void SetFlag() {
            flag = true;
        }
    }

    public class ActionPredicate<T> : ActionPredicate, IPredicate {
        Action<T> _eventReaction;

        public ActionPredicate(ref Action<T> eventReaction) : base(ref DummyAction) {
            _eventReaction = eventReaction;
        }

        public new void Enable() {
            _eventReaction += SetFlag;
        }
    
        public new void Disable() {
            _eventReaction -= SetFlag;
        }

        void SetFlag(T arg) => base.SetFlag();
    
        static Action DummyAction = () => {};
    }
    
    public class ActionPredicate<T1, T2> : ActionPredicate, IPredicate {
        Action<T1, T2> _eventReaction;

        public ActionPredicate(ref Action<T1, T2> eventReaction) : base(ref DummyAction) {
            _eventReaction = eventReaction;
        }

        public new void Enable() {
            _eventReaction += SetFlag;
        }
    
        public new void Disable() {
            _eventReaction -= SetFlag;
        }

        void SetFlag(T1 arg1, T2 arg2) => base.SetFlag();
    
        static Action DummyAction = () => {};
    }
}