
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBarController : MonoBehaviour {

    public static GearBarController Instance;

    private GearBarView m_GearBarView;
	//private GearBarModel m_GearBarModel;
	private GameObject currentHolding = null;   //手持ちの装備
    private GameObject currentActiveModel = null;
    private List<GameObject> slotList = null;
    private Dictionary<GameObject, GameObject> toolBarDic = null;
    //private int currentKeyCode = -1;                

    public GameObject CurrentActiveModel { get { return currentActiveModel; } }

    void Awake() {
        Instance = this;
    }

    void Start () {
		Init();
		CreateSlotAll();
	}
	
	private void Init() {
		m_GearBarView = gameObject.GetComponent<GearBarView>();
		//m_GearBarModel = gameObject.GetComponent<GearBarModel>();

		slotList = new List<GameObject>();
        toolBarDic = new Dictionary<GameObject, GameObject>();
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
        //if (slotList[keyNum].GetComponent<Transform>().Find("InventoryItem") == null)
        //{
          //  return;
        //}

        if (currentHolding != null && currentHolding != slotList[keyNum]){
            currentHolding.GetComponent<GearBarSlotController>().Normal();
            currentHolding = null;
        }

        currentHolding = slotList[keyNum];
        currentHolding.GetComponent<GearBarSlotController>().ButtonClick();

        /*if (currentKeyCode == keyNum && currentActiveModel != null)
        {
           
            currentActiveModel.SetActive(false);
            currentActiveModel = null;
        }
        else
        {
           
            //FindInventoryItem();
        }*/

       
        //currentKeyCode = keyNum;
    }

    /*private void FindInventoryItem()
    {
        Transform m_temp = currentHolding.GetComponent<Transform>().Find("InventoryItem");
        StartCoroutine("CallGunFactory", m_temp);
    }*/

    /*private IEnumerator CallGunFactory(Transform m_temp)
    {
        if (m_temp != null)
        {
           
            if (currentActiveModel != null)
            {
    if(currentActiveModel.tag != "Build" && currentActiveModel.tag != "Hand"){
    currentActiveModel.GetComponent<GunControllerBase>().SetAnimation();
                yield return new WaitForSeconds(1);
    }
                
     if(currentActiveModel.tag == "Hand"){
    currentActiveModel.GetComponent<StoneHatchet>().Holster();
                yield return new WaitForSeconds(1);
    }
                currentActiveModel.SetActive(false);
            }

            
            GameObject temp = null;
            toolBarDic.TryGetValue(m_temp.gameObject, out temp);
            
            if (temp == null)
            {
               
                //temp = GunFactory.Instance.CreateGun(m_temp.GetComponent<Image>().sprite.name, m_temp.gameObject);
                
                toolBarDic.Add(m_temp.gameObject, temp);
            }
            else
            {
                //if (currentHolding.GetComponent<GearBarSlotController>().SelfState)
                {
                    temp.SetActive(true);
                }
            }
            currentActiveModel = temp;
        }
    }*/






}
