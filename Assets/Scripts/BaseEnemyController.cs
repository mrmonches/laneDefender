using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] private int EnemyHealth;

    public void DamageTaken()
    {
        EnemyHealth--;

        if (EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
