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
            curLine = lines[lineIndex];

            string[] columns = curLine.Split(",");
            int move = Int32.Parse(columns[0]);
            int jump = Int32.Parse(columns[1]);

            // print($"{lineIndex}: {move}, {jump}");

            lowLevelMovement.AlterMoveDir(move);
            lowLevelMovement.Jump(jump);

            lineIndex++;
        }
    }

    public float EvaulateFitnessScore()
    {
        return transform.position.x;
    }
}
