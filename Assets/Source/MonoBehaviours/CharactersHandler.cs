using Source.EasyECS;
using UnityEngine;

namespace Source.MonoBehaviours
{
    public class CharactersHandler : EasyMonoBehaviour
    {
        [SerializeField] private Character playerCharacter;
        public Character PlayerCharacter => playerCharacter;

    }
}