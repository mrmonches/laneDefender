using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction moveAction;
    private InputAction shootAction;

    private Rigidbody2D _rb2d;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float ShootTimer;
    private float moveValue;

    [SerializeField] private GameObject BulletObject;
    [SerializeField] private Transform BulletSpawnPoint;

    private bool canShoot;
    private bool shootHeld;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        moveAction = _playerInput.currentActionMap.FindAction("Move");
        shootAction = _playerInput.currentActionMap.FindAction("Shoot");

        moveAction.started += MoveAction_started;
        moveAction.canceled += MoveAction_canceled;

        shootAction.started += ShootAction_started;
        shootAction.canceled += ShootAction_canceled;

        canShoot = true;

        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void ShootAction_started(InputAction.CallbackContext obj)
    {
        shootHeld = true;
    }
    private void ShootAction_canceled(InputAction.CallbackContext obj)
    {
        shootHeld = false;
    }

    private void MoveAction_started(InputAction.CallbackContext obj)
    {
        moveValue = moveAction.ReadValue<float>() * MoveSpeed;
    }

    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        moveValue = 0;
    }

    private void ShootBullet()
    {
        if (canShoot)
        {
            Instantiate(BulletObject, BulletSpawnPoint.position, Quaternion.identity);

            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        if (true)
        {
            canShoot = false;

            yield return new WaitForSeconds(ShootTimer);

            canShoot = true;
        }
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(0.0f, moveValue);

        if (canShoot && shootHeld)
        {
            ShootBullet();
        }
    }

    private void OnDestroy()
    {
        moveAction.started -= MoveAction_started;
        moveAction.canceled -= MoveAction_canceled;

        shootAction.started -= ShootAction_started;
        shootAction.canceled -= ShootAction_canceled;
    }
}
