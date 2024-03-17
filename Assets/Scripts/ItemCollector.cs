using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int apples = 0;
    [SerializeField] private TextMeshProUGUI applesText;

    [SerializeField] private AudioClip colletItemSound;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            ++apples;
            applesText.text = "apples: " + apples;
            audioSource.PlayOneShot(colletItemSound);
        }
    }
}
