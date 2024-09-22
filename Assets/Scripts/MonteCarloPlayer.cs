using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;

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

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        internalMovement = GetComponent<InternalMovement>();

        TestPlayer();
    }
    
    void LateUpdate()
    {
        if(isReading == true)
        {
            string curLine;
            if (lineIndex < lines.Length)
            {
                curLine = lines[lineIndex];
            }
            else
            {
                curLine = "00";
            }

            internalMovement.AlterMoveDir((float)Char.GetNumericValue(curLine[0]) - 1);
            internalMovement.Jump((int)Char.GetNumericValue(curLine[1]));
            
            lineIndex++;
        }
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
