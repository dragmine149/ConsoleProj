using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class HelpCommands : MonoBehaviour
{
    private GameObject FindChild => FindObjectOfType<GetChildren>().helpContent; // get help obj
    public bool Created = false; // detect if it has already been made, no point in remaking it.

    public void Activate()
    {
        if (!Created)
        {
            // This function will complain that it is not used, but it is in non editor builds.
            string[] GetCommands()
            {
                string[] Commands = new string[CommandsMain.scriptsForOthers.Count];
                for (int i = 0; i < CommandsMain.scriptsForOthers.Count; i++)
                {
                    MonoBehaviour script = CommandsMain.scriptsForOthers[i];
                    Commands[i] = script.name;
                }
                return Commands;
            }

            // INCLUDE ALL COMMANDS
#if UNITY_EDITOR
            string[] Commands = AssetDatabase.FindAssets("", new[] { "Assets/Console/Commands/BuiltIn", "Assets/Console/Commands/Custom" });
#else
            string[] Commands;
            Commands = GetCommands();
#endif

            foreach (string command in Commands)
            {
                // Get info about commands
#if UNITY_EDITOR
                string ncommand = AssetDatabase.GUIDToAssetPath(command);
                ncommand = Path.GetFileNameWithoutExtension(ncommand);
                Type t = Type.GetType(ncommand);
#else
                Type t = Type.GetType(command);
#endif



                // check if enabled
                bool Enabled = true;
                try
                {
                    FieldInfo fi = t.GetField("Enabled");
                    Enabled = (bool)fi.GetValue(fi);
                }catch (NullReferenceException){}

                if (Enabled)
                {       
                    // make object
                    GameObject Obj = GetAsset();
                    Obj.name = t.Name;
                    Obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);

                    // get description (and set)
                    try
                    {
                        FieldInfo fi = t.GetField("Description");
                        string Result = (string)fi.GetValue(fi);
                        Obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Result.TrimStart("".ToCharArray()) == "" ? $"{t.Name}: No Description found!" : $"{t.Name}: {Result}";
                    }
                    catch (NullReferenceException)
                    {
                        Obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{t.Name}: No Description found!";
                    }
                }
            }
            Created = true;
        }
    }

    private GameObject GetAsset()
    {
        // Get the asset that makes the button.
        UnityEngine.Object prefab = Resources.Load("Log");
        GameObject button = (GameObject)Instantiate(prefab);
        button.transform.SetParent(FindChild.transform);
        return button;
    }
}
