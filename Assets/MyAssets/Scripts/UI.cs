using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UI : MonoBehaviour
{
    private Spawner _Spawner;
    private ResourceManager _ResourceManager;

    private Transform BPause, PauseMenu, MenuLVL, DefeatPanel, WaveClearPanel, VictoryPanel, SpawnEnemy, Gold, KillEnemy, Wave;
    private Text SpawnEnemyText, KillEnemyText, WaveText, GoldText;

    private Button[] ButtonLVL;
    //private Text[] DeathPanelText;

    public string Lvl_status { get { return lvl_status; } }
    private string lvl_status;

    public bool isPaused { get { return _IsPaused; } }
    private bool _IsPaused = false;

    public AudioSource Source;
    public AudioClip click;


    // Start is called before the first frame update
    void Start()
    {
        Events.Victory?.AddListener(Victory);
        Events.WaveComplited?.AddListener(WaveClear);
        Events.Defeat?.AddListener(Defeat);

        foreach (Transform name in transform.GetComponentsInChildren<Transform>())
        {
            if (name.name == "BPause") BPause = name;
            if (name.name == "PauseMenu") PauseMenu = name;
            if (name.name == "MenuLVL") MenuLVL = name;
            if (name.name == "WaveClear") WaveClearPanel = name;
            if (name.name == "Victory") VictoryPanel = name;
            //if (name.name == "SpawnEnemy") SpawnEnemy = name;
            if (name.name == "Gold") Gold = name;
            //if (name.name == "KillEnemy") KillEnemy = name;
            if (name.name == "Hearth") KillEnemy = name; 
            if (name.name == "Wave") Wave = name;
            if (name.name == "DefeatPanel") DefeatPanel = name;

        }
        _ResourceManager = FindObjectOfType<ResourceManager>();

        _Spawner = GameObject.Find("SpawnerEnemy").GetComponent<Spawner>();
        //DeathPanelText = DeathPanel.GetComponentsInChildren<Text>();
        ButtonLVL = MenuLVL.GetComponentsInChildren<Button>();

        GoldText = Gold.GetComponentInChildren<Text>();
        KillEnemyText = KillEnemy.GetComponentInChildren<Text>();
        WaveText = Wave.GetComponentInChildren<Text>();

        lvl_status = "Medium";
        lvl_status = PlayerPrefs.GetString("lvl_status", lvl_status);
        ColorButton(lvl_status);

        WaveClearPanel.gameObject.SetActive(false);
        VictoryPanel.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        MenuLVL.gameObject.SetActive(false);
        DefeatPanel.gameObject.SetActive(false);
        InvokeRepeating("UIStats", 0, 0.25f);
    }

    private void UIStats()
    {
        //SpawnEnemyText.text = "LeftSpawn : " + _Spawner.LeftEnemy.ToString();
        KillEnemyText.text = "Hearth : " + _ResourceManager.Hearth.ToString();
        WaveText.text = "Wave : " + _Spawner.Wave.ToString();
        GoldText.text = "Gold : " + _ResourceManager.Gold.ToString();
    }


    public void Victory()
    {
        StartCoroutine(VictoryON());
    }

    public void WaveClear()
    {
        StartCoroutine(WaveClearON());
    }

    public IEnumerator VictoryON()
    {
        VictoryPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        VictoryPanel.gameObject.SetActive(false);
    }

    public IEnumerator WaveClearON()
    {
        WaveClearPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        WaveClearPanel.gameObject.SetActive(false);
    }


    public void PlaySound()
    {
        Source.PlayOneShot(click);
    }

    public void ClickPause()
    {
        //SpawnEnemy.gameObject.SetActive(false);
        KillEnemy.gameObject.SetActive(false);
        Wave.gameObject.SetActive(false);
        BPause.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        _IsPaused = true;
    }

    public void ClickContinue()
    {
        BPause.gameObject.SetActive(true);
        //SpawnEnemy.gameObject.SetActive(true);
        KillEnemy.gameObject.SetActive(true);
        Wave.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        _IsPaused = false;
    }
    public void ClickLevel()
    {
        MenuLVL.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
    }
    public void ClickLevelEasy()
    {
        lvl_status = "Easy";
        ColorButton(lvl_status);
    }
    public void ClickLevelMedium()
    {
        lvl_status = "Medium";
        ColorButton(lvl_status);
    }

    public void ClickLevelHard()
    {
        lvl_status = "Hard";
        ColorButton(lvl_status);
    }

    public void ClickCloseLevel()
    {
        MenuLVL.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        PlayerPrefs.SetString("lvl_status", lvl_status);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpeedUp()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 2;
        else if (Time.timeScale == 2)
            Time.timeScale = 4;
        else if (Time.timeScale == 4)
            Time.timeScale = 8;
        else if (Time.timeScale == 8)
            Time.timeScale = 1;
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        DefeatPanel.gameObject.SetActive(true);
    }

    private void ColorButton(string status)
    {
        for (int i = 0; i < ButtonLVL.Length; i++)
            ButtonLVL[i].GetComponent<Graphic>().color = Color.gray;
        switch (status)
        {
            case "Easy":
                ButtonLVL[0].GetComponent<Graphic>().color = Color.green;
                break;
            case "Medium":
                ButtonLVL[1].GetComponent<Graphic>().color = Color.yellow;
                break;
            case "Hard":
                ButtonLVL[2].GetComponent<Graphic>().color = Color.red;
                break;
        }
    }

}
