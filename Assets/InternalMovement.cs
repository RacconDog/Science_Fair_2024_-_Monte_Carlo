using UnityEngine;
using UnityEngine.InputSystem;

public class InternalMovement : MonoBehaviour
{
    [SerializeField] float speed;
    PlayerInput playerInput;
    InputAction moveAction;

    Rigidbody2D rb;

    [SerializeField] float dir;
    [SerializeField] bool debugControlMode;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions["Move"];
    }

    void Update()
    {
        if(debugControlMode)
        {
            dir = moveAction.ReadValue<Vector2>().x;
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
}