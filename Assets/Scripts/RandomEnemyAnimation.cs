using System.Collections;
using UnityEngine;

public class RandomEnemyAnimation : MonoBehaviour
{
    public Animator animator;
    public AnimationClip newAnimationClip;
    public AnimationClip oldAnimationClip;

    public float enemyFormTime = 2.0f;
    public float waitingTimeForEnemyForm = 10.0f;

    private void Start()
    {
        StartCoroutine(CallEnemyFormAppear());
    }

    IEnumerator CallEnemyFormAppear()
    {
        while (true)
        {
            if (!animator.GetBool("isDead"))
            {
                EnemyFormAppear();
                yield return new WaitForSeconds(waitingTimeForEnemyForm);
            }
            else
            {
                yield break; // Exit the coroutine if isDead is true
            }
        }
    }

    void EnemyFormAppear()
    {
        // Set the initial random time
        float randomTime = Random.Range(0f, newAnimationClip.length);

        animator.Play(newAnimationClip.name, -1, randomTime);

        StartCoroutine(ReturnToOriginalAnimation());
    }

    IEnumerator ReturnToOriginalAnimation()
    {
        yield return new WaitForSeconds(enemyFormTime);

        animator.Play(oldAnimationClip.name, -1);
    }
}
