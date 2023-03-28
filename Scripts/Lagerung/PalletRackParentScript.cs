using Mono.Cecil;
using UnityEngine;

public class PalletRackParentScript : MonoBehaviour
{
    public static PalletRackParentScript Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public PalletRackScript GetRack(int rackNumber)
    {
        return gameObject.transform.GetChild(rackNumber).GetComponent<PalletRackScript>();
    
    }

    public PalletRackScript GetRackWithGoods()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PalletRackScript>().RackFilled)
            {
                return transform.GetChild(i).GetComponent<PalletRackScript>();
            }
        }

        return null;
    }

    public PalletRackScript GetFreeRack()
    {
        // Achtung Bottleneck?
        for(int i=0; i < transform.childCount; i++)
        {
            if (!gameObject.transform.GetChild(i).GetComponent<PalletRackScript>().RackFull)
            {
                // print(transform.GetChild(i).name);
                return gameObject.transform.GetChild(i).GetComponent<PalletRackScript>();
            }
        }
        return null;
    }

    /*
    public PalletRackScript GetFilledRack()
    {
        for (int i=0; i <transform.childCount; i++)
        {
            // if abfrage ändern
            if (gameObject.transform.GetChild(i).GetComponent<PalletRackScript>().Slots.)
            {

            }
        }
        return null;
    }
    */
}