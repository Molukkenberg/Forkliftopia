using System;
using UnityEngine;

public class SpreadGoods : MonoBehaviour
{
    public Transform spreadingArea;
    public GameObject goodToSpread;
    [SerializeField] private int possiblePalletsX, possiblePalletsZ, possiblePalletsSum;
    private void Awake()
    {
        // Mögliche Anzahl an Paletten in der IssueArea.
        possiblePalletsX = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.x / goodToSpread.GetComponent<MeshRenderer>().bounds.size.x));
        possiblePalletsZ = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.z / goodToSpread.GetComponent<MeshRenderer>().bounds.size.z));
        possiblePalletsSum = possiblePalletsX * possiblePalletsZ;
        SetMaxPallets(possiblePalletsSum);
        // print("Sum: " + possiblePalletsSum + " " + gameObject);
    }

    private void Start()
    {
        possiblePalletsX = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.x / goodToSpread.GetComponent<MeshRenderer>().bounds.size.x));
        possiblePalletsZ = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.z / goodToSpread.GetComponent<MeshRenderer>().bounds.size.z));
        possiblePalletsSum = possiblePalletsX * possiblePalletsZ;
        SetMaxPallets(possiblePalletsSum);
    }

    // Recalculate max pallets after resize.
    public void CalculateMaxPallets()
    {
        possiblePalletsX = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.x / goodToSpread.GetComponent<MeshRenderer>().bounds.size.x));
        possiblePalletsZ = Convert.ToInt32(Math.Floor(spreadingArea.GetComponent<MeshRenderer>().bounds.size.z / goodToSpread.GetComponent<MeshRenderer>().bounds.size.z));
        possiblePalletsSum = possiblePalletsX * possiblePalletsZ;
        SetMaxPallets(possiblePalletsSum);
    }

    // Spreading pallets along the area.
    public void Spread()
    {
        // First Position
        Vector3 firstPos = new Vector3(spreadingArea.GetComponent<MeshRenderer>().bounds.min.x,0, spreadingArea.GetComponent<MeshRenderer>().bounds.min.z);
        firstPos.x += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.x + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.x % goodToSpread.GetComponent<MeshRenderer>().bounds.size.x / (possiblePalletsX + 1)));
        firstPos.z += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.z + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.z % goodToSpread.GetComponent<MeshRenderer>().bounds.size.z / (possiblePalletsZ + 1)));

        // Algorithmus zum Setzen der Positionen im Grid
        Vector3 aktuellePosition= Vector3.zero;
        for (int i=0; i < spreadingArea.childCount; i++)
        {
            if (i < possiblePalletsSum)
            {
                if (i == 0)
                {
                    spreadingArea.GetChild(i).transform.position = firstPos;
                }
                else if (i % possiblePalletsX == 0)
                {
                    Vector3 xNullAberZPlus = Vector3.zero;
                    xNullAberZPlus.z = NextPosZ(aktuellePosition, possiblePalletsZ).z;
                    xNullAberZPlus.x = firstPos.x;
                    spreadingArea.GetChild(i).transform.position = xNullAberZPlus;
                }
                else if (possiblePalletsX != i)
                {
                    spreadingArea.GetChild(i).transform.position = NextPosX(aktuellePosition, possiblePalletsX);
                }
                aktuellePosition = spreadingArea.GetChild(i).transform.position;
            }
        }
    }

    public void SpreadParent()
    {
        // First Position
        Vector3 firstPos = new Vector3(spreadingArea.GetComponent<MeshRenderer>().bounds.min.x, 0, spreadingArea.GetComponent<MeshRenderer>().bounds.min.z);
        firstPos.x += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.x + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.x % goodToSpread.GetComponent<MeshRenderer>().bounds.size.x / (possiblePalletsX + 1)));
        firstPos.z += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.z + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.z % goodToSpread.GetComponent<MeshRenderer>().bounds.size.z / (possiblePalletsZ + 1)));

        // Algorithmus zum Setzen der Positionen im Grid
        Vector3 aktuellePosition = Vector3.zero;
        for (int i = 0; i < spreadingArea.parent.childCount-1; i++)
        {
            if (i < possiblePalletsSum)
            {
                if (i == 0)
                {
                    spreadingArea.parent.GetChild(i+1).transform.position = firstPos;
                }
                else if (i % possiblePalletsX == 0)  
                {
                    Vector3 xNullAberZPlus = Vector3.zero;
                    xNullAberZPlus.z = NextPosZ(aktuellePosition, possiblePalletsZ).z;
                    xNullAberZPlus.x = firstPos.x;
                    spreadingArea.parent.GetChild(i+1).transform.position = xNullAberZPlus;
                }
                else if (possiblePalletsX != i)
                {
                    spreadingArea.parent.GetChild(i+1).transform.position = NextPosX(aktuellePosition, possiblePalletsX);
                }
                aktuellePosition = spreadingArea.parent.GetChild(i+1).transform.position;
            }
        }
    }

    public Vector3 NextPosition()
    {
        Vector3 firstPos = new Vector3(spreadingArea.GetComponent<MeshRenderer>().bounds.min.x, 0, spreadingArea.GetComponent<MeshRenderer>().bounds.min.z);
        firstPos.x += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.x + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.x % goodToSpread.GetComponent<MeshRenderer>().bounds.size.x / (possiblePalletsX + 1)));
        firstPos.z += (goodToSpread.GetComponent<MeshRenderer>().bounds.extents.z + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.z % goodToSpread.GetComponent<MeshRenderer>().bounds.size.z / (possiblePalletsZ + 1)));

        // Algorithmus zum Setzen der Positionen im Grid
        Vector3 aktuellePosition = Vector3.zero; // raus?
        for (int i = spreadingArea.parent.childCount-1; i < possiblePalletsSum; i++)
        {
                if (i == 0)
                {
                    return firstPos;
                }
                else if (i % possiblePalletsX == 0)
                {
                    aktuellePosition = spreadingArea.parent.GetChild(i).transform.position;
                    Vector3 xNullAberZPlus = Vector3.zero;
                    xNullAberZPlus.z = NextPosZ(aktuellePosition, possiblePalletsZ).z;
                    xNullAberZPlus.x = firstPos.x;
                    return xNullAberZPlus;
                    
                }
                else if (possiblePalletsX != i)
                {
                    aktuellePosition = spreadingArea.parent.GetChild(i).transform.position;
                    return NextPosX(aktuellePosition, possiblePalletsX);
                }
        }
        return firstPos;
    }

    private Vector3 NextPosX(Vector3 from, int possiblePallets)
    {
        var returnVec = from;
        returnVec.x += goodToSpread.GetComponent<MeshRenderer>().bounds.size.x + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.x % goodToSpread.GetComponent<MeshRenderer>().bounds.size.x / (possiblePallets + 1));
        return returnVec;
    }

    private Vector3 NextPosZ(Vector3 from, int possiblePallets)
    {
        var returnVec = from;
        returnVec.z += goodToSpread.GetComponent<MeshRenderer>().bounds.size.z + (spreadingArea.GetComponent<MeshRenderer>().bounds.size.z % goodToSpread.GetComponent<MeshRenderer>().bounds.size.z / (possiblePallets + 1));
        return returnVec;
    }

    public int GetMaxPallets()
    {
        return possiblePalletsSum;
    }
    public void SetMaxPallets(int max)
    {
        possiblePalletsSum = max;
    }
}