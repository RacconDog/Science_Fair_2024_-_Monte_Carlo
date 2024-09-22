using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    float highFitnessScore;
    [SerializeField] float lifeTime = 100;
    [SerializeField] float childrenPerGeneration = 0;
    float curGenerationAge = 0;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject fittestAgent;

    [HideInInspector] public bool terminalAge;

    List<MonteCarloPlayer> aliveAgents = new List<MonteCarloPlayer>();

    bool simStarted = false;

    public void StartSimulation()
    {
        BirthNextGeneration();
    }

    void Update()
    {
        if (!simStarted) return;
        
        if (terminalAge == true)
        {
            aliveAgents.Clear();
        }
        if (curGenerationAge == lifeTime)
        {
            terminalAge = true;
        }
        curGenerationAge++;
    }

    public void BirthNextGeneration()
    {
        for (int i = 0; i < childrenPerGeneration; i++)
        {
            aliveAgents.Add(Instantiate(agentPrefab, fittestAgent.transform.position, Quaternion.identity)
                .GetComponent<MonteCarloPlayer>());
        }
    }
}
