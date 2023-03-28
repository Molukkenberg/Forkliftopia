using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnForklift : MonoBehaviour
{
    [SerializeField] private GameObject _ForkliftPrefab;
    [SerializeField] private Transform _ForkliftParent;
    [SerializeField] private bool _ForkliftBool;

    void Update()
    {
        if (_ForkliftBool)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SpawnForkliftPrefab();
            }
        }
    }

    public void ToggleForkliftBool(bool newState)
    {
        _ForkliftBool = newState;
    }

    public void SpawnForkliftPrefab()
    {
        if (ChangeScore.Instance.score >= 100)
        {
            if (MousePosition.GetMouseWorldPosition() != Vector3.zero)
            {
                ChangeScore.Instance.ModifyScore(-100);
                var newForklift = _ForkliftPrefab;
                newForklift.GetComponent<PickGoods>().palletRackParentScript = PalletRackParentScript.Instance;
                newForklift.GetComponent<PickGoods>().CollectorParent = transform.GetComponent<SpawnZone>().GetCollectorParent().GetComponent<SpreadGoodsParent>();
                newForklift.GetComponent<WorkerNavigation>().SetCollectorZoneSpreadGoodsParent(transform.GetComponent<SpawnZone>().GetCollectorParent().GetComponent<SpreadGoodsParent>());
                Instantiate(newForklift, MousePosition.GetMouseWorldPosition(), Quaternion.identity, _ForkliftParent);
            }
        }
        else
        {
            Debug.Log("Du hast nicht genug geld um einen Gabelstapler zu kaufen!");
        }
    }
}
