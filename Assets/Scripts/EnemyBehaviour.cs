using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerBehaviour player;
    public float speed = 4f;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip dieClipSound;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (player == null)
        {
            Debug.Log("Player not found");
        }
        animator = gameObject.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("Couldn't find audio source component in player");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag== "Player")
        {
            other.GetComponent<PlayerBehaviour>().damageLives();

            //debug.Log(health);
            //respawn();
            animator.SetTrigger("IsEnemyDied");
            
            speed = 0;
            playAudio();
            Destroy(transform.gameObject, 2.8f);
        }

        else if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            
           if (player != null)
            {
                player.addToScore();
            }

            //Debug.Log("hit the enemy");
            //respawn();
            animator.SetTrigger("IsEnemyDied");
            Debug.Log("animation working");
            speed = 0;
            playAudio();
            Destroy(transform.gameObject, 2.8f);
        }

        else if (other.tag == "Shield")
        {
            Debug.Log("Hit the shield");
            Destroy(other.gameObject);
            animator.SetTrigger("IsEnemyDied");
            speed = 0;
            playAudio();
            Destroy(this.gameObject, 2.8f);
        }
    }

    void playAudio()
    {
        if (audioSource!=null)
        {
            audioSource.clip = dieClipSound;
            audioSource.Play();
        }
    }
    

    private void Update()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);

        if (transform.position.y < -4.36)
        {
            var x = Random.Range(-4.34f, 9.02f);
            transform.position = new Vector3(x, 4.25f);
        }
    }

    
}
