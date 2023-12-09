using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Handles movement and health for Boss2.]
 */

public class Boss2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public int health = 1;
    public GameObject playerToFollow;

    public bool moving = true;
    public bool awake = false;
    public bool isInvincible = false;

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            FacePlayer();
            Move();
            Die();
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
    /// Handles moving the boss.
    /// </summary>
    private void Move()
    {
        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// Handles when the boss takes damage.
    /// </summary>
    /// <param name="damageDealt">The amount of damage the boss should take.</param>
    private void HandleDamage(int damageDealt)
    {
        if (!isInvincible)
        {
            health -= damageDealt;
            StartCoroutine(Invincibility());
        }
    }

    /// <summary>
    /// Checks whether the boss is below a certain amount of HP and destroys them if they are.
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
    /// Handles toggling the boss's invincibility after getting hit by the player.
    /// </summary>
    /// <returns></returns>
    IEnumerator Invincibility()
    {
        //Debug.Log("In coroutine");
        isInvincible = true;
        //Debug.Log("isInvincible set to true");
        yield return new WaitForSeconds(.25f);
        isInvincible = false;
        //Debug.Log("Ending Coroutine");
    }

    /// <summary>
    /// Makes the boss turn to face the player.
    /// </summary>
    private void FacePlayer()
    {
        transform.LookAt(playerToFollow.transform.position);
    }
}
