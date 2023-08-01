using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    Vector3 diff;

    public float bulletSpeed = .2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = diff * bulletSpeed;
    }

    private void OnEnable()
    {
        bulletSpeed = Random.Range(0.1f, 0.3f);
        this.transform.LookAt(Player.Instance.transform.position);
        diff = Player.Instance.transform.position - rb.transform.position;
        diff.Normalize();
        diff.y = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "player")
        {
            if(!GameManager.Instance.isGameOver) GameManager.Instance.GameOver();
        }
        else if(collision.gameObject.name == "Bullet")
        {

        }
        else 
        {
            this.gameObject.SetActive(false);
        }
    }
}
