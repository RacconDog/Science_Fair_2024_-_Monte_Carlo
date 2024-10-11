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

    InternalMovement internalMovement;

    int lineIndex = 0;

    public List<string> lines = new List<string>();
    public List<string> additiveMutation = new List<string>();

    AgentManager agentManager;

    // float fitnessScore;

    void Start()
    {   
        internalMovement = GetComponent<InternalMovement>();
        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        // additiveMutation = mcGen.GenAdditiveMutaion(agentManager.lifeTime);
        for (int i = 0; i < additiveMutation.Count; i++)
        {
            // lines.
        }

        TestPlayer();
    }

    public void TestPlayer()
    {
        lines = File.ReadAllLines("Save.txt").OfType<string>().ToList();
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

            internalMovement.AlterMoveDir(move);
            internalMovement.Jump(jump);

            lineIndex++;
        }
    }

    public float EvaulateFitnessScore()
    {
        return transform.position.x;
    }
}
