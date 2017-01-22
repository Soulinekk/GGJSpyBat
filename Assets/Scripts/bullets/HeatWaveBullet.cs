using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWaveBullet : MBullet {

    public override void Reset()
    {
        //transform.localPosition = startingPos;
        Destroy(gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Deactive());
    }
    void OnDisable()
    {
        Reset();
    }
}
