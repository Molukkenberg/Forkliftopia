using UnityEngine;
using UnityEngine.InputSystem; 
using Cinemachine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform CameraFollowTarget;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float ScreenEdgeDetectionOffset = .05f;
    [SerializeField] private CharacterController _controller;

    private void Start()
    {
        _controller = CameraFollowTarget.gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            RotateAround(45);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            RotateAround(-45);
        }
    }
    private void HandleMovement()
    {
        var _movement = Vector3.zero;

        _movement = HandleKeyboardMovement(_movement);
        _movement = HandleMouseMovement(_movement);
        _controller.Move(Quaternion.Euler(0, CameraFollowTarget.eulerAngles.y, 0)*_movement * movementSpeed * Time.deltaTime);
    }

    private void RotateAround(float angle)
    {
        _controller.enabled = false;
        var rotation = CameraFollowTarget.eulerAngles;
        rotation.y += angle;
        CameraFollowTarget.eulerAngles = rotation;
        _controller.enabled = true;
    }

    private Vector3 HandleKeyboardMovement(Vector3 movement)
    {
        if (Keyboard.current.wKey.isPressed)
        {
                movement.z = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
                movement.z = -1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
                movement.x = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
                movement.x = 1;
        }
        return movement;
    }

    private Vector3 HandleMouseMovement(Vector3 movement)
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        var mousePosition = Mouse.current.position.ReadValue();

        if (mousePosition.y >= screenHeight * (1 - ScreenEdgeDetectionOffset))
        {
                movement.z = 1;
        } else if(mousePosition.y<= screenHeight * ScreenEdgeDetectionOffset)
        {
                movement.z = -1;
        }

        if(mousePosition.x>= screenWidth * (1 - ScreenEdgeDetectionOffset))
        {
                movement.x = 1;
        }
        else if (mousePosition.x <= screenWidth * ScreenEdgeDetectionOffset)
        {
                movement.x = -1;
        }
        return movement;
    }
}
