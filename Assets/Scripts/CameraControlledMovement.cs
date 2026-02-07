using UnityEngine;

public class CameraControlledMovement : MonoBehaviour
{
    public Transform cameraTransform;   // 사용자가 지정할 카메라 Transform
    public float moveSpeed = 5f;        // 기본 이동 속도
    public float verticalSpeed = 3f;    // 상하 이동 속도
    public float sprintMultiplier = 2f; // 빠른 이동 속도 배수 (예: 2배 속도)

    public Vector3 minBounds;
    public Vector3 maxBounds;

    public ThirdPersonCamera thirdPersonCamera; //tab키로 이쪽 스크립트에 있는 카메라 조절 함수 호출용

    void Update()
    {
        MoveCharacter(); //프레임마다 움직임 갱신
    }

    void MoveCharacter()
    {
        if (!cameraTransform) //할당이 안된 경우 리턴해서 종료
            return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; // 수평 이동에만 집중
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.zero; //기본 상태: 무방향

        if (Input.GetKey(KeyCode.W)) //조작을 통해 방향 갱신
        {
            direction += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += right;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) //Tab키로 thirdPersonCamera에 있는 스크립트로 카메라 거리 조절 함수 호출
        {
            if (thirdPersonCamera != null)
            {
                thirdPersonCamera.controlDistance();
            }
            else
            {
                Debug.LogError("ThirdPersonCamera is not assigned!");
            }
        }

        // 대시 기능 (Left Ctrl 누르면 속도 증가)
        float currentSpeed = moveSpeed; //기본 이동속도로 현재 이동속도 설정
        if (Input.GetKey(KeyCode.LeftShift)) //shift를 누르는 동안 현재 이동속도의 n배수
        {
            currentSpeed *= sprintMultiplier;
        }

        //캐릭터 이동 방향으로 회전(수평)
        if (direction != Vector3.zero) //방향이 없을 때를 제외하고
        {
            // 캐릭터 방향을 이동 방향으로 회전시키기
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
            //방향이 바뀌었을 때 프레임마다 15% 돌도록 하여 비교적 자연스럽게 회전하도록 함
        }

        // 수직 이동 처리
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                direction += Vector3.down * verticalSpeed; // space와 LeftControl이 동시에 입력이 들어올 경우 아래로 이동이 우선됨
            }
            else
            {
                direction += Vector3.up * verticalSpeed; // 위로 이동
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            direction += Vector3.down * verticalSpeed; // 아래로 이동
        }


        //transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
        Vector3 newPosition = transform.position + direction * currentSpeed * Time.deltaTime; //위치 갱신

        // x, y, z 축의 위치를 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        transform.position = newPosition;
    }
}
