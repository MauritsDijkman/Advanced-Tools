using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class BetterTriangleNoter : MonoBehaviour
{
    private string path = "";

    private int triangleCount = 0;
    private List<int> triangleList = new List<int>();

    private bool measureValues = true;
    private bool nameSet = false;

    [Header("File title")]
    [SerializeField] private string fileTitle = "Triangle_Log";

    [Header("Timer")]
    [SerializeField] private float timeBeforeQuit = 69f;
    [SerializeField] private int howManyTimes = 5;

    private void Start()
    {
        measureValues = false;
        nameSet = false;

        CheckFile();
        StartCoroutine(Timer(timeBeforeQuit, howManyTimes));
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

    private IEnumerator Timer(float pTmeBeforeQuit, int pHowManyTimes)
    {
        for (int i = 0; i < pHowManyTimes; i++)
        {
            measureValues = true;

            yield return new WaitForSeconds(pTmeBeforeQuit);

            measureValues = false;
            WriteValues();
        }

        Debug.Log("All tests are done!");
    }

    private void WriteValues()
    {
        if (!nameSet)
        {
            File.AppendAllText(path, $"{fileTitle}\n\n");
            nameSet = true;
        }

        triangleList.Sort();

        File.AppendAllText(path, $"Highest triangle count: {triangleList[triangleList.Count - 1]}\n");
        File.AppendAllText(path, $"Lowest triangle count: {triangleList[0]}\n");
        File.AppendAllText(path, $"Total triangle values measured: {triangleList.Count}\n");
        File.AppendAllText(path, $"\n\n\n");

        triangleList.Clear();

        Debug.Log("Triangle values are written in the document!");
    }
}
