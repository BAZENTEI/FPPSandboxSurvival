using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    private int hitPoint = 1000;  //体力ポイント
    private int stamina = 100;  //スタミナ
    private int index = 0;

    public int HitPoint { get { return hitPoint; } set { hitPoint = value; } }
    public int Stamina { get { return stamina; } set { stamina = value; } }

    private FirstPersonController m_FirstPersonController;

    void Start(){
        StartCoroutine("RestoreVIT");
        m_FirstPersonController = GetComponent<FirstPersonController>();

    }


    void Update(){
        Debug.Log("player state:" + m_FirstPersonController.M_PlayerState);
        CutVit();
        Debug.Log("スタミナ:" + this.Stamina);
    }


    public void CutHP(int HPValue){
        this.HitPoint -= HPValue;
    }

    
    public void CutVit(){
        if (m_FirstPersonController.M_PlayerState == PlayerState.WALK){
            index++;
            if (index >= 20){
                this.Stamina -= 1;
                ResetSpeed();
                index = 0;
            }
        }

        if (m_FirstPersonController.M_PlayerState == PlayerState.RUN){
            index++;
            if (index >= 20){
                this.Stamina -= 2;
                ResetSpeed();
                index = 0;
            }
        }
    }


    private IEnumerator RestoreVIT(){
        Vector3 tempPos;
        while (true){
            tempPos = transform.position;
            yield return new WaitForSeconds(1);
            if (this.Stamina <= 95 && tempPos.Equals(transform.position)){
                this.Stamina += 5;
                ResetSpeed();
            }
        }
    }


    private void ResetSpeed(){
        m_FirstPersonController.M_WalkSpeed = 5 * (this.Stamina * 0.01f);
        m_FirstPersonController.M_RunSpeed = 10 * (this.Stamina * 0.01f);

    }
}
