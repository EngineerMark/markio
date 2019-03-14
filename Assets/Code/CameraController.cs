using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float maxLeftPosition;
    public float maxRightPosition;
    public bool isMovable = true;

    private Vector3 cameraPosition;

    private float lowestYpoint = 3.0f;
    private float playerYcheck = 9.0f;
    private float smoothTime = 0.3F;
    private float yVelocity = 0.0F;

    public bool isBossMode = false;
    public Vector2 bossPosition;
    public float bossFov;

    private Vector2 tempPos;
    private float tempFov;
    private float tick = 0;

    void Update()
    {

        if (!isBossMode)
        {
            if (player.transform.position.y > playerYcheck)
                cameraPosition.y = Mathf.SmoothDamp(cameraPosition.y, player.transform.position.y, ref yVelocity, smoothTime);
            else
                cameraPosition.y = Mathf.SmoothDamp(cameraPosition.y, lowestYpoint, ref yVelocity, smoothTime);
            if (transform.position.x >= maxLeftPosition && isMovable)
            {
                cameraPosition.x = player.transform.position.x;
                transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
                if (transform.position.x < maxLeftPosition)
                {
                    transform.position = new Vector3(maxLeftPosition, cameraPosition.y, -10);
                    cameraPosition.x = maxLeftPosition;
                }
            }
        }
        else
        {
            tick += Time.deltaTime;
            tick = Mathf.Clamp(tick, 0, 1);
            transform.position = Vector2.Lerp(tempPos, bossPosition, tick);
            Camera.main.fieldOfView = Mathf.Lerp(tempFov, bossFov, tick);
        }
    }

    public void StartBossCam()
    {
        isBossMode = true;
        tempPos = transform.position;
        tempFov = Camera.main.fieldOfView;
    }
}
