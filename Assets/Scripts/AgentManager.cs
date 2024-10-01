using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AgentManager : MonoBehaviour
{
    float highFitnessScore = 0;
    [SerializeField] float childrenPerGeneration = 0;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject fittestAgent;

    [SerializeField] public int lifeTime;

    List<MonteCarloPlayer> aliveAgents = new List<MonteCarloPlayer>();

    bool simStarted = false;

    public void StartSimulation()
    {
        for (int i = 0; i < childrenPerGeneration; i++)
        {
            aliveAgents.Add(Instantiate(agentPrefab, fittestAgent.transform.position, Quaternion.identity)
                .GetComponent<MonteCarloPlayer>());
        }
    }

    public void BirthNextGeneration()
    {
        for (int i = 0; i < childrenPerGeneration; i++)
        {
            float fitScore = aliveAgents[i].EvaulateFitnessScore();
            if (fitScore > highFitnessScore)
            {
                highFitnessScore = fitScore;
                StreamWriter sw = new StreamWriter("Save.txt");

                for (int ii = 0; ii < aliveAgents[i].additiveMutation.Count; ii++)
                {  
                    sw.WriteLine(aliveAgents[i].additiveMutation[ii]);
                }

                sw.Close();
            }
        }
    }
}
