using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public List<GameObject> collidingEntities;

    private void OnTriggerEnter(Collider other)
    {
        collidingEntities.Add(other.gameObject);

        if (other.gameObject.tag == "Player")
        {
            ToggleEnemies(true);
        }
        
        Debug.Log(collidingEntities.Count);
    }

    private void ToggleEnemies(bool goTime)
    {
        foreach (var entity in collidingEntities)
        {
            if (entity.tag == "SlimeEnemy")
            {
                entity.GetComponent<SlimeEnemy>().awake = goTime;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        collidingEntities.Remove(other.transform.gameObject);

        if(other.gameObject.tag == "Player")
        {
            ToggleEnemies(false);
        }
    }

}
