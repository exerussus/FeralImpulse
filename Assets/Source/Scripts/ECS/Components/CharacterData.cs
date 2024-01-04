
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    public struct CharacterData
    {
        public ICharacter Value;

        public void InitializeValues(ICharacter character)
        {
            Value = character;
        }
        
    }
}