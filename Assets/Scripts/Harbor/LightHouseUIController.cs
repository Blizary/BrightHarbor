using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightHouseUIController : MonoBehaviour
{
    [SerializeField] private PlayerPermenantInfo playerdata;
    [SerializeField] private Button clearShadowsButton;
    [SerializeField] private Button increaseLampLightButton;
    [SerializeField] private GameEvent_Integer onClearShadows;
    [SerializeField] private GameEvent_Integer onIncreaseLampLight;
    [SerializeField] private int currentLightLvl;

    private float availableLighShards;

    private void OnEnable()
    {
        availableLighShards = 0;
        CheckAvailableOptions();
        currentLightLvl = playerdata.harborlightLvl;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearShadows()
    {
        currentLightLvl += 1;
        playerdata.harborlightLvl += currentLightLvl;
        onClearShadows.Raise(currentLightLvl);
        
        RemoveLightShards(2);
        CheckAvailableOptions();
    }

    public void IncreaseLampLight()
    {
        RemoveLightShards(4);
        Debug.Log("Imagine you have more light");
    }

    private void RemoveLightShards(int _cost)
    {
        //check what is currently available and turn the correct buttons on
        foreach (MaterialAmount m in playerdata.totalMaterials)
        {
            if (m.materialType == MaterialType.LIGHTFRAGMENT)
            {
                m.amount -= _cost;
            }
        }
    }


    private void CheckAvailableOptions()
    {
        //check what is currently available and turn the correct buttons on
        foreach (MaterialAmount m in playerdata.totalMaterials)
        {
            if (m.materialType == MaterialType.LIGHTFRAGMENT)
            {
                availableLighShards = m.amount;
            }
        }

        clearShadowsButton.interactable = false;
        increaseLampLightButton.interactable = false;

        if (availableLighShards > 2)
        {
            clearShadowsButton.interactable = true;
        }
        if(availableLighShards >4)
        {
            increaseLampLightButton.interactable = true;
        }
        
    }
}
