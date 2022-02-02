using UnityEngine;
using TMPro;

public class GetChildren : MonoBehaviour
{
    // This script just holds game objects for other scripts to refrence from.
    public GameObject logsContent;
    public GameObject helpContent;
    public GameObject helpObject;
    public GameObject otherObject;
    public TextMeshProUGUI logContentText;

    public void ChangeMode()
    {
        FindObjectOfType<ConsoleGui>().ChangeMode();
    }

    public GameObject GetLogChild()
    {
        return logsContent;
    }
}
