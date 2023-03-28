using UnityEngine;

public class SpreadGoodsParent : MonoBehaviour
{
    [SerializeField] private int MaxPossiblePallets;
    public SpreadGoods GetFreeZone()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            //MaxPossiblePallets += transform.GetChild(i).GetComponent<SpreadGoods>().GetMaxPallets();

            if (transform.GetChild(i).childCount-1  < transform.GetChild(i).GetComponent<SpreadGoods>().GetMaxPallets())
            {
                return transform.GetChild(i).GetComponent<SpreadGoods>();
            }
        }
        //MaxPossiblePallets = transform.GetChild(i).GetComponent<SpreadGoods>().GetMaxPallets()
        return null;
    }
}
