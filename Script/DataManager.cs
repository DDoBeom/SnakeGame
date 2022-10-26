using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

class HeroStat
{
    public int level = 1;
    public int hp = 50;
    public int atk;
}

[System.Serializable]

class HeroStatList
{
    public List<HeroStat> heroStats = new List<HeroStat>();
}




public class DataManager : MonoBehaviour
{
    public static DataManager dm;

    HeroStatList hsList = new HeroStatList();

    [SerializeField]
    int gems = 0;
    public int GEM { get { return gems; } }
    int currentHero = 0;
    public int CURRENTHERO { get { return currentHero; } }

    AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        dm = this;
        Load();
        DontDestroyOnLoad(gameObject);
        clip = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("GEMS"))
        {
            gems = PlayerPrefs.GetInt("GEMS");
        }
        else
        {
            SaveGem(0);
        }

        LobbyManager.lm.SetGemUI(gems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStartHero(int id)
    {
        currentHero = id;
        LobbyManager.lm.SetStarUI(hsList.heroStats[id].level);
    }

    public void SetData()
    {
        HeroStat[] hs = new HeroStat[9];

        for(int i = 0; i < 9; i++)
        {
            hs[i] = new HeroStat();
        }

        hs[0].atk = 5;
        hs[1].atk = 7;
        hs[2].atk = 2;
        hs[3].atk = 4;
        hs[4].atk = 4;
        hs[5].atk = 5;
        hs[6].atk = 3;
        hs[7].atk = 3;
        hs[8].atk = 4;

        for (int i = 0; i < 9; i++)
        {
            hsList.heroStats.Add(hs[i]);
        }

        string json = JsonUtility.ToJson(hsList, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Saves/HeroStatList.json", json);
    }

    public void Load()
    {
        if(!System.IO.File.Exists(Application.dataPath + "/Saves/HeroStatList.json"))
        {
            SetData();
        }
        else
        {
            string json = System.IO.File.ReadAllText(Application.dataPath + "/Saves/HeroStatList.json");
            hsList = JsonUtility.FromJson<HeroStatList>(json);
        }
    }

    public void UpgradeHero()
    {
        int level = hsList.heroStats[currentHero].level;

        if (level == 1)
        {
            if (gems > 500)
            {
                hsList.heroStats[currentHero].level = 2;
                hsList.heroStats[currentHero].atk += 3;
                hsList.heroStats[currentHero].hp += 20;
                gems -= 500;
                clip.Play();
            }
            {
                //젬이 부족합니다
            }
        }
        else if (level == 2)
        {
            if (gems > 1009)
            {
                hsList.heroStats[currentHero].level = 3;
                hsList.heroStats[currentHero].atk += 5;
                hsList.heroStats[currentHero].hp += 30;
                gems -= 1000;
                clip.Play();
            }
            {
                //젬이 부족합니다
            }
        }
        else
        {
            //풀 업그레이드 입니다
        }

        SaveGem(gems);
        LobbyManager.lm.SetGemUI(gems);
        LobbyManager.lm.SetStarUI(hsList.heroStats[currentHero].level);

        string json = JsonUtility.ToJson(hsList, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Saves/HeroStatList.json", json);
    }

    public void SaveGem(int _gem)
    {
        PlayerPrefs.SetInt("GEMS", _gem);
    }
}
