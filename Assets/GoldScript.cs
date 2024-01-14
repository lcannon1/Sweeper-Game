using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    public Text goldText;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        goldText.text = player.gameObject.GetComponent<PlayerStats>().gold +  " Gold";
    }
}
