using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private PlayerBehaviour player;
    [SerializeField]
    private Sprite[] lives_image;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI gameOver;
    [SerializeField]
    private float flickerTime = 0.5f;
    [SerializeField]
    private TextMeshProUGUI restartText;
    private GameManager gameManager;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        image.sprite = lives_image[lives];
        if (lives < 1)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOver());
        gameManager.GameOver();
    }
    IEnumerator FlickerGameOver()
    {
        while (true)
        {
            gameOver.text = "Game Over";
            yield return new WaitForSeconds(flickerTime);
            gameOver.text = " ";
            yield return new WaitForSeconds(flickerTime);
        }
    }
}
