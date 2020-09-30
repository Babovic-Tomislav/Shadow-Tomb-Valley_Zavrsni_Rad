using UnityEngine;

public class fps : MonoBehaviour
{
    public int targetFrameRate = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {

    }
}