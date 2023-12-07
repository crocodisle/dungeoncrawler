using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Description of the file's basic functions]
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
