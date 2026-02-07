using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public Rigidbody rb;            //캐릭터 리지드바디
    public GameObject player;       //플레이어블 캐릴터
    public int speed = 400;         //플레이어 기본 속도
    public GameObject cam;          //플레이어 회전속도
    int player_speed;               //플레이어 현재 속도

    Quaternion quaternion;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        //player = GameObject.Find("Player");
        player_speed = speed;       //속도 초기화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Player position
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxisRaw("Jump") * -1, rb.velocity.z) * player_speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxisRaw("Jump"), rb.velocity.z) * player_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftControl))   //달리기
        {
            player_speed = speed + 400;
        }
        else
        {
            player_speed = speed;
        }

        //Player rotation
        player.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
    }
}
