using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class FPSNoter : MonoBehaviour
{
    [Header("File title")]
    [SerializeField] private string fileTitle = "FPS_Log";

    [Header("Timer")]
    [SerializeField] private float timeBeforeQuit = 10f;

    [HideInInspector]
    public List<int> currentFPS_Time = null;

    private string path = "";

    private void Start()
    {
        CheckFile();
        StartCoroutine(Timer(timeBeforeQuit));
    }

    private void CheckFile()
    {
        path = $"{Application.dataPath}/{fileTitle}.txt";

        if (File.Exists(path))
            File.WriteAllText(path, "");
        else if (!File.Exists(path))
            File.WriteAllText(path, "");
    }

    private void WriteValues()
    {
        File.AppendAllText(path, $"{fileTitle.ToString()}\n\n");

        int totalLines = 0;
        int totalFPS = 0;
        int averageFPS = 0;

        foreach (int FPS_Value in currentFPS_Time)
        {
            totalLines++;
            totalFPS += FPS_Value;
        }

        averageFPS = totalFPS / totalLines;
        File.AppendAllText(path, $"Average FPS: {averageFPS.ToString()}\n");
        File.AppendAllText(path, $"Total FPS measured: {totalLines.ToString()}\n\n");
        File.AppendAllText(path, $"All FPS values:\n");

        foreach (int FPS_Value in currentFPS_Time)
        {
            string content = FPS_Value.ToString() + "\n";
            File.AppendAllText(path, content);
        }

        Debug.Log("Values are written in the document!");
    }

    private IEnumerator Timer(float timeBeforeQuit)
    {
        yield return new WaitForSeconds(timeBeforeQuit);

        WriteValues();
        Application.Quit();
    }
}
