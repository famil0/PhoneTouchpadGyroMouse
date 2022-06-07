using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using WindowsInput;
using WindowsInput.Native;
using TMPro;

public class ClientController : NetworkBehaviour
{
    Vector3 prevMouse, deltaMouse;
    Quaternion prevGyro, deltaGyro;
    public Sprite sprite;
    public float speed = 0.1f;
    InputSimulator inputSimulator;

    private Gyroscope gyro;
    private Quaternion rot;
    private bool gyroActive;
    public int sensitivity;

    int width = NetworkManagerHUD.buttonWidth;
    int height = NetworkManagerHUD.buttonHeight;

    [Command]
    public void MoveCursorBy(int byx, int byY)
    {
        inputSimulator.Mouse.MoveMouseBy(byx, byY);
    }

    private void Start()
    {
        Application.runInBackground = true;
        inputSimulator = new InputSimulator();
        transform.localScale = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, 1));
    }

    [ClientCallback]
    private void OnGUI()
    {        
        int startX = Screen.width / 2 - width - (int)(0.5f * width);
        int y = Screen.height - 5 * height;
        if (GUI.Button(new Rect(startX, y, width, height), "Left"))
        {
            Click("left");
        }
        if (GUI.Button(new Rect(startX + width, y, width, height), "Middle"))
        {
            Click("middle");

        }
        if (GUI.Button(new Rect(startX + 2 * width, y, width, height), "Left"))
        {
            Click("right");

        }
    }

    [Command]
    public void Click(string s)
    {
        if (s == "left")
            inputSimulator.Mouse.LeftButtonClick();
        else if (s == "middle")
            inputSimulator.Mouse.MiddleButtonClick();
        else if (s == "right")
            inputSimulator.Mouse.RightButtonClick();
    }

    [ClientCallback]
    private void Update()
    {
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    [ClientCallback]
    private void OnMouseDrag()
    {
        if (Input.GetMouseButtonDown(0)) prevMouse = Input.mousePosition;
        deltaMouse = Input.mousePosition - prevMouse;
        if (isLocalPlayer)
        {
            MoveCursorBy((int)deltaMouse.x, -(int)deltaMouse.y);
        }
        prevMouse = Input.mousePosition;
    }

}
