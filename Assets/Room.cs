using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    Animator anim;
    bool StartExit;

	// Use this for initialization
	void Start () {
        anim.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))
        {
            anim.SetBool("StartExit", true);
            StartExit = true;
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow))||(Input.GetKeyDown(KeyCode.S)))
        {
            anim.SetBool("StartExit", false);
            StartExit = false;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (StartExit == true)
                MissionStart();
            else
                Application.Quit();
        }
	}

    void MissionStart()
    {

    }
}
