using UnityEngine;

public class KeyActive : MonoBehaviour
{
    public KeyCode Key = KeyCode.F9;
    public GameObject console;

    public void Start()
    {
        console = FindObjectOfType<ConsoleGui>().consoleUiChild;
    }

    public void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            console.SetActive(!console.activeInHierarchy);
        }
    }
}
