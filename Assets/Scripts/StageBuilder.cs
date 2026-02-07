using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    //public string restrictedTag = "Player";
    

    // Collider 밖으로 벗어나는 오브젝트를 감지하는 메서드
    void OnTriggerExit(Collider other)
    {
        // 특정 태그를 가진 오브젝트만 삭제
        if (other.CompareTag("E_Attack") || other.CompareTag("P_Attack") || other.CompareTag("oneUp"))
        {
            Destroy(other.gameObject);
        }
        /*
        if (other.CompareTag("Player"))
        {
            // 오브젝트를 Collider 안으로 다시 이동시킴
            // 벗어나려는 방향과 반대 방향으로 약간 이동
            Vector3 directionToCenter = transform.position - other.transform.position;
            other.transform.position += directionToCenter.normalized * 0.1f;
        }
        */
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
