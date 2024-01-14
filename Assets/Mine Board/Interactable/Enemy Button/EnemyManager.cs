using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameHandler combatList;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // Update is called once per frame
    void Update()
    {
        switch (combatList.combatList.Count)
        {
            case 1:
                button1.SetActive(true);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);

                button1.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[0];
                break;

            case 2:
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(false);
                button4.SetActive(false);

                button1.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[0];
                button2.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[1];
                break;

            case 3:
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(false);

                button1.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[0];
                button2.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[1];
                button3.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[2];
                break;

            case 4:
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);

                button1.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[0];
                button2.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[1];
                button3.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[2];
                button4.gameObject.GetComponent<EnemyAssignment>().enemy = combatList.combatList[3];
                break;

            default:
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                break;
        }
    }
}
