using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;
using Unity.VisualScripting;

public class MonteCarloPlayer : MonoBehaviour
{
    [SerializeField] int debugLineIndex;
    PlayerInput playerInput;

    InternalMovement internalMovement;
    string[] readString;

    bool isReading;
    string[] lines;
    [SerializeField] int lineIndex = 0;

    string[] additiveMutation;
    AgentManager agentManager;

    float fitnessScore;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        internalMovement = GetComponent<InternalMovement>();

        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        TestPlayer();
    }
    
    void LateUpdate()
    {
        if (agentManager.terminalAge == true)
        {
            Death();
        }
        if(isReading == true)
        {
            string curLine;
            if (lineIndex < lines.Length)
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

    void Death()
    {

    }

    float EvaulateFitnessScore()
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
