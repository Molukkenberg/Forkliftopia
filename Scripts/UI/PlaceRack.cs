using UnityEngine;

public class PlaceRack : MonoBehaviour
{
    [SerializeField] private GameObject PalletRack;
    [SerializeField] private bool takingInputs;

    public void ToggleChange(bool newBool)
    {
        takingInputs = newBool;
    }

    void Update()
    {
        if (takingInputs)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ChangeScore.Instance.score >= 50)
                {
                    if (MousePosition.GetMouseWorldPosition() != Vector3.zero)
                    {
                        ChangeScore.Instance.ModifyScore(-50);
                        var spawningPalletRack = PalletRack;
                        Instantiate(spawningPalletRack, MousePosition.GetMouseWorldPosition(), Quaternion.identity, PalletRackParentScript.Instance.transform);
                        // Debug.Log(MousePosition.GetMouseWorldPosition());
                    }
                }
            }
        }
    }
}

// Erägnzungen
// Layer Racks hinzufügen?
// Hier irgendwo die Abfrage genügend Geld einbauen.