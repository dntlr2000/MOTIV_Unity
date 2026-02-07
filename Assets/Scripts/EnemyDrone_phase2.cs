using System.Collections;
using UnityEngine;

public class EnemyDrone_phase2 : MonoBehaviour
{
    public GameObject laserPrefab; // 레이저 오브젝트의 프리팹
    public GameObject missilePrefab; // 미사일 오브젝트의 프리팹
    public GameObject ballPrefab; // Ball 오브젝트의 프리팹
    public string target;
    public float laserSpawnInterval = 6f; // 레이저 생성 간격
    public float missileSpeed = 10f; // 미사일 이동 속도
    public float ballSpeed = 5f; // Ball 오브젝트의 이동 속도
    public int numberOfBalls = 9; // 발사할 Ball 오브젝트의 개수
    public float ballSpawnDistance = 1f; // Ball 오브젝트가 타겟 주변에서 생성되는 거리

    void Start()
    {
        StartCoroutine(SpawnLasers());
        StartCoroutine(SpawnMissiles());
        StartCoroutine(ShootBalls());
    }

    IEnumerator SpawnLasers()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserSpawnInterval);

            for (int i = 0; i < 32; i++)
            {
                // 32개의 무작위 방향으로 발사되는 레이저
                Vector3 direction = Random.onUnitSphere; // 단위 구의 표면에서 무작위 방향 선택
                Quaternion rotation = Quaternion.LookRotation(direction);

                // A의 위치에서 레이저 생성
                Instantiate(laserPrefab, transform.position, rotation);
            }

            //SpawnBalls();
            /*
            if (target != null)
            {
                // 미사일을 A의 위치에서 생성
                GameObject missileInstance = Instantiate(missilePrefab, transform.position, Quaternion.identity);

                // 타겟을 추적하는 스크립트 추가
                HomingMissile homingMissile = missileInstance.AddComponent<HomingMissile>();
                homingMissile.targetTag = target;
                homingMissile.speed = missileSpeed;

                // 타겟 주변을 향해 Ball 오브젝트 발사
            
                SpawnBalls();
            }
            */

        }
    }
    
    IEnumerator SpawnMissiles()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {

            GameObject targetObject = GameObject.FindWithTag(target); //태그를 통해 플레이어 식별
            if (targetObject != null)
            {
                Vector3[] directions = new Vector3[] { //처음에 발사 주체에서 발사되는 방향 설정
                Vector3.forward,  // 앞쪽
                Vector3.back,     // 뒤쪽
                Vector3.left,     // 왼쪽
                Vector3.right     // 오른쪽
            };

                foreach (Vector3 dir in directions) //각 방향으로 발사
                {
                    // 미사일을 A의 위치에서 생성하고 방향을 설정
                    GameObject missileInstance = Instantiate(missilePrefab, transform.position, Quaternion.LookRotation(dir));

                    // 타겟을 추적하는 스크립트 추가 : HomingMissile
                    HomingMissile homingMissile = missileInstance.AddComponent<HomingMissile>();
                    //hominhMissile 객체의 필드값을 조절하여 타겟 설정 및 속도 조절
                    homingMissile.targetTag = target;
                    homingMissile.speed = 30f;
                }
            }

            yield return new WaitForSeconds(12f);
        }
    }

    IEnumerator ShootBalls()
    {
        while (true)
        {
            //yield return new WaitForSeconds(laserSpawnInterval);
            while (true)
            {
                yield return new WaitForSeconds(2f);

                SpawnBalls();
            }


        }
    }


    void SpawnBalls()
    {
        GameObject targetObject = GameObject.FindWithTag(target);
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.transform.position;

            for (int i = 0; i < numberOfBalls; i++)
            {
                // Ball 오브젝트를 A의 위치에서 생성
                GameObject ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);

                // Ball 오브젝트를 타겟 주변을 향해 발사
                Vector3 randomOffset = Random.onUnitSphere * ballSpawnDistance;
                Vector3 targetDirection = (targetPosition + randomOffset - transform.position).normalized;

                Rigidbody rb = ballInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // 기본 속도에 무작위 요소를 추가하여 속도 계산
                    float randomSpeedFactor = Random.Range(0.8f, 1.2f); // 0.8부터 1.2 사이의 무작위 계수
                    rb.velocity = targetDirection * ballSpeed * randomSpeedFactor;
                }
            }
        }
    }

}
