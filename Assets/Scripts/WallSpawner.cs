using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public List<GameObject> models;
    public Players players;

    public List<Transform> positions;

    public float minInterval = 3;
    public float maxInterval = 8;

    float currentTime = 0;
    float nextTime = 3;

    bool gameOver = false;

    public AudioSource preWee;
    public List<AudioClip> prewees;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        minInterval -= 0.02f * Time.deltaTime;
        minInterval = Mathf.Max(1f, minInterval);

        maxInterval -= 0.005f * Time.deltaTime;
        maxInterval = Mathf.Max(maxInterval, 2);

        currentTime += Time.deltaTime;

        if (currentTime > nextTime)
        {
            currentTime = 0;
            nextTime = Random.Range(minInterval, maxInterval);

            if (players.slots.Any(x => !x.occupied))
            {
                if (Random.Range(1, 5) == 1)
                {
                    players.SpawnPlayer();
                    return;
                }
            }
            int max;
            var score = GameManager.instance.score;

            if (score < 25)
            {
                max = 1;
            }
            else if (score < 50)
            {
                max = 2;
            }
            else if (score < 100)
            {
                max = 3;
            } else if (score < 200)
            {
                max = 4;
            }
            else if (score < 400)
            {
                max = 5;
            } else
            {
                max = 6;
            }

            int index = Random.Range(0, max);
            preWee.clip = prewees[Random.Range(0, prewees.Count)];
            preWee.pitch = Random.Range(0.8f, 1.2f);
            preWee.Play();
            Instantiate(models[index], positions[index].position, Quaternion.identity, transform);
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
