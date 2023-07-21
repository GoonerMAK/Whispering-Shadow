using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyDMG : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currentHealth;

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

    }

    void Die()
    {
        Debug.Log("I died");

        animator.SetBool("IsDead", true);

        this.enabled = false;
    }



    void DisableGameObject()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

}
