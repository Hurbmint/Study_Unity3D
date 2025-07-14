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
        // �Է�
        // �Է�
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // �̵�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ȸ��
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // �ִϸ�����
        bool Walking = 0 < moveDirection.magnitude;
        // moveDirection.magnitude : ������ ���̸� ��ȯ�մϴ�.
        // �Է� ���� ������ ������ ���̰� 0���� Ŀ���鼭 True�� ��ȯ�մϴ�.
        Walk_Idle.SetBool("Walking", Walking);
        // anicon_PicoChan�̶�� �ִϸ����͸� ���� ������ �����մϴ�.
        // Bool Ÿ���� Parameter�� �����Ͽ��⿡ SetBool�Լ��� ����մϴ�.

        //����
        if (Input.GetKey(KeyCode.E)) // 'A'�� ������ ����
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
        if (Input.GetKey(KeyCode.A)) // 'A'�� ������ ���� 
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) // 'D'�� ������ ������ 
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W)) // 'W'�� ������ �� 
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) // 'S'�� ������ �� 
        {
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
    }*/
}