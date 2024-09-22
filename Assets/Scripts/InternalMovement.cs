using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InternalMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    Rigidbody2D rb;

    [SerializeField] float dir;
    [SerializeField] bool debugControlMode;

    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(debugControlMode)
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }
    }

    void Update()
    {
        if(debugControlMode)
        {
            dir = moveAction.ReadValue<Vector2>().x;
        }
        if(debugControlMode && jumpAction.ReadValue<float>() == 1)
        {
            Jump(1);
        }

        rb.linearVelocityX = dir * speed;
        
    }

    public void AlterMoveDir(float dirArg)
    {
        if(!debugControlMode)
        {
            dir = dirArg;
        }
    }

    public void Jump(int jump)
    {
        if (isGrounded == true && jump == 1)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // if (other.gameObject.tag != "Floor") return;
        isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Floor") return;
        isGrounded = true;
    }
}