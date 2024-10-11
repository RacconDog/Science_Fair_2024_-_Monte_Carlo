using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector2 velocity{ get; private set; }
    public Vector2 acceration{ get; private set; }

    [SerializeField] const float speedCap = 0;

    [SerializeField] const float staticFriction = 0; 
    [SerializeField] const float kineticFriction = 0; 

    //when to switch between static and dynamic friction based on speed
    [SerializeField] const float staticKineticBoundary = 0;

    [SerializeFields] const float gravity = 0;

    [SerializeField] const float mass = 0;

    [SerializeField] float floorTheta = 0;

    public AddForce(Vector2 force)
    {
        acceration += force / mass;
    }

    void Update()
    {
        //apply acceration
        velocity += acceration;

        //apply friction
        if (velocity.magnitude < staticKineticBoundary)
        {
            AddForce(
                -velocity.normalized 
                * staticFriction 
                * GetNormalForce());
        }
        else
        {
            AddForce(
                -velocity.normalized 
                * kineticFriction 
                * GetNormalForce());
        }
        
        //apply speedcap
        if(velocity.magnitude > speedCap) 
        {
            velocity = velocity.normalized * speedCap;
        }
    }

    float GetNormalForce()
    {
        return mass * gravity * Mathf.Cos(floorTheta);
    }
}
