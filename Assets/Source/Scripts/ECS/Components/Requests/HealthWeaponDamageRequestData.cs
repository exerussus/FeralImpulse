using System.Collections.Generic;
using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Requests
{
    
    /// <summary>
    /// Запрос на фиксирование урона от оружия.
    /// </summary>
    /// 

    /// /// <param name="int">OriginEntity</param>
    /// /// <param name="List [int]">TargetEntities</param>
    public struct HealthWeaponDamageRequestData : IEcsRequestData<int, List<int>>
    {
        /// <summary> Сущность - источник урона. </summary>
        public int OriginEntity;
        /// <summary> Сущности, которым будет нанесён урон. </summary>
        public List<int> TargetEntities;
        
        public void InitializeValues(int argument1, List<int> argument2)
        {
            OriginEntity = argument1;
            TargetEntities = argument2;
        }
    }
}