using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;

public class MonteCarloPlayer : MonoBehaviour
{
    PlayerInput playerInput;

    LowLevelMovement lowLevelMovement;

    int lineIndex = 0;

    public List<string> lines = new List<string>();
    public List<string> additiveMutation = new List<string>();

    AgentManager agentManager;

    // float fitnessScore;

    void Start()
    {   
        lowLevelMovement = GetComponent<LowLevelMovement>();
        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        TestPlayer();
    }

    public void TestPlayer()
    {
        lines = File.ReadAllLines(agentManager.savePath)
            .OfType<string>()
            .ToList();
    }
    
    void FixedUpdate()
    {
        string curLine = "10";

        if (lineIndex < lines.Count)
        {
            curLine = lines[lineIndex].Trim();

            string[] columns = curLine.Split(",");

            if (columns.Length < 2)
            {
                Debug.LogError($"Invalid line format: Less than 2 columns. Line content: '{curLine}'");
                lineIndex++;  // Move to the next line, so we don’t get stuck
                return;
            }

        // Try parsing the first column as move
            if (int.TryParse(columns[0].Trim(), out int move))
            {
            // Try parsing the second column as jump
                if (int.TryParse(columns[1].Trim(), out int jump))
                {
                // Both parsed successfully
                    lowLevelMovement.AlterMoveDir(move);
                    lowLevelMovement.Jump(jump);
                }
                else
                {
                    Debug.LogError($"Invalid jump value at line {lineIndex}: '{columns[1]}'");
                }
            }
            else
            {
                Debug.LogError($"Invalid move value at line {lineIndex}: '{columns[0]}'");
            }

            lineIndex++;
        }
    }

    public float EvaulateFitnessScore()
    {
        return transform.position.x;
    }
}
