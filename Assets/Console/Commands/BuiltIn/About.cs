using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour
{
    // Values for the command
    public static bool Enabled = false;
    public static readonly string Description = "Shows information about the console";
    public static readonly string Usage = "{About}";

    // Custom Game Objects
    public GameObject info;
    public static GameObject infO;

    // Getting this command 
    public static About CreateScriptRefrence()
    {
        GameObject gameObject = null;

        // This only creates the gameobject once.
        if (gameObject == null)
        {
            gameObject = new GameObject();
        }
        return gameObject.AddComponent<About>();
    }

    // Activates the command
    public static void Activate(string Paramaters)
    {
        //About about = new About(); // Do not worry about the warning this gives. It will still work
        //About about = CreateScriptRefrence();

        if (infO != null)
        {
            infO.SetActive(!infO.activeInHierarchy);
            infO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "About\n\n" + "This console was made by dragmine149.\n" + "More to come!";
        }
        else
        {
            console.LogMsg.LogError("No about object found!");
        }
    }

    // Some extra onStart stuff
    public void Start()
    {
        Debug.Log(info);
        infO = info; // this makes the non-static static
    }
}
