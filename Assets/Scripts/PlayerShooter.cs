using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform projectilePrefab;  // 발사될 프로젝타일의 프리팹
    public Transform target;            // 목표 오브젝트 C
    public float launchForce = 10f;     // 발사 힘
    public float launchInterval = 2f;   // 발사 간격 (초)
    public float projectileLifetime = 5f;  // 발사체 생존 시간

    private float launchTimer;          // 발사 타이머
    public bool canshoot;               // 발사 가능 여부 확인


    private void Start()
    {
        canshoot = true;
    }

    void Update()
    {
        // 타이머 업데이트
        launchTimer += Time.deltaTime;

        // 발사 간격이 되면 발사
        if (launchTimer >= launchInterval && canshoot)
        {
            LaunchProjectile();
            launchTimer = 0;  // 타이머 리셋
        }

        //transform.Rotate(1, 0, 0);
        
    }


    void LaunchProjectile()
    {
        if (target == null) return;  // 목표가 없으면 함수를 종료

        // 발사 위치를 오브젝트의 전방으로 조금 이동
        Vector3 launchPosition = transform.position + transform.forward * 2.0f;  // 1.0f는 오브젝트의 전방으로 1 유닛만큼 이동

        // 프로젝타일 인스턴스 생성 및 발사 위치와 방향 설정
        Transform projectileInstance = Instantiate(projectilePrefab, launchPosition, Quaternion.identity);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = projectileInstance.gameObject.AddComponent<Rigidbody>();  // Rigidbody가 없다면 추가
        }

        // 발사 방향 설정
        Vector3 direction = (target.position - launchPosition).normalized;
        projectileInstance.LookAt(target);  // 프로젝타일이 목표를 바라보도록 설정
        //projectileInstance.Rotate(0, -90, 90); // 방향 보정

        rb.AddForce(direction * launchForce, ForceMode.Impulse);

        // 발사된 오브젝트를 일정 시간 후에 파괴
        Destroy(projectileInstance.gameObject, projectileLifetime);
    }
}
