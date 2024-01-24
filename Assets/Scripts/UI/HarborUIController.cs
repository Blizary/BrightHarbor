using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HarborUIController : MonoBehaviour
{


    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameEventListener_Bool onExitMenuShow;

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

    public void StayInHarborButton()
    {
        //just close menu

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

    void OnUpdateToHeldMaterials()
    {
        
    }


}
