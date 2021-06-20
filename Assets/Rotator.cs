using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator: MonoBehaviour
{
    public Vector3 rotationEuler;
    public GameObject[] blades;

    void Update()
    {
        transform.parent.Rotate(rotationEuler * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            foreach (var blade in blades)
            {
                blade.transform.parent = null;
                Rigidbody rb = blade.AddComponent<Rigidbody>();
                rb.AddRelativeForce(0, 10, 0, ForceMode.Impulse);
                blade.GetComponent<SelfDestroyer>().enabled = true;
            }
            Destroy(gameObject);
        }
    }

}
