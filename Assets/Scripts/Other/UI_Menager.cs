using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Menager : MonoBehaviour {
    public Text scoreText;

    void LateUpdate()
    {
        scoreText.text = Convert.ToString(PlayerData.Points, 2);// "Data Collected: "+ Convert.ToString(PlayerData.Points, 2)+" bytes";
    }
}
