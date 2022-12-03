using UnityEngine;

public class QuitTracker : MonoBehaviour
{

    bool isPlatformAndroid;
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            isPlatformAndroid = true;
        else
            isPlatformAndroid = false;
    }
    void Update()
    {
        if (isPlatformAndroid)
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}
