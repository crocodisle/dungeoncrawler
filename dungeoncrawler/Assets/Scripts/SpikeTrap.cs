using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Handles spike trap behavior.]
 */

public class SpikeTrap : MonoBehaviour
{
    public GameObject spikeHome;
    public GameObject[] targets;
    public float speed = 10f;

    public bool awake = false;
    public bool triggered = false;
    public Vector3 currentTarget;
    public bool moving = false;
    public bool returning = false;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Handles how the spike trap moves in relation to its sibling objects.
    /// </summary>
    private void Movement()
    {
        if (moving)
        {
            transform.position += Vector3.Normalize(currentTarget - transform.position) * Time.deltaTime * speed;

            if (Vector3.Distance(transform.position, currentTarget) < .2f)
            {
                moving = false;
                returning = true;
            }
        }
        if (returning)
        {
            transform.position += Vector3.Normalize (spikeHome.transform.position - transform.position) * Time.deltaTime * (speed/3);

            if (Vector3.Distance(transform.position, spikeHome.transform.position) < .1f)
            {
                returning = false;
                triggered = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if (!triggered && awake)
        {
            RaycastHit hit;
            foreach (GameObject target in targets)
            {
                if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, Vector3.Distance(transform.position, target.transform.position)))
                {
                    //Debug.Log("Checking " + target);
                    Debug.Log(Vector3.Distance(transform.position, target.transform.position));
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        //Debug.Log("Triggering!");
                        currentTarget = target.transform.position;
                        moving = true;
                        triggered = true;
                    }
                }
            }
        }
        
    }


}
