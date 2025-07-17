using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ĳ����")]
    [SerializeField] Rigidbody rigid;
    [SerializeField] Transform character;
    [SerializeField] Animator anicon;
    [SerializeField] float moveSpeed; // �̵� �ӵ�

    [Header("ī�޶�")]
    public Transform camArm;
    public float camAngleSpeed;

    Vector2 moveInput; // �Է¹��� �̵� ������ ����� ����

    [Header("����")]
    public float jumpPower; // ������
    public int MaxJumpCount; // �ִ� ���� Ƚ��
    [SerializeField] int nowJumpCount; // ���� ���� Ƚ��

    bool isJump;


    void Awake()
    {
        nowJumpCount = MaxJumpCount;
        isJump = false;
    }

    void Update()
    {
        Move();
        LookAround();
        Jump();
        Attack();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && 0 < nowJumpCount)
        {
            rigid.velocity = Vector3.up * jumpPower;
            nowJumpCount--;
            isJump = true;
            anicon.SetTrigger("JUMP");
            anicon.SetBool("JUMPEND", false);
        }

        if (rigid.velocity.y <= 0 && Physics.Raycast(character.position + (Vector3.up * 0.1f), Vector3.down, 0.2f, LayerMask.GetMask("Ground")))
        {
            nowJumpCount = MaxJumpCount;
            isJump = false;
            anicon.SetBool("JUMPEND", true);
        }
    }

    void Move()
    {
        // �Է�
        Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.x = Mathf.MoveTowards(moveInput.x, rawInput.x, Time.deltaTime * 10);
        moveInput.y = Mathf.MoveTowards(moveInput.y, rawInput.y, Time.deltaTime * 10);
        float moveValue = moveInput.magnitude;

        // �̵�
        if (moveValue != 0)
        {
            Vector3 lookForward = new Vector3(camArm.forward.x, 0f, camArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(camArm.right.x, 0f, camArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            character.forward = moveDir;

            rigid.MovePosition(character.position + (moveDir * Time.deltaTime * moveSpeed));

            if (moveInput != Vector2.zero)
            {
                Vector3 inputForward = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(inputForward);
                character.rotation = Quaternion.Slerp(character.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        // �ִϸ��̼�
        if(isJump == false)
        {
            anicon.SetBool("ISWALK", moveValue != 0);
        }
    }

    public void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * camAngleSpeed, Input.GetAxis("Mouse Y") * camAngleSpeed);
        Vector3 camAngle = camArm.rotation.eulerAngles;

        float camAngleX = camAngle.x - mouseDelta.y;

        if (camAngleX < 180f)
        {
            camAngleX = Mathf.Clamp(camAngleX, -1f, 70f);
        }
        else
        {
            camAngleX = Mathf.Clamp(camAngleX, 340f, 361f);
        }

        camArm.rotation = Quaternion.Euler(camAngleX, camAngle.y + mouseDelta.x, camAngle.z);

    }

    [SerializeField] int attackRange;
    [SerializeField] int attackAngle;

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anicon.SetTrigger("ATTACK");
        }
    }

    public void AttackMonster()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitColliders)
        {
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                Vector3 directionToTarget = (monster.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(transform.forward, directionToTarget);

                float angleThreshold = Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad);

                if (dot >= angleThreshold)
                {
                    // ���� �� ���Ϳ��� ����
                    monster.Damaged();
                }
            }
        }
    }

    // ���� ���� �ð�ȭ (Scene �信���� ����)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 forward = transform.forward;
        Quaternion leftRotation = Quaternion.Euler(0, -attackAngle / 2, 0);
        Quaternion rightRotation = Quaternion.Euler(0, attackAngle / 2, 0);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * attackRange);
    }
}
