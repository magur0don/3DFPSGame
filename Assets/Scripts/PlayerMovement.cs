using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    public float moveSpeed = 5f;

    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        playerRigidbody.linearVelocity = move * moveSpeed;
    }

    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }
}
