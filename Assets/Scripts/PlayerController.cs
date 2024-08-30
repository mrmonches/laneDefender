using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction restartAction;

    private Rigidbody2D _rb2d;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float ShootTimer;
    private float moveValue;

    [SerializeField] private GameObject BulletObject;
    [SerializeField] private Transform BulletSpawnPoint;
    
    private AudioSource bulletSource;
    [SerializeField] private AudioClip BulletClip;

    [SerializeField] private Animator _animator;

    private GameManager _gameManager;

    private bool canShoot;
    private bool shootHeld;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        moveAction = _playerInput.currentActionMap.FindAction("Move");
        shootAction = _playerInput.currentActionMap.FindAction("Shoot");
        restartAction = _playerInput.currentActionMap.FindAction("Restart");

        moveAction.started += MoveAction_started;
        moveAction.canceled += MoveAction_canceled;

        shootAction.started += ShootAction_started;
        shootAction.canceled += ShootAction_canceled;

        canShoot = true;

        _rb2d = GetComponent<Rigidbody2D>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bulletSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Makes shootHeld true, so player can hold down shoot
    /// </summary>
    /// <param name="obj"></param>
    private void ShootAction_started(InputAction.CallbackContext obj)
    {
        shootHeld = true;
    }

    /// <summary>
    /// Makes shootHeld false, so player won't be holding shoot down anymore
    /// </summary>
    /// <param name="obj"></param>
    private void ShootAction_canceled(InputAction.CallbackContext obj)
    {
        shootHeld = false;
    }

    /// <summary>
    /// Directly adjusts moveValue when called
    /// </summary>
    /// <param name="obj"></param>
    private void MoveAction_started(InputAction.CallbackContext obj)
    {
        moveValue = moveAction.ReadValue<float>() * MoveSpeed;
    }

    /// <summary>
    /// Makes moveValue 0
    /// </summary>
    /// <param name="obj"></param>
    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        moveValue = 0;
    }

    private void OnRestart()
    {
        _gameManager.ReloadScene();
    }

    /// <summary>
    /// Spawns bullet, starts animation, and triggers delay
    /// </summary>
    private void ShootBullet()
    {
        if (canShoot)
        {
            Instantiate(BulletObject, BulletSpawnPoint.position, Quaternion.identity);

            bulletSource.PlayOneShot(BulletClip);

            _animator.SetTrigger("IsShot");

            StartCoroutine(ShootDelay());
        }
    }

    /// <summary>
    /// Coroutine that creates a shoot delay so player doesn't infinitely shoot
    /// </summary>
    /// <returns></returns>
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