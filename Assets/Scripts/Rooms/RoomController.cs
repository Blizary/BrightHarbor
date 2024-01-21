using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : EnviromentController
{
    [SerializeField] private int maxShadowLvl;
    [SerializeField] private GameObject roomCenter;
    [SerializeField] private GameObject enemyHost;
    [SerializeField] private GameObject creepingShadowsHost;
    //[SerializeField] private GameObject lightHost;
    [SerializeField] private GameObject baseLight;
    private ShadowLvlCamController shadowLvlCamController;

    [SerializeField] private bool playerInRoom;

    private void Awake()
    {
        enemyHost.SetActive(false);
        shadowLvlCamController = GameObject.FindFirstObjectByType<ShadowLvlCamController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRoomCenter()
    {
        return roomCenter.transform.position;
    }

    public void OnEnterRoom()
    {
        //Activate enemies
        enemyHost.SetActive(true);
        playerInRoom = true;
        UpdateShadowEffectOnRoom();

    }

    public void LeftRoom()
    {
        playerInRoom = false;
    }

    public override void UpdateShadowEffectOnRoom()
    {
        shadowLvlCamController.UpdateCamShadowLvl(currentShadowlvl);
        if (currentShadowlvl >= maxShadowLvl)
        {
            baseLight.SetActive(false);
        }
    }

    public override int GetCurrentShadowLvl()
    {
        return currentShadowlvl;
    }

    public override void UpdateShadowLvl(int _newShadowlvl)
    {
        currentShadowlvl = _newShadowlvl;
        if(playerInRoom)
        {
            UpdateShadowEffectOnRoom();
        }

    }

    public override void IncreaseShadowlvl()
    {
        currentShadowlvl += 1;
        if (playerInRoom)
        {
            UpdateShadowEffectOnRoom();
        }

        if(creepingShadowsHost!=null)
        {
            foreach (Transform t in creepingShadowsHost.transform)
            {
                t.gameObject.SetActive(false);
            }
            creepingShadowsHost.transform.GetChild(currentShadowlvl-1).gameObject.SetActive(true);
        }
        
    }
}
