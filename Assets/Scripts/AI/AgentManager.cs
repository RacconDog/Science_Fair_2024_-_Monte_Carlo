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
        string savePath = Path.Combine(Application.persistentDataPath, "Save.txt");

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);  // Create the directory if it doesn't exist
        }

        // Open the file in overwrite mode without writing anything to it
        using (StreamWriter writer = new StreamWriter(savePath, false))
        {
            // Do nothing, this clears the file by opening and closing in overwrite mode
        }

        Debug.Log("Save.txt file cleared.");
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
        string savePath = Path.Combine(Application.persistentDataPath, "Save.txt");

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);  // Create the directory if it doesn't exist
        }

        // Log the path to see if it's correct
        Debug.Log($"Saving to: {savePath}");

        using (StreamWriter writer = new StreamWriter(savePath, false))  // false means overwrite the file
        {
            writer.WriteLine(GenerateGenes(lifeTime));  // Writes the initial line "0,0"
            
            for (int i = 0; i < arrayToWrite.Length; i++)
            {
                writer.WriteLine(arrayToWrite[i]);  // WriteLine already appends a newline
            }
        }

        Debug.Log("Array written to Save.txt file.");
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
