using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
 */

public class BatEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int health = 1;
    private float randomDirection;

    public bool moving = false;
    public bool awake = false;
    public bool newRandom = true;


    // Start is called before the first frame update
    void Start()
    {

    }

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

}
