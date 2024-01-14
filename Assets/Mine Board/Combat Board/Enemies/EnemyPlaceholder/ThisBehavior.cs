using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisBehavior : MonoBehaviour
{
    public PlayerStats player;
    public GameHandler combatList;
    public TurnManager startTurn;
    public int index;

    private bool begin = true;
    private int move;
    public int health;
    public int healthMax;
    public int damage;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        healthMax = 10;
        damage = 5;
        intent();
    }

    void Update()
    {
        if (startTurn.turn && begin)
        {
            begin = false;
            turn();
            startTurn.turn = false;
            begin = true;
        }
    }

    public void turn()
    {
        switch (move)
        {
            case 1:
                move1();
                break;

            default:
                baseAttack();
                break;
        }

        intent();
    }

    private void intent()
    {
        move = random.Next(0, 1);
    }

    private void baseAttack()
    {
        player.damagePlayer(damage);
    }

    public void damageEnemy(int damageAmount)
    {
        health = Mathf.Max(0, health - damageAmount);

        if (health == 0)
        {
            combatList.enemyDeath(index);
        }
    }

    private void move1()
    {
        print("special move");
    }
}
