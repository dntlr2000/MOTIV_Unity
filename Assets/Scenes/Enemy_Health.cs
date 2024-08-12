using System.Collections;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth; // 적의 최대 체력
    private float currentHealth; // 현재 체력
    public int life = 3; // 적의 생명 수 (체력의 단위로 설정)
    private int currentLife; // 현재 생명 수
    public string damageTag = "P_Attack"; // 적에게 피해를 주는 태그
    public GameObject hitEffect; // 적이 피격될 때 생성할 이펙트
    public float abilityTime = 2.0f; // 페이즈 전환 시 무적 시간
    public float healthRecoveryRate = 30.0f; // 체력 회복량 (초당)
    public float recoveryDelay = 3.0f; // 피해를 입지 않았을 때 회복 시작 대기 시간 (조건)

    private bool isInvulnerable = false; // 무적 상태(불값)
    private float recoveryTimer; // 회복 타이머
    private bool isRecovering = false; // 회복 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        // 페이즈에 따라 체력 변화
        if (life == 3)
        {
            maxHealth = 1000; // 3페이즈
        }
        else if (life == 2)
        {
            maxHealth = 1500; // 2페이즈
        }
        else if (life == 1)
        {
            maxHealth = 2000; // 1페이즈
        }

        currentHealth = maxHealth; // 게임 시작 시 현재 체력을 최대 체력으로 설정
        currentLife = life; // 게임 시작 시 현재 생명 수를 설정

        if (life == 1)
        {
            // 생명 수가 1일 때 
            recoveryTimer = recoveryDelay;
        }
    }

    // 충돌 
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체의 태그가 damageTag와 일치할 떄
        if (other.tag == damageTag && !isInvulnerable)
        {
            Vector3 spawnPosition = other.transform.position;

            Quaternion spawnRotation = Quaternion.LookRotation(-other.transform.forward);

            // hitEffect 객체를 생성하여 충돌 위치에 배치
            Instantiate(hitEffect, spawnPosition, spawnRotation);

            // 피해를 처리하는 함수 호출
            damaged(5.0f); // 5.0의 피해를 적용
        }

        // 충돌한 객체를 제거
        Destroy(other.gameObject);
    }

    // 적이 피해를 입었을 때 호출되는 함수
    public void damaged(float damage)
    {
        // 무적 상태가 아닐 때만 피해를 처리
        if (!isInvulnerable)
        {
            currentHealth -= damage; // 현재 체력에서 피해량을 차감
            Debug.Log($"Damaged: {damage}, Current Health: {currentHealth}"); // 피해량과 현재 체력 출력

            // 회복 타이머 초기화
            if (life == 1)
            {
                recoveryTimer = recoveryDelay;
                isRecovering = false;
            }

            // 현재 체력이 0보다 작으면
            if (currentHealth <= 0)
            {
                life -= 1; // 생명 수를 1 감소
                if (life > 0) // 생명 수가 여전히 남아 있으면
                {
                    // 다음 페이즈 체력 설정
                    if (life == 2)
                    {
                        maxHealth = 1500; // 2페이즈 체력
                    }
                    else if (life == 1)
                    {
                        maxHealth = 2000; // 1페이즈 체력
                    }

                    currentHealth = maxHealth; // 체력을 다음 페이즈의 최대 체력으로 재설정
                    StartCoroutine(ability()); // 페이즈 전환 시 무적 시간 시작
                }
                else
                {
                    // 생명 수가 0이 되면 적을 제거
                    Debug.Log("Enemy Defeated"); // 적이 패배했다는 메시지 출력
                    gameObject.SetActive(false); // 적 게임 오브젝트를 비활성화
                }
            }
        }
    }

    // 무적 시간
    private IEnumerator ability()
    {
        isInvulnerable = true; // 무적 상태로 설정
        yield return new WaitForSeconds(abilityTime); // 무적 시간 동안 대기
        isInvulnerable = false; // 무적 상태 해제
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 1)
        {
            // 생명 수가 1일 때만 회복 타이머 및 체력 회복 
            if (!isRecovering)
            {
                recoveryTimer -= Time.deltaTime;
                if (recoveryTimer <= 0)
                {
                    RecoverHealth(healthRecoveryRate * Time.deltaTime); // 회복
                }
            }
        }
    }

    // 체력 회복 
    void RecoverHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); //최대 체력까지 회복 (난이도에 따라 조정 필요)
        Debug.Log($"Recovered Health: {amount}, Current Health: {currentHealth}"); //회복 확인 로그
        isRecovering = true; // 회복 상태로 설정
    }
}
