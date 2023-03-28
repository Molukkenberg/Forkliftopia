using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleZone : MonoBehaviour
{
    [SerializeField] private Vector2 _prevMousePosition;
    [SerializeField] private float _sizingXFactor = .008f;
    [SerializeField] private float _sizingZFactor = .035f;
    Vector3 minimumScale = new(.5f, .1f, 1f);

    [SerializeField] private SpreadGoods _spreadGoods;
    [SerializeField] private SpreadGoods _spreadParentsGoods; // Parent oder ZoneParent

    private void Awake()
    {
        _spreadGoods = GetComponent<SpreadGoods>();
        _spreadParentsGoods = transform.parent.GetComponent<SpreadGoods>();
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            if (SelectionManager.Instance.GetSelection() == transform)
            {
                // Change the scale of mainObject by comparing previous frame mousePosition with this frame's position, modified by sizingFactor.
                Vector3 scale = transform.localScale;
                scale.x = scale.x + (mousePosition.x - _prevMousePosition.x) * _sizingXFactor;
                //scale.y = scale.x;
                scale.z = scale.z + (mousePosition.y - _prevMousePosition.y) * _sizingZFactor;
                transform.localScale = scale;

                // Checking if current scale is less than minimumScale, if yes, Object scales takes value from minimumScale
                if (scale.x < minimumScale.x)
                {
                    transform.localScale = new(minimumScale.x, transform.localScale.y, transform.localScale.z);
                }
                if (scale.z < minimumScale.z)
                {
                    transform.localScale = new(transform.localScale.x, transform.localScale.y, minimumScale.z);
                }
                _spreadGoods.CalculateMaxPallets(); // Damit wird nur das SpreadGoods auf der Zone angesprochen, nicht die Vom Worker.. Die Muss aber aktualisiert werden
                _spreadGoods.Spread();
                //
                _spreadParentsGoods.CalculateMaxPallets(); // => Änderung seit SpreadGoodsParent

                // Zusätzlich muss SpreadGoods aufgerufen werden, sodass die Paletten neu verteilt werden können.
            }

            // print(_spreadGoods.GetMaxPallets());
        }
        _prevMousePosition = mousePosition;
    }
    public int GetScaleZoneMax() => _spreadGoods.GetMaxPallets();

    // Durch diese Funktion müsste die Klasse umbenannt werden zu HandleZone. => Clean Code
    void OnMouseDrag()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            var position = MousePosition.GetMouseWorldPosition();
                position.y = gameObject.transform.parent.position.y;
            gameObject.transform.parent.position = position;
        }
    }
}
