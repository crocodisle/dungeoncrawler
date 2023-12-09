using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
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

    private void Move()
    {
        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
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
