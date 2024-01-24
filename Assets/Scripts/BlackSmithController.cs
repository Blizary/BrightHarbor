using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithController : MonoBehaviour
{
    [SerializeField] private GameObject blackSmithTalkingUIOBJ;
    [SerializeField] private GameObject blackSmithUIOBJ;
    [SerializeField] private List<string> conversations;
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private BoxCollider2D triggerBoxCollider;
    [SerializeField] private int requiredLightLvl;
    [SerializeField] private bool isActive;
    [SerializeField] private GameEventListener_Integer onClearShadows;
    [SerializeField] private PlayerPermenantInfo playerInfo;


    private void Awake()
    {
        onClearShadows.Response.AddListener(OnClearShadows);
        if(playerInfo.harborlightLvl>=requiredLightLvl)
        {
            isActive = true;
            triggerBoxCollider.enabled = true;
        }else
        {
            isActive = false;
            triggerBoxCollider.enabled = false;
        }
        
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
        if(!isActive)
        {
            if(_lvl>=requiredLightLvl)
            {
                isActive = true;
                triggerBoxCollider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (!playerInRange && isActive)
            {
                blackSmithTalkingUIOBJ.SetActive(true);
                blackSmithTalkingUIOBJ.GetComponent<TalkingUiController>().NewText(conversations[0]);
                blackSmithUIOBJ.SetActive(true);
            }
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (playerInRange)
            {
                blackSmithTalkingUIOBJ.SetActive(false);
                blackSmithUIOBJ.SetActive(false);
            }
            playerInRange = false;

        }
    }
}
