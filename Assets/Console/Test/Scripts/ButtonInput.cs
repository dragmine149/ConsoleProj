using UnityEngine;

// This is a script for the buttons in the scene 'TestScene'
public class ButtonInput : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("On Start");
    }
    public void Message()
    {
        Debug.Log("Hello World");
    }
    public void WarnMessage()
    {
        Debug.LogWarning("This is a warning");
    }
    public void ErrorMessage()
    {
        Debug.LogError("This is an error");
    }
}
