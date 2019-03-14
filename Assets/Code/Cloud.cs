using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{

    public Vector3 currentPosition;
    public float flyVelocity;

    private GameManager gamemanager;

    void Start()
    {
        flyVelocity = Random.Range(0.01f, 0.1f);
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        currentPosition = transform.position;
    }

    void Update()
    {
        currentPosition.x = currentPosition.x - flyVelocity;
        if (currentPosition.x <= gamemanager.cloudBorderPoints.x)
            currentPosition.x = gamemanager.cloudBorderPoints.y;
        transform.position = currentPosition;
    }
}
