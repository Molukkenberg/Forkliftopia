using UnityEngine;
using UnityEngine.InputSystem;

public class HandleForklift : MonoBehaviour
{
    [SerializeField] Material _defaultMaterialOne;
    [SerializeField] Material _defaultMaterialExtension;
    [SerializeField] Material _selectMaterial;
    [SerializeField] Material[] _defaultMaterials = new Material[2];
    [SerializeField] Material[] _selectMaterials = new Material[2];

    private void Start()
    {
        _defaultMaterialOne = transform.GetChild(0).GetComponent<Renderer>().material;
        _defaultMaterials[0] = _defaultMaterialOne;
        _defaultMaterials[1] = _defaultMaterialOne;

        _defaultMaterialExtension = transform.GetChild(5).GetChild(1).GetComponent<MeshRenderer>().material;

        _selectMaterials[0] = _selectMaterial;
        _selectMaterials[1] = _selectMaterial;
    }

    private void OnMouseEnter()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i == 0)
            {
                transform.GetComponent<MeshRenderer>().materials = _selectMaterials;
            }
            else if(i<6)
            {
                transform.GetChild(i - 1).GetComponent<MeshRenderer>().material = _selectMaterial;
            }
            else 
            {
                transform.GetChild(5).GetChild(0).GetComponent<MeshRenderer>().material = _selectMaterial;
                transform.GetChild(5).GetChild(1).GetComponent<MeshRenderer>().material= _selectMaterial;
                transform.GetChild(5).GetChild(2).GetComponent<MeshRenderer>().material = _selectMaterial;
            }
        }
    }

    private void OnMouseDrag()
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
        for (int i = 0; i < 7; i++)
        {
            if (i == 0)
            {
                transform.GetComponent<MeshRenderer>().materials = _defaultMaterials;
            }
            else if(i<6)
            {
                transform.GetChild(i - 1).GetComponent<MeshRenderer>().material = _defaultMaterialOne;
            }
            else 
            {
                transform.GetChild(5).GetChild(0).GetComponent<MeshRenderer>().material = _defaultMaterialOne;
                transform.GetChild(5).GetChild(1).GetComponent<MeshRenderer>().material = _defaultMaterialExtension;
                transform.GetChild(5).GetChild(2).GetComponent<MeshRenderer>().material = _defaultMaterialExtension;
            }
        }
    }
}
