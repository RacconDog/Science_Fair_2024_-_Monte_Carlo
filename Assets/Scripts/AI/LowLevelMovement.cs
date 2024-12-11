using UnityEngine;
using UnityEngine.InputSystem;

public class LowLevelMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    Rigidbody2D rb;

    [SerializeField] bool debugControlMode;
 
    float dir;
    bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (debugControlMode)
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }
    }

    void FixedUpdate()
    {
        // Jump(1);
        if (debugControlMode)
        {
            dir = moveAction.ReadValue<Vector2>().x;
            if (jumpAction.ReadValue<float>() == 1)
            {
                Jump(1);
            }
        }

        rb.linearVelocityX = dir * speed;
    }

    public void AlterMoveDir(float dirArg)
    {
        if (! debugControlMode)
        {
            dir = dirArg;
        }
    }

    public void Jump(int shouldJump)
    {
        if (isGrounded && shouldJump == 1)
        {
            isGrounded = false;
            rb.linearVelocityY = 0;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Floor") return;
        isGrounded = true;
    }
}