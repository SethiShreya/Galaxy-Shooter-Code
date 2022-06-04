using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] powerups;
    
    private bool stopInstantiating=false;
    [SerializeField]
    private float time = 5f;
    //public float time = 5f;

    // Update is called once per frame
    public void SpawningRoutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (stopInstantiating==false)
        {
            var x = Random.Range(-3.86f, 4.06f);
            Instantiate(enemy, new Vector3(x, 3.02f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (stopInstantiating == false)
        {
            var x = Random.Range(-3.86f, 4.06f);
            var randomIndex = Random.Range(0, 3);
            Instantiate(powerups[randomIndex], new Vector3(x, 3.02f, 0f), Quaternion.identity);
            var t = Random.Range(3f, 7f);
            //Debug.Log("spawn in " + t+ " time");
            yield return new WaitForSeconds(t);
        }
    }
    

    public void StopInstantialting()
    {
        stopInstantiating = true;
    }
}
