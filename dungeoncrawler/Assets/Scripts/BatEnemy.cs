using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Contains code that determines the bat enemy's behavior.]
 */

public class BatEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int health = 1;
    private float randomDirection;

    public bool moving = false;
    public bool awake = false;
    public bool newRandom = true;
    public bool isInvincible = false;

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            Move();
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
    /// Checks for a collision with the enemy a ways away from the monster so the monster knows to move out of the way.
    /// </summary>
    private void CheckNearby()
    {
        if (!moving)
        {
            Collider[] nearbyObjects = Physics.OverlapBox(transform.position, transform.localScale * 4);
            foreach (Collider detectedObject in nearbyObjects)
            {
                if (detectedObject.gameObject.tag == "Player")
                {
                    StartCoroutine(StartMove());
                }
            }
        }
    }

    /// <summary>
    /// Handles how the monster moves and starts the coroutine that causes the monster to fidget.
    /// </summary>
    private void Move()
    {
        
        if (moving)
        {
            if (newRandom)
            {
                StartCoroutine(RandomNumber());
            }
            
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// Handles how the monster takes damage.
    /// </summary>
    /// <param name="damageDealt">The amount of damage to do to the monster.</param>
    private void HandleDamage(int damageDealt)
    {
        if (!isInvincible)
        {
            health -= damageDealt;
            StartCoroutine(Invincibility());
        }
        
    }

    /// <summary>
    /// Handles monster death.
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
    /// Changes the monster's rotation based on a periodically generated random number.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomNumber()
    {
        newRandom = false;
        randomDirection = Random.Range(0, 12);

        if (randomDirection < 3)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        if (randomDirection > 2 && randomDirection < 6)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }
        if (randomDirection > 5 && randomDirection < 9)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }
        if (randomDirection > 8)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
        yield return new WaitForSeconds(.25f);
        newRandom = true;
    }

    /// <summary>
    /// Determines how long the monster should move for.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartMove()
    {
        if (awake)
        {
            if (!moving)
            {
                moving = true;
                yield return new WaitForSeconds(2f);
                moving = false;
            }
        }
    }

    /// <summary>
    /// Sets the monster invincible. Only applies to a boss version of this monster.
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
}
