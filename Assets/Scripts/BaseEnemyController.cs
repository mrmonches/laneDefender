using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] private int EnemyHealth;
    [SerializeField] private float EnemySpeed;

    [SerializeField] private int EnemyValue;

    private GameManager _gameManager;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.velocity = new Vector2(-EnemySpeed, 0.0f);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DamageTaken()
    {
        EnemyHealth--;

        if (EnemyHealth <= 0)
        {
            _gameManager.UpdateScore(EnemyValue);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
