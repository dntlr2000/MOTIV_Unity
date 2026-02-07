using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone_phase3 : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // 발사할 프로젝타일 프리팹 배열
    public GameObject laserPrefab;

    public GameObject missilePrefab;
    public string target = "Player";
    public float missileSpeed = 30f; // 미사일 이동 속도

    public float launchForce = 5.0f; // 발사 힘
    public int numberOfLaunches = 10; // 발사할 오브젝트의 총 개수

    public Enemy_Health enemyHealth; //체력 가져오기
    private float angryHealth1;
    private float angryHealth2;
    private bool isAngry1 = true;
    private bool isAngry2 = true;




    private void Start()
    {
        StartCoroutine(StartWithDelay());
        
    }

    private void Update()
    {
        //isAngry를 통해 다시 호출되지 않도록 함
        if (enemyHealth != null && enemyHealth.currentHealth <= angryHealth1 && !isAngry1) //1차 패턴 강화
        {
            StartCoroutine(SpawnLasers());
            isAngry1 = true;
        }

        if (enemyHealth != null && enemyHealth.currentHealth <= angryHealth2 && !isAngry2) //2차 패턴 강화
        {
            StartCoroutine(SpawnMissiles());
            isAngry2 = true;
        }
    }

    IEnumerator StartWithDelay()
    {
        // 3초의 초기 지연
        yield return new WaitForSeconds(3f);

        angryHealth1 = enemyHealth.maxHealth * 2 / 3;
        angryHealth2 = enemyHealth.maxHealth/ 3;
        isAngry1 = false;
        isAngry2 = false;

        // 지연 후 코루틴 시작
        StartCoroutine(LaunchProjectiles());

        //yield return new WaitForSeconds(20f);
        //StartCoroutine(SpawnLasers());
    }

    IEnumerator LaunchProjectiles()
    {
        //int ProjectileChoose = Random.range(0, projectilePrefabs.Length);
        //int forceControl = 2; //추후 오브젝트마다 속도를 다르게 하기 위한 장치
        while (true)
        {
            for (int i = 0; i < numberOfLaunches; i++)
            {
                // 무작위로 프로젝타일 선택
                GameObject selectedProjectile = projectilePrefabs[Random.Range(0, projectilePrefabs.Length)];

                // 발사 위치는 이 오브젝트의 위치
                Vector3 launchPosition = transform.position;

                // 무작위 방향 생성
                Vector3 randomDirection = Random.onUnitSphere;
                //randomDirection.y = Mathf.Abs(randomDirection.y); // 발사 방향을 주로 위쪽으로 제한하기 위해 y값을 양수로 설정

                // 프로젝타일 인스턴스 생성 및 발사
                Quaternion rotation = Quaternion.LookRotation(randomDirection);
                GameObject projectile = Instantiate(selectedProjectile, launchPosition, rotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb == null)
                {
                    rb = projectile.AddComponent<Rigidbody>();
                }

                //rb.useGravity = true;

                // Rigidbody에 힘 적용
                rb.AddForce(randomDirection * launchForce, ForceMode.Impulse);

                // 0.1초 대기

            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SpawnLasers()
    {
        while (true)
        {

            for (int i = 0; i < 16; i++)
            {
                // 32면체의 각 면 방향으로 레이저를 생성
                Vector3 direction = Random.onUnitSphere; // 단위 구의 표면에서 무작위 방향 선택
                Quaternion rotation = Quaternion.LookRotation(direction);

                // A의 위치에서 레이저 생성
                Instantiate(laserPrefab, transform.position, rotation);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SpawnMissiles()
    {
        
        while (true)
        {

            GameObject targetObject = GameObject.FindWithTag(target);
            if (targetObject != null)
            {
                Vector3[] directions = new Vector3[] {
                //Vector3.forward,  // 앞쪽
                //Vector3.back,     // 뒤쪽
                Vector3.left,     // 왼쪽
                Vector3.right     // 오른쪽
            };

                foreach (Vector3 dir in directions)
                {
                    // 미사일을 A의 위치에서 생성하고 방향을 설정
                    GameObject missileInstance = Instantiate(missilePrefab, transform.position, Quaternion.LookRotation(dir));

                    // 타겟을 추적하는 스크립트 추가
                    HomingMissile homingMissile = missileInstance.AddComponent<HomingMissile>();
                    homingMissile.targetTag = target;
                    homingMissile.speed = 30f;
                }
            }

            yield return new WaitForSeconds(8f);
        }
    }
}

