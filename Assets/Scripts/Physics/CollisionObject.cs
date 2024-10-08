using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    private float groundCheckDistance = 4f; // Culling distance for ground check
    [SerializeField]
    private LayerMask groundLayer; // Layer for ground detection
    [SerializeField]
    private float wallCheckDistance = 0.5f; // Distance for wall checks
    private RaycastHit2D hit;

    public bool IsGrounded { get; private set; }
    public bool IsTouchingLeftWall { get; private set; }
    public bool IsTouchingRightWall { get; private set; }

    private Rigidbody2D rb; // Reference to Rigidbody2D for interaction with physics

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false; // Ensures the object is under physics control
        rb.simulated = true; // Physics simulation enabled
    }

    void Update() // Run on frame step, NOT FixedUpdate
    {
        CheckForFloor(); // Ground check
        CheckForWallsLeftAndRight(); // Wall check
    }

    void CheckForFloor()
    {
        // Raycast downward to check for the floor
        hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        if (hit.collider != null && hit.collider.CompareTag("Collider"))
        {
            IsGrounded = true;
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green); // Debug line when grounded
        }
        else
        {
            IsGrounded = false;
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red); // Debug line when not grounded
        }
    }

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
    }

    // Optionally, you can handle collision events with trigger colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collider"))
        {
            Debug.Log("Collided with: " + other.name);
        }
    }
}
