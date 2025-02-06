using Unity.VisualScripting;
using UnityEditor;
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
    
    [Header("Debug: ---> Don't edit!!! <----")]
    [SerializeField] bool isGrounded = false;
    [SerializeField] float dir;

    SpriteRenderer sprite;

    public bool win = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (debugControlMode)
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }

        sprite = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        isGrounded = false;
        dir = 0;
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
     // print ("dir:  " + dir + "||| speed:  " + speed + "||| dir * speed:  " + dir*speed);
        rb.linearVelocityX = dir * speed;

        bool flip = true;

        if (dir == 1) {
            flip = false;
        } else if (dir == -1) {
            flip = true;
        } else {
            flip = false;
        }
        sprite.flipX = flip;

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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "deathBlock") 
        {
            print("hit death block");
            // GameObject.Find("Agent Manager").GetComponent<AgentManager>().fittestAgent = null;
            AgentManager am = GameObject.Find("Agent Manager").GetComponent<AgentManager>();
            am.highFitnessScore = 0;
            am.childrenFallCount += 1;

            GetComponent<MonteCarloPlayer>().FallToDeath();
        }
        if (other.gameObject.tag == "Floor") {isGrounded = true;}

        if (other.gameObject.tag == "Win") {win = true;}
    }
}