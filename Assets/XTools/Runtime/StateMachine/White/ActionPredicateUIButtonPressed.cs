using UnityEngine;

namespace XTools.SM.White {
    public class ActionPredicateUIButtonPressed : MonoBehaviour, IPredicate {
        [SerializeField] XToolsEvents.UIButtonTypes _referenceObject;

        bool _flag;

        // public ActionPredicateUIButtonPressed() {
        //     XToolsEvents.UIButtonPressed += delegate(XToolsEvents.UIButtonTypes compareObject) {
        //         _flag = _referenceObject.Equals(compareObject);
        //     };
        // }

        public void Enable() {
            XToolsEvents.UIButtonPressed += delegate(XToolsEvents.UIButtonTypes compareObject) {
                _flag = _referenceObject.Equals(compareObject);
            };
        }

        public void Disable() {
            XToolsEvents.UIButtonPressed -= delegate(XToolsEvents.UIButtonTypes compareObject) {
                _flag = _referenceObject.Equals(compareObject);
            };
        }

        public bool Evaluate() {
            var result = _flag;
            _flag = false;
            return result;
        }
    }
}