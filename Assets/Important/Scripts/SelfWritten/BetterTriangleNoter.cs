using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class BetterTriangleNoter : MonoBehaviour
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

    private void Start()
    {
        fileNumer = 1;
        measureValues = false;

        originalFileTitle = fileTitle;

        StartCoroutine(Timer(timeBeforeQuit, howManyTimes));
    }

    private void CheckFile()
    {
        path = $"{Application.dataPath}/{fileTitle}.csv";

        if (File.Exists(path))
            File.WriteAllText(path, "");
        else if (!File.Exists(path))
            File.WriteAllText(path, "");
    }

    private void Update()
    {
        if (measureValues)
        {
            //triangleCount = UnityEditor.UnityStats.triangles;
            //triangleList.Add(triangleCount);
            triangleList.Add(UnityEditor.UnityStats.triangles);
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
        if (fileNumer >= 1)
            fileTitle = $"{originalFileTitle}{fileNumer++}";

        CheckFile();

        File.AppendAllText(path, $"{fileTitle}\n\n");

        File.AppendAllText(path, $"Total triangle values measured: {triangleList.Count}\n");
        File.AppendAllText(path, $"\n\n");

        foreach (int triangleValue in triangleList)
            File.AppendAllText(path, $"{triangleValue}\n");

        File.AppendAllText(path, $"=GEMIDDELDE(A6:A{triangleList.Count + 5})\n");

        triangleList.Clear();


        Debug.Log("Triangle values are written in the document!");
    }
}
