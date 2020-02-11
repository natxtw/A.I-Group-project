using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingBehavior : ScriptableObject
{
    //Using an abstract class because i don't want to instansiate any behaviour in this script

    public abstract Vector2 MovementCalculation(FlockingAgent agent, List<Transform> Neighbours, Flocking Flock);
}
