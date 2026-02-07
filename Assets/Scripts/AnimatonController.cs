using System.Collections;
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
        //입력하기
        if (isMoving > 0) {
            animator.SetBool("goFront", true);
        }
        else {
            animator.SetBool("goFront", false);
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            
            animator.SetBool("goDown", true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
           
            animator.SetBool("goUp", true);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("goUp", false);
                animator.SetBool("goDown", true);
            }


        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            animator.SetBool("ifDash", true);
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            isMoving += 1; //모든 조작키를 떼야 통상 모션이 나오기 위해 선택한 방법
        }


        

        //떼기
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("goDown", false);
            animator.SetBool("goUp", false);
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("goUp", false);

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            animator.SetBool("ifDash", false);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {

            //animator.SetBool("goFront", false);
            isMoving -= 1;
            if (isMoving < 0) {
                isMoving = 0;
            }
        }

        
    }

    public void startHitMotion() //피격 모션이 나오도록 코루틴 호출하는 함수. 외부에서 접근 가능함
    {
        StartCoroutine(hitMotion());
    }

    IEnumerator hitMotion()
    {
        Debug.Log("getHit!");
        animator.SetBool("getHit", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("getHit", false);
    }

}
