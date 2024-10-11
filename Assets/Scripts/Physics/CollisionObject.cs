using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    CustomRigidBody aiRigidBody = new CustomRigidBody(); // Custom Rigidbody representation for the AI
    [SerializeField] private float groundCheckDistance = 4f; // Distance to check for the ground
    [SerializeField] private float wallCheckDistance = 0.5f; // Distance to check for walls
    [SerializeField] private LayerMask groundLayer; // Layer for ground and walls
    private RaycastHit2D hit;

    public bool IsGrounded { get; private set; }
    public bool IsTouchingLeftWall { get; private set; }
    public bool IsTouchingRightWall { get; private set; }

    void Start()
    {
        // Ensure the player can interact with physics, even with custom logic
        aiRigidBody.isGrounded = false;
        aiRigidBody = Gameobject.GetComponent<PhysicsObject>();
    }

    void Update() // Run on frame step, NOT FixedUpdate
    {
        CheckForFloor(); // Ground check
        CheckForWallsLeftAndRight(); // Wall check
    }

    // Ground check logic using raycasting
    void CheckForFloor()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null && hit.collider.CompareTag("Collider"))
        {
            IsGrounded = true;
            aiRigidBody.isGrounded = true;
            aiRigidBody.ResetVelocityY(); // Reset vertical velocity when grounded
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green); // Debug line when grounded
        }
        else
        {
            IsGrounded = false;
            aiRigidBody.isGrounded = false;
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red); // Debug line when not grounded
        }
    }

    // Wall check logic using raycasting
    void CheckForWallsLeftAndRight()
    {
        // Raycast to the left
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, groundLayer);
        IsTouchingLeftWall = hitLeft.collider != null && hitLeft.collider.CompareTag("Collider");

        // Raycast to the right
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, groundLayer);
        IsTouchingRightWall = hitRight.collider != null && hitRight.collider.CompareTag("Collider");

        // Debugging wall detection
        Debug.DrawRay(transform.position, Vector2.left * wallCheckDistance, IsTouchingLeftWall ? Color.blue : Color.gray);
        Debug.DrawRay(transform.position, Vector2.right * wallCheckDistance, IsTouchingRightWall ? Color.blue : Color.gray);

        // Stop horizontal movement if touching walls
        if (IsTouchingLeftWall || IsTouchingRightWall)
        {
            aiRigidBody.ResetVelocityX(); // Stop horizontal velocity if hitting a wall
        }
    }
}
