using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    [Header("Loading Text")]
    [SerializeField] private TMP_Text loadingText_Title;
    private string loadingText = "'Lucid Dream (�ڰ���)'";
    
    [SerializeField] private TMP_Text loadingText_Content_1;
    private string ContentText_1 = "�ڽ��� ���� �ٰ� �ִٴ� ���� �����ϰ� �ִ� ���¿��� ���� �ٴ� ��";

    [SerializeField] private TMP_Text loadingText_Content_2;
    private string ContentText_2 = "����� �ٲٰų� ���ϴ� �ι��� ��ȯ�ϰ� �ʴɷ��� ����ϴ� ���� �����ϴٰ� �Ѵ�.";

    [SerializeField] private TMP_Text loadingText_Content_3;
    private string ContentText_3 = "���� �ڰ����� ��Ʈ�� ���� ���� ��ü���̰� ���������� �Ǹ��� �ٰ� �ȴٰ� �Ѵ�.";

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        audioSource.Play();

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

        audioSource.Stop();
    }
}
