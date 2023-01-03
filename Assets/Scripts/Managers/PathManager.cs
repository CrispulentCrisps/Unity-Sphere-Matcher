using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathManager : MonoBehaviour
{
    [Header("These are percentages of the path")]
    public PathCreator PC;
    public float DangerZone;
    public float StartZone;
    public float StartSpeed;
    public float DangerSpeed;
    public float NormalSpeed;

    private void Update()
    {
        float BallPerc = GameManager.SAI[0].Traversed() / PC.path.length;
        if (BallPerc > DangerZone)
        {
            GameManager.AimSpeed = DangerSpeed;
        }
        else if (BallPerc > StartZone && BallPerc < DangerZone)
        {
            GameManager.AimSpeed = NormalSpeed;
        }
        else
        {
            GameManager.AimSpeed = StartSpeed;
        }
    }
}