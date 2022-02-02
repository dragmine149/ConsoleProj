using UnityEngine;

public class Echo : MonoBehaviour
{
    public static readonly string Description = "Prints what you put in the console back to the console";
    public static readonly string Usage = "{Echo} <Input>";

    // This command prints what you put in into the console.
    public static void Activate(string Input)
    {
        console.LogMsg.Log(Input);
    }

}
