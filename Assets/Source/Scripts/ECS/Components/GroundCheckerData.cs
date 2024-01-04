using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    public struct GroundCheckerData
    {
        public GroundChecker Value;

        public void InitializeValues(IGroundChecker groundChecker)
        {
            Value = groundChecker.GroundChecker;
        }
    }
}