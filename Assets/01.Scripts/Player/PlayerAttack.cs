using System.Collections;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpriteRenderer playerFlipX, cameraFlipX, cameraLightFilpX, cameraSprite;
    private Sprite videoSprite;
    private Transform cameraPos, cameraLightPos;
    private Collider2D hit;
    private Vector3 playerPos;
    public Vector2 size, videoSize;

    private bool isVideo;

    void Start()
    {
        playerFlipX = GetComponent<SpriteRenderer>();
        cameraPos = transform.Find("Camera").GetComponent<Transform>();
        cameraLightPos = transform.Find("Camera Light").GetComponent<Transform>();
        cameraFlipX = transform.Find("Camera").GetComponent<SpriteRenderer>();
        cameraLightFilpX = transform.Find("Camera Light").GetComponent<SpriteRenderer>();
        cameraLightFilpX.color = new Color(255, 255, 255, 0);
        cameraSprite = transform.Find("Camera").GetComponent<SpriteRenderer>();
        GameObject videoObject = (GameObject)Resources.Load("04.Prefabs/Video");
        SpriteRenderer spriteRenderer = videoObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) 
        {
            videoSprite = spriteRenderer.sprite;
        } 
        else 
        {
            Debug.LogError("SpriteRenderer X");
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        StartCoroutine(Picture_Attack(x));

        if (x != 0)
        {
            playerFlipX.flipX = Filp(x);
            cameraFlipX.flipX = Filp(x);
            cameraLightFilpX.flipX = Filp(x);
        }

        if (x != 0)
        {
            cameraPos.localPosition = new Vector3(0.73f * x,
                                                  cameraPos.localPosition.y, 
                                                  cameraPos.localPosition.z);
            cameraLightPos.localPosition = new Vector3(1.57f * x,
                                                       cameraLightPos.localPosition.y,
                                                       cameraLightPos.localPosition.z);
        }
    }

    private IEnumerator Picture_Attack(float x)
    {
        Collider2D hit = Physics2D.OverlapBox(playerPos, size, 0); 

        if (x < 0) playerPos = new Vector3(transform.position.x - videoSize.x - 2, transform.position.y, transform.position.z); 
        else if (x > 0) playerPos = new Vector3(transform.position.x + videoSize.x + 2, transform.position.y, transform.position.z);
        else playerPos = transform.position; 

        if (Input.GetMouseButtonDown(0) && isVideo != true)
        {
            float currentTime = 0;

            while (currentTime <= 0.5f)
            {
                yield return null;

                currentTime += Time.deltaTime;
                float time = currentTime / 0.5f;
                cameraLightFilpX.color = new Color(255, 255, 255, Mathf.Lerp(0.0f, 1.0f, time));
            }
            cameraLightFilpX.color = new Color(255, 255, 255, 0);
        }
        else cameraLightFilpX.color = new Color(255, 255, 255, 0);

        if (Input.GetMouseButtonDown(1))
        {
            isVideo = true;

            cameraSprite.sprite = videoSprite;


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerPos, size);
    }

    private bool Filp(float x)
    {
        return x < 0 ? true : false;
    }
}
