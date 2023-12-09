using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Handles skeleton behavior.]
 */

public class SkeletonEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public int health = 1;
    public GameObject playerToFollow;

    public bool moving = true;
    public bool awake = false;
    public bool isDashing = false;
    public bool dashOnCooldown = true;
    public bool isInvincible = false;

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            FacePlayer();
            Move();
            CheckPlayerSwing();
            Die();
        }
    }

    private void FixedUpdate()
    {
        CheckNearby();
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
    /// Checks a nearby collission to see if the player is near, and whether it should be able to dash or not.
    /// </summary>
    private void CheckNearby()
    {
        if (awake)
        {
            Collider[] nearbyObjects = Physics.OverlapBox(transform.position, transform.localScale * 3);
            bool playerDetected = false;
            foreach (Collider detectedObject in nearbyObjects)
            {
                if (detectedObject.gameObject.tag == "Player")
                {
                    playerDetected = true;
                }
            }

            if (playerDetected)
            {
                //Debug.Log("Detected");
                dashOnCooldown = false;
            }
            else
            {
                //Debug.Log("Not Detected");
                dashOnCooldown = true;
            }
        }
    }

    /// <summary>
    /// Checks to see if the keycode to swing the player's sword is pressed and starts the dash subroutine if it is.
    /// </summary>
    private void CheckPlayerSwing()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isDashing && !dashOnCooldown)
            {
                StartCoroutine(ToggleDash());
            }
        }
    }

    /// <summary>
    /// Handles all skeleton movement.
    /// </summary>
    private void Move()
    {
        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (isDashing)
        {
            transform.position += -transform.forward * Time.deltaTime * (speed * 10);
        }
    }

    /// <summary>
    /// Handles taking damange.
    /// </summary>
    /// <param name="damageDealt">The amount of damage to take.</param>
    private void HandleDamage(int damageDealt)
    {
        if (!isInvincible)
        {
            health -= damageDealt;
            StartCoroutine(Invincibility());
        }
    }

    /// <summary>
    /// Checks to see if health drops below 1 and destroys the monster if so.
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
    /// Handles turning off and on the flags needed for the skeleton to dash away from attacks.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ToggleDash()
    {
        if (awake)
        {
            moving = false;
            isDashing = true;
            yield return new WaitForSeconds(.10f);
            isDashing = false;
            moving = true;
            dashOnCooldown = true;
            yield return new WaitForSeconds(.50f);
            dashOnCooldown = false;
        }
    }

    /// <summary>
    /// Handles invincibility (only relevant for a boss version of this monster).
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
    /// Makes the skeleton look at the player to it can move towards them.
    /// </summary>
    private void FacePlayer()
    {
        transform.LookAt(playerToFollow.transform.position);
    }
}
