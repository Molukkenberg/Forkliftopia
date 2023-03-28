using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGoods : MonoBehaviour
{
    public Transform SpawnGoodsHere;
    public GameObject GoodToSpawn;
    public Stack<GameObject> SpawnedGoods = new();

    public int maxPallets { get; private set; }
    public static SpawnGoods Instance { get; private set; }
    private SpreadGoods spreadGoods;
    [SerializeField] private TMP_InputField inputField;

    // Neu
    [SerializeField] private GameObject _visualParent;
    [SerializeField] private int palletsSum = 0;
    [SerializeField] private SpreadGoodsParent _spreadGoodsParent;

    private void Awake()
    {
        if(Instance!= null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        spreadGoods = GetComponent<SpreadGoods>();
    }

    /*private void Start()
    {
        maxPallets = spreadGoods.GetMaxPallets();
        print("SpawnGoods max pal : " + spreadGoods.GetMaxPallets());
    }
    */

    // Veraltet
    public void InstantiateGoods() 
    {
        int.TryParse(inputField.text, out int playerInput); // TryParse versucht den Input zu parsen und bei einem Zahlenwert wird in einen Integer umgewandelt. Bei Text funktionierts nicht.
        for (int i=0;  i<playerInput; i++)
        {
            if (SpawnedGoods.Count < spreadGoods.GetMaxPallets())
            {
                var Good = Instantiate(GoodToSpawn, SpawnGoodsHere, true);
                Good.transform.position = SpawnGoodsHere.position;
                SpawnedGoods.Push(Good);
                spreadGoods.Spread(); // Zuerst müsste sSpread() aufgerufen werden, weil darin die maxPallets bestimmt werden
            }
            else
            {
                ChangeScore.Instance.ModifyScore(-20);
                Debug.Log("Zu wenig Platz um Güter anzunehmen, der Lieferant musste seine Ware wieder mitnehmen und wir müssen jetzt Strafe zahlen..");
            }
        }
        //print(SpawnedGoods.Count + " " + maxPallets);
        WorkerParent.Instance.SetDeliveredGoods(SpawnedGoods.Count);
    }

    // Neu. | Von einer Zone muss auf alle Children des VisualZoneParent gearbeitet werden
    public void InstantiateGoodsInChildren()
    {
        
        palletsSum = 0;
        for (int i =0; i < _visualParent.transform.childCount; i++)
        {
            //print("Children: " + i + " MaxPallets: " + _visualParent.transform.GetChild(i).GetComponent<SpreadGoods>().GetMaxPallets());
            palletsSum += _visualParent.transform.GetChild(i).GetComponent<SpreadGoods>().GetMaxPallets();
        }

        int.TryParse(inputField.text, out int playerInput); 
        for (int i = 0; i < playerInput; i++)
        {
            if (_spreadGoodsParent.GetFreeZone() != null)
            {
               if (SpawnedGoods.Count <= palletsSum)  // => palletsSum wird verkehrt berechnet 
                {
                   // if (_spreadGoodsParent.GetFreeZone() != null)
                    //{ 
                        var freeZone = _spreadGoodsParent.GetFreeZone().transform;
                        var Good = Instantiate(GoodToSpawn, freeZone, true); 
                        SpawnedGoods.Push(Good);
                        freeZone.GetComponent<SpreadGoods>().SpreadParent();
                    //}
                    /*
                    else
                    {
                        ChangeScore.Instance.ModifyScore(-20);
                        Debug.Log("Zu wenig Platz um Güter anzunehmen, der Lieferant musste seine Ware wieder mitnehmen und wir müssen jetzt Strafe zahlen..");
                    }*/
                }

            }
            else
            {
                ChangeScore.Instance.ModifyScore(-20);
                Debug.Log("Zu wenig Platz um Güter anzunehmen, der Lieferant musste seine Ware wieder mitnehmen und wir müssen jetzt Strafe zahlen..");
            }
        }
        WorkerParent.Instance.SetDeliveredGoods(SpawnedGoods.Count);
        print("spawnedGoods.count: " + SpawnedGoods.Count);
    }
}
