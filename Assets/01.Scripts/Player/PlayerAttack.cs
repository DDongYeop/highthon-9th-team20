using System.Transactions;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpriteRenderer playerFlipX;
    private Transform cameraPos;

    void Start()
    {
        playerFlipX = GetComponent<SpriteRenderer>();
        cameraPos = transform.Find("Camera").GetComponent<Transform>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x < 0)
        {
            playerFlipX.flipX = true;
        } 
        else if (x > 0)
        {
            playerFlipX.flipX = false;
        } 

        if (x != 0)
        {
            cameraPos.localPosition = new Vector3(transform.localPosition.x,
                                                  cameraPos.localPosition.y, 
                                                  cameraPos.localPosition.z);
        }
    }
}
