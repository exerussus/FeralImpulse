using Source.EasyECS;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.MonoBehaviours
{
    
    public class InfoHub : EasyMonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        public PlayerInfo PlayerInfo => playerInfo;
    }
}