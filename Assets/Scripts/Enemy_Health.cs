using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth = 1000; //각 페이즈마다 체력을 다르게 설정해야 함
    private float currentHealth;
    public int life = 3;
    private int currentLife;
    public string damageTag = "P_Attack";
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLife = life;
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == damageTag)
        {
            Vector3 spawnPosition = other.transform.position;
            // other 오브젝트가 바라보는 방향의 반대로 회전 설정
            Quaternion spawnRotation = Quaternion.LookRotation(-other.transform.forward);
            // hitEffect를 생성
            Instantiate(hitEffect, spawnPosition, spawnRotation);


            damaged(5.0f);
        }
        Destroy(other.gameObject);
    }

    public void damaged(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Damaged: {damage}, Current Health: {currentHealth}");
        

        if (currentHealth < 0)
        {
            life -= 1;
            currentHealth = maxHealth;

        }

        if (life == 0) {
            Debug.Log("Enemy Defeated");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
