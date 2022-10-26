using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    bool isGameStart = false;
    public bool GameStart { get { return isGameStart; } }

    bool isGameOver = false;
    public bool GameOver { get { return isGameOver; } }

    int gems = 0;
    GameObject gemsUI;
    GameObject gameOverUI;

    private void Awake()
    {
        gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gemsUI = GameObject.Find("GemsUI");
        gameOverUI = GameObject.Find("GameOverUI");
    }

    public void AddGems(int amount)
    {
        gems += amount;
        gemsUI.GetComponentInChildren<Text>().text = "x" + gems.ToString();
    }

    public void GameBegin()
    {
        isGameStart = true;
    }

    public void GameEnd()
    {
        GetComponent<AudioSource>().Stop();
        isGameOver = true;
        gameOverUI.transform.GetChild(0).gameObject.SetActive(true);

        DataManager.dm.SaveGem(PlayerPrefs.GetInt("GEMS") + gems);
        gameOverUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = gems.ToString();
        gameOverUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("GEMS").ToString();

        Destroy(DataManager.dm.gameObject);
        
    }

    public void ReStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
