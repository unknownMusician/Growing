using AreYouFruits.Assertions;
using UnityEngine;

namespace Player
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private LayerMask groundLayer;

        private void Update()
        {
            if (GetMoveDirection() is var direction && direction != Vector2.zero)
            {
                Move(direction);
            }
        }
        
        private static Vector2 GetMoveDirection()
        {
            var result = Vector2.zero;
            
            if (Input.GetKey(KeyCode.W)) { result += Vector2.up; }
            if (Input.GetKey(KeyCode.A)) { result += Vector2.left; }
            if (Input.GetKey(KeyCode.S)) { result += Vector2.down; }
            if (Input.GetKey(KeyCode.D)) { result += Vector2.right; }
        
            return result;
        }
        
        private void Move(Vector2 direction)
        {
            // todo
            var cameraTransform = Camera.main.transform;
            
            var upDirection = cameraTransform.up;
            var rightDirection = cameraTransform.right;

            var moveDirection = upDirection * direction.y + rightDirection * direction.x;

            var newPosition = transform.position + moveDirection * (Time.deltaTime * speed);

            transform.position = GetGroundedPosition(newPosition);

            transform.up = transform.position - Vector3.zero;
        }

        private Vector3 GetGroundedPosition(Vector3 newPosition)
        {
            var downDirection = Vector3.zero - newPosition;
            newPosition -= downDirection * 0.1f;
            Physics.Raycast(
                new Ray(newPosition, downDirection),
                out var hit,
                Vector3.Distance(newPosition, Vector3.zero),
                groundLayer).Expect(true);

            newPosition = hit.point;
            return newPosition;
        }
    }
}