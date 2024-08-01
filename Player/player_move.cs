using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    public int speed = 500;
    public int camSpeed = 100;

    Quaternion quaternion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
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
            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxisRaw("Jump") * -1, rb.velocity.z) * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxisRaw("Jump"), rb.velocity.z) * speed * Time.deltaTime;
        }

        //Player rotation
        if (player.transform.rotation.eulerAngles.x >= 80 && player.transform.rotation.eulerAngles.x <= 100)
        {
            quaternion.x = 79;
        }
        if (player.transform.rotation.eulerAngles.x >= 260 && player.transform.rotation.eulerAngles.x <= 280)
        {
            quaternion.x = 281;
        }
        if (player.transform.rotation.eulerAngles.x <= 80 || player.transform.rotation.eulerAngles.x >= 280)
        {
            quaternion.x = Input.GetAxisRaw("Mouse Y") * -1 * camSpeed * Time.deltaTime + player.transform.rotation.eulerAngles.x;
        }

        player.transform.rotation = Quaternion.Euler(quaternion.x, Input.GetAxisRaw("Mouse X") * camSpeed * Time.deltaTime + player.transform.rotation.eulerAngles.y, 0);

    }
}
