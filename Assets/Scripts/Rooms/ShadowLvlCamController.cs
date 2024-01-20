using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLvlCamController : MonoBehaviour
{
    [SerializeField] private List<GameObject> shadows;
    [SerializeField] private int shadowlvl;

    [SerializeField] private GameEvent_String onMusicChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCamShadowLvl(int _newLvl)
    {
        shadowlvl = _newLvl;
        
        for(int i=0;i< shadows.Count; i++)
        {
            if(shadowlvl==0)
            {
                if (shadows[i].GetComponent<SpriteRenderer>().enabled)
                {
                    shadows[i].GetComponent<Animator>().SetTrigger("FadeOut");
                }
            }
            else
            {
                if (i <= shadowlvl - 1)
                {
                    if (!shadows[i].GetComponent<SpriteRenderer>().enabled)
                    {
                        shadows[i].GetComponent<Animator>().SetTrigger("FadeIn");
                    }
                }
                else
                {
                    if(shadows[i].GetComponent<SpriteRenderer>().enabled)
                    {
                        shadows[i].GetComponent<Animator>().SetTrigger("FadeOut");
                    }
                    
                }
            }
            
        }

        if(shadowlvl>=2)
        {
            Debug.Log("Darkness MUSIC ON PLS");
            onMusicChange.Raise("Darkness");
        }
        else
        {
            Debug.Log("Exploring MUSIC ON PLS");
            onMusicChange.Raise("Exploring");
        }
    }
}
