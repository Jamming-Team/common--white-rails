namespace XTools.SM.White {
    public interface IPredicate {
        bool Evaluate();
        void Enable();
        void Disable();
    }
}