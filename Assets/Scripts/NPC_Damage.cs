using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Damage : MonoBehaviour
{
    [SerializeField] public float lives;
    [SerializeField] public Animator animator;

    public AudioClip dieSound;
    public AudioSource dieAudio;
    public float waitForDeath = 1.0f;
    public HealthBar healthBar;

    // Defining a delegate for the NPC death event
    public delegate void NPCDeathEventHandler(string npcTag);

    // Defining the event itself
    public static event NPCDeathEventHandler onNpcDeath;



    // Start is called before the first frame update
    void Start()
    {
        lives = 4;
        healthBar.SetMaxHealth((int)lives);
        dieAudio = Camera.main.GetComponent<AudioSource>();
    }
    public void TakeDamage(float damage)
    {
        lives--;

        healthBar.SetHealth((int) lives);

        if (lives <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("NPC died");

        animator.SetBool("isDead", true);
        dieAudio.PlayOneShot(dieSound);
       

        // Raising the NPC death event with the NPC's tag
        onNpcDeath?.Invoke(gameObject.tag);

        gameObject.tag = "Untagged";

        // Destroy the health bar object when the NPC dies
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        StartCoroutine(SetNPCInactive());
    }

    IEnumerator SetNPCInactive()
    {
        yield return new WaitForSeconds(waitForDeath);
        gameObject.SetActive(false);
    }

}
