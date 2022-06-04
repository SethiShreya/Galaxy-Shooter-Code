using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBehaviour : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 19;
    [SerializeField]
    private GameObject anim;
    GameObject instantiatedAnim;
    private SpawnBehaviour spawnBehaviour;
    private AudioSource player;
    [SerializeField]
    private AudioClip explosionSound;

    private void Start()
    {
        spawnBehaviour = GameObject.Find("Spawn_Manager").GetComponent<SpawnBehaviour>();
        player = GameObject.Find("Player").GetComponent<AudioSource>();
        if (player == null)
        {
            Debug.Log("Audio Source not found");
        }
    }

    void Update()
    {
        transform.Rotate(transform.forward * rotationSpeed * Time.deltaTime);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Debug.Log("Trigger collision");
            instantiatedAnim=Instantiate(anim, transform.position, transform.rotation);
            spawnBehaviour.SpawningRoutine();
            player.clip = explosionSound;
            player.Play();
            Destroy(this.gameObject);
        }
    }

    
}
