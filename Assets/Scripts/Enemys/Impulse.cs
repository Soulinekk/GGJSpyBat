using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : PathedEnemy {

    AudioSource impulsSound;
    protected  override void  Start()
    {
        impulsSound = GetComponent<AudioSource>();
        base.Start();
        InvokeRepeating("PlaySound", 0, 3);
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

    void PlaySound()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4)
            impulsSound.Play();
    }
}
