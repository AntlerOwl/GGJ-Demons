using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RewardText : MonoBehaviour
{
    private float speed = 30f;
    private float duration = 1f;
    public Text textReward;
    public Transform origPos;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void Initialize(string text, Color color, Vector3 pos)//, Transform parent)
    {
        // Set the text
        textReward.text = text;
        textReward.color = color;
        // Set the parent and position the object
        //transform.SetParent (parent, false);
        transform.position = origPos.position;
        // Activate
        gameObject.SetActive (true);

        // Deactivate in a certian amount of time
        Invoke ("Deactivate", duration);
    }

    void Deactivate()
    {
        GUIManager.instance.DestroyRewardText(this);
    }
}
