using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Animator anim_Santa;

    void Update()
    {
        // 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 회전
        // 입력값(moveDirection)이 0이 아닌경우에는 우리는 움직이는것으로 판단한다.
        // moveDirection이 0일때는 - Idle 애니메이션
        // moveDirection이 0이 아닌경우 - Move 애니메이션

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim_Santa.SetTrigger("ATTACK");
        }

        // 애니메이터
        bool isWalk = 0 < moveDirection.magnitude;
        // moveDirection.magnitude : 백터의 길이를 반환합니다.
        // 입력 값을 받으면 백터의 길이가 0보다 커지면서 True를 반환합니다.
        anim_Santa.SetBool("ISWALK", isWalk); // SetBool(파라미터의 이름, 설정 값)

        // anicon_Santa.SetInteger() : Int
        // anicon_Santa.SetFloat() : Float
        // anicon_Santa.SetBool() : Bool
        // anicon_Santa.SetTrigger() : Trigger

        // anicon_PicoChan이라는 애니메이터를 담을 변수를 생성합니다.
        // Bool 타입의 Parameter를 생성하였기에 SetBool함수를 사용합니다.
    }
}
