using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 3.5f;
    public GameObject laser;
    public GameObject leftExplosion;
    public GameObject rightExplosion;
    [HideInInspector]
    public int lives = 3;
    private SpawnBehaviour spawnBehaviour;
    [SerializeField]
    private float firerate = 0.5f;
    private float canfire = -1f;
    [SerializeField]
    private bool enableTripleShot = false;
    public GameObject tripleShotPrefab;
    [SerializeField]
    private float disableTripleShot = 5f;
    public GameObject shieldprefab;
    [SerializeField]
    private int _score=0;
    private UIManager uIManager;
    [SerializeField]
    private AudioClip laserSound;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosureSound;
    [SerializeField]
    private GameObject anim;
    
    private void Start()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        spawnBehaviour = GameObject.Find("Spawn_Manager").GetComponent<SpawnBehaviour>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("No audio source in player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        SpawningLaser();
    }

    public void damageLives()
    {
        
        lives--;
        uIManager.UpdateLives(lives);
        
        switch (lives)
        {
            case 2: 
                leftExplosion.SetActive(true);
                break;
            case 1:
                rightExplosion.SetActive(true);
                break;
            case 0:
                break;
        }
        
        if (lives < 1f)
        {
            
            Destroy(transform.gameObject);
            spawnBehaviour.StopInstantialting();
        }
    }

    void SpawningLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& Time.time>canfire)
        {
            laserFire();
        }
    }

    void PlayerMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        float verticalAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 direction = new Vector3(horizontalAxis, verticalAxis, 0f);
        transform.Translate(direction);

        if (transform.position.x > 3.79f)
        {
            transform.position = new Vector3(3.79f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -3.79f)
        {
            transform.position = new Vector3(-3.79f, transform.position.y, transform.position.z);
        }


        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else if (transform.position.y < -1.92f)
        {
            transform.position = new Vector3(transform.position.x, -1.92f, transform.position.z);
        }
    }

    void laserFire()
    {
        //Debug.Log(Time.time);
        canfire = firerate + Time.time;
        if (enableTripleShot)
        {
            Instantiate(tripleShotPrefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(laser, transform.position + new Vector3(0f, 0.75f, 0f), Quaternion.identity);
        }
        PlaySound();
    }

    void PlaySound()
    {
        audioSource.clip = laserSound;
        audioSource.priority = 0;
        audioSource.Play();
    }

    public void ActivateTripleShot()
    {
        enableTripleShot = true;
        //Debug.Log("Triple shot enabled");
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(disableTripleShot);
        //Debug.Log("Triple shot disabled");
        enableTripleShot = false;
    }

    public void SpeedUpActivate()
    {
        speed *= 2f;
        StartCoroutine(SpeedUpRoutine());
    }

    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        speed /= 2f;
    }

    

    public void InstantiateShield()
    {
        GameObject instantiatedshield=Instantiate(shieldprefab, transform.position, Quaternion.identity);
        instantiatedshield.transform.parent = this.transform;
    }

    public void addToScore()
    {
        _score += 10;
        if (uIManager != null)
        {
            uIManager.UpdateScore(_score);
        }
    }
}

