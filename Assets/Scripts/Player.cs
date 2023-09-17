using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigid;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject buleltStart;

    private Vector3 playerVelocity;
    private Vector3 playerRotation;
    private Vector3 shootTarget;

    private float xMoveInput;
    private float zMoveInput;
    private float moveSpeed = 5;
    private float curAttackTime;
    public float maxAttackTime;
    public float hp;
    public float autoAttackDamage;

    private bool bDodge;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        curAttackTime += Time.deltaTime;
        SetAim();
        AutoAttack();
    }

    void FixedUpdate()
    {
        Move();
        SetRotation();
    }

    private void AutoAttack()
    {
        if (Input.GetMouseButton(0) && curAttackTime > maxAttackTime)
        {
            Instantiate(bullet, buleltStart.transform.position, Quaternion.LookRotation(shootTarget));
            curAttackTime = 0;
        }
    }

    private void SetAim()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 15, Color.red);
        shootTarget = cam.transform.position + cam.transform.forward * 30;
        shootTarget = (shootTarget - transform.position).normalized;
        Debug.DrawRay(transform.position, shootTarget * 10, Color.red);
    }

    private void Move()
    {
        xMoveInput = Input.GetAxisRaw("Horizontal");
        zMoveInput = Input.GetAxisRaw("Vertical");

        playerVelocity = new Vector3(xMoveInput, 0, zMoveInput);
        playerVelocity = cam.transform.TransformDirection(playerVelocity);
        playerVelocity.y = 0;
        playerVelocity = playerVelocity.normalized;
        playerRigid.velocity = playerVelocity * moveSpeed;
    }

    private void SetRotation()
    {
        playerRotation = cam.transform.forward;
        playerRotation.y = 0;

        transform.rotation = Quaternion.LookRotation(playerRotation);
    }

    public void GetDamage(float dmg)
    {
        if (bDodge)
            return;
        hp -= dmg;
        Debug.Log(hp);
    }

}
