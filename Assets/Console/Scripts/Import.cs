#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// This script setups the menus required for the console
public class Import : Editor
{
    public static bool LogView = false; // false = all, true = compact

    #region Console
    [MenuItem("Console/Setup", priority = -1)]
    public static void SetupConsole()
    {
        // This function is nice and easy to start, Easier to do like this rather than getting them to follow a path
        Create();
    }

    [MenuItem("Console/Reset", priority = -1)]
    public static void ResetConsole()
    {
        // This allows quick testing of something to do with the console obj.
        Remove();
        Create();
    }

    [MenuItem("Console/Mobile", priority = -1)]
    public static void MobileControl()
    {
        bool create = true;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (obj.name == "Console")
            {
                bool newButton = true;
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    if (obj.transform.GetChild(i).name == "MobileButtons")
                    {
                        EditorUtility.DisplayDialog("Console", "Mobile buttons have already been found, please remove to recreate", "Ok");
                        Debug.Log("Mobile buttons have already been found, please remove to recreate");
                        newButton = false;
                        break;
                    }
                }
                if (newButton)
                {
                    var mobilePrefab = Resources.Load("MobileButtons");
                    GameObject mobileButton = PrefabUtility.InstantiatePrefab(mobilePrefab) as GameObject;
                    mobileButton.transform.parent = obj.transform;
                }
                create = false;
                break;
            }
        }
        if (create)
        {
            EditorUtility.DisplayDialog("Console", "Console object has not been found, please add one for mobile control", "Ok");
            Debug.LogError("Console object has not been found, please add one for mobile control");
        }
    }
    #endregion

    #region Console/Console
    [MenuItem("Console/Console/Create", priority = 0)]
    public static void Create()
    {
        //makes sure that there is ONLY 1 object in the scene at once.
        bool create = true;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (obj.name == "Console")
            {
                // keep the old one
                Debug.LogError("Console Object Found, Please remove for new object");
                EditorUtility.DisplayDialog("Console", "Console object found, please remove to create a new object", "Ok");
                create = false;
            }
        }

        if (create)
        {
            // This creates the console in the scene
            GameObject console = new GameObject("Console");

            // This lets us add a menu for the undo, basically when undoing you know what happened here
            Undo.RegisterCreatedObjectUndo(console, "Create Console Object");

            // TODO: add componenets.
            console.AddComponent<Console>();
            console.AddComponent<ConsoleGui>();
            console.AddComponent<CreateLogs>();
            console.AddComponent<HelpCommands>();
            console.AddComponent<KeyActive>();

            // Adds the ui to the console
            var inst = FindObjectOfType<ConsoleGui>();
            inst.CreateUi();
            inst.HideUi();

            // Adds the commands prefab to the console
            var CommandsPrefab = Resources.Load("Commands");
            GameObject CommandsObj = PrefabUtility.InstantiatePrefab(CommandsPrefab) as GameObject;
            CommandsObj.transform.parent = console.transform;

            // setup a component for the console to talk to.
            console.GetComponentInChildren<Console>().logs = FindObjectOfType<CreateLogs>();
            console.GetComponentInChildren<KeyActive>().console = inst.consoleUiChild;
        }
    }

    [MenuItem("Console/Console/Remove", priority = 0)]
    public static void Remove()
    {
        // This removes the console object
        GameObject Console = GameObject.Find("Console");
        DestroyImmediate(Console);
    }

    [MenuItem("Console/Console/Mobile", priority = 0)]
    public static void Mobile()
    {
        MobileControl();
    }
    #endregion

    #region Console/Test
    [MenuItem("Console/Test/Text", priority = 1)]
    public static void Menu()
    {
        // Test sending a message
        Debug.Log("Menu did stuff");
    }

    [MenuItem("Console/Test/Lots", priority = 1)]
    public static void Lots()
    {
        // Tests sending lots of random messages (untill error)
        for (int i = 1; i < Random.Range(10, 100); i++)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Debug.Log(i);
                    break;
                case 1:
                    Debug.LogWarning(i);
                    break;
                case 2:
                    Debug.LogError(i);
                    break;
                default:
                    break;
            }
        }
    }


    [MenuItem("Console/Test/Delete", priority = 1)]
    public static void Delete()
    {
        // Test deletion
        FindObjectOfType<CreateLogs>().DeleteOldLogs();
    }
    #endregion

    #region Console/Test/Colaspe
    [MenuItem("Console/Test/Colaspe/Setup", priority = 2)]
    public static void ColaspeSetup()
    {
        // Changes view
        ShowUi();
        ChangeLogView();
    }

    [MenuItem("Console/Test/Colaspe/Initiate", priority = 2)]
    public static void CollaspeTest()
    {
        // Test
        for (int i = 0; i < 4; i++)
        {
            Debug.LogWarning("warning");
        }
    }
    #endregion

    #region Console/Ui
    // These below will have buttons in-game to work. For now this.
    // (self-explanotory commands)
    [MenuItem("Console/Ui/Show", priority = 3)]
    public static void ShowUi()
    {
        FindObjectOfType<ConsoleGui>().ShowUi();
    }

    [MenuItem("Console/Ui/Hide", priority = 3)]
    public static void HideUi()
    {
        FindObjectOfType<ConsoleGui>().HideUi();
    }

    [MenuItem("Console/Ui/ChangeLogs", priority = 3)]
    public static void ChangeLogView()
    {
        bool Current = CreateLogs.CollaspeMode;
        FindObjectOfType<CreateLogs>().ChangeCollaspe(!Current);
    }
    #endregion

    #region Console/Commands
    [MenuItem("Console/Command/Custom", priority = 4)]
    public static void Commands()
    {
        // Might remove this
        EditorUtility.DisplayDialog("Console", "This is still a wip feature. The above is for later...", null);
    }

    [MenuItem("Console/Command/FindCommands", priority = 4)]
    public static void FindCommands()
    {
        var inst = new CommandsMain();
        inst.GetCommands();
    }
    #endregion
}
#endif