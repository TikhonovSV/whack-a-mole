using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawner;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject winMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private TMP_Text textHP;
    [SerializeField]
    private TMP_Text textTimer;
    [SerializeField]
    private TMP_Text textScore;

    private int score = 0;
    private int targetScore = 100;

    private int time = 60;
    private int HP = 100;

    public static GameManager instance;


    void Start()
    {
        instance = this;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartHealthPlaymode()
    {
        startMenu.SetActive(false);
        spawner.SetActive(true);
        textHP.gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(HealthPlaymode());
    }

    IEnumerator HealthPlaymode()
    {
        yield return new WaitForEndOfFrame();
        if (HP <= 0)
        {
            textHP.text = "0";
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
        StartCoroutine(HealthPlaymode());
    }

    public void StartTimerPlaymode()
    {
        startMenu.SetActive(false);
        spawner.SetActive(true);
        textTimer.gameObject.transform.parent.gameObject.SetActive(true);
        Debug.Log(textTimer.gameObject.name);
        Debug.Log(textTimer.gameObject.gameObject.name);
     
        StartCoroutine(TimerPlaymode());
    }

    IEnumerator TimerPlaymode()
    {
        while (true)
        {
            if (time <= 0)
            {
                GameOver();
            }
            textTimer.text = time.ToString();
            yield return new WaitForSeconds(1);
            time -= 1;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        textScore.text = score.ToString();
        if (score >= targetScore)
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
        }
    }

    public void TakeDamage(int value)
    {
        HP -= value;
        textHP.text = HP.ToString();
    }
}
