using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using WindowsInput;
using WindowsInput.Native;

public class ClientController : NetworkBehaviour
{
    public GameController gameController;
    Vector3 prevMouse, deltaMouse;
    Quaternion prevGyro, deltaGyro;
    public float speed = 0.1f;
    InputSimulator inputSimulator;

    private Gyroscope gyro;
    private Quaternion rot;
    private bool gyroActive;
    public int sensitivity;

    [Command]
    public void MoveCursorBy(int byx, int byY)
    {
        inputSimulator.Mouse.MoveMouseBy(byx, byY);
    }

    private void Start()
    {
        Application.runInBackground = true;
        inputSimulator = new InputSimulator();

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActive = true;
        }
    }

    [ClientCallback]
    private void Update()
    {
        //deltaGyro = GyroToUnity(gyro.attitude) * Quaternion.Inverse(prevGyro);
        if (Input.GetMouseButtonDown(0)) prevMouse = Input.mousePosition;
        deltaMouse = Input.mousePosition - prevMouse;
        if (isLocalPlayer)
        {
            MoveCursorBy((int)deltaMouse.x, -(int)deltaMouse.y);
        }
        prevMouse = Input.mousePosition;

        //if (gyroActive)
        //{
        //    rot = GyroToUnity(gyro.attitude);
        //    MoveCursorBy(-(int)(deltaGyro.z * sensitivity), -(int)(deltaGyro.y * sensitivity));
        //    transform.rotation = rot;
        //}
        //prevGyro = GyroToUnity(gyro.attitude);
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

}
