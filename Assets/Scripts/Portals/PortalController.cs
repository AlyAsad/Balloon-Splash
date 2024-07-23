using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Balloon"))
            if (Vector2.Distance(other.transform.position, transform.position) > 0.25f)
                other.transform.position = destination.transform.position;


        if (other.CompareTag("Enemy_Balloon"))
            if (Vector2.Distance(other.transform.position, transform.position) > 0.25f)
                other.transform.position = destination.transform.position;

        if (other.CompareTag("Water_Bomb"))
            if (Vector2.Distance(other.transform.position, transform.position) > 0.3f)
                other.transform.position = destination.transform.position;
    }
}
