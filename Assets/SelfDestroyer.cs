using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float timeToDestroy = 1;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
