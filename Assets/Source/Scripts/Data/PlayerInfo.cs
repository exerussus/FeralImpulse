
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float jumpForce = 5f;
    [SerializeField]private float health = 30f;
    
    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float Health => health;
}
