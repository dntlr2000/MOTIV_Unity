using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public Vector2 rotationSpeed = new Vector2(120.0f, 120.0f);
    public float minY = -20f;
    public float maxY = 80f;

    private float angleX = 0;
    private float angleY = 0;

    public float vibrationIntensity = 0.2f;
    public float vibrationTime = 1f;
    private bool isVibrating = false; // 진동 중인지 확인하는 플래그

    private bool isFar = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        angleX = angles.y;
        angleY = maxY;
        UpdateCameraPosition();
    }

    void LateUpdate() //진동 관련 코드는 더미데이터화가 되어버림
    {
        if (target && !isVibrating) // 진동 중이 아닐 때만 카메라 위치 갱신
        {
            //마우스로부터 x,y축 움직임을 받고
            angleX += Input.GetAxis("Mouse X") * rotationSpeed.x * Time.deltaTime;
            angleY -= Input.GetAxis("Mouse Y") * rotationSpeed.y * Time.deltaTime;
            //y축은 최대 높낮이를 설정
            angleY = Mathf.Clamp(angleY, minY, maxY);

            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition() //카메라 위치 갱신
    {
        Quaternion rotation = Quaternion.Euler(angleY, angleX, 0); //오일러 각도를 이용하여 회전 생성
        Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position; //위치 계산

        //위치, 방향 갱신
        transform.rotation = rotation;
        transform.position = position;
    }

    
    public void Vibrate()  //진동 관련 코드는 더미데이터화
    {
        StartCoroutine(DoVibration());
    }

    private IEnumerator DoVibration()
    {
        Vector3 originalPos = transform.position;
        isVibrating = true; // 진동 시작
        float elapsedTime = 0f;

        while (elapsedTime < vibrationTime)
        {
            float x = originalPos.x + Random.Range(-vibrationIntensity, vibrationIntensity);
            float y = originalPos.y + Random.Range(-vibrationIntensity, vibrationIntensity);
            float z = originalPos.z + Random.Range(-vibrationIntensity, vibrationIntensity);
            transform.position = new Vector3(x, y, z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos; // 진동이 끝나면 원래 위치로 복구
        isVibrating = false; // 진동 종료
    }

    public void controlDistance() //Tab 키 누르면 다른 스크립트에서 호출됨
    {
        if (isFar)
        {
            distance = (distance / 3);
        }
        else
        {
            distance = distance * 3;
        }
        isFar = !isFar;
    }
}
