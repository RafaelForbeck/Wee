using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int score = 0;
    int hiScore = 0;
    float speed = 8;

    float minSpeed = 5;
    float maxSpeed = 10;

    float upIncrease = 0.02f;
    float downIncrease = -0.05f;

    bool up = true;
    public bool startGame;

    string hiKey = "hikey";

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey(hiKey))
        {
            hiScore = PlayerPrefs.GetInt(hiKey);
        }
        else
        {
            PlayerPrefs.SetInt(hiKey, 0);
        }
    }

    private void Start()
    {
        up = true;
    }

    public void Reset()
    {
        speed = 7;

        minSpeed = 5;
        maxSpeed = 10;

        upIncrease = 0.02f;
        downIncrease = -0.05f;

        score = 0;
        startGame = false;
        up = true;
    }

    public void UpdateHiScore()
    {
        if (score > hiScore)
        {
            PlayerPrefs.SetInt(hiKey, score);
            hiScore = score;
        }
    }

    public int GetHiScore()
    {
        return hiScore;
    }

    private void Update()
    {
        if (!startGame)
        {
            return;
        }

        if (up)
        {
            Increase();
        } else
        {
            Decrease();
        }
    }

    void Increase()
    {
        speed += upIncrease * Time.deltaTime;
        if (speed > maxSpeed)
        {
            up = false;
            maxSpeed += 2;
        }
    }

    void Decrease()
    {
        speed += downIncrease * Time.deltaTime;
        if (speed < minSpeed)
        {
            up = true;
            minSpeed -= 0.2f;
            if (minSpeed < 4)
            {
                minSpeed = 4;
            }
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
