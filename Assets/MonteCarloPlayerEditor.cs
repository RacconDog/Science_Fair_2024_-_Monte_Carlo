using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonteCarloPlayer))]
public class MonteCarloPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        MonteCarloPlayer player = (MonteCarloPlayer)target;
        if(GUILayout.Button("TestPlayer"))
        {
            player.TestPlayer();
        }
    }
}
