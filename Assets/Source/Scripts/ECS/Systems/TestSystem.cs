using Source.EasyECS;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class TestSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
        
        }

        public void Run(IEcsSystems systems)
        {
            if (Input.GetKeyDown(KeyCode.T))
                Debug.Log("T pressed");
        }
    }
}
