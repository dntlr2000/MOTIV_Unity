using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnMouseUp()
    {
        this.gameObject.SetActive(false);
    }
}
