using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Controls the behavior of the player character.]
 */


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int facingDirection = 0; //0 = North, 1 = East, 2 = South, 3 = West
    public bool isInvincible = false;
    public int hitPoints = 8;
    public int smallKeysHeld = 0;
    public int bossKeysHeld = 0;

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
        if (other.gameObject.tag == "SmallKey")
        {
            smallKeysHeld++;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "RegularDoor")
        {
            if (smallKeysHeld >= 1)
            {
                smallKeysHeld--;
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag == "BossKey")
        {
            bossKeysHeld++;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "BossDoor")
        {
            if (bossKeysHeld >= 1)
            {
                bossKeysHeld--;
                other.gameObject.SetActive(false);
            }
        }

    }

    /// <summary>
    /// Handles player movement and rotation.
    /// </summary>
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

    /// <summary>
    /// Handles dealing damage to the player. Also starts invincibility coroutine.
    /// </summary>
    /// <param name="damage">The amount of damage to deal to the player.</param>
    public void TakeDamage(int damage)
    {
        if (isInvincible == false)
        {
            Debug.Log("Starting Invincibility Coroutine");
            StartCoroutine(Invincibility());
            hitPoints -= damage;
        }
    }

    /// <summary>
    /// Sets player position to respawn point object.
    /// </summary>
    private void Respawn()
    {
        transform.position = spawnPoint.transform.position;
    }

    /// <summary>
    /// Checks to see if the player drops below a certain HP.
    /// </summary>
    private void HandleDeath()
    {
        if (hitPoints <= 0)
        {
            Debug.Log("Player died.");
            SceneManager.LoadScene(1);
        }

    }

    /// <summary>
    /// Coroutine for temporary invincibility and blinking.
    /// </summary>
    /// <returns></returns>
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
