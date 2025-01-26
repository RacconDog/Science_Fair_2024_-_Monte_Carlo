using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using UnityEditor;

public class AgentManager : MonoBehaviour
{
    [HideInInspector] public string savePath;
    [SerializeField] GameObject agentPrefab;
    
    [SerializeField] float childrenPerGeneration;
    [SerializeField] public int framesPerGeneration;
    [SerializeField] float jumpChance;

    [SerializeField] public string curGenesPath;
    [SerializeField] public string totalGenesPath;

    [Header("Debug: ---> Don't edit!!! <----")]
    [SerializeField] public GameObject fittestAgent;
    [SerializeField] public float highFitnessScore;
    [SerializeField] public Vector2 parentPos;
    public List<GameObject> deadChildren = new List<GameObject>();

    [Header("Collision Settings")]
    [SerializeField] private LayerMask collisionLayer; // Assign this to your block layer
    [SerializeField] private float collisionCheckDistance = 0.1f;

    int curGen = 0;

    void Awake()
    {
        savePath = Path.Combine(Application.dataPath, curGenesPath);
        curGenesPath = "Assets/" + curGenesPath; 
        if (File.Exists(savePath))
        {
            string content = File.ReadAllText(savePath);
                // Debug.Log(content);
        }
        else
        {
            Debug.LogError("File not found" + savePath);
        }
    }

    void Update()
    {
        HandleCollisions();
    }

    private void HandleCollisions()
    {
        foreach (GameObject agent in GameObject.FindGameObjectsWithTag("Agent"))
        {
            Rigidbody2D rb = agent.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            Vector2 position = rb.position;
            Vector2 velocity = rb.linearVelocity;

            // Horizontal collision check
            Vector2 direction = velocity.x > 0 ? Vector2.right : Vector2.left;
            RaycastHit2D horizontalHit = Physics2D.Raycast(position, direction, collisionCheckDistance, collisionLayer);

            if (horizontalHit.collider != null)
            {
                velocity.x = 0; // Stop horizontal movement
            }

            // Ground collision check
            Vector2 groundCheckPosition = position + new Vector2(0, -0.5f); // Adjust offset based on agent size
            RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPosition, Vector2.down, collisionCheckDistance, collisionLayer);

            bool isGrounded = groundHit.collider != null;

            if (isGrounded)
            {
                velocity.y = Mathf.Max(velocity.y, 0); // Prevent downward movement
            }

            rb.linearVelocity = velocity;
        }
    }

    public void BirthNewChildren()
    {
        for (int i = 0; i < deadChildren.Count; i++)
        {
            Destroy(deadChildren[i]);
        }

        deadChildren.Clear();

        for (int i = 0; i < childrenPerGeneration; i++)
        {
            Instantiate(agentPrefab, new Vector3(parentPos.x, 0, 0), Quaternion.identity);
        }
    }

    public void ClearGen()
    {
        string savePath = Path.Combine(Application.dataPath, curGenesPath);

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Open the file in overwrite mode without writing anything to it
        using (StreamWriter writer = new StreamWriter(savePath, false))
        {
            // Do nothing, this clears the file by opening and closing in overwrite mode
        }

        Debug.Log(curGenesPath + " file cleared.");
    }

    public void NewGenes()
    {   
        print(savePath);
        string[] genes = GenerateGenes(framesPerGeneration);

        WriteToSave(genes, curGenesPath);
    }
    
    public void WriteToSave(string[] arrayToWrite, string path)
    {
        string savePath = Path.Combine(Application.dataPath, path);

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (StreamWriter writer = new StreamWriter(savePath, false))  // false means overwrite the file
        {            
            for (int i = 0; i < arrayToWrite.Length; i++)
            {
                writer.WriteLine(arrayToWrite[i]);
            }
        }

        Debug.Log("Array written to" + path + " file.");
    }

    public string[] GenerateGenes(int g_FramesPerGeneration)
    {
        string[] returnArray = new string[g_FramesPerGeneration];
        for (int i = 0; i < g_FramesPerGeneration; i++)
        {
            // Random int between -1 and 1
            int moveResult = Random.Range(-1, 2);

            int jumpResult = 0;
            if (Random.Range(0, 100) < jumpChance)
            {
                jumpResult = 1;
            }

            string line = 
                moveResult.ToString().PadLeft(2)
                + "," +
                jumpResult.ToString();
            returnArray[i] = line;
        }

        return returnArray;
    }
}
