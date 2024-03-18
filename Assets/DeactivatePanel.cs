using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePanel : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
