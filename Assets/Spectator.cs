using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    public Transform ball;
    public float lerpDrag = 8;

    void Update()
    {
        transform.position = ball.position;
    }

    public void Rotate(Vector3 rotation, float speed)
    {
        transform.Rotate(rotation * speed, Space.World);
    }

    public void VerticalTiltSmoothly(int toAngle, int deltaTimeMultiplier)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(toAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)), Time.deltaTime * deltaTimeMultiplier * (1f / Time.timeScale));
    }

    public void ChangeFOVSmoothly(int toAngle, int deltaTimeMultiplier)
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, toAngle, Time.deltaTime * deltaTimeMultiplier * (1f / Time.timeScale));
    }

    public void SetFOV(int angle)
    {
        Camera.main.fieldOfView = angle;
    }
}
