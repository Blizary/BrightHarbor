using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image lightAmountVisuals;
    [SerializeField] private Image healthAmountVisuals;

    [SerializeField] private GameObject materialUIPrefab;
    [SerializeField] private MaterialBarController materialIngameHost;
    [SerializeField] private MaterialBarController materialInDeathHost;

    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;

    [SerializeField] private GameEventListener_Bool onExitMenuShow;
    [SerializeField] private GameEventListener onPlayerDeath;
    [SerializeField] private GameEventListener_Bool onPauseMenuShow;
    [SerializeField] private GameEvent_Bool onPauseMenuShowActive;
    [SerializeField] private GameEventListener onUpdateToHeldMaterials;

    private DataManager dataManager;

    private DungeonManager dungeonManager;
    private void Awake()
    {
        dataManager = GameObject.FindObjectOfType<DataManager>();
        onExitMenuShow.Response.AddListener(OnExitMenuShow);
        onPauseMenuShow.Response.AddListener(OnPauseMenuShow);
        onPlayerDeath.Response.AddListener(OnPlayerDeath);
        onUpdateToHeldMaterials.Response.AddListener(OnUpdateToHeldMaterials);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnExitMenuShow(bool _state)
    {
        if (_state)
        {
            exitMenu.SetActive(true);

        }
        else
        {
            exitMenu.SetActive(false);

        }
    }

    private void OnPauseMenuShow(bool _state)
    {
        if (_state)
        {
            pauseMenu.SetActive(true);

        }
        else
        {
            pauseMenu.SetActive(false);

        }
    }



    public void NextDungeonButton()
    {
        dungeonManager = GameObject.FindFirstObjectByType<DungeonManager>().GetComponent<DungeonManager>();
        dungeonManager.NextDungeonScene();
    }

    public void GoToHarborButton()
    {
        dungeonManager = GameObject.FindFirstObjectByType<DungeonManager>().GetComponent<DungeonManager>();
        dungeonManager.ResetLvl();
        SceneManager.LoadScene("Harbor");
    }

    public void DeathGoToHarborButton()
    {
        dataManager.ClearAll();
        SceneManager.LoadScene("Harbor");
    }

    public void UpdateLightBar(float _amount)
    {
        lightAmountVisuals.fillAmount = _amount;
    }

    public void UpdateHealthBar(float _amount)
    {
        healthAmountVisuals.fillAmount = _amount;
    }

    public void PauseMenuContinueButton()
    {
        onPauseMenuShowActive.Raise(false);
    }

    public void PauseMenuMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseMenuQuitButton()
    {
        Application.Quit();
    }

    public void OnPlayerDeath()
    {
        deathMenu.SetActive(true);
    }

    void OnUpdateToHeldMaterials()
    {
        materialIngameHost.UpdateVisuals();
        //materialInDeathHost.UpdateVisuals();
    }



}
