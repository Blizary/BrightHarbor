using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    
    [SerializeField] private List<string> easyDungeons;
    [SerializeField] private List<string> mediumDungeons;
    [SerializeField] private List<string> hardDungeons;
    [SerializeField] private List<string> bossDungeons;

    private List<string> ogEasyDungeons;
    private List<string> ogMediumDungeons;
    private List<string> ogHardDungeons;
    private List<string> ogBossDungeons;

    [SerializeField] public int currentDificulty = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        foreach(string s in easyDungeons)
        {
            ogEasyDungeons.Add(s);
        }
        foreach (string s in mediumDungeons)
        {
            ogMediumDungeons.Add(s);
        }

        foreach (string s in hardDungeons)
        {
            ogHardDungeons.Add(s);
        }

        foreach (string s in bossDungeons)
        {
            ogBossDungeons.Add(s);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        DungeonManager otherDM = GameObject.FindFirstObjectByType<DungeonManager>();
        if (otherDM != null)
        {
            if(otherDM!=this)
            {
                if (otherDM.currentDificulty > currentDificulty)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(otherDM.gameObject);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDungeonScene()
    {
        currentDificulty++;
        string nextScene = "";
        switch(currentDificulty)
        {
            case 1:
                nextScene = PickDungeonName(easyDungeons);
                break;
            case 2:
                nextScene = PickDungeonName(easyDungeons);
                break;
            case 3:
                nextScene = PickDungeonName(mediumDungeons);
                break;
            case 4:
                nextScene = PickDungeonName(mediumDungeons);
                break;
            case 5:
                nextScene = PickDungeonName(hardDungeons);
                break;
            case 6:
                nextScene = PickDungeonName(mediumDungeons);
                break;
            case 7:
                nextScene = PickDungeonName(hardDungeons);
                break;
            case 8:
                nextScene = PickDungeonName(hardDungeons);
                break;
            case 9:
                nextScene = PickDungeonName(bossDungeons);
                break;
            case 10:
   
                //end dungeon
                break;
        }

        SceneManager.LoadScene(nextScene);

    }

    private string PickDungeonName(List<string> _lvls)
    {
        int randomNewScene = Random.Range(0, _lvls.Count);
        string nextSceneName = _lvls[randomNewScene];
        _lvls.Remove(nextSceneName);
        return nextSceneName;
    }

    public void ResetLvl()
    {
        currentDificulty = 0;

        easyDungeons.Clear();
        mediumDungeons.Clear();
        hardDungeons.Clear();
        bossDungeons.Clear();

        foreach (string s in ogEasyDungeons)
        {
            easyDungeons.Add(s);
        }
        foreach (string s in ogMediumDungeons)
        {
            mediumDungeons.Add(s);
        }

        foreach (string s in ogHardDungeons)
        {
            hardDungeons.Add(s);
        }

        foreach (string s in ogBossDungeons)
        {
            bossDungeons.Add(s);
        }
    }
}
