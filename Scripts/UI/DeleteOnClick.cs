using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnClick : MonoBehaviour
{
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
                if (MousePosition.GetGameObjectOnRay() != null)
                {
                    if (!MousePosition.GetGameObjectOnRay().CompareTag("Undestroyable"))
                    {
                        Destroy(MousePosition.GetGameObjectOnRay());
                    }
                }
            }
        }
    }
}
