using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float startingTime = 60f;

    public TextMeshProUGUI countdownText;

    public KillCounter killCounter;

    private bool evil = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;

            evil = killCounter.IsPlayerEvil();

            if(evil)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

            else
            {
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                Debug.Log("Loading scene with index: " + nextSceneIndex);
                SceneManager.LoadScene(nextSceneIndex);
            }
        }    
    }


}
