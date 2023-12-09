using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Contains code that determines whether or not the player is in a room.]
 */

public class CameraBoundary : MonoBehaviour
{
    public List<GameObject> collidingEntities;
    public bool playerPresent = false;
    public bool isKeySpawnable = false;
    public GameObject keyToSpawn;
    public bool isBossRoom = false;
    public GameObject bossMonster;


    private void Update()
    {
        if (playerPresent)
        {
            collidingEntities.RemoveAll(entities => entities == null);
            if ((collidingEntities.Count < 2) && isKeySpawnable){
                keyToSpawn.SetActive(true);
                isKeySpawnable = false;
            }
            if (isBossRoom && !collidingEntities.Contains(bossMonster))
            {
                SceneManager.LoadScene(2);
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

    /// <summary>
    /// "Awakens" the enemies that are colliding with this CameraBoundary. 
    /// </summary>
    /// <param name="goTime">A boolean value that determines whether enemies should be awake or not.</param>
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
            if (entity.tag == "SpikeTrap")
            {
                Debug.Log("Awakening Spike Traps");
                entity.GetComponentInChildren<SpikeTrap>().awake = goTime;
            }
            if (entity.tag == "Boss2")
            {
                Debug.Log("Awakening Boss2");
                entity.GetComponent<Boss2>().awake = goTime;
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
