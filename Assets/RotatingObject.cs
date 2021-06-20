using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public Vector3 rotationEuler;
    public Rigidbody[] blades;

    void Update()
    {
        transform.parent.Rotate(rotationEuler * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            Destroy(this);
            foreach (var blade in blades)
            {
                blade.transform.parent = null;
                blade.isKinematic = false;
                blade.AddRelativeForce(0, 10, 0, ForceMode.Impulse);
                blade.GetComponent<SelfDestroyer>().enabled = true;
            }
        }
    }

}
