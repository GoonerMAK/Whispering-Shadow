using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private int villagersKilled;
    [SerializeField] private int enemiesKilled;

    private void OnEnable()
    {
        // Subscribe to the NPC death event when the script is enabled
        NPC_Damage.onNpcDeath += IncrementKillCount;
    }

    private void OnDisable()
    {
        // Unsubscribe from the NPC death event when the script is disabled
        NPC_Damage.onNpcDeath -= IncrementKillCount;
    }


    // Start is called before the first frame update
    void Start()
    {
        villagersKilled = 0;
        enemiesKilled = 0;
    }

    public void IncrementKillCount(string npcTag)
    {
        if (npcTag == "Villager")
        {
            villagersKilled++;
        }
        else if (npcTag == "Enemy")
        {
            enemiesKilled++;
        }

        Debug.Log("Villagers killed: " + villagersKilled);
        Debug.Log("Enemies killed: " + enemiesKilled);
    }

    public bool IsPlayerEvil()
    {
        if(villagersKilled > enemiesKilled)
        {
            return true;
        }

        else 
        {
            return false;
        }
    }

    public int DamageMultiplier()
    {
        return villagersKilled - enemiesKilled;
    }

}
