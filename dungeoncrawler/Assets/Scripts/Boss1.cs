using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
 */

public class Boss1 : MonoBehaviour
{

    public int health = 5;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            HandleDamage(1);
        }
        if (other.gameObject.tag == "FallLimit")
        {
            Destroy(transform.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }


    private void HandleDamage(int damageDealt)
    {
        health -= damageDealt;
    }

    private void Die()
    {
        if (health <= 0)
        {
            //transform.gameObject.SetActive(false);
            Destroy(transform.gameObject);
        }
    }

}
