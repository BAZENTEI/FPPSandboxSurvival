using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int hitPoint = 1000;  //体力ポイント
    [SerializeField] private float stamina = 100;  //スタミナ
    [SerializeField] private float staminaRestoreRate = 8.0f;
    private int index = 0;
    private FirstPersonController m_FirstPersonController;
    private PlayerStatusPanel m_PlayerStatusPanel;
    private BloodSplatterScreen m_BloodSplatterScreen;

    public int HitPoint { get { return hitPoint; } set { hitPoint = value; } }
    public float Stamina { 
        get { return stamina; } 
        set { stamina = value; if (stamina > 100.0f) stamina = 100.0f; } }

    

    void Start(){
        StartCoroutine("RestoreVIT");
        m_FirstPersonController = GetComponent<FirstPersonController>();
        m_PlayerStatusPanel = GameObject.Find("Canvas/GearBarPanel/PlayerStatus").GetComponent<PlayerStatusPanel>();
        m_BloodSplatterScreen = GameObject.Find("Canvas/BloodSplatter").GetComponent<BloodSplatterScreen>();


    }

    void Update(){
        //Debug.Log("player state:" + m_FirstPersonController.M_PlayerState);
        CutVit();
        //Debug.Log("スタミナ:" + this.Stamina + ",HP:" + this.hitPoint);
    }


    public void CutHP(int HPValue){
        this.HitPoint -= HPValue;
        //HPバー
        m_PlayerStatusPanel.SetHpBar(this.hitPoint);
        //BloodSplatterエフェクト
        m_BloodSplatterScreen.SetImageAlpha();

    }


    public void CutVit(){
        if (m_FirstPersonController.M_PlayerState == PlayerState.WALK){
            index++;
            if (index >= 20){
                this.Stamina -= 0.2f;
                ResetSpeed();
                index = 0;
            }
        }

        if (m_FirstPersonController.M_PlayerState == PlayerState.RUN){
            index++;
            if (index >= 20){
                this.Stamina -= 1;
                ResetSpeed();
                index = 0;
            }
        }

        m_PlayerStatusPanel.SetStaminaBar(stamina);
    }


    private IEnumerator RestoreVIT(){
        Vector3 tempPos;
        while (true){
            tempPos = transform.position;
            yield return new WaitForSeconds(0.5f);
            if (this.Stamina <= 92 && tempPos.Equals(transform.position)){
                this.Stamina += staminaRestoreRate;
                ResetSpeed();
            }

            m_PlayerStatusPanel.SetStaminaBar(stamina);
        }

        

    }

    private void ResetSpeed(){
        m_FirstPersonController.M_WalkSpeed = 5 * (this.Stamina * 0.01f);
        m_FirstPersonController.M_RunSpeed = 10 * (this.Stamina * 0.01f);

    }

    void OnCollisionEnter(Collision coll){
        if (coll.collider.tag == "RockMaterial"){
            //アイテム数プラス1
            InventoryPanelController.Instance.ForAllSlot(coll.gameObject.GetComponent<RockMaterialController>().RockMaterialName);

            GameObject.Destroy(coll.gameObject);
        }
    }

}
