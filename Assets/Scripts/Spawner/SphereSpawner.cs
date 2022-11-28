using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    //Sphere colours
    const int RED = 0,
        GREEN = 1,
        BLUE = 2,
        YELLOW = 3,
        CYAN = 4,
        ORANGE = 5,
        PURPLE = 6,
        WHITE = 7,
        BLACK = 8;
    public GameObject Sphere;
    public float SpawnSpeed;
    float t = 0;//Timer variable
    private void Update()
    {
        t += Time.deltaTime * SpawnSpeed;
        if (t > 1f)
        {
            Instantiate(Sphere, new Vector3(-90f, -90f), Quaternion.identity);
            t = 0;
        }
    }
}
