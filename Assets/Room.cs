using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    Animator anim;
    bool StartExit;
    float dampTime;
    float dampProgress;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        dampTime = 0;
        dampProgress = 0;
        StartExit = true;
	}
	
	// Update is called once per frame
	void Update () {
        if ((StartExit == false) && (Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W))) 
        {
            anim.SetBool("StartExit", true);
            StartExit = true;
        }
        if ((StartExit == true) && (Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S)))
        {
            anim.SetBool("StartExit", false);
            StartExit = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || (Input.GetKeyDown(KeyCode.Space)))
        {
            Debug.Log("bla");
            if (StartExit == true)
                MissionStart();
            else
                Application.Quit();
        }
	}

    void MissionStart()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().shouldFollow = true;
        // Invoke("CameraSizeDamp", 0.05f);
        StartCoroutine(CameraSizeDamp());
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator CameraSizeDamp()
    {
        while (dampProgress < 1)
        {
            dampProgress += 0.05f;
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = (9.2f - (5f * dampProgress));
            yield return null;
        }
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        yield return null;
    }


}
