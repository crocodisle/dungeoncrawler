using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Controls the boss's sword whenever the player swings, but with a longer delay between swings.]
 */
public class BossSword : MonoBehaviour
{
    private bool isSwinging = false;

    private MeshRenderer thisMeshRenderer;
    private Collider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisMeshRenderer = GetComponent<MeshRenderer>();
        thisCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSwinging();
    }

    private void OnTriggerEnter(Collider other)
    {
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
    /// Checks to see if the player is swinging their sword and swings the boss's sword at the same time.
    /// </summary>
    private void HandleSwinging()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isSwinging)
            {
                StartCoroutine(Swing());
            }
        }
    }

    /// <summary>
    /// Sets the delays on how long the boss's sword is swung and adds an extra cooldown compared to the player's sword. 
    /// </summary>
    /// <returns></returns>
    IEnumerator Swing()
    {
        isSwinging = true;
        thisMeshRenderer.enabled = true;
        thisCollider.enabled = true;
        yield return new WaitForSeconds(.25f);
        thisMeshRenderer.enabled = false;
        thisCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        isSwinging = false;
    }
}
