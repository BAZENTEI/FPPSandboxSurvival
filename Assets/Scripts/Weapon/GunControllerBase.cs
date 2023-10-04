using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunControllerBase : WeaponControllerBase {
    override protected void Start(){
        base.Start();
    }

    override protected void MouseButtonDownLeft(){
        base.MouseButtonDownLeft();
        PlayFireEffect();
        PlayFireAnimation();
    }

    protected abstract void PlayFireEffect();
    protected abstract void PlayFireAnimation();

}
