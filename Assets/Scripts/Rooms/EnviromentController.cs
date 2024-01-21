using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    [SerializeField] protected int currentShadowlvl;
    [SerializeField] protected ShadowLvlCamController shadowlvlCamController;

    public virtual void UpdateShadowEffectOnRoom()
    {
        shadowlvlCamController.UpdateCamShadowLvl(currentShadowlvl);
    }

    public virtual int GetCurrentShadowLvl()
    {
        return currentShadowlvl;
    }

    public virtual void UpdateShadowLvl(int _newShadowlvl)
    {
        currentShadowlvl = _newShadowlvl;
        //UpdateShadowEffectOnRoom();
    }

    public virtual void IncreaseShadowlvl()
    {
        currentShadowlvl += 1;
        //UpdateShadowEffectOnRoom();
    }
}
