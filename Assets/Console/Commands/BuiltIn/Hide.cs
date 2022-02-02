using UnityEngine;

public class Hide : MonoBehaviour
{
    public static readonly string Description = "Hide a gameobject in the scene (GAMEOBJECT IS STILL ACTIVE)";
    public static readonly string Usage = "{Hide} <Game Object>";

    // Hides selected object
    public static void Activate(string Paramaters)
    {
        GameObject obj = GameObject.Find(Paramaters);
        if (obj.GetComponentInChildren<CanvasGroup>() == null)
        {
            obj.AddComponent<CanvasGroup>();
        }
        obj.GetComponentInChildren<CanvasGroup>().alpha = 0;
    }
}
