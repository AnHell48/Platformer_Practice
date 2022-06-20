using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviors : MonoBehaviour
{
    //public GameObject target;
    float maxSpeed, targetDistance;
    Vector3 velocity, desiredVelocity, steer;

    private void Start()
    {
        maxSpeed = 2f;
        velocity = new Vector3(2, 0, 0);
    }

    public void Follow(GameObject target, float moveSpeed)
    {
        maxSpeed = moveSpeed;
        desiredVelocity = ((target.transform.position - this.transform.position).normalized) * maxSpeed;
        steer = desiredVelocity - velocity;
        steer = Vector3.ClampMagnitude(steer, 5);
        steer /= 10;
        velocity = Vector3.ClampMagnitude(velocity + steer, maxSpeed);

        //lock Y position
        velocity.y = 0;

        this.transform.position += velocity * Time.deltaTime;
        this.transform.forward = velocity.normalized;

        //look at own Y position so it doesn't shift direction when rotating (look up or down)
        this.transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
        
    }  
}
