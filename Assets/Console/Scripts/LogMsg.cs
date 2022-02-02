using UnityEngine;
namespace console
{
    // This makes the output be '[Console]: ' to make it look like it's from the console.
    public static class LogMsg
    {
        public static void Log(object Message)
        {
            Debug.Log($"[Console]: {Message}");
        }

        public static void LogWarning(object Message)
        {
            Debug.LogWarning($"[Console Warning]: {Message}");
        }

        public static void LogError(object Message)
        {
            Debug.LogError($"[Console Error]: {Message}");
        }
    }
}
