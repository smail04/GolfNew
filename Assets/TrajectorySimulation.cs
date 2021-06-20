using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulation : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public int maxIterations = 10000;
    public int maxSegmentCount = 300;
    public float segmentStepModulo = 10f;

    private Vector3[] segments;
    private int numSegments = 0;

    public bool Enabled
    {
        get
        {
            return lineRenderer.enabled;
        }
        set
        {
            lineRenderer.enabled = value;
        }
    }

    public void Start()
    {
        Enabled = false;
    }

    public void SimulatePath(GameObject gameObject, Vector3 forceDirection)
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        float timestep = Time.fixedDeltaTime;

        float stepDrag = 1 - rigidbody.drag * timestep;
        Vector3 velocity = forceDirection / rigidbody.mass * timestep;
        Vector3 gravity = Physics.gravity * timestep * timestep;
        Vector3 position = gameObject.transform.position + rigidbody.centerOfMass;

        if (segments == null || segments.Length != maxSegmentCount)
        {
            segments = new Vector3[maxSegmentCount];
        }

        segments[0] = position;
        numSegments = 1;

        for (int i = 0; i < maxIterations && numSegments < maxSegmentCount && position.y > 0f; i++)
        {
            velocity += gravity;
            velocity *= stepDrag;

            position += velocity;

            if (i % segmentStepModulo == 0)
            {
                segments[numSegments] = position;
                numSegments++;
            }
        }

        lineRenderer.positionCount = numSegments;
        lineRenderer.SetPositions(segments);
    }

}
