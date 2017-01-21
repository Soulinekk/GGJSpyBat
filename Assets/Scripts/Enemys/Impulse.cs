using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : PathedEnemy {
   
    protected  override void  Start()
    {
        
        base.Start();
        waitTime = 0f;
    }
    protected override IEnumerator MoveUP(int n)
    {
        while (transform.position != points[n])
        {
            transform.position = Vector3.MoveTowards(transform.position, points[n], Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);

        if (n == points.Length - 1)
        {
            transform.position = startingPoint;
            StartCoroutine(MoveUP(0));
        }
        else
            StartCoroutine(MoveUP(n + 1));

    }
}
