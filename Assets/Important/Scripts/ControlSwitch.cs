using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
    [Header("Movement")]
    public KeyCode toggleKey = KeyCode.C;
    public GameObject manualController;
    public GameObject automaticController;
    public GameObject staticController;

    [Header("FSP")]
    public int targetFPS = 60;

    [Header("Vsync")]
    public bool vsyncOn = false;

    private bool useAutomaticControl = false;
    private const string automaticControlDefaultCMDLineArgument = "-automaticControl";

    void Awake()
    {
        Application.targetFrameRate = targetFPS;

        if (vsyncOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        useAutomaticControl = HasCommandLineArgument(automaticControlDefaultCMDLineArgument);
        SetControllerState(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            Toggle();
    }

    void Toggle()
    {
        useAutomaticControl = !useAutomaticControl;
        SetControllerState(useAutomaticControl);
    }

    void SetControllerState(bool useAutomaticControl)
    {
        /**
        if (manualController != null)
        manualController.SetActive(!useAutomaticControl);
        /**/

        if (automaticController != null)
            automaticController.SetActive(useAutomaticControl);

        if (staticController != null)
            staticController.SetActive(!useAutomaticControl);
    }

    bool HasCommandLineArgument(string argument)
    {
        string[] passedArguments = System.Environment.GetCommandLineArgs();
        foreach (string passedArgument in passedArguments)
        {
            if (passedArgument.Equals(argument))
                return true;
        }
        return false;
    }
}
