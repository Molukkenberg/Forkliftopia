using UnityEngine;

public class StoreGoods : MonoBehaviour
{
    public GameObject GoodToStore;
    public PalletRackScript PalletRack;

    public void StoreGood()
    {
        // PalletRack.SetSlot(GoodToStore);

        if (SpawnGoods.Instance.SpawnedGoods.Count > 0)
        {
            PalletRack.SetSlot(SpawnGoods.Instance.SpawnedGoods.Pop());
        }
    }

    public void GetFreePalletRack()
    {
        if (SpawnGoods.Instance.SpawnedGoods.Count > 0)
        {
            if(PalletRackParentScript.Instance.GetFreeRack()!=null)
                PalletRackParentScript.Instance.GetFreeRack().SetSlot(SpawnGoods.Instance.SpawnedGoods.Pop());
        }
    }
}
