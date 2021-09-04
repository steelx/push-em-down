using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    public Canvas thirdPersonCanvas;
    public Canvas aimCanvas;
    [SerializeField]
    private PlayerInput playerInput;

    private InputAction aimAction;
    private CinemachineVirtualCamera virtualCamera;

    private int priorityBoostAmount = 10;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        // subscribe to event
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
        aimCanvas.enabled = false;
    }

    private void OnDisable()
    {
        // unsubscribe
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        virtualCamera.Priority += priorityBoostAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}
