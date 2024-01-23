using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image lightAmountVisuals;
    [SerializeField] private Image healthAmountVisuals;

    

    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameEventListener_Bool onExitMenuShow;
    [SerializeField] private GameEventListener_Bool onPauseMenuShow;
    [SerializeField] private GameEvent_Bool onPauseMenuShowActive;

    private DungeonManager dungeonManager;
    private void Awake()
    {
        onExitMenuShow.Response.AddListener(OnExitMenuShow);
        onPauseMenuShow.Response.AddListener(OnPauseMenuShow);
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



}
