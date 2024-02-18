
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBarController : MonoBehaviour {

    public static GearBarController Instance;

    private GearBarView m_GearBarView;
//	private GearBarModel m_GearBarModel;
	private GameObject currentHolding = null;   //手持ちの装備
    private List<GameObject> slotList = null;

    void Awake() {
        Instance = this;
    }

    void Start () {
		Init();
		CreateSlotAll();
	}
	
	private void Init() {
		m_GearBarView = gameObject.GetComponent<GearBarView>();
//		m_GearBarModel = gameObject.GetComponent<GearBarModel>();

		slotList = new List<GameObject>();
	}

	//スロット生成
	private void CreateSlotAll() {
        for (int i = 0; i < 9; i++){
			GameObject slot = GameObject.Instantiate<GameObject>(m_GearBarView.Prefab_GearBarSlot, m_GearBarView.Grid_Transform);
			//名づける
			slot.GetComponent<GearBarSlotController>().Init(m_GearBarView.Prefab_GearBarSlot.name + i, i + 1);
			slotList.Add(slot);
		}
    }

	//アクティブなスロットの切り替え
	private void SwitchActiveSlot(GameObject activeSlot){
        if (currentHolding != null && currentHolding != activeSlot) {
			currentHolding.GetComponent<GearBarSlotController>().Normal();

			currentHolding = null;
		}

		currentHolding = activeSlot;

    }

    public void SaveActiveSlotByKey(int keyNum){
        if (currentHolding != null && currentHolding != slotList[keyNum]){
            currentHolding.GetComponent<GearBarSlotController>().Normal();
            currentHolding = null;
        }
        currentHolding = slotList[keyNum];
        currentHolding.GetComponent<GearBarSlotController>().ButtonClick();
    }
}
