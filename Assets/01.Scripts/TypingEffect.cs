using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class T : MonoBehaviour
{
    [Header("Loading Text")]
    [SerializeField] private TMP_Text loadingText_Title;
    private string loadingText = "'Lucid Dream (자각몽)'";
    
    [SerializeField] private TMP_Text loadingText_Content_1;
    private string ContentText_1 = "자신이 꿈을 꾸고 있다는 것을 인지하고 있는 상태에서 꿈을 꾸는 것";

    [SerializeField] private TMP_Text loadingText_Content_2;
    private string ContentText_2 = "배경을 바꾸거나 원하는 인물을 소환하고 초능력을 사용하는 것이 가능하다고 한다.";

    [SerializeField] private TMP_Text loadingText_Content_3;
    private string ContentText_3 = "가끔 자각몽을 컨트롤 하지 못해 구체적이고 혐오스러운 악몽을 꾸게 된다고 한다.";

    private void Start()
    {
        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i <loadingText.Length; i++)
        {
            loadingText_Title.text = loadingText.Substring(0, ++i);
            yield return new WaitForSeconds(0.15f);
        }

        for (int i = 0; i < ContentText_1.Length; i++)
        {
            loadingText_Content_1.text = ContentText_1.Substring(0, ++i);
            yield return new WaitForSeconds(0.15f);
        }

        for (int i = 0; i < ContentText_2.Length; i++)
        {
            loadingText_Content_2.text = ContentText_2.Substring(0, ++i);
            yield return new WaitForSeconds(0.15f);
        }

        for (int i = 0; i < ContentText_3.Length; i++)
        {
            loadingText_Content_3.text = ContentText_3.Substring(0, ++i);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
