using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootAction : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletHitMissDistance = 50f;

    [SerializeField]
    private Transform barrelTransform;

    private PlayerInput playerInput;
    private InputAction shootAction;
    private Transform cameraTransform;

    // Awake happens before OnEnable
    void Awake()
    {
        cameraTransform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        // subscribe to event
        shootAction.performed += _ => ShootWeapon();
    }

    private void OnDisable()
    {
        // unsubscribe
        shootAction.performed -= _ => ShootWeapon();
    }

    private void ShootWeapon()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        // assuming this Bullet has BulletController attached
        BulletController bulletController = bullet.GetComponent<BulletController>();
        float rayMaxDistance = 100f;// eg. Mathf.Infinity
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayMaxDistance))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        } else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
            bulletController.hit = false;
        }
    }
}
