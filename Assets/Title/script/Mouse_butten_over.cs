using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_butten_over : MonoBehaviour
{
    public void Mouse_over()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Mouse_over_out()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }
}
