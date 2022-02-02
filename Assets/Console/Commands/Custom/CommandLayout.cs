using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLayout : MonoBehaviour
{
    // OPTIONAL, have this to false if you do not want it to show on the help menu.
    // If this is not in the script, then it will be enabled by default. (Same as if it was set to true)
    public static bool Enabled = false;

    // Other scripts will refrence this value. For example the help script to help the user
    // If the string gets changed due to external variables, assaign it to a function to begin with.
    public static string Description = UpdateDescription();
    // If the string is not going to get changed, include readonly variable
    // public static readonly string Description = "This is a description";

    // This is another variable other scripts will refrence.
    // The point of this is to explain to the user how to use that command
    public static string Usage = "None";
    // Recommended layout '{Name} <Paramater1> <Paramater2> [?Optional Paramater1] [?Optional Paramater2]'
    // If this is not used, the output would be 'No Usage Found'
    // Same with description, add readonly variable if not going to be changed.


    // GAMEOBJECTS
    // Adding game objects to a script is easy, If a script requires a game object add the script to the "Commands" prefab under the console object.
    // After doing so, the commands will still work the same but you can refrence anything you want!
    // IT is not recommened that you call a function that is on the command object. It is perfectly fine to do so but it's there to get an outside object.
    // Want to orginase commands? Create empty gameobject children. These children can help you orgainse your commands.
    //
    // MOST IMPORTANTLY
    // Save your work to the prefab, If you want to re-import your console or use some commands in other scenes, saving the changes to the prefab will make
    // sure you don't have to reasign anything major.
    public static new GameObject gameObject;
    public GameObject nonStaticObject; // don't need new word for nonstatic.

    /// <summary>
    /// THIS IS A REQUIRED FUNCTION!!!
    ///
    /// This function is needed for this script to work, As the console relaies off using file names instead of called each class directly,
    /// this function needs to be here to start the progess.
    /// </summary>
    /// <param name="Paramaters">The inputs you get from the string.</param>
    public static void Activate(string Paramaters)
        // Paramater value is needed, without it the command will not work probably.
        // You might not need this value, but it still has to be passed through
    {
        // This won't work, static to non-static won't work
        //NonStatic();

        // This will let you call the non-static function.
        // CHANGE 'CommandLayout' to the name of the class!
        // Do not worry about the warning this gives. It will still work
        CommandLayout newThis = new CommandLayout(); // ~~can't have new() as unity gets annoyed~~
        newThis.NonStatic();
        newThis.PrivateNonStatic();

        // Static functions (private or public) will be able to be called normaly
        PrivateStatic();
        PublicStatic();

        // same thing happens when returning things, Just make sure to assaign them to a variable
        // '_' = discard the result.
        _ = newThis.ReturnNonStatic();
        _ = ReturnStatic();

        console.LogMsg.Log("Console Log Msg"); // New custom function, Logs messages like it was the console.
        console.LogMsg.LogWarning("Console Log Warning"); // same as the above.
        console.LogMsg.LogError("Console Log Error");

        // Working with gameobjects.
        // Works the same with private game objects, although why private game objects?
        gameObject.SetActive(true); // static required (in declariation) for it to work like this
        newThis.nonStaticObject.SetActive(false); // Or call the function (as shown on line 42) to work with non static objects.

        // Want to have 1 value with spaces? Make sure it has speach marks sourinding it.
        // This function will take the input and give you the output of the first value that has `"` around it
        _ = Remove(Paramaters);
    }

    /// <summary>
    /// This is an optional function, If in the unity editor, changes the description.
    /// Useful for cases where doing a command in the editor is different to in-game.
    ///
    /// Has to get called from GetDescription function to work.
    /// </summary>
    public static string UpdateDescription()
    {
        return Application.isEditor ? "List of commands (editor)" : "List of commands (play)";
    }

    #region NonStatic()
    public void NonStatic()
    {

    }

    private void PrivateNonStatic()
    {

    }
    #endregion

    #region Static()
    private static void PrivateStatic()
    {

    }

    public static void PublicStatic()
    {

    }
    #endregion

    #region Return()
    public string ReturnNonStatic()
    {
        return "Hello";
    }
    public static string ReturnStatic()
    {
        return "Hello";
    }
    #endregion

    #region RemoveSpeach()
    private static string Remove(string Paramaters)
    {
        string[] splitParm = Paramaters.Split(" ".ToCharArray());
        string objName = splitParm[0];
        if (splitParm[0].StartsWith('"'))
        {
            objName = "";
            foreach (string str in splitParm)
            {
                objName += str.Replace('"', ' ');
                if (str.EndsWith('"'))
                {
                    break;
                }
                objName += " ";
            }
        }
        return objName;
    }
    #endregion
}
