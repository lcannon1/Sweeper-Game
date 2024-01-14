using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    public Sprite skillSprite;
    public Sprite costSprite;
    private int cost;

    public PlayerStats player;

    // Start is called before the first frame update
    public void activate()
    {
        if (player.energy >= cost)
        {
            player.useEnergy(cost);
            // Skill Effect
        }
    }
}
