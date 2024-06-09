using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            gameObject.SetActive(false);
            AudioManager.instance.PlayCoinSound();
            GameSession.instance.AddPoint();
            Destroy(gameObject);
        }
    }
}
