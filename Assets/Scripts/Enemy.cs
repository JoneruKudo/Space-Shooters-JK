using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int health = 10;
    [SerializeField] GameObject destroyVFX;
    [SerializeField] AudioClip deathSFX;

    [Header("Enemy Weapon Stats")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float timePerShoot = 1f;
    [SerializeField] bool shootLooping = true;
    [SerializeField] float laserProjectileSpeed = 10f;
    [SerializeField] AudioClip laserSFX;
    [Range(0f,1f)][SerializeField] float laserVolume;

    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        StartCoroutine(Fire());
    }


    private IEnumerator Fire()
    {
        while (shootLooping) 
        { 
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, timePerShoot));
            var laserPos = new Vector2(transform.position.x, transform.position.y - 0.7f);
            GameObject laser = Instantiate(enemyLaser, laserPos, Quaternion.identity) as GameObject;
            myAudio.PlayOneShot(laserSFX, laserVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserProjectileSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<DamageDealer>()) { return; }
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().UpdateScore();
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
