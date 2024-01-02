using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbodyValue;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private SideChecker leftSideChecker;
        [SerializeField] private SideChecker rightSideChecker;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        //[SerializeField] private Transform viewTransform;

        public Animator Animator => animator;

        public SpriteRenderer SpriteRenderer => spriteRenderer;

        public Rigidbody2D Rigidbody => rigidbodyValue;
        public GroundChecker GroundChecker => groundChecker;

        public SideChecker LeftSideChecker => leftSideChecker;

        public SideChecker RightSideChecker => rightSideChecker;

        public Transform Transform => rigidbodyValue.transform;


    }
}