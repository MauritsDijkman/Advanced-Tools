using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TriangleNoter : MonoBehaviour
{
    private List<int> triangleList = new List<int>();
    private int triangleCount = 0;
    private bool measureValues = true;

    private string path = "";

    [Header("File title")]
    [SerializeField] private string fileTitle = "Triangle_Log";

    [Header("Timer")]
    [SerializeField] private float timeBeforeQuit = 60f;

    private void Start()
    {
        measureValues = true;
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

    private void Update()
    {
        if (measureValues)
        {
            triangleCount = UnityEditor.UnityStats.triangles;
            triangleList.Add(triangleCount);
        }
    }

    private void WriteValues()
    {
        File.AppendAllText(path, $"{fileTitle}\n\n");

        //int totalLines = 0;
        //int totalTriangleAmount = 0;
        //int averageTriangleAmount = 0;

        //foreach (int triangleAmount in triangleList)
        //{
        //    totalLines++;
        //    totalTriangleAmount += triangleAmount;
        //}

        //averageTriangleAmount = totalTriangleAmount / totalLines;

        triangleList.Sort();

        //File.AppendAllText(path, $"Average triangles measured: {averageTriangleAmount}\n");
        File.AppendAllText(path, $"Highest triangle count: {triangleList[triangleList.Count - 1]}\n");
        File.AppendAllText(path, $"Lowest triangle count: {triangleList[0]}\n\n");
        //File.AppendAllText(path, $"Total triangle amount: {totalTriangleAmount}\n\n");

        File.AppendAllText(path, $"Total triangle values measured: {triangleList.Count}\n");
        File.AppendAllText(path, $"All triangle values (lowest to highest):\n");

        foreach (int triangle in triangleList)
            File.AppendAllText(path, $"{triangle}\n");

        Debug.Log("Triangle values are written in the document!");
    }

    private IEnumerator Timer(float timeBeforeQuit)
    {
        yield return new WaitForSeconds(timeBeforeQuit);

        measureValues = false;
        WriteValues();
    }
}
