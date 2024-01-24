using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBarController : MonoBehaviour
{
    [SerializeField] private GameObject materialUIPrefab;

    private DataManager dataManager;

    private void Awake()
    {
        dataManager = GameObject.FindFirstObjectByType<DataManager>();
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVisuals()
    {
        if (dataManager != null)
        {
            if (dataManager.GetCurrentMat().Count > 0)
            {
                //clean previous
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                //generate new

                List<MaterialAmount> list = new List<MaterialAmount>();
                list = dataManager.GetCurrentMat();
                foreach (MaterialAmount mat in list)
                {
                    GameObject newMaterial = Instantiate(materialUIPrefab, transform);
                    newMaterial.GetComponent<UIMaterialController>().SetUIMaterial(mat.materialType, mat.amount);
                }
            }
        }
    }
}
