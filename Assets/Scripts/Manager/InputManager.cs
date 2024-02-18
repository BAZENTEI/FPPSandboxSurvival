using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private bool inventoryState = false;

    void Start(){
        InventoryPanelController.Instance.UIPanelHide();
    }

    void Update(){
        InventoryPanelKey();
        ToolBarPanelKey();
    }

    private void InventoryPanelKey(){
        if (Input.GetKeyDown(GameConst.InventoryPanelKey)){
            if (inventoryState){
                inventoryState = false;
                InventoryPanelController.Instance.UIPanelHide();
            }else{
                inventoryState = true;
                InventoryPanelController.Instance.UIPanelShow();
            }
        }
    }

  
    private void ToolBarPanelKey(){
        ToolBarKey(GameConst.ToolBarPanelKey1, 0);
        ToolBarKey(GameConst.ToolBarPanelKey2, 1);
        ToolBarKey(GameConst.ToolBarPanelKey3, 2);
        ToolBarKey(GameConst.ToolBarPanelKey4, 3);
        ToolBarKey(GameConst.ToolBarPanelKey5, 4);
        ToolBarKey(GameConst.ToolBarPanelKey6, 5);
        ToolBarKey(GameConst.ToolBarPanelKey7, 6);
        ToolBarKey(GameConst.ToolBarPanelKey8, 7);
        ToolBarKey(GameConst.ToolBarPanelKey9, 8);
    }

    
    private void ToolBarKey(KeyCode keyCode, int keyNum){
        if (Input.GetKeyDown(keyCode)){
            GearBarController.Instance.SaveActiveSlotByKey(keyNum);
        }
    }
}
