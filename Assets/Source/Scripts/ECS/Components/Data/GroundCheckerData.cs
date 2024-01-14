using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит GroundChecker.
    /// </summary>
    
    /// /// <param name="GroundChecker">Value</param>

    public struct GroundCheckerData : IEcsData<IGroundChecker>
    {
        public GroundChecker Value;

        public void InitializeValues(IGroundChecker groundChecker)
        {
            Value = groundChecker.GroundChecker;
        }
    }
}