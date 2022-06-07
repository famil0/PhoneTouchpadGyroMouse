using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScreenController : MonoBehaviour
{
    public Toggle isFullScreen;
    public TMP_InputField width;
    public TMP_InputField height;

    private void Start()
    {
        isFullScreen.isOn = Screen.fullScreen;
        width.text = Screen.width.ToString();
        height.text = Screen.height.ToString();
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = isFullScreen.isOn;
    }

    public void SetScreenResolution()
    {
        Screen.SetResolution(int.Parse(width.text), int.Parse(height.text), isFullScreen.isOn);
    }
}
