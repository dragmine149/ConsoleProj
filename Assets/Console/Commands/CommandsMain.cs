using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class CommandsMain : MonoBehaviour
{
    public InputField commandInput;
    public List<MonoBehaviour> Scripts;
    public static List<MonoBehaviour> scriptsForOthers;

    public void Start()
    {
        // Copies the Scripts list to the static list for use in other scripts
        scriptsForOthers = Scripts;
    }

    // get the command and fields
    public void CommandSubmit()
    {
        string calledCommand = commandInput.text;
        commandInput.text = ""; // reset for next use

        ProcessCommand(calledCommand);
    }

    // Get commands from the folders.
    #if UNITY_EDITOR
    private List<string> Commands => new List<string>(AssetDatabase.FindAssets("", new[] { "Assets/Console/Commands/BuiltIn", "Assets/Console/Commands/Custom" }));
    #endif

    private void ProcessCommand(string commandInput)
    {
        // split the command up to the smaller parts.
        string[] commandBreak = commandInput.Split(" ".ToCharArray());
        string commandCalled = commandBreak[0];
        string[] paramaters = new string[] { commandInput.Replace(commandCalled + " ", "") };

        bool found = false;
        // tries and finds command in the list of commands
#if UNITY_EDITOR
        foreach (string command in Commands)
        {
            string commandName = AssetDatabase.GUIDToAssetPath(command);
            commandName = Path.GetFileNameWithoutExtension(commandName);
            if (commandName.ToLower() == commandCalled.ToLower())
            {
                // runs command
                Type t = Type.GetType(commandName);
                MethodInfo info = t.GetMethod("Activate");
                info.Invoke(null, paramaters);
                found = true;
            }
        }
#else
        foreach (MonoBehaviour script in Scripts)
        {
            Type t = Type.GetType(script.name);
            MethodInfo info = t.GetMethod("Activate");
            info.Invoke(null, paramaters);
            found = true;
        }
#endif

        if (!found)
        {
            console.LogMsg.LogError($"{commandCalled} is not a valid command!");
        }
        else
        {
            FindObjectOfType<ChangeVisible>().EnterCommand(); // hides the command section after use
        }
    }

    public void GetCommands()
    {
        // To list all commands in doing the ListCommand function. (import script)
        string commands = "Commands:\n";
#if UNITY_EDITOR
        foreach (string command in Commands)
        {
            string ncommand = AssetDatabase.GUIDToAssetPath(command);
            ncommand = Path.GetFileNameWithoutExtension(ncommand);
            commands += $"{ncommand},\n";
        }
#else
        foreach (MonoBehaviour script in Scripts)
        {
            commands += $"{script.name},\n";
        }
#endif

        Debug.Log(commands);
    }
}
