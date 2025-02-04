using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class Playback : MonoBehaviour
{
    LowLevelMovement lowLevelMovement;

    [Header("Debug: ---> Don't edit!!! <----")]
    [SerializeField] int lineIndex = 0;
    [SerializeField] int jump;
    [SerializeField] int move;
    [SerializeField] bool isDead = false;

    List<string> genes = new List<string>();

    AgentManager agentManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lowLevelMovement = GetComponent<LowLevelMovement>();
        agentManager = GameObject.Find("Agent Manager").GetComponent<AgentManager>();

        // List<string> parentGenes = File.ReadAllLines(agentManager.curGenesPath)
        //     .OfType<string>()
        //     .ToList();

        // List<string> additiveMutation 
        //     = new List<string>(agentManager.GenerateGenes(agentManager.framesPerGeneration));

        genes.AddRange(File.ReadAllLines(agentManager.savePath));
        // genes.AddRange(additiveMutation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string curLine = " 0,0";

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

        if (lowLevelMovement.win == true)
        {
            lowLevelMovement.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.enabled = false;
        }
        
        lowLevelMovement.AlterMoveDir(move);
        lowLevelMovement.Jump(jump);
    }
}
