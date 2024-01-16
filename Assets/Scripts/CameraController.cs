using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float panningSpeed;
    [SerializeField] private GameEventListener_Vector3 onEnterRoom;
    [SerializeField] private GameEventListener onEnterConnection;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 lastCenter;
    private Camera cam;
    [SerializeField] private bool isFollowing;


    private void Awake()
    {
        cam = GetComponent<Camera>();
        onEnterRoom.Response.AddListener(OnEnterRoom);
        onEnterConnection.Response.AddListener(OnEnterConnection);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
        {
            FollowPlayer();
        }
    }
    void OnEnterConnection()
    {
        isFollowing = true;
    }

    void OnEnterRoom(Vector3 _pos)
    {
        lastCenter = _pos;
        isFollowing = false;
        _pos.z = offset.z;
        // Smoothly move the camera to the target location
        StartCoroutine(MoveCamera(transform, _pos, panningSpeed));
    }

    // Coroutine to smoothly move the camera
    IEnumerator MoveCamera(Transform cameraTransform, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(cameraTransform.position, targetPosition) > 0.2f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
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

