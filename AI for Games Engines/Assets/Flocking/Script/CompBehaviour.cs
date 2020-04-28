using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]

public class CompBehaviour : FlockingBehavior
{
    //Array of behaviour that is compositing together
    public FlockingBehavior[] behaviors;
    public float[] FlockWeights;

    public override Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock)
    {
        //Exception Throw to handle any data mismatching that may (but shouldn't) occur.
        if (FlockWeights.Length != behaviors.Length)
        {
            Debug.LogError("Data error" +  name, this);
            return Vector2.zero; //Stops the movement of said agent with error.
        }

        Vector2 move = Vector2.zero;
        //Iterating through all the behaviours.
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 MinorMovement = behaviors[i].MovementCalculation(agent, Neighbours, Flock) * FlockWeights[i];
            //A check to see if the weight is being limited to the extent of the weight.
            if (MinorMovement != Vector2.zero)//Checking to see if the movement is active.
            {
                if(MinorMovement.sqrMagnitude > FlockWeights[i] * FlockWeights[i])//Checking to see if the overall movement exceed the FlockWeight.
                {
                    MinorMovement.Normalize(); //Normalising the Vector2 back to 1.
                    MinorMovement *= FlockWeights[i]; //Multiplying back by the weight.
                }
                move += MinorMovement; //Setting the move
            }
        }

        return move;
    }
}
