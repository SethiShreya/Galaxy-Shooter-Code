using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public float time = 2f;
   

    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
        if (transform.position.y > 2.87f)
        {
            //Debug.Log("exit the y");
            if (transform.parent != null)
            {
                //Debug.Log("Parent destroyed");
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }
}
