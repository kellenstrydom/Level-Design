using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        
        col.gameObject.GetComponentInParent<PlayerInfo>().EnterCamouflage();
        col.gameObject.GetComponent<Outline>().enabled = true;

    }
    
    private void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        
        col.gameObject.GetComponentInParent<PlayerInfo>().ExitCamouflage();
        col.gameObject.GetComponent<Outline>().enabled = false;

    }
}
