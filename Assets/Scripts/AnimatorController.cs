using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private int isMoving = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving > 0) {
            animator.SetBool("goFront", true);
        }
        else {
            animator.SetBool("goFront", false);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 'Jump' 애니메이션 실행
            //animator.SetTrigger("goDown");
            animator.SetBool("goDown", true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // 'Jump' 애니메이션 실행
            //animator.SetTrigger("goUp");
            animator.SetBool("goUp", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 'Jump' 애니메이션 실행
                //animator.SetTrigger("goDown");
                animator.SetBool("goUp", false);
                animator.SetBool("goDown", true);
            }


        }
       
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            // 'Run' 애니메이션으로 전환
            //animator.SetBool("goFront", true);
            isMoving += 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("goDown", false);
            animator.SetBool("goUp", false);
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("goUp", false);

        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {

            //animator.SetBool("goFront", false);
            isMoving -= 1;
        }
    }
}
