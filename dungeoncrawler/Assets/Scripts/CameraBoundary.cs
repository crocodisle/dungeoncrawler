using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public List<GameObject> collidingEntities;
    public bool playerPresent = false;
    public bool isKeySpawnable = false;
    public GameObject keyToSpawn;


    private void Update()
    {
        if (playerPresent)
        {
            collidingEntities.RemoveAll(entities => entities == null);
            if ((collidingEntities.Count < 2) && isKeySpawnable){
                keyToSpawn.SetActive(true);
                isKeySpawnable = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidingEntities.Add(other.gameObject);

        if (other.gameObject.tag == "Player")
        {
            playerPresent = true;
            ToggleEnemies(true);
            
        }
    }

    private void ToggleEnemies(bool goTime)
    {
        foreach (var entity in collidingEntities)
        {
            if (entity.tag == "SlimeEnemy")
            {
                entity.GetComponent<SlimeEnemy>().awake = goTime;
            }
            if (entity.tag == "BatEnemy")
            {
                //Debug.Log("Awakening Bats");
                entity.GetComponent<BatEnemy>().awake = goTime;
            }
            if (entity.tag == "SkeletonEnemy")
            {
                Debug.Log("Awakening Skeletons");
                entity.GetComponent<SkeletonEnemy>().awake = goTime;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        collidingEntities.RemoveAll(entities => entities == null);
        collidingEntities.Remove(other.transform.gameObject);

        if(other.gameObject.tag == "Player")
        {
            playerPresent = false;
            ToggleEnemies(false);
            Debug.Log(collidingEntities.Count);
        }
    }

}
