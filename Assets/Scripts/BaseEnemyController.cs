using System.Collections;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] private int EnemyHealth;
    [SerializeField] private float EnemySpeed;
    [SerializeField] private float StaggerTimer;

    [SerializeField] private int EnemyValue;

    private bool isDead;

    private Animator _animator;

    private GameManager _gameManager;

    private Rigidbody2D _rb2d;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip HitClip, DeathClip, LifeClip;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        isDead = false;

        _rb2d.velocity = new Vector2(-EnemySpeed, 0.0f);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// When called, damage is "taken" by the enemy
    /// Also handles logic if enemy dies
    /// </summary>
    public void DamageTaken()
    {
        EnemyHealth--;

        if (EnemyHealth <= 0)
        {
            isDead = true;

            StartCoroutine(KillDelay());
        }
        else
        {
            _animator.SetTrigger("IsHit");

            StartCoroutine(DamageStagger());
        }
    }

    /// <summary>
    /// Coroutine to allow for a stagger
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageStagger()
    {
        if (true)
        {
            _audioSource.PlayOneShot(HitClip);

            _rb2d.velocity = Vector2.zero;

            yield return new WaitForSeconds(StaggerTimer);

            _rb2d.velocity = new Vector2(-EnemySpeed, 0.0f);
        }
    }

    /// <summary>
    /// Trigger to check if enemy collided with player or back barrier
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Barrier")) &&  !isDead)
        {
            _gameManager.UpdateLives();

            _audioSource.PlayOneShot(LifeClip);

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Coroutine to delay enemy's destruction so animations can run
    /// </summary>
    /// <returns></returns>
    private IEnumerator KillDelay()
    {
        if (true)
        {
            _audioSource.PlayOneShot(DeathClip);

            _rb2d.velocity = Vector2.zero;

            _animator.SetTrigger("IsKilled");

            yield return new WaitForSeconds(StaggerTimer);

            _gameManager.UpdateScore(EnemyValue);

            Destroy(gameObject);
        }
    }
}