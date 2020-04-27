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
    [Range(1.0f, 100.0f)] public float Drive = 10.0f;
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
        
    }
}
