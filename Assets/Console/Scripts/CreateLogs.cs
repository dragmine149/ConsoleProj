using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#region LogInfo
// This class keeps information about the log stuff. (what unity gives and more)
public class LogInfo
{
    public string condition;
    public string stackTrace;
    public LogType type;
    public int Count;
    public GameObject logGMobj;
    public DateTime logTime;

    public override bool Equals(object obj)
    {
        LogInfo b = (LogInfo)obj;
        return condition == b.condition && stackTrace == b.stackTrace && type == b.type;
    }
    public override int GetHashCode() => condition.GetHashCode() ^ stackTrace.GetHashCode() ^ type.GetHashCode();
    public static bool operator ==(LogInfo a, LogInfo b) => a.condition == b.condition && a.stackTrace == b.stackTrace && a.type == b.type;
    public static bool operator !=(LogInfo a, LogInfo b) => a.condition != b.condition || a.stackTrace != b.stackTrace || a.type != b.type;
    public LogInfo ShallowCopy() => (LogInfo)MemberwiseClone();
}
#endregion

public class CreateLogs : MonoBehaviour
{
    // Holds lists
    public static List<LogInfo> Logs = new List<LogInfo>();
    public static List<LogInfo> Shrink = new List<LogInfo>();
    public static bool CollaspeMode = false;
    private GameObject FindChild => FindObjectOfType<GetChildren>().logsContent;

    /// <summary>
    /// Changes from collaspe mode to no collaspe mode and vis-versa.
    /// </summary>
    /// <param name="Input"></param>
    public void ChangeCollaspe(bool Input)
    {
        if (CollaspeMode != Input) // if change
        {
            CollaspeMode = Input;
            Shrink.Clear(); // should we do this in GetShortLogs?
        }
        DeleteOldLogs(); // can we make it so we don't always have to do this?
        List<LogInfo> temp = Logs;
        if (CollaspeMode)
        {
            temp = GetShortLogs();
        }
        for (int i = 0; i < temp.Count; i++)
        {
            CreateUi(i + 1, temp);
        }
    }

    /// <summary>
    /// This gets called when the 
    /// </summary>
    public void UpdateLogUi()
    {
        ChangeCollaspe(CollaspeMode);
    }

    /// <summary>
    /// Adds a log to the list. Useful for when saving/
    /// </summary>
    /// <param name="condition">Log info</param>
    /// <param name="stackTrace">Where log happened</param>
    /// <param name="type">Type of log</param>
    public void AddLog(string condition, string stackTrace, LogType type)
    {
        LogInfo log = new LogInfo // create a new log
        {
            condition = condition,
            type = type,
            stackTrace = stackTrace,
            Count = 1,
            logTime = DateTime.Now
        };

        Logs.Add(log); // add to list of logs.

        bool newLog = false; // This is for shrink mode, Lets you add a new log in shrink mode.

        // Collaspe mode check becuase there is no point if not in collaspe mode.
        if (CollaspeMode)
        {
            if (Shrink.Contains(log))
            {
                LogInfo result = Shrink[Shrink.FindIndex(r => r == log)];
                result.Count++;
            } else
            {
                Shrink.Add(log);
                newLog = true;
            }
        }

        try
        {
            if (FindChild.activeInHierarchy) // if the child is active.
            {
                if (!CollaspeMode || newLog) // not collasped, or new log will activate this.
                {
                    CreateUi(Logs.Count, Logs);
                }
                else
                {
                    UpdateUi(Shrink.FindIndex(r => r == log));
                }
            }
        }
        catch (NullReferenceException){}
    }


    /// <summary>
    /// Creates the logs ui. (The things we see in the console)
    /// </summary>
    /// <param name="Id">The id in the list</param>
    /// <param name="logsToConvert">The list to convert</param>
    public void CreateUi(int Id, List<LogInfo> logsToConvert)
    {
        if (Id > 0)
        {
            LogInfo log = logsToConvert[Id - 1];

            // get button (and make)
            UnityEngine.Object prefab = Resources.Load("Log"); // get
            GameObject button = (GameObject)Instantiate(prefab); // spawn
            button.transform.SetParent(FindChild.transform); // set child
            log.logGMobj = button; // set to logInfo

            // Get the type of log
            string Type = log.type.ToString();
            string Condition = log.condition;
            if (log.condition.StartsWith("[Console"))
            {
                // update log type and condition if use custom built in log msg service.
                Type = log.condition.Split("]".ToCharArray())[0].Replace("[","");
                Condition = log.condition.Split("]".ToCharArray())[1];
            }

            // Set the colour of the button depending on what happened
            switch (Type)
            {
                case "Log":
                case "Console":
                    button.GetComponentInChildren<Image>().color = new Color(128, 128, 128, (float)0.5);
                    break;

                case "Warning":
                case "Console Warning":
                    button.GetComponentInChildren<Image>().color = new Color(255, 165, 0, (float)0.5);
                    break;

                case "Error":
                case "Console Error":
                case "Exception":
                    button.GetComponentInChildren<Image>().color = new Color(255, 0, 0, (float)0.5);
                    break;
            }

            // finish doing things with the button
            string text = $"[{log.logTime.TimeOfDay}],[{Type}]{Condition}";
            if (CollaspeMode)
            {
                text += $" ({log.Count})";
            }
            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
            button.name = Id.ToString();
        }
    }

    /// <summary>
    /// This gets called if the ui is updated whilst in collaspeMode
    /// </summary>
    /// <param name="Id"></param>
    public void UpdateUi(int Id)
    {
        LogInfo log = Shrink[Id]; // we are not going to be updating if not in shrink
        log.logGMobj.GetComponentInChildren<TextMeshProUGUI>().text = $"[{log.logTime.TimeOfDay}],[{log.type}]:{log.condition} ({log.Count})";
    }

    /// <summary>
    /// Deletes old logs. TODO: would be nice to not do this, but it seems like the easiest way.
    /// </summary>
    public void DeleteOldLogs()
    {
        var children = new List<GameObject>();
        foreach (Transform child in FindChild.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    /// <summary>
    /// Make a short log list from the long log list
    /// </summary>
    public List<LogInfo> GetShortLogs()
    {
        foreach (LogInfo log in Logs)
        {
            if (Shrink.Contains(log))
            {
                // If already in list, add 1 to the counter.
                LogInfo result = Shrink[Shrink.FindIndex(r => r == log)];
                result.Count++;
            } else
            {
                Shrink.Add(log.ShallowCopy()); // copy the log and then add it
            }
        }
        return Shrink;
    }
}