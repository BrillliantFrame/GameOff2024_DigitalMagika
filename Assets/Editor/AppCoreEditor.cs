using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AppCore))]
class AppCoreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Back to main"))
            AppCore.Instance?.BackToMain();
    }
}