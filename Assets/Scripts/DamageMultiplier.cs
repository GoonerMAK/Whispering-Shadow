using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMultiplier : MonoBehaviour
{
    private int villagersKilled;
    private int enemiesKilled;
    [SerializeField] private int multiplier = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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

        multiplier = enemiesKilled - villagersKilled;
    }

    public int Multiplier()
    {
        return multiplier * 10;
    }


}
