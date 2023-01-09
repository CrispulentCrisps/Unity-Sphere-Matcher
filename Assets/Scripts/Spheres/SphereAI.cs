using UnityEngine;
using PathCreation;

public class SphereAI : MonoBehaviour
{
    PathCreator PC;
    float AreaTravelled;
    public bool IsMoving = false;
    public bool Puller = false;
    public bool Pullee = false;
    public float Speed;
    public float AimSpeed;
    float DampeningSpeed = 3f;
    public int ID;
    public int CID;
    //DEBUG
    private Color SourceColour;
    private void Start()
    {
        PC = GameObject.FindGameObjectWithTag("ActivePath").GetComponent<PathCreator>();
        ID = GameManager.SphereNum;
        Speed = GameManager.ZoneSpeed;
        gameObject.name = "Sphere - ID: " + ID;
        GameManager.SphereNum += 1;
        //DEBUG
        SourceColour = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (IsMoving)
        {
            if (Speed < AimSpeed)
            {
                Speed -= AimSpeed * Time.deltaTime;
            }
            else if (Speed > AimSpeed)
            {
                Speed += AimSpeed * Time.deltaTime;
            }

            AreaTravelled += Speed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (Puller || Pullee)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            Speed -= DampeningSpeed * Time.deltaTime;
            AreaTravelled += Speed * Time.deltaTime;
        }
        else
        {
            AreaTravelled += Speed * Time.deltaTime;
            Speed *= .9999999f;
            gameObject.GetComponent<SpriteRenderer>().color = SourceColour;
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
