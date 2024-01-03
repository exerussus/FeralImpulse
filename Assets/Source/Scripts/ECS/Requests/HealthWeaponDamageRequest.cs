using System.Collections.Generic;

namespace Source.Scripts.ECS.Requests
{
    // запрос о фиксировании урона от оружия
    public struct HealthWeaponDamageRequest
    {
        public int OriginEntity;
        public List<int> TargetEntities;
    }
}