using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Floating_Balloon_Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fb1;
    public GameObject fb2;
    public GameObject fb3;
    public GameObject fb4;
    public GameObject fb5;
    public GameObject fb6;

    private Vector3 randomSpawnPosition;
    private int magicNumber;
    private GameObject newBalloon;
    private Rigidbody2D rb2d;
    private int maxTime = 6;
    private int currentTime = 0;
    private float randomSize;
    private float randomForce;
    private float randomOpacity;

    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (maxTime != currentTime)
        {
            currentTime++;
            return;
        }
        currentTime = 0;

        magicNumber = Random.Range(1, 7);
        randomSize = Random.Range(0.5f, 1f);
        randomSpawnPosition = new Vector3(Random.Range(-2.5f, 3f), -7f, 0);
        randomForce = Random.Range(2f, 4f);
        randomOpacity = Random.Range(0.25f, 0.4f);


        if (magicNumber == 1)
        {
            newBalloon = Instantiate(fb1, randomSpawnPosition, Quaternion.identity);
        }
        else if (magicNumber == 2)
        {
            newBalloon = Instantiate(fb2, randomSpawnPosition, Quaternion.identity);

        }
        else if (magicNumber == 3)
        {
            newBalloon = Instantiate(fb3, randomSpawnPosition, Quaternion.identity);

        }
        else if (magicNumber == 4)
        {
            newBalloon = Instantiate(fb4, randomSpawnPosition, Quaternion.identity);

        }
        else if (magicNumber == 5)
        {
            newBalloon = Instantiate(fb5, randomSpawnPosition, Quaternion.identity);

        }
        else if (magicNumber == 6)
        {

            newBalloon = Instantiate(fb6, randomSpawnPosition, Quaternion.identity);
        }

        rb2d = newBalloon.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        newBalloon.transform.localScale = new Vector3(randomSize, randomSize, 0f);
        rb2d.velocity = new Vector2(0, randomForce);


        Renderer renderer = newBalloon.GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = randomOpacity;
        renderer.material.color = color;

        Destroy(newBalloon, 10f);
    }
}
