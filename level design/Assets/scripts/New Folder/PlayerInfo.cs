using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInfo : MonoBehaviour
{
    public bool isCamouflaged;
    [SerializeField] private Material playerMaterial;

    public void EnterCamouflage()
    {
        isCamouflaged = true;
        Color theColorToAdjust = playerMaterial.color;
        theColorToAdjust.a = 0.5f;
        playerMaterial.color = theColorToAdjust;
    }

    public void ExitCamouflage()
    {
        isCamouflaged = false;
        Color theColorToAdjust = playerMaterial.color;
        theColorToAdjust.a = 1f;
        playerMaterial.color = theColorToAdjust;

    }
}
