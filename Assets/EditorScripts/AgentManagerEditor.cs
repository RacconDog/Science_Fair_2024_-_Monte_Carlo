using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentManager))]
public class AgentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        AgentManager agentManager = (AgentManager)target;
        if(GUILayout.Button("Next Gen Debug"))
        {
            agentManager.BirthNextGeneration();
        }

        if(GUILayout.Button("Start Simulation"))
        {
            agentManager.BirthNextGeneration();
        }
    }
}
