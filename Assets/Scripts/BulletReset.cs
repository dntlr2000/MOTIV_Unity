using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReset : MonoBehaviour
{
    public string targetTag = "E_Attack"; // 삭제할 오브젝트의 태그

    //void Start() { }
    public void resetBullet()
    {
        // 특정 태그를 가진 모든 오브젝트 찾기
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(targetTag); //지정한 태그의 오브젝트를 전부 찾고
        Debug.Log($"Found {objectsToDestroy.Length} objects with tag {targetTag}");
        // 찾은 모든 오브젝트를 foreach로 제거한다
        foreach (GameObject obj in objectsToDestroy) //objectsToDestroy의 각 obj에 대하여 반복문
        {
            //Debug.Log($"Destroying object: {obj.name}");
            Destroy(obj);
            obj.transform.parent = null; //오브젝트 뒤처리
        }
    }
}
