using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ether : PathedEnemy
{
    AudioSource movingSound;
    void Awake()
    {
        movingSound = GetComponent<AudioSource>();
    }
    protected override IEnumerator MoveUP(int n)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 8)
            movingSound.Play();
        RotateTowardTarget(points[n]);
        StartCoroutine(base.MoveUP(n));
        yield return null;


    }
    protected override IEnumerator MoveStart()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 8)
            movingSound.Play();
        RotateTowardTarget(startingPoint);
        StartCoroutine(base.MoveStart());
        yield return null;
    }

}

