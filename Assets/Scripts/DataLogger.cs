using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    [SerializeField] bool append = true;
    [SerializeField] bool logData = true;

    public void AddDataRow(float totalGenerations, float childrenPerGeneration, float framesPerGeneration)
    {
        if (!logData) return;
        string savePath = Path.Combine(Application.dataPath, "Data");
        
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (StreamWriter writer = new StreamWriter(savePath, append))  // false means overwrite the file
        {            
            writer.WriteLine(
                totalGenerations + "\t" +
                childrenPerGeneration + "\t" +
                framesPerGeneration);
        }

    }
}
