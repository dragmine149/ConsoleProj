using UnityEngine;

public class Show : MonoBehaviour
{
    public static readonly string Description = "Shows a gameobject in the scene";
    public static readonly string Usage = "{Show} <Game Object>";

    // Get gameobject (THAT IS ACTIVE) and shows it
    public static void Activate(string Paramaters)
    {
        GameObject obj = GameObject.Find(Paramaters);
        if (obj.GetComponentInChildren<CanvasGroup>() == null)
        {
            obj.AddComponent<CanvasGroup>();
        }
        obj.GetComponentInChildren<CanvasGroup>().alpha = 1;
    }
}
