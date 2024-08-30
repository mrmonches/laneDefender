using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    [SerializeField] private float BulletSpeed;

    [SerializeField] private GameObject ExplosionObject;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.velocity = new Vector2(BulletSpeed, 0.0f);
    }

    /// <summary>
    /// Checks collision against possible types
    /// Also generates explosion when conditions are met
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BaseEnemyController BEC = collision.gameObject.GetComponent<BaseEnemyController>();

            BEC.DamageTaken();

            Instantiate(ExplosionObject, collision.GetContact(0).point, Quaternion.identity);
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}