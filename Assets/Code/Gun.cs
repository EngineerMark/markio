using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public AudioClip shotSound;

    private bool startTimer = false;
    private float timer;
    private float cooldown = 0.2f;
    private GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        SoundManager.AddAudio(shotSound);
    }

    public void Shoot(int direction)
    {
        if (!startTimer)
        {
            GameObject _new = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Bullet _bullet = _new.GetComponent<Bullet>();
            _bullet.direction = direction;
            _bullet.collide = true;
            gamemanager.player.GetComponent<Player>().sfx.PlayOneShot(SoundManager.GetAudio("shoot"), 1.0F);
            startTimer = true;
        }
    }

    private void Update()
    {
        if (startTimer)
            timer += Time.deltaTime;

        if (timer > cooldown)
        {
            startTimer = false;
            timer = 0;
        }
    }
}
