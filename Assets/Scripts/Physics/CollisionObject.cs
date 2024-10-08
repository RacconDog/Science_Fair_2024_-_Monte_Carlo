using System.Collections.Generic;
using UnityEngine;

public class CollisionObject : MonoBehaviour
{
    //Editor Notes --
    //  -This is any object that will be moving and need collision
    //  -Have this interact with RigidBody2D trigger colliders
    //  -Use Physics2D.Raycast for low-level detection
    //  - VERY VERY IMPORTANT -----> EVERYTHING ON FRAME STEP, Not fixed step

    //^^^^^^^^ like holy shit can not stress this enough.

    // DON"T FORGET WESTON -------> have Raycasts Hit Triggers enabled in ProjectSettings -> Physics
    float floorCullingDist = 4;

    void Update()
    {
        CheckForFloor();
        CheckForWallsLeftAndRight();
    }

    void CheckForFloor()
    {
        if (Physics2D.Raycast(transform.position, transform.down, out RaycastHit hit, floorCullingDist))
        {
            if(hit.GameObject.Tag == "Collider")
            {
                // Placeholder
                someCustomRigidBody.yVelocity = 0;
                someCustomRigidBody.isGrounded = true;
            }
        }
    }

    void CheckForWallsLeftAndRight()
    {
        //blah blah blah
    }
}
