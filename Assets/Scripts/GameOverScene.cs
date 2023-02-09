using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    float delay = 0;

    public Text scoreText;
    public Text highScoreText;

    public GameObject newRecordText;

    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.PlayIntro();

        newRecordText.gameObject.SetActive(false);
        int score = GameManager.instance.score;
        scoreText.text = score.ToString("N0").Replace(',', '.');
        highScoreText.text = GameManager.instance.GetHiScore().ToString("N0").Replace(',', '.');

        GameManager.instance.UpdateHiScore();
        GameManager.instance.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;

        if (Input.anyKeyDown && delay > 1)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
