using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/NoMindlessWandering")]
public class StopMindlessWandering : FlockingBehavior
{
    public Vector2 CenterPoint;
    public float radius = 20f;

    public override Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock)
    {
        //find out how far away the agent is from the centerpoint and based on where the agent is, move back towards the center.
        Vector2 CenterOffset = CenterPoint - (Vector2)agent.transform.position;
        float BoundaryCheck = CenterOffset.magnitude / radius; //if the BoundaryCheck is 0, the agent is at the center.
        //if it's at 1, the agent is at the radius. If higher, its away from the radius.

        if(BoundaryCheck < 0.9f) //If the Check is below 0.9, there's no need to change anything
        {
            return Vector2.zero;
        }
        return CenterOffset * BoundaryCheck * BoundaryCheck;
    }

}
