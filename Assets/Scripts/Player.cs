using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player Stats")]
    [SerializeField] int health = 5;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 0.5f;
    [SerializeField] float yTopPadding = 5f;

    [Header("Player Weapon Stats")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserProjectileSpeed = 20f;
    [SerializeField] float shootFrequency = 0.05f;
    [SerializeField] AudioClip laserSFX;
    [Range(0f, 1f)] [SerializeField] float laserVolume;


    // cache references
    Coroutine firingCoroutine, firingCoroutine2;
    AudioSource myAudio;
    VariableJoystick varJoystick;
    ShootButton shootbutton;

    // state variables
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMovementBoundaries();
        myAudio = GetComponent<AudioSource>();
        varJoystick = FindObjectOfType<VariableJoystick>();
        shootbutton = FindObjectOfType<ShootButton>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalJoystick();
        VerticalJoystick();
    }

    //private void Fire()
    //{
    //    if (shootbutton.isButtonPressed())
    //    {
    //        firingCoroutine = StartCoroutine(KeepFiring());
    //        Debug.Log("qwerty");
    //    }
    //    if(!shootbutton.isButtonPressed())
    //    {
    //        Debug.Log("hey");
    //        if(firingCoroutine != null)
    //        {
    //            StopCoroutine(firingCoroutine);
    //        }
    //    }
    //}

    IEnumerator KeepFiring()
    {
            yield return new WaitForSeconds(shootFrequency);
            var laserPos = new Vector2(transform.position.x, transform.position.y + 0.7f);
            GameObject laser = Instantiate(playerLaser, laserPos, Quaternion.identity) as GameObject;
            myAudio.PlayOneShot(laserSFX, laserVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserProjectileSpeed);
    }

    public void FireFromButton()
    {
       firingCoroutine2 = StartCoroutine(KeepFiringFromButton());
    }

    public void StopFiring()
    {
        if(firingCoroutine2 != null)
        {
            StopCoroutine(firingCoroutine2);
        }
    }

    IEnumerator KeepFiringFromButton()
    {
        while (true)
        {
            var laserPos = new Vector2(transform.position.x, transform.position.y + 0.7f);
            GameObject laser = Instantiate(playerLaser, laserPos, Quaternion.identity) as GameObject;
            myAudio.PlayOneShot(laserSFX, laserVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserProjectileSpeed);
            yield return new WaitForSeconds(shootFrequency);
        }
    }

    private void HorizontalJoystick()
    {
        var deltaX = varJoystick.Direction.x * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void VerticalJoystick()
    {
        var deltaY = varJoystick.Direction.y * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + xPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yTopPadding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHit(collision);
        PlayerDead();
    }

    private void PlayerDead()
    {
        if (health < 0)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
            FindObjectOfType<Level>().LoadGameOverScene();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void EnemyHit(Collider2D collision)
    {
        health -= 1;
        AudioSource.PlayClipAtPoint(deathSFX, collision.transform.position);
        Instantiate(deathVFX, collision.transform.position, Quaternion.identity);
        collision.gameObject.SetActive(false);
        Destroy(collision.gameObject);
    }

    public int GetPlayerHealth()
    {
        return health;
    }
}
