using System.Collections;
using UnityEngine;

public class EnemyBeam : MonoBehaviour
{
    public Transform laserTransform; // 자식 오브젝트인 레이저의 Transform
    public float expandTime = 1f; // y축 확장까지 걸리는 시간
    public float shrinkTime = 2f; // 나머지 축이 줄어드는 시간
    public float finalYScale = 100f; // y축 최종 크기
    public float shrinkDuration = 1f; // 나머지 축이 0으로 줄어드는데 걸리는 시간

    private Vector3 initialScale;

    void Start()
    {
        if (laserTransform == null)
        {
            Debug.LogError("Laser Transform is not assigned!");
            return;
        }

        initialScale = laserTransform.localScale;
        StartCoroutine(LaserBehavior());
    }

    IEnumerator LaserBehavior()
    {
        // 부모 오브젝트 생성 후 1초 대기
        yield return new WaitForSeconds(expandTime);

        // y축을 100으로 확장
        Vector3 expandedScale = new Vector3(initialScale.x, finalYScale, initialScale.z);
        laserTransform.localScale = expandedScale;

        // 2초 후 나머지 축을 줄이기 시작
        yield return new WaitForSeconds(shrinkTime);
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / shrinkDuration;

            // x축과 z축의 크기를 점점 줄임
            laserTransform.localScale = new Vector3(
                Mathf.Lerp(initialScale.x, 0f, progress),
                laserTransform.localScale.y,
                Mathf.Lerp(initialScale.z, 0f, progress)
            );

            yield return null;
        }

        // 레이저 오브젝트 삭제
        Destroy(laserTransform.gameObject);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}