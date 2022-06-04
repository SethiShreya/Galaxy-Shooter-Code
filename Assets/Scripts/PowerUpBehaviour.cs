using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip audioClip;

    void Update()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime); 
        if (transform.position.y < -3 )
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            //Debug.Log("PowerUp collected");
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            Debug.Log("PowerUp sound played");
            PlayerBehaviour player = collision.transform.GetComponent<PlayerBehaviour>();
            
            if (player)
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        //Debug.Log("Speed Powerup collected");
                        player.SpeedUpActivate();
                        break;
                    case 2:
                        //Debug.Log("Shield power up collected");
                        player.InstantiateShield();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
            }
            Destroy(transform.gameObject);
        }
    }
    
}
