using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SphereAI : MonoBehaviour
{
    PathCreator PC;
    float AreaTravelled;
    public bool IsMoving = false;
    public bool Backwards = false;
    public float Speed;
    public float AimSpeed;
    float DampeningSpeed = 1f;
    public int ID;
    public int CID;
    private void Start()
    {
        PC = GameObject.FindGameObjectWithTag("ActivePath").GetComponent<PathCreator>();
        ID = GameManager.SphereNum;
        Speed = GameManager.SphereSpeed;
        gameObject.name = "Sphere - ID: " + ID;
        GameManager.SphereNum += 1;
    }

    private void Update()
    {
        if (IsMoving)
        {
            if (Backwards)
            {
                Speed -= DampeningSpeed * Time.deltaTime;
            }

            else if (Speed < AimSpeed)
            {
                Speed += AimSpeed * Time.deltaTime;
            }
            else if (Speed > AimSpeed)
            {
                Speed -= AimSpeed * Time.deltaTime;
            }

            AreaTravelled += Speed * Time.deltaTime;
        }
        else if (Backwards)
        {
            AreaTravelled -= Speed * Time.deltaTime;
        }

        transform.position = PC.path.GetPointAtDistance(AreaTravelled, EndOfPathInstruction.Stop);
    }

    public int SetID(int id)
    {
        Debug.Assert(id >= 0);
        return ID = id;
    }

    public float Traversed()
    {
        return AreaTravelled;
    }
    public float SetTraversed(float set)
    {
        return AreaTravelled = set;
    }

    public Transform SpherePos()
    {
        return transform;
    }
}
