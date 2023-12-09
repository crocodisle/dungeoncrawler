using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Contains code that determines the player's sword behavior.]
 */

public class Sword : MonoBehaviour
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

    /// <summary>
    /// Gets input for swinging the sword and starts the coroutine for having it out.
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
    /// The coroutine for making the sword appear and collide with enemies.
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
        isSwinging = false;
    }
}
