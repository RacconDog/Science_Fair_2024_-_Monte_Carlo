using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class AgentManager : MonoBehaviour
{
    [SerializeField] float childrenPerGenertaion = 0;
    [SerializeField] int lifeTime = 0;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject fittestAgent;

    // List<MonteCarloPlayer> aliveAgents = new List<MonteCarloPlayer>();

    [SerializeField] float jumpChance = 15;

    int curGen = 0;

    public void ClearGen()
    {
        File.WriteAllText("Save.txt", string.Empty);
        print("Clear Gen");
    }

    public void TestGen()
    {        
        File.WriteAllLines("Save.txt", GenerateGenes(lifeTime));

        // Delete last line (which was an empty line)
        var lines = System.IO.File.ReadAllLines("Save.txt");

        print("Test Gen");
    }

    public string[] GenerateGenes(int g_lifeTime)
    {
        string[] returnArray = new string[g_lifeTime];
        for (int i = 0; i < g_lifeTime; i++)
        {
            // Random int between -1 and 1
            int moveResult = Random.Range(-1, 2);

            int jumpResult = 0;
            if (Random.Range(0, 100) < jumpChance)
            {
                jumpResult = 1;
            }

            string line = 
                moveResult.ToString().PadLeft(2)
                + "," +
                jumpResult.ToString();
            returnArray[i] = line;
        }

        return returnArray;
    }
}
