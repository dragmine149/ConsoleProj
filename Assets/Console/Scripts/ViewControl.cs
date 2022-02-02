using UnityEngine;
using TMPro;

public class ViewControl : MonoBehaviour
{
    public GameObject console;
    public TextMeshProUGUI text;

    public void Start()
    {
        console = FindObjectOfType<ConsoleGui>().consoleUiChild;
    }

    public void Change()
    {
        console.SetActive(!console.activeInHierarchy);
    }

    public void Update()
    {
        text.text = console.activeInHierarchy ? "Close Console" : "Open Console";
    }
}
