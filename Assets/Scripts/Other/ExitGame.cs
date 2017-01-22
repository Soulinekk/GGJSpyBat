using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

    bool pause = false;
    public GameObject noLine1;
    public GameObject yesLine;
    public GameObject exitGui;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                pause = true;
                Time.timeScale = 0;
                exitGui.SetActive(true);
                noLine1.SetActive(true);
            }
            else
            {
                TurnOffGui();
            }
        }

        if (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (noLine1 == true)
                {
                    noLine1.SetActive(false);
                    yesLine.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.A))
            {
                if (yesLine == true) 
                {
                    yesLine.SetActive(false);
                    noLine1.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Return)))
            {
                if (yesLine.activeSelf)
                {
                    Debug.Log("quit");
                    Application.Quit();
                }
                else
                {
                    TurnOffGui();
                }
            }
        }
	}

    void TurnOffGui()
    {
        pause = false;
        noLine1.SetActive(false);
        yesLine.SetActive(false);
        exitGui.SetActive(false);
        Time.timeScale = 1;
    }
}
