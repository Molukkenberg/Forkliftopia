using Unity.VisualScripting;
using UnityEngine;

public class PalletRackScript : MonoBehaviour
{
    public GameObject[] Slots = new GameObject[6];
    public bool RackFilled;
    public bool RackFull;
    public int RackSlotsFilled = 0;

    public void SetSlot(GameObject GoodToStore)
    {
        RackFilled = true;
        var slot = GetFirstFreeSlot();
        if (slot != -1)
        {
            GoodToStore.transform.SetPositionAndRotation(transform.GetChild(slot).position, Quaternion.identity);
            GoodToStore.gameObject.transform.parent = transform.GetChild(slot).gameObject.transform;
            Slots[slot] = GoodToStore;
            RackSlotsFilled += 1;
        }

        if (RackSlotsFilled == 6)
        {
            RackFull = true;
            print("Rack: " + gameObject + " is full.");
        }
    }

    public GameObject GetGameObjectInSlot(int SlotNumber)
    {
        if (Slots[SlotNumber] != null)
            return Slots[SlotNumber];
        else
            return null;
    }

    public GameObject GetFirstGameObjectInSlots()
    {
        for (int i=0; i<Slots.Length; i++)
        {
            if (Slots[i] != null)
            {
                return Slots[i];
            }
        }
        return null;
    }
    public int GetFirstFilledSlot()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null)
            {
                return i;
            }
        }
        return -1;
    }
    public void ClearSlot(int SlotNumber)
    {
        Slots[SlotNumber] = null;
        RackFull = false;
        RackSlotsFilled -= 1;

        if (RackSlotsFilled < 1)
        {
            RackFilled = false;
        }
    }

    public bool isRackFilled()
    {
        return RackFull;
    }

    public int GetFirstFreeSlot()
    {
        for (int i =0; i<=5; i++)
        {
            if (Slots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
