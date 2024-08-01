using UnityEngine;

public class FlyObject1 : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    public float directionChangeInterval = 2f; // 방향 전환 주기
    public float maxRotationSpeed = 100f; // 최대회전속도 (필요 유무 확인)

    private Vector3 currentDirection;
    private float timeSinceLastChange = 0f; //방향 전환 후 경과 시간

    void Start()
    {
        SetRandomDirection(); //이동 방향 랜덤
    }

    void Update()
    {
        // 방향 전환 주기에 따라 방향 변경
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= directionChangeInterval) //방향 전환 후 경과 시간이 전환 주기 이상이어야 행동함
        {
            SetRandomDirection();
            timeSinceLastChange = 0f;
        }

        Move();
        Rotate();
    }

    void Move()
    {
        // 속도,시간에 비례해 이동
        transform.position += currentDirection * speed * Time.deltaTime;
    }

    void SetRandomDirection()
    {
        // 방향 랜덤
        currentDirection = Random.onUnitSphere;

        // 속도 일정
        currentDirection.Normalize();


        // 로그 확인
        Debug.Log($"New Direction: {currentDirection}");
    }

    void Rotate()
    {
        // 현재 회전 방향 계산
        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);

        // 현재 회전에서 목표 회전으로 부드럽게 회전
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
    }
}
