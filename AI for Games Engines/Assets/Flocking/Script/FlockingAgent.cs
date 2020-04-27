using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))] //using the 2d collider to check for any overlapping collision.

public class FlockingAgent : MonoBehaviour
{
    Collider2D AgentCollider;
    public Collider2D PublicAccessAgentCollider { get { return AgentCollider; } } //To access the collider without assigning it.


    void Start()
    {
        AgentCollider = GetComponent<Collider2D>(); //Attatching the collider.
    }

    public void Move(Vector2 Velocity)
    {
        transform.up = Velocity; //Turns the agent towards the direction it's going to move.
        transform.position += (Vector3)Velocity * Time.deltaTime; //Casting Velocity as a Vector3 so I'm able to add them together, adding Vector2 throws errors.
    }
    void Update()
    {
        
    }
}
