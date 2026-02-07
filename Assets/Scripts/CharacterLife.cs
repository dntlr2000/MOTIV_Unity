using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    //public float maxHealth = 1000; //플레이어는 일단 체력은 없음
    //private float currentHealth;
    public int life = 3; //전체 목숨 개수
    public int currentLife; //현재 목숨
    public string damageTag = "E_Attack"; //피격 판정 처리할 태그들
    public BulletReset bulletReset; // public으로 선언하여 Inspector에서 할당 가능
    public GameObject hitEffect; //피격 이펙트
    public GameObject invincibilityEffect; //무적 시간동안 나올 이펙트

    void Start()
    {

        currentLife = life; //시작할 때 목숨 개수 설정

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damageTag))
        {
             
            Instantiate(hitEffect, transform); //현재 위치에 피격 이펙트 생성
            GameObject invincibility = Instantiate(invincibilityEffect, transform); //피격 후 무적 이펙트 생성
            currentLife -= 1; //목숨 감소
            //damaged(5.0f); //이것도 Enemy_Health와 좀 코드가 공유된 흔적? -> 이후 정리해서 상속으로 해결 가능?
            //Destroy(other.gameObject);
            if (bulletReset != null)
            {
                bulletReset.resetBullet(); //bulletReset 스크립트의 탄 제거 함수 호출
            }
            if (currentLife == 0)
            {
                Debug.Log("You Lost!");
                gameObject.SetActive(false); //게임 오버 시 캐릭터 삭제. 현재로선 더미데이터화
            }
        }

        if (other.CompareTag("oneUp")) //먹은 아이템 태그명이 oneUp이면 목숨 개수 증가
        {
            currentLife += 1;
            Destroy(other.gameObject);
        }

    }


    void Update()
    {
        //삭제 테스트용
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletReset != null)
            {
                bulletReset.resetBullet();
            }
        }
        */
    }
}
