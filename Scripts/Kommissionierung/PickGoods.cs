using UnityEngine;

public class PickGoods : MonoBehaviour
{
    public PalletRackParentScript palletRackParentScript;
    public PalletRackScript palletRackScript;
    public Transform issueArea;
    private SpreadGoods spreadGoods;

    //public Transform CollectorTruck;
    [SerializeField] public SpreadGoodsParent CollectorParent;

    public GameObject GoodPicked;

    private void Awake()
    {
      //  spreadGoods = CollectorTruck.GetComponent<SpreadGoods>();
    }

    public void PickFirst() // Button Pick
    {
        spreadGoods.CalculateMaxPallets();

        if (issueArea.childCount < spreadGoods.GetMaxPallets())
        {
            var RackToPickFrom = palletRackParentScript.GetRackWithGoods();

            if (RackToPickFrom != null)
            {
                if (RackToPickFrom.GetFirstGameObjectInSlots() != null)
                {
                    var GoodToPick = RackToPickFrom.GetFirstGameObjectInSlots();
                    GoodToPick.transform.parent = issueArea;
                    spreadGoods.Spread();

                    if (RackToPickFrom.GetFirstFilledSlot() != -1)
                        RackToPickFrom.ClearSlot(RackToPickFrom.GetFirstFilledSlot());
                }
            }
            else
                print("No goods stored");
        }
    }

    public void PickFirstObject() // Pick by Forklift
    {

        if (issueArea.childCount < spreadGoods.GetMaxPallets())
        {
            var RackToPickFrom = palletRackParentScript.GetRackWithGoods();

            if (RackToPickFrom != null)
            {
                if (RackToPickFrom.GetFirstGameObjectInSlots() != null)
                {
                    var GoodToPick = RackToPickFrom.GetFirstGameObjectInSlots();
                    GoodToPick.transform.parent = transform;

                    if (Vector3.Distance(transform.position, issueArea.transform.position) < 5)
                    {
                        GoodToPick.transform.parent = issueArea.transform; // ??? müsste ned der parent der stapler sein?
                        spreadGoods.Spread();
                    }
                    if (RackToPickFrom.GetFirstFilledSlot() != -1)
                        RackToPickFrom.ClearSlot(RackToPickFrom.GetFirstFilledSlot());
                }
            }
            else
                print("No goods stored");
        }
        else
        {
            print("IssueArea full!");
        }
    }

    public GameObject GetFirstObject() // Pick by WorkerNavigation
    {
        // ZoneManager Childcount wird gebraucht
        if (issueArea.parent.childCount-1 < issueArea.GetComponent<SpreadGoods>().GetMaxPallets()) // PArent.childcount wird abefragt, weil die einzelnen paletten unter dem parent liegen, damit nicht fehlskaliert wird.
        {
            var RackToPickFrom = palletRackParentScript.GetRackWithGoods();

            if (RackToPickFrom != null)
            {
                if (RackToPickFrom.GetFirstGameObjectInSlots() != null)
                {
                    return RackToPickFrom.GetFirstGameObjectInSlots();
                }
            }
            return null;
        }
        else return null;
    }
    
    public GameObject GetFirstPick()
    {
        //CollectorParent.transform.childCount // macht das sinn?
        if (CollectorParent.GetFreeZone()!=null)
        {
            var FreeZone = CollectorParent.GetFreeZone();
            if (FreeZone != null)
            {
                var RackToPickFrom = palletRackParentScript.GetRackWithGoods();

                if (RackToPickFrom != null)
                {
                    if (RackToPickFrom.GetFirstGameObjectInSlots() != null)
                    {
                        return RackToPickFrom.GetFirstGameObjectInSlots();
                    }
                }
                return null;
            }
        }
        return null;
    }

    public void ClearFirstPickableSlot()
    {
        var RackToPickFrom = palletRackParentScript.GetRackWithGoods();

        if (RackToPickFrom != null)
        {
            if (RackToPickFrom.GetFirstGameObjectInSlots() != null)
            {
                RackToPickFrom.ClearSlot(RackToPickFrom.GetFirstFilledSlot());
            }
        }
    }



    public void PickLast()
    {
        if (palletRackScript.GetGameObjectInSlot(5) != null)
        {
            var GoodToPick = palletRackScript.GetGameObjectInSlot(5);
            GoodToPick.transform.position = issueArea.position;
            palletRackScript.ClearSlot(5);
        }
    }

    public void PickRandom()
    {

    }
}
