using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ether : PathedEnemy
{

    protected IEnumerator MoveUP(int n)
    {
        RotateTowardTarget(points[n]);
        base.MoveUP(n);
        yield return null;


    }
    protected IEnumerator MoveStart()
    {

        RotateTowardTarget(startingPoint);
        base.MoveStart();
        yield return null;
    }

}

