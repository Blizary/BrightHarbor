using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerPermenantInfo", order = 1)]
public class PlayerPermenantInfo : ScriptableObject
{
    public bool tutorialComplete;
    public int harborlightLvl;
    public List<MaterialAmount> totalMaterials;
    
}
