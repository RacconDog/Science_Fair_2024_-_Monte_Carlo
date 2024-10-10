using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    // Custom properties for AI physics behavior
    public class CustomRigidBody
    {
        public Vector2 velocity { get; private set; } = Vector2.zero; // Velocity in x and y directions
        public Vector2 acceleration { get; private set; } = Vector2.zero; // Acceleration in x and y directions
        public bool isGrounded = false; // Is the object grounded?

        // Add force to the object, modifying acceleration
        public void AddForce(Vector2 force)
        {
            acceleration += force; // Apply force to acceleration
        }

        // Update velocity based on current acceleration
        public void UpdateVelocity(float deltaTime)
        {
            velocity += acceleration * deltaTime; // v = v0 + a*t
            acceleration = Vector2.zero; // Reset acceleration after it has been applied
        }

        // Reset velocity in a specific direction
        public void ResetVelocityY()
        {
            velocity = new Vector2(velocity.x, 0); // Stop vertical movement
        }

        public void ResetVelocityX()
        {
            velocity = new Vector2(0, velocity.y); // Stop horizontal movement
        }
    }

    CustomRigidBody aiRigidBody = new CustomRigidBody(); // Custom Rigidbody representation for the AI

    [SerializeField] private float moveSpeed = 5f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    [SerializeField] private float gravity = -9.8f; // Gravity
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
    }

    void Update() // Run on frame step, NOT FixedUpdate
    {
        CheckForFloor(); // Ground check
        CheckForWallsLeftAndRight(); // Wall check
        ApplyCustomPhysics(); // Custom physics applied each frame
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

    // AI-driven movement and custom physics applied
    void ApplyCustomPhysics()
    {
        // Apply gravity when not grounded
        if (!aiRigidBody.isGrounded)
        {
            aiRigidBody.AddForce(new Vector2(0, gravity)); // Apply gravity as a downward force
        }

        // Update velocity based on forces applied
        aiRigidBody.UpdateVelocity(Time.deltaTime);

        // Example movement logic
        if (!IsTouchingRightWall)
        {
            MoveRight(); // Keep moving right unless a wall is hit
        }

        // Example jump logic based on wall proximity
        if (IsGrounded && (IsTouchingLeftWall || IsTouchingRightWall))
        {
            Jump();
        }

        // Apply the calculated velocity to the player's position
        transform.position += new Vector3(aiRigidBody.velocity.x, aiRigidBody.velocity.y) * Time.deltaTime;
    }

    // Move AI player to the right
    void MoveRight()
    {
        aiRigidBody.AddForce(new Vector2(moveSpeed, 0)); // Apply force to move right
    }

    // Make AI player jump if conditions are met
    void Jump()
    {
        if (IsGrounded)
        {
            aiRigidBody.AddForce(new Vector2(0, jumpForce)); // Apply jump force
            aiRigidBody.isGrounded = false; // AI is no longer grounded after jumping
        }
    }

    // Optionally, handle trigger collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collider"))
        {
            Debug.Log("Collided with: " + other.name);
        }
    }
}
