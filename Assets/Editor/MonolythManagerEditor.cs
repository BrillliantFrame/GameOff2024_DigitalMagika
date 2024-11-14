using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonolythManager))]
class MonolythManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Shuffle Monolyths"))
            (target as MonolythManager).ShuffleMonolyths();
    }
}