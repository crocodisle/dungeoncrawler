using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
 */


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int facingDirection = 0; //0 = North, 1 = East, 2 = South, 3 = West
    public bool isInvincible = false;
    public int hitPoints = 8;

    private Rigidbody thisRigidbody;
    private MeshRenderer thisMeshRenderer;

    public Camera sceneCamera;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraBoundary")
        {
            sceneCamera.transform.position = new Vector3(other.transform.position.x, 10, other.transform.position.z);
            spawnPoint.transform.position = transform.position;
        }

        if (other.gameObject.tag == "FallLimit")
        {
            Respawn();
            TakeDamage(2);
        }

        if(other.gameObject.tag == "SlimeEnemy")
        {
            TakeDamage(1);
        }


    }


    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (facingDirection != 0)
            {
                facingDirection = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (facingDirection != 3)
            {
                facingDirection = 3;
                transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (facingDirection != 2)
            {
                facingDirection = 2;
                transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (facingDirection != 1)
            {
                facingDirection = 1;
                transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            }
        }



        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        
    }

    private void TakeDamage(int damage)
    {
        if (isInvincible == false)
        {
            Debug.Log("Starting Invincibility Coroutine");
            StartCoroutine(Invincibility());
            hitPoints -= damage;
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint.transform.position;
    }

    private void HandleDeath()
    {
        if (hitPoints <= 0)
        {
            Debug.Log("Player died.");
            SceneManager.LoadScene(1);
        }

    }

    IEnumerator Invincibility()
    {
        //Debug.Log("In coroutine");
        isInvincible = true;
        //Debug.Log("isInvincible set to true");
        for (int index = 0; index <= 6; index++)
        {
            //Debug.Log("In for loop");
            if (index % 2 == 0)
            {
                //Debug.Log("Set mesh to false");
                thisMeshRenderer.enabled = false;
            }
            else
            {
                //Debug.Log("Set mesh to true");
                thisMeshRenderer.enabled = true;
            }
            //Debug.Log("waiting");
            yield return new WaitForSeconds(.25f);
        }
        thisMeshRenderer.enabled = true;
        isInvincible = false;
        //Debug.Log("Ending Coroutine");
    }
}
