using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
