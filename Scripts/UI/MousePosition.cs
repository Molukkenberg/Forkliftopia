using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePosition : MonoBehaviour
{
    public static MousePosition Instance { get; private set; }
    
    [SerializeField] private LayerMask mouseColliderLayerMask = new();
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            transform.position = raycastHit.point;
        }
    }

    public static GameObject GetGameObjectOnRay() => Instance.GetGameObjectOnRay_Instance();
    private GameObject GetGameObjectOnRay_Instance()
    {
        if (!IsPointerOverUI())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         // if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) => Mit layerMask
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                return raycastHit.transform.gameObject;
            }
        }
        return null;
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();
    private Vector3 GetMouseWorldPosition_Instance() 
    {
        if (!IsPointerOverUI())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {
                return raycastHit.point;
            }
            else
                return Vector3.zero;
        }
        else
            return Vector3.zero;
    }

    public static bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}