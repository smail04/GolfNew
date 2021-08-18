using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    
    public float refinement = 0.1f;
    public float multiplier = 25;
    public float timeMultiplier = 0.2f;
    public int cubesize = 7;
    public Material material;

    private float perlinNoise = 0;
    private Renderer rend;

    class Cube {
        public Transform transform;
        public int i, j;

        public Cube(Transform transform, int i, int j)
        {
            this.transform = transform;
            this.i = i;
            this.j = j;
        }
    }

    List<Cube> cubes = new List<Cube>();

    void Start()
    {
        rend = GetComponent<Renderer>();
        for (int i = Mathf.CeilToInt(rend.bounds.min.x - transform.position.x); i < Mathf.CeilToInt(rend.bounds.max.x - transform.position.x); i+=cubesize)
            for (int j = Mathf.CeilToInt(rend.bounds.min.z - transform.position.z); j < Mathf.CeilToInt(rend.bounds.max.z - transform.position.z); j+=cubesize)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = transform;
                go.transform.localScale = new Vector3(cubesize / transform.localScale.x, cubesize / transform.localScale.y, cubesize / transform.localScale.z);
                go.transform.position = transform.position + new Vector3(i, transform.position.y, j);
                go.GetComponent<Collider>().enabled = false;
                go.GetComponent<Renderer>().material = material;
                cubes.Add(new Cube(go.transform, i, j));
            }
        rend.enabled = false;
    }

    void Update()
    {
        foreach (Cube cube in cubes)
        {
            float elapsed = Time.time * timeMultiplier;
            perlinNoise = Mathf.PerlinNoise(elapsed + cube.i * refinement, elapsed + cube.j * refinement);
            cube.transform.position = new Vector3(transform.position.x + cube.i, 
                                                  perlinNoise * multiplier + transform.position.y, 
                                                  transform.position.z + cube.j);
        }
    }

}
