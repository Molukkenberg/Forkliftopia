using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerParent : MonoBehaviour
{
    public static WorkerParent Instance;

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

    [SerializeField] private int deliveredGoods;
    [SerializeField] private int storedGoods;

    public int GetDeliveredGoods()
    {
        return deliveredGoods;
    }
    public void SetDeliveredGoods(int newDeliverey)
    {
        deliveredGoods = newDeliverey;
    } 

    public int GetStoredGoods()
    {
        return storedGoods;
    }
    public void SetStoredGoods(int newStored)
    {
        storedGoods = newStored;
    }


    // Einzelne Worker mit Arbeit versorgen.

    public WorkerNavigation GetRandomWorker()
    {
        WorkerNavigation worker;
        worker = transform.GetChild(Random.Range(0, transform.childCount)).gameObject.GetComponent<WorkerNavigation>();
        if (!worker.GetBusy())
        {
            return worker;
        }
        return null;
    }

    private void Update()
    {
        // QUEUE?

        // Zuerst nach Verfügbarem Worker Checken, dann nach Aufgaben.
        if (GetRandomWorker() != null)
        {
            // Wenn mehr als 1 Güter gespawnt werden, dann gehen auch mehrere calls raus, sich die zu holen.
            if (PalletRackParentScript.Instance.GetFreeRack() != null)
            {
                if (SpawnGoods.Instance.SpawnedGoods.Count > 0) 
                {
                    var _worker = GetRandomWorker();
                    if (_worker != null)
                    {
                        _worker.GetSpawnedGood();
                    }
                }
            }

            // Gleichzeitig beide aufgaben an einen Worker verteilen klappt glaube ich nicht.

            // ÜBERARBEITEN
            if (storedGoods > 0)
            {
                var _worker = GetRandomWorker();
                if (_worker != null)
                {
                    _worker.PickStoredGood();
                }
            }
        }
    }
}