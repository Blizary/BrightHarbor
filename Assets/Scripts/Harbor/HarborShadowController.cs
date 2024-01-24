using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HarborShadowController : MonoBehaviour
{
    
    [SerializeField] private PlayerPermenantInfo playerInfo;
    [SerializeField] private List<GameObject> shadowsAll;
    [SerializeField] private List<GameObject> shadowsStartHarbor;
    [SerializeField] private List<GameObject> shadowsFarmHarbor;
    [SerializeField] private List<GameObject> shadowsMoreHarbor;

    [SerializeField] private GameEventListener_Integer onClearShadows;

    private void Awake()
    {
        onClearShadows.Response.AddListener(OnClearShadows);
        OnClearShadows(playerInfo.harborlightLvl);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClearShadows(int _lvl)
    {
        Debug.Log("Shadows for lvl " + _lvl);
        switch(_lvl)
        {
            case 0:
                //all shadows on
                break;
            case 1:
                foreach(GameObject g in shadowsAll)
                {
                    g.SetActive(false);
                }
                foreach (GameObject g in shadowsStartHarbor)
                {
                    g.SetActive(true);
                }
                break;
            case 2:
                foreach (GameObject g in shadowsStartHarbor)
                {
                    g.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject g in shadowsFarmHarbor)
                {
                    g.SetActive(false);
                }
                break;
        }
    }
}
