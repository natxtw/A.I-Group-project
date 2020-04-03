using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/SteerTogether")]
public class SteerTogetherBehavior : FlockingBehavior
{
    Vector2 CurrentVelocity;
    public float AgentTransition;

    public override Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock)
    {
        if (Neighbours.Count == 0)//if theres no neighbours, do nothing.
        {
            return Vector2.zero;
        }

        //Finds and adds all of the points together.
        Vector2 MovingTogether = Vector2.zero; //Starts at zero.
        foreach (Transform item in Neighbours)
        {
            MovingTogether += (Vector2)item.position; //Casting it as a Vector2 to stop an error, it needs to be casted or else its too ambigious, remember for other projects.
        }
        MovingTogether /= Neighbours.Count; //Averaging out all of the points.

        MovingTogether -= (Vector2)agent.transform.position; //sets the offset from agent position.
        MovingTogether = Vector2.SmoothDamp(agent.transform.up, MovingTogether, ref CurrentVelocity, AgentTransition);
        return MovingTogether;
    }


}
