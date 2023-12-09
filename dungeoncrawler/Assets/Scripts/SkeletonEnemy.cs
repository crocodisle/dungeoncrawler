using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
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

    private void HandleDamage(int damageDealt)
    {
        if (!isInvincible)
        {
            health -= damageDealt;
            StartCoroutine(Invincibility());
        }
    }

    private void Die()
    {
        if (health <= 0)
        {
            //transform.gameObject.SetActive(false);
            Destroy(transform.gameObject);
        }
    }



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

    IEnumerator Invincibility()
    {
        //Debug.Log("In coroutine");
        isInvincible = true;
        //Debug.Log("isInvincible set to true");
        yield return new WaitForSeconds(.25f);
        isInvincible = false;
        //Debug.Log("Ending Coroutine");
    }

    private void FacePlayer()
    {
        transform.LookAt(playerToFollow.transform.position);
    }
}
