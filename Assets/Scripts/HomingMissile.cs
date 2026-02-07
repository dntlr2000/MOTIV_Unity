using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public string targetTag = "Player"; // 타겟으로 설정할 태그
    public float speed = 10f; // 미사일 속도
    public float rotateSpeed = 100f; // 회전 속도

    private Transform target; // 추적할 타겟
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Rigidbody가 없으면 추가
        }
        rb.useGravity = false; // 중력 영향 받지 않도록 설정

        // 특정 태그를 가진 오브젝트를 찾아 타겟으로 설정
        GameObject targetObject = GameObject.FindWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
    }

    void FixedUpdate() //물리 연산과 관련된거기 때문에 Update 대신 FixedUpdate 사용.
    {
        if (target == null) return;

        // 타겟 방향 계산
        Vector3 direction = (target.position - rb.position).normalized;

        // 타겟을 향해 회전
        Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
        //Vector3.Cross = 외적값
        rb.angularVelocity = rotationAmount * rotateSpeed * Time.fixedDeltaTime; //회전 속도
      

        // 타겟을 향해 전진
        rb.velocity = transform.forward * speed;
    }
}