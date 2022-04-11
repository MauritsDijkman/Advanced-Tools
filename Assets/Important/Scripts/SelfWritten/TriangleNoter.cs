using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class TriangleNoter : MonoBehaviour
{
    private string path = "";
    private List<int> triangleList = new List<int>();
    private bool measureValues = true;

    [Header("File title")]
    [SerializeField] private string fileTitle = "Triangle_Log";
    private string originalFileTitle = "";
    private int fileNumer = 1;

    [Header("Timer")]
    [SerializeField] private float timeBeforeQuit = 69f;
    [SerializeField] private int howManyTimes = 5;

    [Header("Information")]
    [SerializeField] private bool notateMaxAndMixValue = false;

    private void Start()
    {
        // Set the standard values
        fileNumer = 1;
        measureValues = false;
        originalFileTitle = fileTitle;

        StartCoroutine(Timer(timeBeforeQuit, howManyTimes));
    }

    private void CheckFile()
    {
        // Create path with the given file title
        path = $"{Application.dataPath}/{fileTitle}.csv";

        // Check if file exists and clear the whole document
        if (File.Exists(path))
            File.WriteAllText(path, "");
        else if (!File.Exists(path))
            File.WriteAllText(path, "");
    }

    private void Update()
    {
        // If the values need to be measured, add the amount of triangles to the list
        if (measureValues)
            triangleList.Add(UnityEditor.UnityStats.triangles);
    }

    private IEnumerator Timer(float pTmeBeforeQuit, int pHowManyTimes)
    {
        // Run the code as many times as given
        for (int i = 0; i < pHowManyTimes; i++)
        {
            // Enable the collection of triangles
            measureValues = true;

            // Wait for the given amount of seconds
            yield return new WaitForSeconds(pTmeBeforeQuit);

            // Disable the collection of triangles and write the current values in a list
            measureValues = false;
            WriteValues();
        }

        // Debug line
        Debug.Log("All tests are done!");
    }

    private void WriteValues()
    {
        // Add a number to the end of the file
        if (fileNumer >= 1)
            fileTitle = $"{originalFileTitle}{fileNumer++}";

        // Check if file exists
        CheckFile();

        // Put the file title at the top of the document
        File.AppendAllText(path, $"{fileTitle}\n\n");

        // Add the total amount of triangles in the document
        File.AppendAllText(path, $"Total triangle values measured: {triangleList.Count}\n");
        File.AppendAllText(path, $"\n");
        File.AppendAllText(path, $"All values:\n");

        // Add all the values to the document
        foreach (int triangleValue in triangleList)
            File.AppendAllText(path, $"{triangleValue}\n");

        // Add the calculation to get the average amount to the document
        File.AppendAllText(path, $"=GEMIDDELDE(A6:A{triangleList.Count + 5})\n\n");

        // If the highest and lowest value needs to be printed, sort the list and print
        if (notateMaxAndMixValue)
        {
            triangleList.Sort();

            File.AppendAllText(path, $"Highest triangle count: {triangleList[triangleList.Count - 1]}\n");
            File.AppendAllText(path, $"Lowest triangle count: {triangleList[0]}\n");
        }

        // Clear the list for the new test
        triangleList.Clear();

        // Debug line
        Debug.Log("Triangle values are written in the document!");
    }
}
