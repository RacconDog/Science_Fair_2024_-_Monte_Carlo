using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Vector3 target;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        target = GameObject.Find("Agent Manager").GetComponent<AgentManager>().target;
        Vector3 targetPosition = new Vector3(target.x + offset.x, offset.y, 0);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
