using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterialController : MonoBehaviour
{
    [SerializeField] private MaterialType currentType;
    [SerializeField] private float amount;
    [SerializeField] private List<Sprite> materialSprites;
    [SerializeField] private Image materialVisualOBJ;
    [SerializeField] private TextMeshProUGUI amountOBJ;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIMaterial(MaterialType _newType,float _amount)
    {
        currentType = _newType;
        amount = _amount;
        amountOBJ.text = amount.ToString();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Sprite newSprite = null;
        switch(currentType)
        {
            case MaterialType.LIGHTFRAGMENT:
                newSprite = materialSprites[0];
                break;
            case MaterialType.METAL:
                newSprite = materialSprites[1];
                break;
            case MaterialType.GEM:
                newSprite = materialSprites[2];
                break;
            case MaterialType.WOOD:
                newSprite = materialSprites[3];
                break;
            case MaterialType.STONE:
                newSprite = materialSprites[4];
                break;
        }
       
        materialVisualOBJ.sprite = newSprite;
    }
}


