using System.Collections.Generic;
using UnityEngine;

namespace XTools.SM.White {
    public class TransitionSequencer {
        public readonly StateMachine machine;
        
        public TransitionSequencer(StateMachine machine) {
            this.machine = machine;
        }
        
        public void RequestTransition(IState from, IState to) {
            // machine.ChangeState(from, to);
            if (to == null || from == null) return;

            
            // BeginTransition(from, to);

            machine.ChangeState(from, to);
        }
        


        public void Tick(float deltaTime) {

            // TODO: Transition sequence logic

            machine.InternalTick(deltaTime); // so we call it if no transition is in progress
        }
        
        // Compute the Lowest Common Ancestor of two states
        public static IState Lca(IState a, IState b) {
            // Create a set of all the parents of 'a'
            var ap = new HashSet<IState>();
            for (var s = a; s != null; s = s.parent) ap.Add(s);
            // Find the first parent of 'b' that is also a parent of 'a'
            for (var s = b; s != null; s = s.parent)
                if (ap.Contains(s))
                    return s;
            // If no common ancestor was found, return null
            Debug.LogError("No LCA found. Probably, SM isn't correctly setup");
            return null;
        }
    }
}