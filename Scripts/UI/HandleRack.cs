using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleRack : MonoBehaviour
{
    [SerializeField] Material _defaultMaterial;
    [SerializeField] Material _selectMaterial;

    private void Start()
    {
        _defaultMaterial = transform.GetChild(6).GetComponent<Renderer>().material;
    }

    private void OnMouseEnter()
    {
        for (int i =6; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material = _selectMaterial;
        }
    }

    void OnMouseDrag()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            var position = MousePosition.GetMouseWorldPosition();
            position.y = transform.position.y;
            transform.position = position;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            transform.Rotate(Vector3.up, 90);
        }
    }
    private void OnMouseExit()
    {
        for (int i = 6; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material = _defaultMaterial;
        }
    }
}
