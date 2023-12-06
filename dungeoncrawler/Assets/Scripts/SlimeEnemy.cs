using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
 */

public class SlimeEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public int health = 1;
    public GameObject playerToFollow;

    public bool moving = true;
    public bool awake = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToggleMove());
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        Move();
    }

    private void Move()
    {
        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

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

    private void FacePlayer()
    {
        transform.LookAt(playerToFollow.transform.position);
    }
}
