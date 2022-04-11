using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField] private string tagName = "Environment";

    private int objectCount = 0;

    private void Start()
    {
        objectCount = GameObject.FindGameObjectsWithTag(tagName).Length;            // Search all GameObjects that have the given tag and store the amount in the integer
        Debug.Log($"Amount of objects that need to be checked: {objectCount}");     // Print the amount of GameObject      
    }
}
