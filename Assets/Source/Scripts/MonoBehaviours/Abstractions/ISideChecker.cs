namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Имеет проверки на касания сторон.
    /// </summary>

    public interface ISideChecker
    {
        public SideChecker LeftSideChecker {get;}
        public SideChecker RightSideChecker {get;}
    }
}