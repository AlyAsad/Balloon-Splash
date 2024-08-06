using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : MonoBehaviour
{
    [SerializeField] private float power = 10f;
    [SerializeField] private float maxDrag = 5f;
    [SerializeField] private float recoilPercent = 0.2f;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject enemyBalloonPrefab;
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float throwingCone;
    [SerializeField] private float maxForce = 10f;
    [SerializeField] ParticleSystem muzzleRecoil;

    [SerializeField] public GameObject PlaceToThrow1;
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
            yield return new WaitForSeconds(Random.Range(1.5f, 2.5f)); // Random interval between throws
            ThrowBalloon();
        }
    }

    private void ThrowBalloon()
    {
        if (player == null) return;


        int choose = Random.Range(1, 3);
        Vector3 throwDirection = (PlaceToThrow1.transform.position - throwPoint.position).normalized;


        // Calculate the force to throw the balloon from the enemy towards the player

        throwDirection.x += Random.Range(-throwingCone, throwingCone);

        Vector3 force = throwDirection * maxDrag * power;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxForce);



        //
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg + 90;
        enemyRb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);


        // Instantiate and throw the balloon
        GameObject balloon = Instantiate(enemyBalloonPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();


        StartCoroutine(HandleMuzzleRecoil());


        rb.AddForce(clampedForce, ForceMode2D.Impulse);

        // Apply recoil to the enemy
        Vector3 recoilForce = -clampedForce * recoilPercent;
        enemyRb.AddForce(recoilForce, ForceMode2D.Impulse);



        // Destroy the balloon after a certain time
        Destroy(balloon, 5f); // Adjust time as needed
    }

    IEnumerator HandleMuzzleRecoil()
    {
        muzzleRecoil.gameObject.SetActive(true);
        muzzleRecoil.Play();
        yield return new WaitForSeconds(1f);
        muzzleRecoil.Stop();
        muzzleRecoil.gameObject.SetActive(false);
    }
}
