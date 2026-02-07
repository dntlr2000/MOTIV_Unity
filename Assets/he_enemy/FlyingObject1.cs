using UnityEngine;

public class FlyObject1 : MonoBehaviour
{
    public float speed = 5f; // 최대 이동 속도
    public float directionChangeInterval = 2f; // 방향 전환 주기
    public float maxRotationSpeed = 100f; // 최대 회전속도 (필요 유무 확인)

    private Vector3 currentDirection;
    private float timeSinceLastChange = 0f; // 방향 전환 후 경과 시간
    private float currentSpeed; // 현재 속도

    public Vector3 minBounds;
    public Vector3 maxBounds;

    void Start()
    {
        SetRandomDirection(); // 이동 방향 랜덤
        currentSpeed = speed; // 초기 속도 설정
    }

    void Update()
    {
        // 방향 전환 주기에 따라 방향 변경
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= directionChangeInterval - 1)
        {
            SetRandomDirection();
            currentSpeed = speed; // 방향 변경 시 속도 초기화
            timeSinceLastChange = 0f;
        }
        else
        {
            // 시간이 지남에 따라 속도 감소
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime / (directionChangeInterval - 1));
        }

        Move();
        Rotate();
    }

    void Move()
    {
        // 속도, 시간에 비례해 이동
        Vector3 newPosition = transform.position + currentDirection * currentSpeed * Time.deltaTime;

        // x, y, z 축의 위치를 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        transform.position = newPosition;
    }

    void SetRandomDirection()
    {
        // 방향 랜덤
        currentDirection = Random.onUnitSphere;
        currentDirection.Normalize();
    }

    void Rotate()
    {
        // 현재 회전 방향 계산
        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
        // 현재 회전에서 목표 회전으로 부드럽게 회전
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
    }
}
