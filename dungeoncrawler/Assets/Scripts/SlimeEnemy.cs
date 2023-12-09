using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Contains code that determines the slime enemy's behavior.]
 */

public class SlimeEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public int health = 1;
    public GameObject playerToFollow;

    public bool moving = true;
    public bool awake = false;
    public bool canMove = false;

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            FacePlayer();
            Move();
            Die();
            if (!canMove)
            {
                canMove = true;
                StartCoroutine(ToggleMove());
            }
        }
        else
        {
            canMove = false;
        }
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

    /// <summary>
    /// The code that makes the enemy move.
    /// </summary>
    private void Move()
    {

        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// The code that handles reducing the monster's health.
    /// </summary>
    /// <param name="damageDealt">The amount of damage to deal to the monster.</param>
    private void HandleDamage(int damageDealt)
    {
        health -= damageDealt;
    }

    /// <summary>
    /// Code that checks to see if the monster drops below 1 hitpoint.
    /// </summary>
    private void Die()
    {
        if (health <= 0)
        {
            //transform.gameObject.SetActive(false);
            Destroy(transform.gameObject);
        }
    }

    
    /// <summary>
    /// Makes the monster move in a funny way by a series of delays.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ToggleMove()
    {
        while (awake)
        {
            if (moving)
            {
                moving = false;
                yield return new WaitForSeconds(.66f);

            }
            else
            {
                moving = true;
                yield return new WaitForSeconds(.20f);
            }
        }
    }

    /// <summary>
    /// Makes the monster face towards the player so it can move towards them.
    /// </summary>
    private void FacePlayer()
    {
        transform.LookAt(playerToFollow.transform.position);
    }
}
