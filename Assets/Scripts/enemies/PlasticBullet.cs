using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public Vector3 direction;

    private Vector3 startPos;
    private float timeLeft = 3;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(bulletSpeed * Time.deltaTime * direction);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("container") || other.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
