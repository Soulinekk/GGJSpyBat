using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luckyShot : MBullet
{
    protected override void OnEnable()
    {
        //random dir
        System.Random r = new System.Random();
        transform.Rotate(0f, 0f, r.Next(-80, 80));

        base.OnEnable();
        StartCoroutine(Deactive());
    }
    protected override IEnumerator Deactive() {
        yield return new WaitForSeconds(5);
        Reset();
    }
    public override void Reset()
    {
        transform.localPosition = startingPos;
        gameObject.SetActive(false);
    }
}
