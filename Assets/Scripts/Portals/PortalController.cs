using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    private float portalDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Balloon") || other.CompareTag("Enemy_Balloon") || other.CompareTag("Water_Bomb")) {
            if (other.GetComponent<BalloonCollision>().justTeleported) return;

            other.GetComponent<BalloonCollision>().justTeleported = true;
            other.GetComponent<BalloonCollision>().makeTeleportedFalse(portalDelay);
            other.transform.position = destination.transform.position;

        }

    }
}
