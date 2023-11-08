using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSplatterScreen : MonoBehaviour{
    private Image m_Image;
    private byte alpha = 0;

    void Start(){
        m_Image = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// alphaの値.
    /// </summary>
    public void SetImageAlpha(){
        alpha += 10;
        Color32 color = new Color32(255, 255, 255, alpha);
        m_Image.color = color;
        Debug.Log("BloodSplatterScreen alpha:" + m_Image.color.a);

    }

}
