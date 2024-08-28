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
    [SerializeField] private Vector3 BulletSpawnPoint;

    [SerializeField] private bool CanShoot;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        moveAction = _playerInput.currentActionMap.FindAction("Move");
        shootAction = _playerInput.currentActionMap.FindAction("Shoot");

        moveAction.started += MoveAction_started;
        moveAction.canceled += MoveAction_canceled;

        CanShoot = true;

        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void MoveAction_started(InputAction.CallbackContext obj)
    {
        moveValue = moveAction.ReadValue<float>() * MoveSpeed;
    }

    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        moveValue = 0;
    }

    private void OnShoot()
    {
        if (CanShoot)
        {
            Instantiate(BulletObject, BulletSpawnPoint, Quaternion.identity);

            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        if (true)
        {
            CanShoot = false;

            yield return new WaitForSeconds(ShootTimer);

            CanShoot = true;
        }
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(0.0f, moveValue);
    }

    private void OnDestroy()
    {
        moveAction.started -= MoveAction_started;
        moveAction.canceled -= MoveAction_canceled;
    }
}
