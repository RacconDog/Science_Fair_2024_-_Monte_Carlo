using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentManager))]
public class AgentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        AgentManager agentManager = (AgentManager)target;
        if(GUILayout.Button("Clear Generation"))
        {
            agentManager.ClearGen();
        }

        if(GUILayout.Button("Generate New Genes"))
        {
            agentManager.NewGenes();
        }

        if(GUILayout.Button("BirthNewChildren"))
        {
            agentManager.BirthNewChildren();
        }
    }
}
