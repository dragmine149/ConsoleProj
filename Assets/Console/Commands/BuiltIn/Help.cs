using UnityEngine;
using TMPro;

public class Help : MonoBehaviour
{
    public static readonly string Description = "Shows this Screen";
    public static readonly string Usage = "{Help}";
    public static TextMeshProUGUI LogText => FindObjectOfType<GetChildren>().logContentText;

    // Shows / Hides the help command
    public static void Activate(string paramaters)
    {
        // gets objs
        GameObject otherObject = FindObjectOfType<GetChildren>().otherObject;
        GameObject helpObj = FindObjectOfType<GetChildren>().helpObject;

        // hides / shows both
        otherObject.SetActive(false);
        helpObj.SetActive(!helpObj.activeInHierarchy);

        LogText.gameObject.SetActive(!helpObj.activeInHierarchy); // hides the information of that message

        if (helpObj.activeInHierarchy)
        {
            FindObjectOfType<HelpCommands>().Activate();
        }
    }
}
