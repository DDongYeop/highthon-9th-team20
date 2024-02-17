using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpriteRenderer playerFilpX;

    void Start()
    {
        playerFilpX = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        playerFilpX.flipX = x == -1 ? true : false;
    }
}
