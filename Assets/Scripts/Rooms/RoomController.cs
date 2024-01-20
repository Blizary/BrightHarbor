using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private int currentShadowLvl;
    [SerializeField] private GameObject roomCenter;
    [SerializeField] private GameObject enemyHost;
    [SerializeField] private ShadowLvlCamController shadowLvlCamController;

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
        shadowLvlCamController.UpdateCamShadowLvl(currentShadowLvl);


    }
}
