using System.Collections;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // 초기 발사될 프로젝타일의 프리팹
    public GameObject secondaryProjectilePrefab; // 2초 후 발사될 추가 프로젝타일의 프리팹
    public float initialLaunchForce = 10f; // 초기 발사 힘
    public float secondaryLaunchForce = 5f; // 추가 발사 힘
    public float launchInterval = 3f; // 발사 간격 (초)
    public float delayForSecondaryLaunch = 2f; // 초기 발사 후 추가 발사까지의 지연 시간
    public int numberOfProjectiles = 128; // 발사될 추가 프로젝타일의 수

    private float launchTimer; // 발사 타이머

    void Update()
    {
        launchTimer += Time.deltaTime;

        if (launchTimer >= launchInterval)
        {
            LaunchProjectile();
            launchTimer = 0;
        }
    }

    void LaunchProjectile() //파란색 구체 발사
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation); //오브젝트 생성
            Rigidbody rb = projectile.GetComponent<Rigidbody>(); //발사한 오브젝트의 rigidBody 불러오기
            if (rb == null)
            {
                rb = projectile.AddComponent<Rigidbody>();
            }
            rb.AddForce(transform.forward * initialLaunchForce, ForceMode.Impulse); //생성한 오브젝트를 앞으로 발사
            //1페이즈를 담당하는 오브젝트는 Aim Constraint로 항상 플레이어를 바라보고 있기 때문에 따로 조준 할 필요 없다.
            StartCoroutine(LaunchSecondaryProjectiles(projectile.transform)); //빨간 구체 퍼지는 메커니즘 추가
        }
    }

    IEnumerator LaunchSecondaryProjectiles(Transform initialProjectile) //빨간색 구체로 퍼져나감
    {
        for (int k = 0; k < 2; k++) //2회 수행시 종료
        {
            yield return new WaitForSeconds(delayForSecondaryLaunch); //퍼지는 간격

            //균등하게 퍼지도록 방향 조절. 수학의 영역
            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float t = (float)i / numberOfProjectiles;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleIncrement * i;

                Vector3 direction = new Vector3(
                    Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                    Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                    Mathf.Cos(inclination)
                );

                Quaternion rotation = Quaternion.LookRotation(direction);
                GameObject secondaryProjectile = Instantiate(secondaryProjectilePrefab, initialProjectile.position, rotation);
                Rigidbody rb = secondaryProjectile.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = secondaryProjectile.AddComponent<Rigidbody>();
                }
                rb.AddForce(direction * secondaryLaunchForce, ForceMode.Impulse);
            }
        }
        
    }
}
