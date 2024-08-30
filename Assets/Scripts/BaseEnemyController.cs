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

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        isDead = false;

        _rb2d.velocity = new Vector2(-EnemySpeed, 0.0f);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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

    private IEnumerator DamageStagger()
    {
        if (true)
        {
            _rb2d.velocity = Vector2.zero;

            yield return new WaitForSeconds(StaggerTimer);

            _rb2d.velocity = new Vector2(-EnemySpeed, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Barrier")) &&  !isDead)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator KillDelay()
    {
        if (true)
        {
            _rb2d.velocity = Vector2.zero;

            _animator.SetTrigger("IsKilled");

            yield return new WaitForSeconds(StaggerTimer);

            _gameManager.UpdateScore(EnemyValue);

            Destroy(gameObject);
        }
    }
}