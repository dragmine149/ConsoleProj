using UnityEngine;

public class Console : MonoBehaviour
{
    // This class basiaclly gives the other scripts needed info. Like the log.

    public string[] Info = new string[2];
    public LogType Type;
    public CreateLogs logs;

    // Automatically assaigns logs the file.
    private void OnValidate()
    {
        logs = FindObjectOfType<CreateLogs>();
    }
    private void OnMessageRecieved(string condition, string stackTrace, LogType type)
    {
        /*
         * Condition is the message that got loged
         * StackTrace is where it got loged from
         * Type is the type of log
         */

        Info[0] = condition;
        Info[1] = stackTrace;
        Type = type;
        if (!condition.Contains("[DEBUG]"))
        {
            logs.AddLog(condition, stackTrace, type);
        }
    }

    // subscribe to log messages recieve from unity.
    public void Start()
    {
        Application.logMessageReceived += OnMessageRecieved;
    }
}