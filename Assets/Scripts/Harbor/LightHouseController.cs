using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseController : MonoBehaviour
{
    [SerializeField] private GameObject lightHouseUIOBJ;
    [SerializeField] private bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (!playerInRange)
            {
                lightHouseUIOBJ.SetActive(true);
            }
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (playerInRange)
            {
                lightHouseUIOBJ.SetActive(false);
            }
            playerInRange = false;

        }
    }
}
