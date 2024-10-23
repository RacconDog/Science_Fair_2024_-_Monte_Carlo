using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class AgentManager : MonoBehaviour
{
    [HideInInspector] public string savePath;
    
    [SerializeField] float childrenPerGenertaion = 0;
    [SerializeField] int lifeTime = 0;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject fittestAgent;

    [SerializeField] float jumpChance = 15;

    int curGen = 0;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Save.txt");

        if (File.Exists(savePath))
        {
            string content = File.ReadAllText(savePath);
            Debug.Log(content);
        }
        else
        {
            Debug.LogError("File not found" + savePath);
        }
    }

    public void ClearGen()
    {
        File.WriteAllText(savePath, string.Empty);
        print("Clear Gen");
        Debug.Log("Clear Gen");
    }

    public void TestGen()
    {   
        print(savePath);
        string[] genes = GenerateGenes(lifeTime);

        SWWriteAllLines(genes);

        Debug.Log(genes[0]);
    }

    void SWWriteAllLines(string[] arrayToWrite)
    {
        using (StreamWriter writer = new StreamWriter(savePath, false))
        {
            writer.WriteLine("Test");
            for (int i = 0; i < arrayToWrite.Length; i++)
            {
                writer.WriteLine(arrayToWrite[i] + "\n");
            }
        }

        Debug.Log("Array written to file with newline characters.");
    }
    //for the length of the aray, write that aray in [i] + /n
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
