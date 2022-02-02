using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public static readonly string Description = "Set an object active or inactive. This is not the same as hide and show as this stops any scripts working.";
    public static readonly string Usage = "{SetActive} <Game Object> <True/False>";

    public static void Activate(string Paramaters)
    {
        // Gets the game object
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
        string objValue = splitParm[splitParm.Length - 1]; // False or True (last value)
        if (objValue.ToLower() != "false" && objValue.ToLower() != "true")
        {
            console.LogMsg.LogError("Invalid option to set object to!");
            return;
        }
        if (objName == "Console")
        {
            console.LogMsg.LogError("Cannot set console activity to false");
            return;
        }

        bool found = false;
        // Changes the activity of the game object.
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (obj.name.Replace(" ", "").ToLower() == objName.Replace(" ", "").ToLower())
            {
                obj.SetActive(objValue.ToLower() == "true");
                found = true;
            }
        }
        if (!found)
        {
            Debug.Log("Unable to find gameobject in scene, Please make sure it is spelt correctly.");
            Debug.Log("Name Found: " + objName);
        }
    }
}
