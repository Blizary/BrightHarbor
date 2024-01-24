using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<MaterialAmount> heldMaterials = new List<MaterialAmount>();
    [SerializeField] private GameEvent onUpdateToHeldMaterials;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<MaterialAmount> GetCurrentMat()
    {
        return heldMaterials;
    }


    public void AddMaterials(MaterialAmount _materialAmount)
    {
        bool wasInList = false;
        foreach (MaterialAmount m in heldMaterials)
        {
            if(m.materialType == _materialAmount.materialType)
            {
                m.amount += _materialAmount.amount;
                wasInList = true;
            }
        }
        if(!wasInList)
        {
            heldMaterials.Add(_materialAmount);
        }

        //update visuals
        onUpdateToHeldMaterials.Raise();
    }

    public void ClearAll()
    {
        heldMaterials.Clear();
    }
}
