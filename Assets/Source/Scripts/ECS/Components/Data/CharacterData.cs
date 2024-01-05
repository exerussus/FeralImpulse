using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит ICharacter.
    /// </summary>
    
    /// /// <param name="ICharacter">Value</param>

    public struct CharacterData : IEcsData<ICharacter>
    {
        public ICharacter Value;

        public void InitializeValues(ICharacter character)
        {
            Value = character;
        }
    }
}