using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ether : PathedEnemy
{

    protected override IEnumerator MoveUP(int n)
    {
        RotateTowardTarget(points[n]);
        StartCoroutine(base.MoveUP(n));
        yield return null;


    }
    protected override IEnumerator MoveStart()
    {

        RotateTowardTarget(startingPoint);
        StartCoroutine(base.MoveStart());
        yield return null;
    }

}

