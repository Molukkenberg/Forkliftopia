using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private Transform _selection;

    public static SelectionManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (_selection != null)
        {  
            if (_selection.GetComponent<Renderer>() != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if(hit.transform.GetComponent<Renderer>()!=null)
                defaultMaterial = hit.transform.GetComponent<Renderer>().material;

            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}
