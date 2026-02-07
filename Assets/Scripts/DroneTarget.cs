using UnityEngine;

public class DroneTarget : MonoBehaviour
{
    public Transform target; // 따라갈 대상 오브젝트
    public float speed = 5.0f; // 따라가는 속도
    public bool smoothFollow = false; // 부드러운 따라가기 활성화 여부
    public float smoothSpeed = 0.125f; // 부드러운 따라가기 속도

    void Update()
    {
        if (target != null)
        {
            if (smoothFollow)
            {
                // 부드러운 따라가기를 사용할 경우
                Vector3 desiredPosition = target.position;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
            else
            {
                // 일반 따라가기를 사용할 경우 : 항상 그 오브젝트 위치랑 겹침
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }
}
