using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class MonteCarloPlayer : MonoBehaviour
{
    [SerializeField] int debugLineIndex;
    PlayerInput playerInput;

    InternalMovement internalMovement;

    bool isReading;
    [SerializeField] int lineIndex = 0;

    public List<string> lines = new List<string>();
    public List<string> additiveMutation = new List<string>();

    AgentManager agentManager;
    MonteCarloGen mcGen;

    float fitnessScore;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        internalMovement = GetComponent<InternalMovement>();

        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();
        mcGen = GameObject.Find("Agent Manager").GetComponent<MonteCarloGen>();

        additiveMutation = mcGen.GenAdditiveMutaion(agentManager.lifeTime);
        for (int i = 0; i < additiveMutation.Count; i++)
        {
            // lines.
        }

        TestPlayer();
    }
    
    void LateUpdate()
    {
        if(isReading == true)
        {
            string curLine;
            if (lineIndex < lines.Count)
            {
                curLine = lines[lineIndex];
            }
            else
            {
                curLine = "10";
            }

            internalMovement.AlterMoveDir((float)Char.GetNumericValue(curLine[0]) - 1);
            internalMovement.Jump((int)Char.GetNumericValue(curLine[1]));

            lineIndex++;
        }
    }

    public float EvaulateFitnessScore()
    {
        return transform.position.x;
    }

    public void TestPlayer()
    {
        // lines = GetFileAsArray();
        lines = File.ReadAllLines("Save.txt");
        isReading = true;
    }

    // string[] GetFileAsArray()
    // {
    //     string[] readString = File.ReadAllLines("Save.txt");
    //     return readString;
    // }
}
