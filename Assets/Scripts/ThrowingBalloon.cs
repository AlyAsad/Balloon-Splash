using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBalloon : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject balloonPrefab;
    public float ballonForce = 20f;
    Vector3 dragStartPos;
    Touch touch;

    [SerializeField] float power = 10f;
    [SerializeField] float maxDrag = 5f;
    [SerializeField] float recoilPercent = 0.2f;

    [SerializeField] LineRenderer lr;
    [SerializeField] Rigidbody2D PlayerRigidBody;
    [SerializeField] float maxForce = 10f;

    // Update is called once per frame
    private void Update()
    {
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
    }
    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    void DragRelease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        clampedForce = Vector3.ClampMagnitude(clampedForce, maxForce);

        GameObject balloon = Instantiate(balloonPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();

        Vector3 playerClampedForce;
        playerClampedForce.x = -1 * clampedForce.x * recoilPercent;
        playerClampedForce.y = -1 * clampedForce.y * recoilPercent;
        playerClampedForce.z = -1 * clampedForce.z * recoilPercent;

        PlayerRigidBody.AddForce(playerClampedForce, ForceMode2D.Impulse);
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }




}
