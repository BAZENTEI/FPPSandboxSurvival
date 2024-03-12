
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBarController : MonoBehaviour{
    public static GearBarController Instance;

    private GearBarView m_GearBarView;
    private List<GameObject> slotList = null;
    private GameObject currentHolding = null;   //手持ちの装備
    private GameObject currentActiveModel = null;//キャラクターの参照
    private int currentKeyCode = -1;    //現時点の数字ボタン                

    private Dictionary<GameObject, GameObject> gearBarDic = null;   //取り入れた装備      
    public GameObject CurrentActiveModel { get { return currentActiveModel; } }

    void Awake(){
        Instance = this;
    }

    void Start(){
        Init();
        CreateSlotAll();
    }

    private void Init(){
        m_GearBarView = gameObject.GetComponent<GearBarView>();
        //m_GearBarModel = gameObject.GetComponent<GearBarModel>();

        slotList = new List<GameObject>();
        gearBarDic = new Dictionary<GameObject, GameObject>();
    }

    //スロット生成
    private void CreateSlotAll(){
        for (int i = 0; i < 9; i++) {
            GameObject slot = GameObject.Instantiate<GameObject>(m_GearBarView.Prefab_GearBarSlot, m_GearBarView.Grid_Transform);
            //名づける
            slot.GetComponent<GearBarSlotController>().Init(m_GearBarView.Prefab_GearBarSlot.name + i, i + 1);
            slotList.Add(slot);
        }
    }

    //アクティブなスロットの切り替え
    private void SwitchActiveSlot(GameObject activeSlot){
        if (currentHolding != null && currentHolding != activeSlot){
            currentHolding.GetComponent<GearBarSlotController>().Normal();

            currentHolding = null;
        }

        currentHolding = activeSlot;

    }



    public void SaveActiveSlotByKey(int keyNum){
        //武器がないボタン無効
        if (slotList[keyNum].transform.Find("InventoryItem") == null) return;

        if (currentHolding != null && currentHolding != slotList[keyNum]){
            currentHolding.GetComponent<GearBarSlotController>().Normal();
            currentHolding = null;
        }

        currentHolding = slotList[keyNum];
        currentHolding.GetComponent<GearBarSlotController>().ButtonClick();

        if (currentKeyCode == keyNum && currentActiveModel != null){
            //降ろす
            currentActiveModel.SetActive(false);
            currentActiveModel = null;
        }else{
            //切り替える
            FindInventoryItem();
        }

        currentKeyCode = keyNum;
    }

    private void FindInventoryItem(){
        Transform currentInventoryItem = currentHolding.transform.Find("InventoryItem");
        StartCoroutine("CallGunFactory", currentInventoryItem);
    }

    private IEnumerator CallGunFactory(Transform currentInventoryItem){
        if (currentInventoryItem != null){
            //画面にある->隠す
            if (currentActiveModel != null){
               
                if (currentActiveModel.tag != "Build" && currentActiveModel.tag != "Hand"){
                    currentActiveModel.GetComponent<GunControllerBase>().SetAnimation();
                    yield return new WaitForSeconds(1);
                }

                if (currentActiveModel.tag == "Hand"){
                    currentActiveModel.GetComponent<StoneHatchet>().Holster();
                    yield return new WaitForSeconds(1);
                }
              
              currentActiveModel.SetActive(false);
            }

            //画面にない場合
            GameObject tempWeapon = null;
            gearBarDic.TryGetValue(currentInventoryItem.gameObject, out tempWeapon);

            if (tempWeapon == null){
                //武器を生成
                tempWeapon = GunFactory.Instance.CreateGun(currentInventoryItem.GetComponent<Image>().sprite.name, currentInventoryItem.gameObject);

                gearBarDic.Add(currentInventoryItem.gameObject, tempWeapon);
            }else if (currentHolding.GetComponent<GearBarSlotController>().SelfState){
                //武器を表示
                tempWeapon.SetActive(true);
            }
            currentActiveModel = tempWeapon;


        }
        
    }
}
