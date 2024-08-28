using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    [SerializeField] private float BulletSpeed;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.velocity = new Vector2(BulletSpeed, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            BaseEnemyController BEC = collision.gameObject.GetComponent<BaseEnemyController>();

            BEC.DamageTaken();
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
