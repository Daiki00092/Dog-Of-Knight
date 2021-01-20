using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider staminaSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;

        staminaSlider.maxValue = playerManager.maxSp;
        staminaSlider.value = playerManager.maxSp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }


    public void Updatestamina(int sp)
    {
        staminaSlider.DOValue(sp, 0.5f);
    }
}
