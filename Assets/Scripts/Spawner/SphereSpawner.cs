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
    private void Start()
    {
        GameObject NewSphere = Instantiate(Sphere, new Vector3(-90f, -90f), Quaternion.identity);
        GameManager.Spheres.Add(NewSphere);
        GameManager.SAI.Add(NewSphere.GetComponent<SphereAI>());
    }

    private void Update()
    {
        Debug.Log("SAI size: " + GameManager.SAI.Count);
        Debug.Log("Spheres size: " + GameManager.Spheres.Count);
        if (GameManager.SAI[GameManager.SAI.Count - 1].Traversed() > .75f)
        {
            GameObject NewSphere = Instantiate(Sphere, new Vector3(-90f, -90f), Quaternion.identity);
            NewSphere.GetComponent<SphereAI>().SetTraversed(GameManager.SAI[GameManager.SAI.Count - 1].Traversed() - 0.75f);
            int Choice = Random.Range(0, 8);
            switch (Choice)
            {
                default:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case RED:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case GREEN:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case BLUE:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case YELLOW:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case CYAN:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.cyan;
                    break;
                case ORANGE:
                    NewSphere.GetComponent<SpriteRenderer>().color = new Color(1f,0.4f,0f,255f);
                    break;
                case PURPLE:
                    NewSphere.GetComponent<SpriteRenderer>().color = new Color(.5f, 0f, .5f, 255f);
                    break;
                case WHITE:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.white;
                    break;
                case BLACK:
                    NewSphere.GetComponent<SpriteRenderer>().color = Color.black;
                    break;
            }
            GameManager.SAI.Add(NewSphere.GetComponent<SphereAI>());
            GameManager.Spheres.Add(NewSphere);
        }

    }
}
