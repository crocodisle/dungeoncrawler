using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
    public float speed = 1.5f;
    public GameObject playerToFollow;

    public bool moving = true;
    public bool awake = false;
    public bool canMove = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            FacePlayer();
            Move();
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
