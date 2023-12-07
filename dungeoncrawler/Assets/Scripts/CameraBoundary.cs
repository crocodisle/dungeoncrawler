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
            WakeUp();
        }
        Debug.Log(collidingEntities.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        collidingEntities.Remove(other.transform.gameObject);
    }

    private void WakeUp()
    {
        foreach (var entity in collidingEntities)
        {
            if (entity.gameObject.tag == "Player")
            {

            }
            else
            {
                entity.SetActive(true);
            }
        }
    }

}
