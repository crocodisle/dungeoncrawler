using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [Handles updating the in-game UI.]
 */
public class GameUIManager : MonoBehaviour
{
    public TMP_Text totalHealthText;
    public TMP_Text smallKeysText;
    public TMP_Text bossKeyText;
    public PlayerController PlayerController;

    // Update is called once per frame
    void Update()
    {
        totalHealthText.text = "Hearts: " + PlayerController.hitPoints;
        smallKeysText.text = "Small Keys: " + PlayerController.smallKeysHeld;
        bossKeyText.text = "Boss Key: " + PlayerController.bossKeysHeld;

    }
}
