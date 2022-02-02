using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public static string Description = GetDescription();
    public static readonly string Usage = "{Quit}";

    public static void Activate(string paramaters)
    {
        Application.Quit();  // Quit app
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();  // Stops playtime
#endif
    }

    public static string GetDescription()
    {
        return Application.isEditor ? "Stops the playmode" : "Closes the app.";
    }
}
