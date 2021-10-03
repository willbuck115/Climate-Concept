using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Population))]
public class DebugPopulation : Editor
{

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Population population = (Population)target;

        if(GUILayout.Button("Increase Population")) {
            population.UpdatePopulation(100);
        }
        else if(GUILayout.Button("Decrease Population")) {
            population.UpdatePopulation(100);
        }
    }

}
