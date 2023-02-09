using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            UpdateGameOver();
        } else
        {
            UpdateGame();
        }
    }

    void UpdateGame()
    {
        transform.position += Vector3.left * Time.deltaTime * GameManager.instance.GetSpeed();
        if (transform.position.x < -1.4f)
        {
            transform.position += Vector3.right * 1.4f;
        }
    }

    void UpdateGameOver()
    {
        transform.position += Vector3.left * Time.deltaTime * 1;
        if (transform.position.x < -1.4f)
        {
            transform.position += Vector3.right * 1.4f;
        }
    }
}
