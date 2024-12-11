using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class MonteCarloPlayer : MonoBehaviour
{
    PlayerInput playerInput;

    LowLevelMovement lowLevelMovement;

    int lineIndex = 0;

    public List<string> genes = new List<string>();
    public List<string> additiveMutation = new List<string>();

    AgentManager agentManager;

    void Start()
    {   
        lowLevelMovement = GetComponent<LowLevelMovement>();
        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        genes = File.ReadAllLines(agentManager.savePath)
            .OfType<string>()
            .ToList();

        genes.
    }
    
    void FixedUpdate()
    {
        string curLine = " 0,0";
        int move = 0;
        int jump = 0;

        if (lineIndex > agentManager.framesPerGeneration)
        {
            agentManager.deadChildren += 1;
            
            if(FitnessScore() > agentManager.highFitnessScore)
            {
                agentManager.highFitnessScore = FitnessScore();
                agentManager.WriteToSave(ameObject);
            }

            Destroy(this);
        }
        
        if (lineIndex < genes.Count)
        {
            curLine = genes[lineIndex].Trim();

            string[] columns = curLine.Split(",");

            if (columns.Length < 2)
            {
                Debug.LogError($"Invalid line format: Less than 2 columns. Line content: '{curLine}'");
                lineIndex++;  // Move to the next line, so we don’t get stuck
                return;
            }

            if (int.TryParse(columns[0].Trim(), out move))
            {
            // Try parsing the second column as jump
                if (int.TryParse(columns[1].Trim(), out jump))
                {
                // Both parsed successfully
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

            // lowLevelMovement.Jump(jump);
            lineIndex++;
        }


        lowLevelMovement.AlterMoveDir(move);
        lowLevelMovement.AlterMoveDir(jump);
    }

    public float FitnessScore()
    {
        return transform.position.x;
    }
}
