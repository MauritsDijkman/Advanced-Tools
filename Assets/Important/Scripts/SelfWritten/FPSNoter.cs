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
        // Create path with the given file title
        path = $"{Application.dataPath}/{fileTitle}.txt";

        // Check if file exists and clear the whole document
        if (File.Exists(path))
            File.WriteAllText(path, "");
        else if (!File.Exists(path))
            File.WriteAllText(path, "");
    }

    private void WriteValues()
    {
        // Put the file title at the top of the document
        File.AppendAllText(path, $"{fileTitle}\n\n");

        // Create integers for later
        int totalFPS = 0;
        int averageFPS = 0;

        // Add the current FPS value to a list
        foreach (int FPS_Value in currentFPS_Time)
            totalFPS += FPS_Value;

        // Calculate the average FPS
        averageFPS = totalFPS / currentFPS_Time.Count;

        // Put the average FPS, total FPS measured and all individual FPS values in the document
        File.AppendAllText(path, $"Average FPS: {averageFPS}\n");
        File.AppendAllText(path, $"Total FPS measured: {currentFPS_Time.Count}\n\n");

        File.AppendAllText(path, $"All FPS values:\n");
        foreach (int FPS_Value in currentFPS_Time)
            File.AppendAllText(path, $"{FPS_Value}\n");

        // Debug line
        Debug.Log("FPS values are written in the document!");
    }

    private IEnumerator Timer(float timeBeforeQuit)
    {
        yield return new WaitForSeconds(timeBeforeQuit);

        WriteValues();      // Write all the values in the document
        Application.Quit(); // Close the application (only works with a build)
    }
}
