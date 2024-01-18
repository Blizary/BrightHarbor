using FunkyCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

   
    [SerializeField] private Light2D lightBase;
    [SerializeField] private Vector3 lightSizeBySummon;
    [SerializeField] private Animator animator;

 
    [SerializeField] private GameEventListener_Integer onSummonSword;
    [SerializeField] private GameEventListener_Integer onAttackTrigger;

    private int damage;

    private void Awake()
    {
        onAttackTrigger.Response.AddListener(OnAttackTrigger);
        onSummonSword.Response.AddListener(OnSummonSword);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnAttackTrigger(int _dir)
    {
        animator.SetTrigger("Attack");
    }

    void OnSummonSword(int _value)
    {
        if(_value>3)
        {
            _value = 3;
        }
        switch(_value)
        {
            case 1:
                lightBase.size = lightSizeBySummon.x;
                damage = 1;
                break;
            case 2:
                lightBase.size = lightSizeBySummon.y;
                damage = 2;
                break;
            case 3:
                lightBase.size = lightSizeBySummon.z;
                damage = 3;
                break;

        }
        transform.GetChild(0).gameObject.SetActive(true);

    }

    public int GetDamage()
    {
        return damage;
    }
}
