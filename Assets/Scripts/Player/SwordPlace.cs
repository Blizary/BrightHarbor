using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPlace : MonoBehaviour
{
    [SerializeField] private List<Transform> places;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate rotation based on input
        if (horizontalInput > 0)
        {
            // Player is moving right
            SetSwordPositionAndRotation(places[0].position, 270 );
        }
        else if (horizontalInput < 0)
        {
            // Player is moving left
            SetSwordPositionAndRotation(places[1].position, 90);
        }
        else if (verticalInput > 0)
        {
            // Player is moving up
            SetSwordPositionAndRotation(places[2].position, 0);
        }
        else if (verticalInput < 0)
        {
            // Player is moving down
            SetSwordPositionAndRotation(places[3].position, 180);
        }



        void SetSwordPositionAndRotation(Vector3 _offsetDirection,float _angle)
        {
            // Set position based on input direction
            transform.position = _offsetDirection;

            if(transform.eulerAngles.z!=_angle)
            {
                // Calculate rotation based on input direction
                Quaternion targetRotation = Quaternion.AngleAxis(_angle, Vector3.forward);

                // Smoothly interpolate to the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 90 * Time.deltaTime);
            }
            
        }
    }
}
