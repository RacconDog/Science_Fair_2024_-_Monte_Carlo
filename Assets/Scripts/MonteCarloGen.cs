using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Random = UnityEngine.Random;

public class MonteCarloGen : MonoBehaviour
{
    [SerializeField] float jumpChance = 15;

    public void ClearGen()
    {
        File.WriteAllText("Save.txt", string.Empty);
    }
    public void TestGen()
    {
        //Hold Right
        StreamWriter sw = new StreamWriter("Save.txt");
        for (int i = 0; i < 5000; i++)
        {
            int jumpResult;
            if (Random.Range(0, 100) < jumpChance)
            {
                jumpResult = 1;
            }
            else
            {
                jumpResult = 0;
            }
            
            int moveResult = Random.Range(0,3);

            string line = moveResult.ToString() + jumpResult.ToString();
            print(line);
            sw.WriteLine(line);
            // sw.WriteLine(1);
        }

        sw.Close();
    }

    public string[] GenAdditiveMutaion(int lifeTime)
    {
        string[] returnArray = new string[lifeTime];
        for (int i = 0; i < lifeTime; i++)
        {
            int jumpResult;
            if (Random.Range(0, 100) < jumpChance)
            {
                jumpResult = 1;
            }
            else
            {
                jumpResult = 0;
            }
            
            int moveResult = Random.Range(0,3);

            string line = moveResult.ToString() + jumpResult.ToString();
            returnArray[i] = line;
        }

        return returnArray;
    }
}
