using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public float rotationSpeed = 80.0f;

    public float upwardForce = 10f;
    public float randomSpread = 5f;

    public float maxFallSpeed = -10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 위로 살짝 발사하는 힘을 적용
            
            Vector3 initialForce = new Vector3(
                Random.Range(-randomSpread, randomSpread), // X축으로 무작위
                upwardForce, // Y축으로 위로 힘
                Random.Range(-randomSpread, randomSpread) // Z축으로 무작위
            );

            // 힘 적용
            rb.AddForce(initialForce, ForceMode.Impulse);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        // Y축 속도가 최대 하강 속도보다 빠르면 제한
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);
        }
    }
    
}
