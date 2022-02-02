using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeVisible : MonoBehaviour
{
    public GameObject LogsUi;
    public GameObject LogsText;
    public GameObject Commands; // This is only going to be shown when a command is needed.
    public GameObject[] Options;

    public void Logs() => LogsUi.SetActive(!LogsUi.activeInHierarchy);
    public void StackTrace() => LogsText.SetActive(!LogsText.activeInHierarchy);
    public void EnterCommand() => Commands.SetActive(!Commands.activeInHierarchy);
    public void ChangeOptions()
    {
        foreach (GameObject option in Options)
        {
            option.SetActive(!option.activeInHierarchy);
        }
    }
}
