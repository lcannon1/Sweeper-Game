using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int energy;
    public int moves;
    public int healthMax;
    public int energyMax;
    public int movesMax;
    public int damage;
    public int mineDamage;
    public int gold;


    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        energy = 0;
        moves = 5;
        healthMax = 50;
        energyMax = 10;
        movesMax = 5;
        damage = 1;
        mineDamage = 5;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move()
    {
        moves = Mathf.Max(0, moves - 1);
    }

    public void damagePlayer(int damageAmount)
    {
        health = Mathf.Max(0, health - damageAmount);
        
        /*if (health == 0)
        {
            print("Death :(");
        }*/
    }

    public void healPlayer(int healAmount)
    {
        health = Mathf.Min(healthMax, health + healAmount);
    }

    public void gainGold(int goldAmount)
    {
        gold += goldAmount;
    }

    public void gainDamage(int goldAmount)
    {
        if (gold >= goldAmount)
        {
            gold -= goldAmount;
            damage += 1;
        }
    }

    public void gainHealth(int goldAmount)
    {
        if (gold >= goldAmount)
        {
            gold -= goldAmount;
            healthMax += 10;
            health += 10;
        }
    }

    public void gainMoves(int goldAmount)
    {
        if (gold >= goldAmount)
        {
            gold -= goldAmount;
            movesMax += 1;
        }
    }

    public void gainEnergy(int energyAmount)
    {
        energy = Mathf.Min(energyMax, energy + energyAmount);
    }

    public void useEnergy(int energyAmount)
    {
        energy = Mathf.Max(0, energy - energyAmount);
    }

    public void newTurn()
    {
        moves = movesMax;
    }

    public void drainEnergy()
    {
        energy = 0;
    }

    public void fillEnergy()
    {
        energy = Mathf.Max(energy, energyMax);
    }

    public void reset()
    {
        Start();
    }
}
