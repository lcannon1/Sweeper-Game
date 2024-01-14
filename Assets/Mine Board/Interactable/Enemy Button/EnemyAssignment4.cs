using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssignment4 : MonoBehaviour
{
    public GameHandler combatList;
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        enemy = combatList.combatList[3];
    }
}
