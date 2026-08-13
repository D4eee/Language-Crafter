using UnityEngine;

namespace Yiyang.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 2.1f;
        public float sprintMultiplier = 1.35f;
        public bool lockToLane = true;
        public float laneZ = 0f;
        public float zMoveScale = 0.35f;
        public PlayerState State { get; private set; }

        private CharacterController controller;
        private float facing = 1f;
        private bool movementLocked;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (movementLocked)
            {
                State = PlayerState.Locked;
                return;
            }

            float x = Input.GetAxisRaw("Horizontal");
            float z = lockToLane ? 0f : Input.GetAxisRaw("Vertical") * zMoveScale;
            bool sprinting = Input.GetKey(KeyCode.LeftShift);
            Vector3 movement = new Vector3(x, 0f, z).normalized;
            float speed = moveSpeed * (sprinting ? sprintMultiplier : 1f);
            controller.SimpleMove(movement * speed);

            if (lockToLane)
            {
                Vector3 p = transform.position;
                transform.position = new Vector3(p.x, p.y, laneZ);
            }

            if (Mathf.Abs(x) > 0.01f)
            {
                facing = Mathf.Sign(x);
                transform.rotation = Quaternion.Euler(0f, facing > 0 ? 90f : -90f, 0f);
            }

            State = movement.sqrMagnitude < 0.01f ? PlayerState.Idle : sprinting ? PlayerState.Sprinting : PlayerState.Walking;
        }

        public void SetMovementLocked(bool locked)
        {
            movementLocked = locked;
        }
    }
}
