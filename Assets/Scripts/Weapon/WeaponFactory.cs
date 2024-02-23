using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFactory : MonoBehaviour
{

    public static GunFactory Instance;
    private Transform m_Transform;

    private GameObject prefab_AssaultRifle;
    private GameObject prefab_Shotgun;
    private GameObject prefab_WoodenBow;
    private GameObject prefab_WoodenSpear;
    private GameObject prefab_Build;
    private GameObject prefab_StoneHatchet;

    private int index = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        PrefabLoad();
    }

    private void PrefabLoad()
    {
        prefab_AssaultRifle = Resources.Load<GameObject>("Gun/Prefabs/Assault Rifle");
        prefab_Shotgun = Resources.Load<GameObject>("Gun/Prefabs/Shotgun");
        prefab_WoodenBow = Resources.Load<GameObject>("Gun/Prefabs/Wooden Bow");
        prefab_WoodenSpear = Resources.Load<GameObject>("Gun/Prefabs/Wooden Spear");
        prefab_Build = Resources.Load<GameObject>("Gun/Prefabs/Building Plan");
        prefab_StoneHatchet = Resources.Load<GameObject>("Gun/Prefabs/Stone Hatchet");
    }


    public GameObject CreateGun(string gunName, GameObject icon)
    {
        GameObject tempGun = null;
        switch (gunName)
        {
            case "Assault Rifle":
                tempGun = GameObject.Instantiate<GameObject>(prefab_AssaultRifle, m_Transform);
                InitGun(tempGun, 100, 15, WeaponType.AssaultRifle, icon);
                break;
            case "Shotgun":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Shotgun, m_Transform);
                InitGun(tempGun, 100, 20, WeaponType.Shotgun, icon);
                break;
            case "Wooden Bow":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenBow, m_Transform);
                InitGun(tempGun, 100, 25, WeaponType.WoodenBow, icon);
                break;
            case "Wooden Spear":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenSpear, m_Transform);
                InitGun(tempGun, 100, 30, WeaponType.WoodenSpear, icon);
                break;
            case "Building":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Build, m_Transform);
                break;
            case "Stone Hatchet":
                tempGun = GameObject.Instantiate<GameObject>(prefab_StoneHatchet, m_Transform);
                StoneHatchet stoneHatchet = tempGun.GetComponent<StoneHatchet>();
                stoneHatchet.Id = index++;
                stoneHatchet.Damage = 100;
                stoneHatchet.Durable = 30;
                stoneHatchet.WeaponType = WeaponType.StoneHatchet;
                stoneHatchet.ToolBarIcon = icon;
                break;
        }
        return tempGun;
    }

    private void InitGun(GameObject gun, int damage, int durable, WeaponType type, GameObject icon)
    {
        GunControllerBase gcb = gun.GetComponent<GunControllerBase>();
        gcb.Id = index++;
        gcb.Damage = damage;
        gcb.Durable = durable;
        gcb.WeaponType = type;
        gcb.ToolBarIcon = icon;
    }
}
