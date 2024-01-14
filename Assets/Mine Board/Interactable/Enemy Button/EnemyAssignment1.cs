using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssignment1 : MonoBehaviour
{
    public GameHandler combatList;
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        enemy = combatList.combatList[0];
    }
}
