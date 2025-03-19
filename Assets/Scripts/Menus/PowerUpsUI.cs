using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsUI : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite fastBulletsSprite;
    [SerializeField] private Sprite fireBulletsSprite;
    [SerializeField] private Sprite explosionBulletsSprite;


    public void changePowerUP(int code)
    {
        if (code == -1 || code == 0)
        {
            targetImage.sprite = noneSprite;
        }
        else
        {
            if (code == 1)
            {
                targetImage.sprite = fastBulletsSprite;
            }
            else if (code == 2)
            {
                targetImage.sprite = fireBulletsSprite;
            }
            else if (code == 3)
            {
                targetImage.sprite = explosionBulletsSprite;
            }
        }
    }



}
