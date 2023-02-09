using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    public Text scoreText;
    public Text multiplierText;
    public List<Slot> slots;
    public GameObject playerModel;
    public List<GameObject> players = new List<GameObject>();
    public GameOver gameOver;

    public bool isGameOver = false;
    float scoreInterval = 1;
    int score = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.PlayGame();
        GameManager.instance.startGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        

        var playersToRemove = new List<GameObject>();
        int multiplier = 0;

        isGameOver = true;
        foreach (var player in players)
        {
            var playerComponent = player.GetComponent<Player>();
            if (playerComponent.status != PlayerStatus.died)
            {
                isGameOver = false;
            }
            if (playerComponent.status != PlayerStatus.died &&
                playerComponent.status != PlayerStatus.waiting)
            {
                multiplier++;
            }
            if (player.transform.localPosition.x < -20)
            {
                player.GetComponent<Player>().Die();
                playersToRemove.Add(player);
            }
        }

        timer += Time.deltaTime;
        if (timer >= scoreInterval)
        {
            timer -= scoreInterval;
            
            scoreInterval = 1f / multiplier;
            multiplierText.text = "X" + multiplier.ToString();
            score++;
            scoreText.text = score.ToString("N0").Replace(',', '.');
            GameManager.instance.score = score;
        }

        foreach (var player in playersToRemove)
        {
            players.Remove(player);
            Destroy(player.gameObject);
        }
        playersToRemove.Clear();

        if (players.Count == 0 || isGameOver)
        {
            isGameOver = true;
            gameOver.GameOverAll();
        }
    }

    public void SpawnPlayer()
    {
        int layer = 100;
        int slotIndex = 0;
        foreach (var slot in slots)
        {
            if (!slot.occupied)
            {
                slot.occupied = true;
                var playerGO = Instantiate(playerModel, transform);
                playerGO.GetComponent<Animator>().runtimeAnimatorController = slot.animator;
                playerGO.GetComponent<SpriteRenderer>().sortingOrder = layer;
                var player = playerGO.GetComponent<Player>();
                player.jumpSound.pitch = GetJumpPitch(slotIndex);
                player.slot = slot.gameObject;
                players.Add(playerGO);
                player.key = slot.key;
                return;
            }
            layer--;
            slotIndex++;
        }
    }

    float GetJumpPitch(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
                return 1f;
            case 1:
                return 1.15f;
            case 2:
                return 0.9f;
            case 3:
                return 1.2f;
            case 4:
                return 0.8f;
            case 5:
                return 1.25f;
            case 6:
                return 1.2f;
            case 7:
                return 0.7f;
            default:
                return 1;
        }
    }
}
