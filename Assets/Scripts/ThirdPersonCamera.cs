using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 (캐릭터)
    public float distance = 5.0f; // 대상으로부터의 거리
    public Vector2 rotationSpeed = new Vector2(120.0f, 120.0f); // 카메라 회전 속도 (X, Y)
    public float minY = -20f; // Y축 회전 최소 각도
    public float maxY = 80f; // Y축 회전 최대 각도

    private float angleX = 0; // 카메라 X축 회전 각도
    private float angleY = 0; // 카메라 Y축 회전 각도

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        angleX = angles.y;
        angleY = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            // 마우스 입력을 기반으로 각도 업데이트
            angleX += Input.GetAxis("Mouse X") * rotationSpeed.x * Time.deltaTime;
            angleY -= Input.GetAxis("Mouse Y") * rotationSpeed.y * Time.deltaTime;
            angleY = Mathf.Clamp(angleY, minY, maxY);

            // 카메라 회전과 위치를 업데이트
            Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}