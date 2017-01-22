using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    Animator anim;
    bool StartExit;
    float dampTime;
    float dampProgress;
    public Transform[] masks;
    bool canQuit;

	// Use this for initialization
	void Start () {
        anim =GetComponent<Animator>();
        anim=GetComponent<Animator>();
        anim = GetComponent<Animator>();
        dampTime = 0;
        dampProgress = 0;
        StartExit = true;
        canQuit = true;
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
            {
                canQuit = false;
                MissionStart();
            }
            else
            if (canQuit == true)
            {
                Application.Quit();
            }
        }
	}

    void MissionStart()
    {
        GameObject.Find("Player").GetComponent<PlayerCollisions>().TurnOnMask();
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().shouldFollow = true;
        // Invoke("CameraSizeDamp", 0.05f);
        StartCoroutine(CameraSizeDamp());
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;

        Destroy(gameObject, 5f);
    }

    IEnumerator CameraSizeDamp()
    {
        while (dampProgress < 1)
        {
            dampProgress += 0.05f;
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = (9.2f - (5f * dampProgress));
            masks[0].localScale = new Vector3((3.3f - (.8f * dampProgress)), 1f, (1.85f - (0.45f * dampProgress)));
            masks[1].localScale = new Vector3((2f - (.55f * dampProgress)), (1.8f - (.45f * dampProgress)), 1);
            masks[2].localScale = new Vector3((2f - (.55f * dampProgress)), (1.8f - (.45f * dampProgress)), 1);
            masks[3].localScale = new Vector3((2f - (.55f * dampProgress)), (1.8f - (.45f * dampProgress)), 1);
            
            yield return null;
        }
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        yield return null;
    }


}
