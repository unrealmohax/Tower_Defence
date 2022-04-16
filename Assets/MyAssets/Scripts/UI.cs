using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UI : MonoBehaviour
{
    private Spawner _Spawner;
    private Transform BPause, PauseMenu, MenuLVL, DeathPanel, WaveClearPanel, VictoryPanel, SpawnEnemy, KillEnemy, Wave;
    private Text SpawnEnemyText, KillEnemyText, WaveText;

    private Button[] ButtonLVL;
    private Text[] DeathPanelText;

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

        foreach (Transform name in transform.GetComponentsInChildren<Transform>())
        {
            if (name.name == "BPause") BPause = name;
            if (name.name == "PauseMenu") PauseMenu = name;
            if (name.name == "MenuLVL") MenuLVL = name;
            if (name.name == "WaveClear") WaveClearPanel = name;
            if (name.name == "Victory") VictoryPanel = name;
            if (name.name == "SpawnEnemy") SpawnEnemy = name;
            if (name.name == "KillEnemy") KillEnemy = name;
            if (name.name == "Wave") Wave = name;
            //if (name.name == "DeathPanel") DeathPanel = name;

        }
        _Spawner = GameObject.Find("SpawnerEnemy").GetComponent<Spawner>();
        //DeathPanelText = DeathPanel.GetComponentsInChildren<Text>();
        ButtonLVL = MenuLVL.GetComponentsInChildren<Button>();

        SpawnEnemyText = SpawnEnemy.GetComponentInChildren<Text>();
        KillEnemyText = KillEnemy.GetComponentInChildren<Text>();
        WaveText = Wave.GetComponentInChildren<Text>();

        lvl_status = "Medium";
        lvl_status = PlayerPrefs.GetString("lvl_status", lvl_status);
        ColorButton(lvl_status);

        WaveClearPanel.gameObject.SetActive(false);
        VictoryPanel.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        MenuLVL.gameObject.SetActive(false);
        // DeathPanel.gameObject.SetActive(false);
        InvokeRepeating("UIStats", 0, 0.25f);
    }

    private void UIStats()
    {
        SpawnEnemyText.text = "LeftSpawn : " + _Spawner.LeftEnemy.ToString();
        KillEnemyText.text = "Kill : " + _Spawner.EnemyDie.ToString();
        WaveText.text = "Wave : " + _Spawner.Wave.ToString();
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
        SpawnEnemy.gameObject.SetActive(false);
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
        SpawnEnemy.gameObject.SetActive(true);
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

    public void ClickQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void DieStats(float distance)
    {
        for (int i = 0; i < DeathPanelText.Length; i++)
        {

        }
        DeathPanel.gameObject.SetActive(true);
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
