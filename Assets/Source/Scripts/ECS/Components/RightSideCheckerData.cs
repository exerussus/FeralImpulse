
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components

{
    public struct RightSideCheckerData
    {
        public SideChecker Value;
        
        
        public void InitializeValues(ISideChecker sideChecker)
        {
            Value = sideChecker.RightSideChecker;
        }
    }
}