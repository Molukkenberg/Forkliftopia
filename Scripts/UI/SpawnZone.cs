using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private bool _deliveryBool;
    [SerializeField] private bool _collectorBool;

    [SerializeField] private GameObject _deliveryZonePrefab;
    [SerializeField] private GameObject _collectorZonePrefab;

    [SerializeField] private GameObject _deliveryZoneParent;
    [SerializeField] private GameObject _collectorZoneParent;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {   
            if (ChangeScore.Instance.score >= 20)
            {
                if (_deliveryBool)
                    SpawnDeliveryZone();

                if (_collectorBool)
                    SpawnCollectorZone();
            }
            else
            {
                Debug.Log("Du hast nicht genug Geld um eine neue Zone zu kaufen!");
            }
        }
    }

    public void ToggleDeliveryBool(bool newState)
    {
        _deliveryBool = newState;
        print("Delivery Bool " + _deliveryBool);
    }
    public void ToggleCollectorBool(bool newState)
    {
        _collectorBool = newState;
        print("Collector Bool " + _collectorBool);
    }

    public void SpawnCollectorZone()
    {
        ChangeScore.Instance.ModifyScore(-20);
        if (MousePosition.GetMouseWorldPosition()!=Vector3.zero)
            Instantiate(_collectorZonePrefab, MousePosition.GetMouseWorldPosition(), Quaternion.identity, _collectorZoneParent.transform);
    }

    public void SpawnDeliveryZone()
    {
        ChangeScore.Instance.ModifyScore(-20);
        if (MousePosition.GetMouseWorldPosition() != Vector3.zero)
            Instantiate(_deliveryZonePrefab, MousePosition.GetMouseWorldPosition(), Quaternion.identity, _deliveryZoneParent.transform);
    }

    public GameObject GetCollectorParent()
    {
        return _collectorZoneParent;
    }
}
