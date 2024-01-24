using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarborCameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float panningSpeed;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 lastCenter;
    private Camera cam;
    [SerializeField] private bool isFollowing;

    private void Awake()
    {
        cam = GetComponent<Camera>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
    }


    void FollowPlayer()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = player.transform.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, camMoveSpeed * Time.deltaTime);
        transform.position = smoothedPosition;


    }
}
