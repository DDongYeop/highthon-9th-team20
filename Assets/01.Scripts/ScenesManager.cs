using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LucidSceneLoad()
    {
        Invoke("LucidLoadScene", 3f);
    }

    void LucidLoadScene()
    {
        SceneManager.LoadScene("LucidDreamScene");
    }
}
