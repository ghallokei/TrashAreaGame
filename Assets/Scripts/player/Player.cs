using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public Slider healthBar;

    public int score;
    public Text pointsText;

    public Slider upgradeProgress;

    public AudioClip damageSound;

    public List<Sprite>dirts = new List<Sprite>();

    public Image dirt;
    public int health;

    private float transparency;

    private Color tempColor;

    private bool gotHit;

    void Start()
    {
        gotHit = false;
        dirt.enabled = false;
        health = 100;
        upgradeProgress.GetComponent<Slider>().value = 0;
    }

    void Update()
    {
        PlayerController();
        RotatePlayer();

        string output = "Score\n" + score;
        pointsText.text = output;

        healthBar.GetComponent<Slider>().value = health;

        if (gotHit)
        {
            StartCoroutine(DirtHit());
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void TakeDamage(int attackDamage)
    {
        GetComponent<AudioSource>().clip = damageSound;
        GetComponent<AudioSource>().Play();
        health -= attackDamage;
        gotHit = true;
    }

    public void AddHealth(int healthAmount)
    {
        health += healthAmount;
    }

    public void AddPoints(int pointValue)
    {
        score += pointValue;
        upgradeProgress.GetComponent<Slider>().value += pointValue;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("bullet")) return;

        TakeDamage(5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("health")) return;

        AddHealth(15);
        Destroy(other.gameObject);
    }

    private void PlayerController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.right);
        }
    }

    private void RotatePlayer()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2) Camera.main.ScreenToViewportPoint(Input.mousePosition);


        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);


        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 80, 0f));
    }

    IEnumerator DirtHit()
    {
        gotHit = false;
        dirt.enabled = true;
        dirt.GetComponent<Image>().sprite = dirts[2];
        yield return new WaitForSeconds(0.75f);
        
        dirt.GetComponent<Image>().sprite = dirts[1];
        yield return new WaitForSeconds(0.75f);
        
        dirt.GetComponent<Image>().sprite = dirts[0];
        yield return new WaitForSeconds(0.75f);
        
        dirt.enabled = false;
    }
}