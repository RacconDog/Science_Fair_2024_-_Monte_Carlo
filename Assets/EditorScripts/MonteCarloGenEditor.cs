using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonteCarloGen))]
public class MonteCarloGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        MonteCarloGen gen = (MonteCarloGen)target;
        if(GUILayout.Button("TestGen"))
        {
            gen.TestGen();
        }
        if(GUILayout.Button("ClearGen"))
        {
            gen.ClearGen();
        }
    }
}
