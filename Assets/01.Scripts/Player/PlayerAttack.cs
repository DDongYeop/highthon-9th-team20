using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int _damage;
    private SpriteRenderer playerFlipX, cameraFlipX, cameraLightFilpX, videoRenderer;
    private GameObject videoObj;
    private Sprite videoSprite;
    private Transform cameraPos, cameraLightPos;
    private Collider2D[] hit = new Collider2D[10];
    private Vector3 playerPos;
    private AudioSource cameraAudio;
    public Vector2 size, videoSize;
    private bool[] skillCool = new bool[2];
    private Image[] skillIcon = new Image[3];

    void Start()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        while (GameManager.Instance == null) yield return null;
        playerFlipX = GetComponent<SpriteRenderer>();
        cameraPos = transform.Find("Camera").GetComponent<Transform>();
        cameraLightPos = transform.Find("Camera Light").GetComponent<Transform>();
        cameraFlipX = transform.Find("Camera").GetComponent<SpriteRenderer>();
        cameraLightFilpX = transform.Find("Camera Light").GetComponent<SpriteRenderer>();
        videoObj = transform.Find("Video Light").gameObject;
        cameraLightFilpX.color = new Color(255, 255, 255, 0);
        videoRenderer = transform.Find("Video Light").GetComponent<SpriteRenderer>();
        GameObject videoObject = (GameObject)Resources.Load("04.Prefabs/Video");
        SpriteRenderer spriteRenderer = videoObject.GetComponent<SpriteRenderer>();
        cameraAudio = GetComponent<AudioSource>();

        for (int i = 1; i < 3; i++)
        {
            skillIcon[i] = GameObject.Find("UI").transform.GetChild(i).GetComponent<Image>();
        }

        if (spriteRenderer != null)
        {
            videoSprite = spriteRenderer.sprite;
            videoObj.SetActive(false);
        }
        else Debug.LogError("SpriteRenderer X");

        for (int i = 0; i < skillCool.Length; i++) { skillCool[i] = true; }
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        StartCoroutine(Picture_Attack(x));

        if (x != 0 && GameManager.Instance.isVideo)
        {
            playerFlipX.flipX = Filp(x);
            cameraFlipX.flipX = Filp(x);
            cameraLightFilpX.flipX = Filp(x);
            videoRenderer.flipX = Filp(x);
        }

        if (x != 0 && GameManager.Instance.isVideo)
        {
            cameraPos.localPosition = new Vector3(0.73f * x,
                                                  cameraPos.localPosition.y, 
                                                  cameraPos.localPosition.z);
            cameraLightPos.localPosition = new Vector3(1.57f * x,
                                                       cameraLightPos.localPosition.y,
                                                       cameraLightPos.localPosition.z);
            videoObj.transform.localPosition = new Vector3(4.21f * x,
                                                           videoObj.transform.localPosition.y,
                                                           videoObj.transform.localPosition.z);
        }

        playerFlipX = transform.GetComponent<SpriteRenderer>(); 
    }

    private IEnumerator Picture_Attack(float x)
    {
        hit = Physics2D.OverlapBoxAll(playerPos, size, 0); 

        if (x < 0) playerPos = new Vector3(transform.position.x - videoSize.x - 2, transform.position.y, transform.position.z); 
        else if (x > 0) playerPos = new Vector3(transform.position.x + videoSize.x + 2, transform.position.y, transform.position.z);
        else playerPos = transform.position;

        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isVideo && skillCool[0])
        {
            skillCool[0] = false;
            cameraAudio.volume = 1.0f;
            cameraAudio.Play();

            foreach (var hit in hit)
            {
                if (hit == null) break;
                if (hit.TryGetComponent<AgentMono>(out AgentMono agent) && !agent.IsPlayer) agent.CurrentHP -= _damage;
            }
            
            float currentTime = 0;

            while (currentTime <= 0.5f)
            {
                yield return null;

                currentTime += Time.deltaTime;
                float time = currentTime / 0.5f;
                cameraLightFilpX.color = new Color(255, 255, 255, Mathf.Lerp(0.0f, 1.0f, time));
            }
            StartCoroutine(CoolTime(skillIcon[1], 0, 2f));
            cameraLightFilpX.color = new Color(255, 255, 255, 0);
        }
        else 
        {   
            cameraLightFilpX.color = new Color(255, 255, 255, 0);
            cameraAudio.Stop(); 
        }
 
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.isVideo)
        {
            skillCool[1] = false;
            GameManager.Instance.isVideo = false;
            Sprite beforeCamera = cameraFlipX.sprite;

            cameraFlipX.sprite = videoSprite;
                    
            videoObj.SetActive(true);
            StartCoroutine(videoCool(beforeCamera));
            StartCoroutine(CoolTime(skillIcon[2], 1, 10f));
        }
    }

    IEnumerator CoolTime(Image img, int n, float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime; 
            img.fillAmount = 1.0f / cool;

            yield return new WaitForFixedUpdate(); 
        }

        skillCool[n] = true;
    }

    IEnumerator videoCool(Sprite s)
    {
        yield return new WaitForSeconds(5f);

        GameManager.Instance.isVideo = true;
        cameraFlipX.sprite = s;
        videoObj.SetActive(false);
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
