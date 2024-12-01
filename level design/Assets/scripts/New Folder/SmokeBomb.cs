using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private GameObject smokeCloud;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Smoke Cloud")) return;

        Instantiate(smokeCloud, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
