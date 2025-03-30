using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameTime = 120f; 
    public TextMeshProUGUI timerText; 
    public bool isGameOver = false;

    public GameObject gammeOverUI;
    public GameObject gammeWinUI;
    public Transform targetParent;

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isGameOver)
            return;

        gameTime -= Time.deltaTime;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.RoundToInt(gameTime).ToString();

        if (gameTime <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gammeOverUI.SetActive(true);
    }

    public void GameWin()
    {
        isGameOver = true;
        gammeWinUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckGameIsWin()
    {
        Invoke("CheckIsGameWinOnDelay", 0.25f);
    }

    void CheckIsGameWinOnDelay()
    {
        if (targetParent.childCount <= 0)
        {
            GameWin();
        }
    }
}
