using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBalloon : MonoBehaviour
{
    private int waitFrames = 0;
    public Transform throwPoint;
    public GameObject balloonPrefab;
    public GameObject bombPrefab;
    public float ballonForce = 20f;
    Vector3 dragStartPos;
    Vector3 draggingPos;
    Vector3 dragReleasePos;
    Touch touch;

    [SerializeField] float power = 10f;
    [SerializeField] float maxDrag = 5f;
    [SerializeField] float recoilPercent = 0.2f;

    [SerializeField] ParticleSystem muzzleRecoil;

    [SerializeField] LineRenderer lr;
    [SerializeField] Rigidbody2D PlayerRigidBody;
    [SerializeField] float maxForce = 10f;

    [SerializeField] public bool isBomb = false;

    [SerializeField] private float minMagnitudeForBalloon;


    // Update is called once per frame
    private void Update()
    {

        // IF GAME IS PAUSED DONT TAKE INPUT
        if (PauseMenu.gameIsPaused == true)
        {
            waitFrames = 2;
            return;
        }


        if (waitFrames > 0)
        {
            waitFrames--;
            return;
        }


        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }

    }

    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);

        Vector3 direction;
        direction.x = dragStartPos.x - throwPoint.position.x;
        direction.y = dragStartPos.y - throwPoint.position.y;
        direction.z = dragStartPos.z - throwPoint.position.z;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        PlayerRigidBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
    }
    void Dragging()
    {
        draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);


        Vector3 resultantDirection = dragStartPos - draggingPos;


        Vector3 direction;
        direction.x = resultantDirection.x - throwPoint.position.x;
        direction.y = resultantDirection.y - throwPoint.position.y;
        direction.z = resultantDirection.z - throwPoint.position.z;

        float angle = Mathf.Atan2(resultantDirection.y, resultantDirection.x) * Mathf.Rad2Deg - 90;
        PlayerRigidBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
    }
    void DragRelease()
    {
        lr.positionCount = 0;

        dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        clampedForce = Vector3.ClampMagnitude(clampedForce, maxForce);


        // a check so player doesnt make balloon with no speed
        if (force.magnitude < minMagnitudeForBalloon) return;


        GameObject balloon;
        if (!isBomb) balloon = Instantiate(balloonPrefab, throwPoint.position, throwPoint.rotation);
        else balloon = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();

        Vector3 playerClampedForce;
        playerClampedForce.x = -1 * clampedForce.x * recoilPercent;
        playerClampedForce.y = -1 * clampedForce.y * recoilPercent;
        playerClampedForce.z = -1 * clampedForce.z * recoilPercent;


        ParticleSystem muzzleRecoilInstance = Instantiate(muzzleRecoil, throwPoint.position, Quaternion.identity);
        muzzleRecoilInstance.Play();
        Destroy(muzzleRecoilInstance.gameObject, 1f);

        PlayerRigidBody.AddForce(playerClampedForce, ForceMode2D.Impulse);




        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }



    public void setIsBomb()
    {
        isBomb = true;
    }


    public void clearIsBomb()
    {
        isBomb = false;
    }

}
