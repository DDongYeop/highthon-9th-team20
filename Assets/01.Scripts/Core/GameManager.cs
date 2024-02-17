using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")] 
    [HideInInspector] public AgentMono Player; //나중에 Player스크립트로 바꾸기.

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;
    }

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<AgentMono>(); //나중에 FindObjectOfType으로 변경. 
    }
}
