using System.IO;
using UnityEditor;
using UnityEngine;

public class MonteCarloGen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearGen()
    {
        File.WriteAllText("Save.txt", string.Empty);
    }
    public void TestGen()
    {
        //Hold Right
        StreamWriter sw = new StreamWriter("Save.txt");
        for (int i = 0; i < 10000; i++)
        {
            sw.WriteLine("1");
        }

        sw.Close();
    }
}
