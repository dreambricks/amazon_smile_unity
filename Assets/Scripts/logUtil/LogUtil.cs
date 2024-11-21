using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class LogUtil : MonoBehaviour
{

    private static string logFilePath;


    public static void SaveLog(DataLog dataLog)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "data_logs");
        Debug.Log("Log salvo em: " + folderPath);


        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        logFilePath = Path.Combine(folderPath, "data_logs.csv");

        if (!File.Exists(logFilePath))
        {
            using (StreamWriter writer = new StreamWriter(logFilePath))
            {
                writer.WriteLine("timePlayed,status,project,additional");
            }
        }

        string formattedDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        dataLog.timePlayed = formattedDateTime;

        string csvLine = string.Format("{0},{1},{2},{3}", dataLog.timePlayed, dataLog.status, dataLog.project, dataLog.additional);

        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine(csvLine);
        }
    }

    public static DataLog GetDatalogFromJson()
    {
        string jsonFileName = "datalog.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        string json = File.ReadAllText(filePath);
        DataLog dataLog = JsonConvert.DeserializeObject<DataLog>(json);

        return dataLog;
    }

}
