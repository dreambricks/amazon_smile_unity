using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager
{
    private Dictionary<string, Dictionary<string, string>> configData;

    public ConfigManager(string fileName = "config.ini")
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        configData = new Dictionary<string, Dictionary<string, string>>();
        LoadConfig(filePath);
    }

    private void LoadConfig(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Arquivo config.ini não encontrado!");
            return;
        }

        string currentSection = "";
        foreach (var line in File.ReadAllLines(filePath))
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#"))
                continue;

            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                if (!configData.ContainsKey(currentSection))
                    configData[currentSection] = new Dictionary<string, string>();
            }
            else if (trimmedLine.Contains("="))
            {
                var split = trimmedLine.Split(new char[] { '=' }, 2);
                string key = split[0].Trim();
                string value = split[1].Trim();
                if (!string.IsNullOrEmpty(currentSection))
                    configData[currentSection][key] = value;
            }
        }
    }

    public string GetValue(string section, string key, string defaultValue = "")
    {
        if (configData.ContainsKey(section) && configData[section].ContainsKey(key))
            return configData[section][key];
        return defaultValue;
    }

    public void SetValue(string section, string key, string value)
    {
        if (!configData.ContainsKey(section))
            configData[section] = new Dictionary<string, string>();
        configData[section][key] = value;
    }

    public void SaveConfig(string fileName = "config.ini")
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        List<string> lines = new List<string>();

        foreach (var section in configData)
        {
            lines.Add($"[{section.Key}]");
            foreach (var kvp in section.Value)
                lines.Add($"{kvp.Key}={kvp.Value}");
            lines.Add("");
        }

        File.WriteAllLines(filePath, lines);
    }
}
