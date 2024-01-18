using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPlace : MonoBehaviour
{
    [SerializeField] private List<Transform> places;
    [SerializeField] private List<Transform> attackLocations;


    [SerializeField] private GameEventListener_Integer onAttackTrigger;
    [SerializeField] private GameEventListener onAttackFinnished;
    [SerializeField] private GameEventListener_Integer onSummonSword;

    [SerializeField] private FacingDirection lastFaceDir;
    private bool isAttacking;
    private bool canAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        onAttackTrigger.Response.AddListener(OnAttackTrigger);
        onAttackFinnished.Response.AddListener(OnAttackFinnish);
        onSummonSword.Response.AddListener(OnSummonSword);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(!isAttacking)
        {
            // Calculate rotation based on input
            if (horizontalInput > 0)
            {
                // Player is moving right
                SetSwordPositionAndRotation(places[0].position,0);
            }
            else if (horizontalInput < 0)
            {
                // Player is moving left
                SetSwordPositionAndRotation(places[1].position,0);
            }
            else if (verticalInput > 0)
            {
                // Player is moving up
                SetSwordPositionAndRotation(places[2].position,0);
            }
            else if (verticalInput < 0)
            {
                // Player is moving down
                SetSwordPositionAndRotation(places[3].position,0);
            }
        }
        

    }

    void SetSwordPos(Vector3 _offsetDirection)
    {
        // Set position based on input direction
        transform.position = _offsetDirection;
    }

    void SetSwordPositionAndRotation(Vector3 _offsetDirection, float _angle)
    {
        // Set position based on input direction
        transform.position = _offsetDirection;
        
        if (transform.eulerAngles.z != _angle)
        {
            // Calculate rotation based on input direction
            Quaternion targetRotation = Quaternion.AngleAxis(_angle, Vector3.forward);

            // Smoothly interpolate to the target rotation
            transform.rotation = Quaternion.Euler(0, 0, _angle);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 90 * Time.deltaTime);
        }
        
    }
    void OnAttackTrigger(int _dir)
    {
        if (canAttack)
        {
            switch (_dir)
            {
                case 0://UP
                    lastFaceDir = FacingDirection.UP;
                    SetSwordPositionAndRotation(attackLocations[0].position, 0);
                    break;
                case 1://DOWN
                    lastFaceDir = FacingDirection.DOWN;
                    SetSwordPositionAndRotation(attackLocations[1].position, 180);
                    break;
                case 2://RIGHT
                    lastFaceDir = FacingDirection.RIGHT;
                    SetSwordPositionAndRotation(attackLocations[2].position, 270);
                    break;
                case 3://LEFT
                    lastFaceDir = FacingDirection.LEFT;
                    SetSwordPositionAndRotation(attackLocations[3].position, 90);
                    break;
            }
            isAttacking = true;
        }
    }

    public void OnAttackFinnish()
    {
        Debug.Log("finnish attack trigger : " + transform.eulerAngles.z);
        isAttacking = false;
        switch (lastFaceDir)
        {
            case FacingDirection.UP://UP
                SetSwordPositionAndRotation(places[2].position, 0);
                break;
            case FacingDirection.DOWN://DOWN
                SetSwordPositionAndRotation(places[3].position, 0);
                break;
            case FacingDirection.RIGHT://RIGHT
                SetSwordPositionAndRotation(places[0].position, 0);
                break;
            case FacingDirection.LEFT://LEFT
                SetSwordPositionAndRotation(places[1].position, 0);
                break;
        }
        transform.rotation = Quaternion.identity;
    }

    void OnSummonSword(int _size)
    {
        canAttack = true;
    }


}
