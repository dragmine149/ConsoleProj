using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEditor;
using System.IO;

public class ButtonClick : MonoBehaviour
{
    public TextMeshProUGUI LogText => FindObjectOfType<GetChildren>().logContentText;
    private bool usage = false;

    // This shows the stacktrace of the button.
    public void getButtonInfo(GameObject button)
    {
        if (!FindObjectOfType<GetChildren>().helpObject.activeInHierarchy)
        {
            bool Collaspe = CreateLogs.CollaspeMode;
            List<LogInfo> Logs = CreateLogs.Logs;
            List<LogInfo> Shrink = CreateLogs.Shrink;
            LogInfo log;

            if (Collaspe)
            {
                log = Shrink[int.Parse(button.name) - 1];
            }
            else
            {
                log = Logs[int.Parse(button.name) - 1];
            }
            LogText.text = $"{log.type}:{log.condition}\n{log.stackTrace}";
        } else
        {
            Type t = Type.GetType(button.name.Split(":")[0]);
            usage = !usage;

            if (usage)
            {
                try
                {
                    FieldInfo fi = t.GetField("Usage");
                    string Result = (string)fi.GetValue(fi);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = Result.TrimStart("".ToCharArray()) == "" ? $"{t.Name}: No Usage found!" : $"{t.Name}: {Result}";
                }
                catch (NullReferenceException)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{t.Name}: No Usage Found!";
                }
            } else
            {
                try
                {
                    FieldInfo fi = t.GetField("Description");
                    string Result = (string)fi.GetValue(fi);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = Result.TrimStart("".ToCharArray()) == "" ? $"{t.Name}: No Description found!" : $"{t.Name}: {Result}";
                }
                catch (NullReferenceException)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{t.Name}: No Description found!";
                }
            }
        }
    }
}
