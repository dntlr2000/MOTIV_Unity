using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        damage(other.gameObject);
        //Ãæµ¹½Ã ÆÄ±«
        Destroy(this.gameObject);
    }

    void damage(GameObject ob)
    {
        ob.GetComponent<HP>().hp -= 1;
    }
}