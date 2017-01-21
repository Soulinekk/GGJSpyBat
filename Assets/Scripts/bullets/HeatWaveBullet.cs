using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWaveBullet : MBullet {

    public override void Reset()
    {
        transform.localPosition = startingPos;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    void OnDisable()
    {
        Reset();
    }
}
