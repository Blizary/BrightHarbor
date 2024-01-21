using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController :EnviromentController
{

    private void Awake()
    {
        shadowlvlCamController = GameObject.FindFirstObjectByType<ShadowLvlCamController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterConnection()
    {
        shadowlvlCamController.UpdateCamShadowLvl(currentShadowlvl);
    }

 

}
