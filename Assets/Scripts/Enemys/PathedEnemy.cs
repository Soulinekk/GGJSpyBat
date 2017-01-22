using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathedEnemy : Enemy {

    public float speed;
    public float waitTime = 2f;
    public float delayTime=0f;
    public Vector3 startingPoint = new Vector3(0, 0, 0);
    public Vector3[] points = new[] { new Vector3(0f, 0f, 0f) };
    // Use this for initialization
    new protected virtual void Start()
    {
        base.Start();
        transform.position = startingPoint;
        Invoke("Begin", delayTime);
    }
    void Begin()
    {
        StartCoroutine(MoveUP(0));
    }
    // Update is called once per frame
    protected virtual IEnumerator MoveUP(int n)
    {
        

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

    protected virtual IEnumerator MoveStart()
    {
       

        while (transform.position != startingPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPoint, Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(MoveUP(0));
    }

    protected void RotateTowardTarget(Vector3 target)
    {
        Quaternion rotation = Quaternion.LookRotation
             (target - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0f, 0f, rotation.z, rotation.w);
    }
}

