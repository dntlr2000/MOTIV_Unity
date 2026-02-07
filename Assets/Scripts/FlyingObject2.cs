using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject2 : MonoBehaviour
{
    //이 스크립트 자체가 더미데이터화
    public float moveSpeed = 5f;  // 이동 속도
    public float moveTime = 2f;   // 이동하는 시간
    public float waitTime = 1f;   // 멈춰 있는 시간

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool moveToCenter = false; // 중앙으로 이동 제어 플래그

    public Vector3 minBounds; // 최소 경계
    public Vector3 maxBounds; // 최대 경계

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MovePattern2());
    }

    IEnumerator MovePattern2()
    {
        while (true)
        {
            if (moveToCenter)
            {
                // 중앙으로 이동
                moveDirection = (-transform.position).normalized;
                rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            else
            {
                // 무작위 방향 설정
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                float timer = moveTime;
                float initialSpeed = moveSpeed;

                // 이동 실행
                while (timer > 0)
                {
                    // 시간에 따라 속도 감소
                    float currentSpeed = Mathf.Lerp(0, initialSpeed, timer / moveTime);

                    Vector3 newPosition = rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime;
                    // 경계 내로 위치 조정
                    newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
                    newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

                    rb.MovePosition(newPosition);
                    timer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }

                // 멈춤
                rb.velocity = Vector3.zero;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }


    // 외부에서 호출하여 중앙으로 이동 시작
    public void MoveToCenter()
    {
        moveToCenter = true;
    }

    // 중앙 이동을 멈추고 무작위 이동 재개
    public void ResumeRandomMove()
    {
        moveToCenter = false;
    }
}