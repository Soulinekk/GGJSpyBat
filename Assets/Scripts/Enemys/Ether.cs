using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ether : PathedEnemy
{

    protected IEnumerator MoveUP(int n)
    {
        Quaternion rotation = Quaternion.LookRotation
             (points[n] - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0f, 0f, rotation.z, rotation.w);

        while (transform.position != points[n])
        {
            transform.position = Vector3.MoveTowards(transform.position, points[n], Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);

        if (n == points.Length - 1)
            StartCoroutine(MoveStart());
        else
            StartCoroutine(MoveUP(n + 1));


    }
}
