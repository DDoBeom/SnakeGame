using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager lm;
    GameObject starUI;
    GameObject gemUI;
    private void Awake()
    {
        lm = this;
        starUI = GameObject.Find("StarUI");
        gemUI = GameObject.Find("GemUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetGemUI(int gem)
    {
        gemUI.GetComponentInChildren<Text>().text = gem.ToString();
    }

    public void SetStarUI(int level)
    {
        Image[] img = starUI.GetComponentsInChildren<Image>();

        if(level == 1)
        {
            img[0].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
            img[1].sprite = Resources.Load("star_gray", typeof(Sprite)) as Sprite;
            img[2].sprite = Resources.Load("star_gray", typeof(Sprite)) as Sprite;
        }
        else if (level == 2)
        {
            img[0].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
            img[1].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
            img[2].sprite = Resources.Load("star_gray", typeof(Sprite)) as Sprite;
        }
        else
        {
            img[0].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
            img[1].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
            img[2].sprite = Resources.Load("star", typeof(Sprite)) as Sprite;
        }
    }
}
