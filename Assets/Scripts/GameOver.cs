using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public WallSpawner spawner;

    bool isGameOver = false;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GameOverAll()
    {
        isGameOver = true;
        spawner.GameOver();
    }
}
