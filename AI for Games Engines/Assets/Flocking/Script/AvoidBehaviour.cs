using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoid")]
public class AvoidBehaviour : FlockingBehavior
{
    public override Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock)
    {
        if (Neighbours.Count == 0)
        {
            return Vector2.zero;
        }

        //Finds and adds all of the points together.
        int NumberOfObjectsToAvoid = 0; //keeping count on te amount of objects around.
        Vector2 AvoidAlone = Vector2.zero; 
        foreach (Transform item in Neighbours)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < Flock.SquaredAvoidanceRadius)
            {
                NumberOfObjectsToAvoid++;
                AvoidAlone += (Vector2)(agent.transform.position - item.position); //Casting it as a Vector2 to stop an error, important to remember.
            }
        }
        
        if(NumberOfObjectsToAvoid > 0) //if the number of objects to avoid is greater than 0, create the average point.
        {
            AvoidAlone /= NumberOfObjectsToAvoid;
        }
        return AvoidAlone;
    }
}
