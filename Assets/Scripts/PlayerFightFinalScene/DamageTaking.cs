using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class DamageTaking : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currentHealth;

    public AudioClip dieSound;
    public AudioSource dieAudio;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        healthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        dieAudio.PlayOneShot(dieSound);
    }

    void Die()
    {
        Debug.Log("I died");

        animator.SetBool("IsDead", true);

        Movement movementScript = GetComponent<Movement>();
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        this.enabled = false;
    }



    void DisableGameObject()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

}
