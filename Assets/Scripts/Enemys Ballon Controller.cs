using System.Collections;
using UnityEngine;

public class EnemyBalloonController : MonoBehaviour
{
    [SerializeField] private float power = 10f;
    [SerializeField] private float maxDrag = 5f;
    [SerializeField] private float recoilPercent = 0.2f;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject enemyBalloonPrefab; 
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float throwingCone;
    [SerializeField] private Vector3 disp;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(ThrowBalloonAtRandomIntervals());
    }

    private IEnumerator ThrowBalloonAtRandomIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Random interval between throws
            ThrowBalloon();
        }
    }

    private void ThrowBalloon()
    {
        if (player == null) return;

        // Calculate the force to throw the balloon from the enemy towards the player
        Vector3 throwDirection = (player.position - throwPoint.position).normalized;

        throwDirection.x += Random.Range(-throwingCone, throwingCone);

        Vector3 clampedForce = throwDirection * maxDrag * power;


        // Instantiate and throw the balloon
        GameObject balloon = Instantiate(enemyBalloonPrefab, throwPoint.position + disp, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();
        rb.AddForce(clampedForce, ForceMode2D.Impulse);

        // Apply recoil to the enemy
        Vector3 recoilForce = -clampedForce * recoilPercent;
        enemyRb.AddForce(recoilForce, ForceMode2D.Impulse);

        // Destroy the balloon after a certain time
        Destroy(balloon, 5f); // Adjust time as needed
    }
}
