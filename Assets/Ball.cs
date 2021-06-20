using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{ 
    Moving,
    Aiming
}
public class Ball : MonoBehaviour
{
    public Spectator spectator;
    public Joystick joystick;
    public Transform arrow;
    public BallState state;
    public Rigidbody _rigidbody;
    public TrajectorySimulation trajectorySimulation;
    public ParticleSystem particles;
    public TrailRenderer trail;
    public LevelTimer levelTimer;
    public LevelSwitcher levelSwitcher;
    public Renderer beltRenderer;
    private float joystickRotationSensetivity = 10;
    public float JoystickRotationSensetivity { get => joystickRotationSensetivity; set => joystickRotationSensetivity = value; }
    private bool joystickControlInverted;
    public bool JoystickControlInverted { get => joystickControlInverted; set => joystickControlInverted = value; }

    Vector3 startPosition;
    Quaternion startRotation;
    bool isMoved;
    private float prevPoint = 0;
    private void Start()
    {
        JoystickControlInverted = false;
        isMoved = false;
        startPosition = transform.position;
        startRotation = spectator.transform.rotation;
    }

    private void Update()
    {
        if (state == BallState.Moving)
        {
            if (_rigidbody.velocity.magnitude < 0.4)
            {
                ForceStop();
            }
        }

        if (state == BallState.Aiming)
        {
            Aim();
        }
        else 
        {
            if (spectator.transform.rotation.eulerAngles.x < 35)
                spectator.VerticalTiltSmoothly(35, 9);
            if (Camera.main.fieldOfView < 60)
                spectator.ChangeFOVSmoothly(60, 9);
        }

        if (transform.position.y < -5)
        {
            levelSwitcher.ReloadLevel();
        }

        particles.transform.LookAt(_rigidbody.velocity.normalized);
        float dot = Vector2.Dot(new Vector2(_rigidbody.velocity.normalized.x, _rigidbody.velocity.normalized.z), new Vector2(spectator.transform.forward.x, spectator.transform.forward.z));
        if (_rigidbody.velocity.magnitude > 23 && dot > 0.77f)
            particles.Play();
        else
            particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);

    }

    public void MoveToStart()
    {
        isMoved = false;
        levelTimer.Clear();
        ForceStop();
        transform.position = startPosition;
        spectator.transform.rotation = startRotation;
        joystick.ReloadPosition();
        trail.Clear();
    }

    public void Charge()
    {
        state = BallState.Aiming;
        if (!isMoved)
        {
            isMoved = true;
            levelTimer.StartTimer();
        }
        trajectorySimulation.Enabled = true;
        arrow.gameObject.SetActive(true); 
    }

    public void Aim()
    {
        //spectator.Rotate(new Vector3(0, ((JoystickControlInverted)? -1 : 1) * joystick.GetPositionRelativeToCenter().x / 10f, 0), JoystickRotationSensetivity * Time.deltaTime);
        
        if (prevPoint != 0)
        {
            spectator.Rotate(new Vector3(0, -(prevPoint - Input.mousePosition.x) * ((JoystickControlInverted) ? -1 : 1), 0), JoystickRotationSensetivity * 10 * Time.deltaTime);
        }
        prevPoint = Input.mousePosition.x;

        Vector3 force = GetForceBasedOnJoystickPosition();
        trajectorySimulation.SimulatePath(gameObject, new Vector3(force.x, _rigidbody.velocity.y, force.z));

        Vector3 arrowTargetPosition = transform.position + new Vector3(spectator.transform.forward.x, 0, spectator.transform.forward.z);
        arrow.LookAt(arrowTargetPosition, Vector3.up);
        arrow.position = transform.position;

        Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f, Time.deltaTime * 15 * (1f / Time.timeScale));

        spectator.ChangeFOVSmoothly(50, 9);
        spectator.VerticalTiltSmoothly(25, 9);
    }

    public void Release()
    {
        state = BallState.Moving;
        prevPoint = 0;
        ForceStop();
        arrow.gameObject.SetActive(false);
        _rigidbody.AddForce(GetForceBasedOnJoystickPosition(), ForceMode.VelocityChange);

        trajectorySimulation.Enabled = false;
        Time.timeScale = 1;
    }

    private void ForceStop()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private Vector3 GetForceBasedOnJoystickPosition()
    {
        float force = Mathf.Clamp(Mathf.Abs(joystick.GetPositionRelativeToCenter().y) / 3, 0, 50);
        return new Vector3(spectator.transform.forward.x, 0, spectator.transform.forward.z) * force;
    }

}
