using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    //Agents
    public FlockingAgent AgentPrefab;
    List<FlockingAgent> Agents = new List<FlockingAgent>(); //Casts all of the agents in here, to later be iterated.
    [Range(50, 500)] public int StartingAgentCount = 200; //Starting number of agents.
    const float AgentDensity = 0.08f;

    //Behaviour
    public FlockingBehavior behavior;
    //Range Adds a slider between two numbers
    [Range(1.0f, 100.0f)] public float Drive = 10.0f; //Drive is an attempt at the boids sudden burst of movement.
    [Range(1.0f, 100.0f)] public float MaxSpeed = 5.0f;
    [Range(1.0f, 10.0f)] public float NeighbourRadius = 3.0f;
    [Range(0.0f, 1.0f)] public float AgentAvoidanceRadiusMultiplier = 0.5f;

    float MaxSpeedSquared; float NeighbourRadiusSquared;
    float AvoidanceRadiusSquared;
    public float SquaredAvoidanceRadius { get { return AvoidanceRadiusSquared; } }

    void Start()
    {
        MaxSpeedSquared = MaxSpeed * MaxSpeed;
        NeighbourRadiusSquared = NeighbourRadius * NeighbourRadius;
        AvoidanceRadiusSquared = NeighbourRadiusSquared * AgentAvoidanceRadiusMultiplier * AgentAvoidanceRadiusMultiplier;

        for (int i = 0; i < StartingAgentCount; i++)
        {
            FlockingAgent newAgent = Instantiate(AgentPrefab, Random.insideUnitCircle * StartingAgentCount * AgentDensity,
            Quaternion.Euler(Vector3.forward * Random.Range(0.0f, 360.0f)), transform);
            newAgent.name = "Agent " + i;
            Agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach(FlockingAgent agent in Agents)
        {
            List<Transform> neighbours = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, neighbours.Count / 6f); //REMOVE LATER
            
            //calculating a move based on the agent and their near by objects
            Vector2 move = behavior.MovementCalculation(agent, neighbours, this);
            move *= Drive;
            if(move.sqrMagnitude > MaxSpeedSquared) //if the movespeed is too fast.
            {
                move = move.normalized * MaxSpeed; //ensures it caps at the maximum speed by normalising it and then multiplying by max speed value.
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockingAgent agent)
    {
        List<Transform> neighbours = new List<Transform>(); //List to populate with the agents neighbours.
        Collider2D[] NeighboursColliders = Physics2D.OverlapCircleAll(agent.transform.position, NeighbourRadius); //Using overlapcircleAll because the circle type collider is best for my agents.

        foreach(Collider2D col in NeighboursColliders) //For each collider in the array. If its not my own collider. Add to the list.
        {
            if(col != agent.PublicAccessAgentCollider) 
            {
                neighbours.Add(col.transform); 
            }

        }
        return neighbours;
    }
}
