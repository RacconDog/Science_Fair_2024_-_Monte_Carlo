using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System;
using NUnit.Framework;
using Unity.VisualScripting;

public class MonteCarloPlayer : MonoBehaviour
{
    PlayerInput playerInput;

    LowLevelMovement lowLevelMovement;

    [Header("Debug: ---> Don't edit!!! <----")]
    [SerializeField] int lineIndex = 0;
    [SerializeField] int jump;
    [SerializeField] int move;
    [SerializeField] bool isDead = false;
    [SerializeField] bool isFallDead = false;
    
    public Vector3 lastSafePos;

    List<string> genes = new List<string>();

    AgentManager agentManager;

    void Start()
    {   
        lowLevelMovement = GetComponent<LowLevelMovement>();
        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        List<string> parentGenes = File.ReadAllLines(agentManager.curGenesPath)
            .OfType<string>()
            .ToList();

        List<string> additiveMutation 
            = new List<string>(agentManager.GenerateGenes(agentManager.framesPerGeneration));

        // genes.AddRange(parentGenes);
        genes.AddRange(additiveMutation);
    }
    
    public void Init(Vector3 birthPosition)
    {
        transform.position = birthPosition;
        lineIndex = 0;
        jump = 0;
        move = 0;
        isDead = false;
        isFallDead = false;
        GetComponent<SpriteRenderer>().enabled = true;

        List<string> additiveMutation 
            = new List<string>(agentManager.GenerateGenes(agentManager.framesPerGeneration));
        genes.AddRange(additiveMutation);
    }

    void FixedUpdate()
    {
        string curLine = " 0,0";

        if (lineIndex == agentManager.framesPerGeneration && isDead == false)
        {
            isDead = true;
            agentManager.deadChildren.Add(this.gameObject);
            
            // if (FitnessScore() > agentManager.highFitnessScore)
            // {
            //     agentManager.fittestGenes.AddRange(genes);
            //     agentManager.parentPos = this.transform.position;
            // }
        }

        // End of generation
        if (lineIndex == agentManager.framesPerGeneration && GetFitnessScore() > agentManager.highFitnessScore)
        {
            bool gonnaDie = false;

            if (! gonnaDie) {
                // Make me the fittest agent
                Debug.Log("end of generation: fittestAgent is " + this.gameObject.name, this.gameObject);
                agentManager.highFitnessScore = GetFitnessScore();
                agentManager.fittestAgent = this;
            }
        }
        
        if (lineIndex < genes.Count)
        {
            curLine = genes[lineIndex].Trim();

            string[] col = curLine.Split(",");

            if (col.Length < 2)
            {
                Debug.LogError($"Invalid line format: Less than 2 columns. Line content: '{curLine}'");
                lineIndex++;  // Move to the next line, so we don’t get stuck
                return;
            }

            if (int.TryParse(col[0].Trim(), out move))
            {
            // Try parsing the second column as jump
                if (int.TryParse(col[1].Trim(), out jump))
                {
                }
                else
                {
                    Debug.LogError($"Invalid jump value at line {lineIndex}: '{col[1]}'");
                }
            }
            else
            {
                Debug.LogError($"Invalid move value at line {lineIndex}: '{col[0]}'");
            }

            lineIndex++;
        }

        if (isDead == true)
        {
            move = 0;
            jump = 0;
        }
        // lowLevelMovement.Jump(jump);
        lowLevelMovement.AlterMoveDir(move);
        lowLevelMovement.Jump(jump);

        if (lowLevelMovement.win == true && !agentManager.hasRecordedDataThisRun)
        {
            agentManager.WriteToSave(agentManager.fittestGenes.ToArray(), agentManager.savePath);

            agentManager.hasRecordedDataThisRun = true;
            agentManager.dataLogger.AddDataRow(
                agentManager.curGen * agentManager.framesPerGeneration,
                agentManager.childrenPerGeneration,
                agentManager.framesPerGeneration);

            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void FallToDeath()
    {
        isFallDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public float GetFitnessScore()
    {
        if (isFallDead) {
            return 0;
        }
        
        return transform.position.x + .8f * transform.position.y;
    }
}
