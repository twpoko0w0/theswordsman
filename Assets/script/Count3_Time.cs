using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count3_Time : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime;
    public GameObject gameOverText, restartButton, menuButton, bloodEffect;

    public GameObject PlayTimeOut;

    [SerializeField] Text countdownText;

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

        if (currentTime <= 0)
        {
            currentTime = 0;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            //Destroy(PlayTimeOut);
            PlayTimeOut.SetActive(false);
        }
    }
}
