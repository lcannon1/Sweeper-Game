using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Text healthText;
    public EnemyAssignment enemy;

    // Update is called once per frame
    void Update()
    {
        healthText.text = enemy.enemy.gameObject.GetComponent<ThisBehavior>().health + " / "
            + enemy.enemy.gameObject.GetComponent<ThisBehavior>().healthMax;
    }
}
