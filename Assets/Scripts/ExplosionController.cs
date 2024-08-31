using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float ExplosionMaxTime;
    private float explosionTime;

    /// <summary>
    /// Short timer so explosion pops in and then out
    /// </summary>
    private void Update()
    {
        explosionTime += Time.deltaTime;

        if (explosionTime >= ExplosionMaxTime)
        {
            Destroy(gameObject);
        }
    }
}
