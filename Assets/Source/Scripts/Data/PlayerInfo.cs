using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float jumpForce = 5f;
    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
}
