using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    public static readonly string Description = "Shows information about this console";
    public static readonly string Usage = "{Info}";
    public static TextMeshProUGUI LogText => FindObjectOfType<GetChildren>().logContentText;

    // Shows / Hides the help command
    public static void Activate(string paramaters)
    {
        // gets objs
        GameObject otherObject = FindObjectOfType<GetChildren>().otherObject;
        GameObject helpObject = FindObjectOfType<GetChildren>().helpObject;

        // hides / shows both
        otherObject.SetActive(!otherObject.activeInHierarchy);
        helpObject.SetActive(false);

        LogText.gameObject.SetActive(!otherObject.activeInHierarchy); // hides the information of that message

        // Gives stuff
        if (otherObject.activeInHierarchy)
        {
            otherObject.GetComponentInChildren<TextMeshProUGUI>().text = "" +
            "Custom Console\n" +
            "First off, thank you for using this console, It doesnt matter if you can't support me, just using this is enough\n" +
            "\n" +
            "This console was made near the end of 2021 where i (dragmine149) wanted a console which shows logs and could use commands for my game.\n" +
            "Making games for other devices (e.g. mobile, console) can't easily be debugged as they might be some issues with that device specifically\n" +
            "In the past i have found a console which shows logs out and does the job very well, but couldn't run commands in...\n" +
            "And when debugging on other devices, it is nice to change values when needed.\n" +
            "\n" +
            "Other the past couple of months (Follow me on twitter to see progress https://twitter.com/DragMine149) i've been making this console to do all the jobs i want it to do\n" +
            "These include outputing logs and letting the user enter commands, As well as this, i wanted the console to be scaleable and expandable in the future\n" +
            "This could have been a small job, But i wanted it to be easy to use and sharable\n" +
            "\n" +
            "\n" +
            "\n" +
            "As their are built-in commands, if you would like to suggest a command, please reply in the itch.io section (not all will get accepted)\n" +
            "Again, thank you and i hope that this console has helped you in your application develepoment process  --drag";
        }
    }
}
