using Source.EasyECS;
using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.MonoBehaviours
{
    public class CharactersHandler : EasyMonoBehaviour
    {
        [SerializeField] private Character playerCharacter;
        public Character PlayerCharacter => playerCharacter;
        

    }
}