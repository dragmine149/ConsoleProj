using UnityEngine;

public class ConsoleGui : MonoBehaviour
{
    public GameObject consoleUiChild;

    // Creates the consoleUI
    public void CreateUi()
    {
        GameObject Console = GameObject.Find("Console");
        GameObject consoleUi = null;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (obj.name == "Ui(Clone)")
            {
                consoleUi = obj;                
            }
        }

        // Check if an ui object is already there
        bool create = consoleUi == null;

        if (create)
        {
            Object prefab = Resources.Load("Ui");
            consoleUi = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);  // copyes the prefab and set it to the center

            consoleUi.transform.SetParent(Console.transform);

            consoleUiChild = consoleUi;
        }
    }

    // These just means we aren't deleting and replace the ui every time. Better.
    public void HideUi()
    {
        GameObject consoleUi = consoleUiChild;
        consoleUi.SetActive(false);
    }
    public void ShowUi()
    {
        GameObject consoleUi = consoleUiChild;
        consoleUi.SetActive(true);
        FindObjectOfType<CreateLogs>().UpdateLogUi(); // update ui on ui show.
    }

    // Changes the mode the console is viewed in (collaspe or expanded)
    public void ChangeMode()
    {
        bool Current = CreateLogs.CollaspeMode;
        FindObjectOfType<CreateLogs>().ChangeCollaspe(!Current);
    }
}
