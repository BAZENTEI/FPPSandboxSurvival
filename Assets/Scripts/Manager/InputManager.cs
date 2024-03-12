using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InputManager : MonoBehaviour {
    public static InputManager Instance; //一人っ子
    private bool inventoryState = false;
    private FirstPersonController m_FirstPersonController;
    private GameObject m_BuildPanel; //建築モジュール

    private bool buildState = false;
    public bool BuildState{
        get { return buildState; }
        set{
            if (m_BuildPanel != null) m_BuildPanel.SetActive(value);
            buildState = value;
            if (buildState == true){
                m_BuildPanel.GetComponent<BuildPanelController>().Reset();
            }else{
                if (m_BuildPanel == null) return;
                m_BuildPanel.GetComponent<BuildPanelController>().DestroyBuildModel();
            }
        }
    }

    void Awake(){
        Instance = this;
    }

    void Start(){
        InventoryPanelController.Instance.UIPanelHide();
        FindInit();
    }

    void Update(){
        InventoryPanelKey();

        if (inventoryState == false) //インベントリ表示中だったら無効する
            GearBarPanelKey();
    }

    private void FindInit(){
        m_FirstPersonController = GameObject.Find("FPPController").GetComponent<FirstPersonController>();
        m_BuildPanel = GameObject.Find("Canvas/BuildPanel");
        m_BuildPanel.SetActive(false);
    }

    private void InventoryPanelKey(){
        if (Input.GetKeyDown(GameConst.InventoryPanelKey)){
            //インベントリが開いている
            if (inventoryState){ 
                inventoryState = false;
                InventoryPanelController.Instance.UIPanelHide();
                m_FirstPersonController.enabled = true;

                if (GearBarController.Instance.CurrentActiveModel != null)
                    GearBarController.Instance.CurrentActiveModel.SetActive(true);

            }else{ //インベントリが閉まっています
                inventoryState = true;
                InventoryPanelController.Instance.UIPanelShow();
                m_FirstPersonController.enabled = false;

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
