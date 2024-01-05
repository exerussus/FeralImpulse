using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data

{
    /// <summary>
    /// Хранит SideChecker.
    /// </summary>
    
    /// /// <param name="SideChecker">Value</param>

    public struct LeftSideCheckerData : IEcsData<ISideChecker>
    {
        public SideChecker Value;

        public void InitializeValues(ISideChecker sideChecker)
        {
            Value = sideChecker.LeftSideChecker;
        }
    }
}