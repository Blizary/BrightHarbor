using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonShadowSpreading : MonoBehaviour
{
    [SerializeField] private int maxShadowlvl;
    [SerializeField] private List<EnviromentController> roomSequence;
    [SerializeField] private float spreadTimer;

    [SerializeField] private int currentRoom = 0;
    [SerializeField] private float innerSpreadTimer;
    private bool isSpreading = true;
    [SerializeField] private EnviromentController current;

    [SerializeField] protected GameEventListener_Bool onExitMenuShow;

    private bool gamePaused = false;

    private void Awake()
    {
        onExitMenuShow.Response.AddListener(OnExitMenuShow);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpreading && !gamePaused)
        {
            SpreadShadow();
        }
    }

    void SpreadShadow()
    {
        if(innerSpreadTimer<spreadTimer)
        {
            innerSpreadTimer += Time.deltaTime;
        }
        else
        {
            //check if room is at max lvl
            if (currentRoom > roomSequence.Count)
            {
                //dungeon is complete
                isSpreading = false;

            } else
            {
                Debug.Log(currentRoom);
                current = roomSequence[currentRoom];
                //get room current lvl of shadow
                int currentShadowInroom = roomSequence[currentRoom].GetCurrentShadowLvl();
                if (currentShadowInroom >= maxShadowlvl)
                {
                    //room is complete
                    currentRoom += 1;

                }
                else
                {
                    roomSequence[currentRoom].IncreaseShadowlvl();

                }

            }
            innerSpreadTimer = 0;
        }
    }

    protected void OnExitMenuShow(bool _state)
    {
        if (_state)
        {
            gamePaused = true;

        }
        else
        {
            gamePaused = false;
        }
    }


}
