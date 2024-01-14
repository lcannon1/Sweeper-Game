using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesScript : MonoBehaviour
{
    public Text movesText;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        movesText.text = "Moves : " + player.gameObject.GetComponent<PlayerStats>().moves + " / " 
            + player.gameObject.GetComponent<PlayerStats>().movesMax;
    }
}
