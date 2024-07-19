using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBalloon : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject balloonPrefab;
    [SerializeField] float power = 10f;
    [SerializeField] float maxDrag = 5f;
    public float ballonForce = 20f;

    [SerializeField] LineRenderer lr;
    Vector3 dragStartPos;
    Touch touch;

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
    /*
    void Shoot()
    {
        GameObject balloon = Instantiate(balloonPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();

        rb.AddForce(throwPoint.up * ballonForce, ForceMode2D.Impulse);
    }*/

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

        GameObject balloon = Instantiate(balloonPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }




}
