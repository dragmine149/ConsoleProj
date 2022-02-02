#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeVisible))]
public class UiEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChangeVisible changeVisible = (ChangeVisible)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Logs"))
        {
            changeVisible.Logs();
        }
        if (GUILayout.Button("LogsText"))
        {
            changeVisible.StackTrace();
        }
        if (GUILayout.Button("Commands"))
        {
            changeVisible.EnterCommand();
        }
        if (GUILayout.Button("Options"))
        {
            changeVisible.ChangeOptions();
        }
    }
}
#endif