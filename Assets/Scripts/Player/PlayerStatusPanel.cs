using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusPanel : MonoBehaviour
{
    private Image hpBar;
    private Image staminaBar;


    void Start(){
        hpBar = transform.Find("HP/Bar").GetComponent<Image>();
        staminaBar = transform.Find("Stamina/Bar").GetComponent<Image>();

    }

    public void SetHpBar(int hp){
        hpBar.fillAmount = hp * 0.001f;
    }

    public void SetStaminaBar(int stamina){
        staminaBar.fillAmount = stamina * 0.01f;
    }
}
