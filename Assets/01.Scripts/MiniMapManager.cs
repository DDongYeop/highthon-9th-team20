using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    [Header("ButtonImageSource")]
    [SerializeField] private Button[] countryButton;
    [SerializeField] private Sprite[] countryButtonDefalutImage;
    [SerializeField] private Sprite[] countryButtonHorrorImage;

    [Header("CountryCheck")]
    private int countryCheckCount = 0;
    private int[] countryCheckList = new int[7];

    [Header("State")]
    private bool isLuciddream = true;
    private bool isNightmare = false;
    private bool next = false;

    public void CountryCheck()
    {
        countryCheckCount++;
    }

    public void ImageChange(int countryNumber)
    {
        countryCheckList[countryNumber] = 1;
    }

    void IsNightmare()
    {
        for(int i = 0;i< countryButton.Length;i++)
        {
            countryButton[i].image.sprite = countryButtonHorrorImage[i];
        }

        next = true;
    }

    void IsLuciddream()
    {
        for(int i = 0; i < countryCheckList.Length; i++)
        {
            if (countryCheckList[i] >= 1)
            {
                countryButton[i].image.sprite = countryButtonDefalutImage[i];
                countryButton[i].image.color = new Color(1f,1f,1f,1f);
            }
        }
        
    }

    private void Update()
    {
        if(countryCheckCount >= 7)
        {
            isNightmare = true;
            isLuciddream = false;
        }

        if (isNightmare)
        {
            IsNightmare();
        }
        else
        {
            IsLuciddream();
        }

        if (next)
        {

        }
    }


}
