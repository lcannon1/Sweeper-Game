using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Text healthText;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health : " + player.gameObject.GetComponent<PlayerStats>().health + " / "
            + player.gameObject.GetComponent<PlayerStats>().healthMax;
    }
}
