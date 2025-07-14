using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inputscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed;
    [SerializeField] Animator Walk_Idle;
    void Update()
    {
        // 입력
        // 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // 애니메이터
        bool Walking = 0 < moveDirection.magnitude;
        // moveDirection.magnitude : 백터의 길이를 반환합니다.
        // 입력 값을 받으면 백터의 길이가 0보다 커지면서 True를 반환합니다.
        Walk_Idle.SetBool("Walking", Walking);
        // anicon_PicoChan이라는 애니메이터를 담을 변수를 생성합니다.
        // Bool 타입의 Parameter를 생성하였기에 SetBool함수를 사용합니다.

        //공격
        if (Input.GetKey(KeyCode.E)) // 'A'를 누르면 공격
        {
            Walk_Idle.SetBool("Attacking", true);
        }
        else
        {
            Walk_Idle.SetBool("Attacking", false);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKey(KeyCode.A)) // 'A'를 누르면 왼쪽 
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) // 'D'를 누르면 오른쪽 
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W)) // 'W'를 누르면 앞 
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) // 'S'를 누르면 뒤 
        {
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
    }*/
}