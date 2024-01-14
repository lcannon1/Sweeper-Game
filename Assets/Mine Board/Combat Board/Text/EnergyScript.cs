using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    public Text energyText;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        energyText.text = "Energy : " + player.gameObject.GetComponent<PlayerStats>().energy + " / " 
            + player.gameObject.GetComponent<PlayerStats>().energyMax;
    }
}
