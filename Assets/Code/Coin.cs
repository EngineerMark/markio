using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public GameObject player;
    public GameManager gamemanager;

    void Start()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("CharacterPlayer");
    }

    public void GrabCoin()
    {
        gamemanager.interfacemanager.AddScore(100);
        gamemanager.interfacemanager.AddCoin(1);
        gameObject.SetActive(false);
    }
}
