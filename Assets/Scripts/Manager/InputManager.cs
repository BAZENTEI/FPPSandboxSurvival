using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InputManager : MonoBehaviour {

    private bool inventoryState = false;
    private FirstPersonController m_FirstPersonController;
    //private GameObject m_GunStar;

    void Start(){
        InventoryPanelController.Instance.UIPanelHide();
        FindInit();
    }

    void Update(){
        InventoryPanelKey();
        if (inventoryState == false)
            GearBarPanelKey();
    }

    private void FindInit(){
        m_FirstPersonController = GameObject.Find("FPPController").GetComponent<FirstPersonController>();
        //m_GunStar = GameObject.Find("Canvas/Crosshair");
    }

    private void InventoryPanelKey(){
        if (Input.GetKeyDown(GameConst.InventoryPanelKey)){
            if (inventoryState){
                inventoryState = false;
                InventoryPanelController.Instance.UIPanelHide();
                m_FirstPersonController.enabled = true;
                //m_GunStar.SetActive(true);
                if (GearBarController.Instance.CurrentActiveModel != null)
                    GearBarController.Instance.CurrentActiveModel.SetActive(true);
            }else{
                inventoryState = true;
                InventoryPanelController.Instance.UIPanelShow();
                m_FirstPersonController.enabled = false;
                //m_GunStar.SetActive(false);

                if (GearBarController.Instance.CurrentActiveModel != null)
                    GearBarController.Instance.CurrentActiveModel.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
               
            }
        }
    }

  
    private void GearBarPanelKey(){
        GearBarKey(GameConst.GearBarPanelKey1, 0);
        GearBarKey(GameConst.GearBarPanelKey2, 1);
        GearBarKey(GameConst.GearBarPanelKey3, 2);
        GearBarKey(GameConst.GearBarPanelKey4, 3);
        GearBarKey(GameConst.GearBarPanelKey5, 4);
        GearBarKey(GameConst.GearBarPanelKey6, 5);
        GearBarKey(GameConst.GearBarPanelKey7, 6);
        GearBarKey(GameConst.GearBarPanelKey8, 7);
        GearBarKey(GameConst.GearBarPanelKey9, 8);
    }

    
    private void GearBarKey(KeyCode keyCode, int keyNum){
        if (Input.GetKeyDown(keyCode)){
            GearBarController.Instance.SaveActiveSlotByKey(keyNum);
        }
    }
}
