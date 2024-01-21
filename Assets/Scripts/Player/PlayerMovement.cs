using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject parent;
    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    void Update()
    {
        Vector3 newPos = parent.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
