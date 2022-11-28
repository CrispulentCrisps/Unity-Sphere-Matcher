using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class SphereAI : MonoBehaviour
{
    PathFollower pf;
    private void Start()
    {
        pf = GetComponent<PathFollower>();
        pf.pathCreator = GameObject.FindGameObjectWithTag("ActivePath").GetComponent<PathCreator>();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
