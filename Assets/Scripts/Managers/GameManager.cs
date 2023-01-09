using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PathManager pathManager;

    public static List<GameObject> Spheres;
    public static List<SphereAI> SAI;

    public static float ZoneSpeed;
    public static float AimSpeed;

    public float SizeThresh;
    public float MinMagDist;

    private float ChanegAmp = 3f;

    public static int SphereNum = 0;

    private bool Strikable = false;

    private void Awake()
    {
        ZoneSpeed = pathManager.StartSpeed;
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
                /*
                for (int i = HitID; i < Spheres.Count; i++)
                {
                    if (SAI[i].ID > 0)
                    {
                        SAI[i].SetID(SAI[i].ID - 1);
                    }
                }
                */
                SphereNum -= 1;
            }
        }

        #endregion
    }

    private void Update()
    {
        DestorySphere();

        //Zone speeds
        if (AimSpeed > ZoneSpeed)
        {
            ZoneSpeed += Time.deltaTime * ChanegAmp;
            ZoneSpeed = Mathf.Min(ZoneSpeed, AimSpeed);
        }
        else if (AimSpeed < ZoneSpeed)
        {
            ZoneSpeed -= Time.deltaTime * ChanegAmp;
            ZoneSpeed = Mathf.Max(ZoneSpeed, AimSpeed);
        }
        else
        {
            ZoneSpeed = AimSpeed;
        }

        #region Sphere collision data

        //Movement
        if (Spheres.Count > 0)
        {
            for (int i = Spheres.Count - 1; i >= 0; i--)
            {
                SAI[i].SetID(i);

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
                        if (Strikable && SAI[i].Puller)
                        {
                            Debug.LogWarning("STRUCK");
                            Strikable = false;
                            for (int j = i; j < Spheres.Count; j++)
                            {
                                //if (SAI[j].ID > 0)
                                //{
                                    SAI[j].Speed = -10;
                                //}
                            }
                            //Remove Pullee Att
                            for (int j = i; j > 0 && SAI[j-1].Pullee; j--)
                            {
                                SAI[j-1].Pullee = false;
                            }

                            SAI[i].Puller = false;
                        }
                    }
                    SAI[i].IsMoving = false;

                    #region Magnetism

                    //Puller
                    if (SAI[i].CID == SAI[i + 1].CID && SAI[i].Traversed() - SAI[i + 1].Traversed() >= SizeThresh + MinMagDist)
                    {
                        SAI[i].IsMoving = false;
                        SAI[i].Puller = true;
                    }
                    //Pullee
                    if (SAI[i + 1].Puller || SAI[i + 1].Pullee)
                    {
                        if (SAI[i].Traversed() - SAI[i + 1].Traversed() <= SizeThresh + MinMagDist)
                        {
                            SAI[i].Pullee = true;
                            SAI[i].AimSpeed = SAI[i + 1].AimSpeed;
                            SAI[i].Speed = SAI[i + 1].Speed;
                        }
                    }

                    #endregion

                    #region SpherePhysics

                    if (SAI[i].Puller && IsStrikable(i))
                    {
                        Strikable = true;
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
    /**/
    public bool IsStrikable(int i)
    {
        if (SAI[i].Traversed() - SAI[i+1].Traversed() <= SizeThresh)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

    public void SetSpeed(float speed)
    {
        ZoneSpeed = speed;
    }
}