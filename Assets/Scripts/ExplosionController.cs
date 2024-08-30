using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float ExplosionMaxTime;
    private float explosionTime;

    private void Update()
    {
        explosionTime += Time.deltaTime;

        if (explosionTime >= ExplosionMaxTime)
        {
            Destroy(gameObject);
        }
    }
}
