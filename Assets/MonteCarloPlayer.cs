using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using NUnit.Framework.Internal;
using Unity.VisualScripting;

public class MonteCarloPlayer : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction playAction;

    InternalMovement internalMovement;
    string[] readString;

    bool startReading;
    string[] lines;
    int i = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playAction = playerInput.actions["Play Monte Carlo"];

        internalMovement = GetComponent<InternalMovement>();
    }
    
    void Update()
    {
        if(startReading == true)
        {
            if (lines[i] == "")
            {
                internalMovement.AlterMoveDir(0);
            }
            else
            {
                float.TryParse(lines[i], out float result);
                print(result);
                internalMovement.AlterMoveDir(result);
            }

            i++;
        }
    }

    public void TestPlayer()
    {
        lines = GetFileAsArray();
        startReading = true;
    }

    string[] GetFileAsArray()
    {
        string[] readString = File.ReadAllLines("Save.txt");
        return readString;
    }
}
