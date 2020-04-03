using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behaviour/Align")]
public class AlignBehavior : FlockingBehavior
{
    public override Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock)
    {
        if (Neighbours.Count == 0)//if theres no neighbours, stay alligned.
        {
            return agent.transform.up; //Continue to face the way you are facing. //Might not be important if the sprite doesn't have a specific face.
        }

        //
        Vector2 AlignTogether = Vector2.zero; 
        foreach (Transform item in Neighbours)
        {
            AlignTogether += (Vector2)item.transform.up; //Casting it as a Vector2 to stop an error, important to remember.
        }
        AlignTogether /= Neighbours.Count; //Averaging out all of the points.

        return AlignTogether;
    }
}
