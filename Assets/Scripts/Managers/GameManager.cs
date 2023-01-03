using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PathManager pathManager;

    public static List<GameObject> Spheres;
    public static List<SphereAI> SAI;

    public static float SphereSpeed;
    public static float AimSpeed;

    public float SizeThresh;
    public float MinMagDist;

    private float ChanegAmp = 3f;

    public static int SphereNum = 0;

    private void Awake()
    {
        SphereSpeed = pathManager.StartSpeed;
        AimSpeed = pathManager.StartSpeed;
        Spheres = new List<GameObject>();
        SAI = new List<SphereAI>();
    }

    public void DestorySphere()
    {
        #region TO-BE-DELETED

        int HitID;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f) && hit.transform.gameObject != null)
            {
                HitID = hit.transform.gameObject.GetComponent<SphereAI>().ID;

                if (HitID != -1)
                {
                    GameObject.Destroy(hit.transform.gameObject);
                    SAI.Remove(SAI[HitID]);
                    Spheres.Remove(Spheres[HitID]);
                    Debug.Log(SAI.Count);
                }

                for (int i = HitID; i < Spheres.Count; i++)
                {
                    if (SAI[i].ID > 0)
                    {
                        SAI[i].SetID(SAI[i].ID - 1);
                    }
                }

                SphereNum -= 1;
            }
        }

        #endregion
    }

    private void Update()
    {
        DestorySphere();

        //Zone speeds
        if (AimSpeed > SphereSpeed)
        {
            SphereSpeed += Time.deltaTime * ChanegAmp;
            SphereSpeed = Mathf.Min(SphereSpeed, AimSpeed);
        }
        else if (AimSpeed < SphereSpeed)
        {
            SphereSpeed -= Time.deltaTime * ChanegAmp;
            SphereSpeed = Mathf.Max(SphereSpeed, AimSpeed);
        }
        else
        {
            SphereSpeed = AimSpeed;
        }

        #region Sphere collision data

        //Movement
        if (Spheres.Count > 0)
        {
            for (int i = Spheres.Count - 1; i >= 0; i--)
            {
                //Pusher
                if (i == Spheres.Count - 1)
                {
                    SAI[i].IsMoving = true;
                }
                //Carriage
                else
                {
                    //Debug.Log("Index: " + i + "," + (i + 1) + " Difference: " + (SAI[i + 1].Traversed() - SAI[i].Traversed()));
                    //Collision
                    if (SAI[i].Traversed() - SAI[i + 1].Traversed() <= SizeThresh)
                    {
                        SAI[i].SetTraversed(SAI[i + 1].Traversed() + SizeThresh);
                        SAI[i].Backwards = false;
                    }
                    SAI[i].IsMoving = false;

                    #region Magnetism

                    //Puller
                    if (SAI[i].CID == SAI[i + 1].CID)
                    {
                        if (SAI[i].Traversed() - SAI[i + 1].Traversed() <= SizeThresh + MinMagDist)
                        {
                            SAI[i].IsMoving = true;
                            SAI[i].Backwards = true;
                        }
                    }
                    //Back-Carriage
                    else if (SAI[i + 1].Backwards)
                    {
                        if (SAI[i].Traversed() - SAI[i + 1].Traversed() <= SizeThresh + MinMagDist)
                        {
                            SAI[i].IsMoving = false;
                            SAI[i].Speed = SAI[i+1].Speed;
                        }
                    }
                    
                    else
                    {
                        SAI[i].IsMoving = false;
                        SAI[i].Backwards = false;
                    }

                    #endregion
                }
            }
        }
        else
        {
            SAI[0].IsMoving = true;
        }
        #endregion
    }

    public void SetSpeed(float speed)
    {
        SphereSpeed = speed;
    }
}