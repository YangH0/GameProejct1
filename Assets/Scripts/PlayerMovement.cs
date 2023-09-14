using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigid;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject buleltStart;

    private float xMoveInput;
    private float zMoveInput;
    private Vector3 playerVelocity;
    private Vector3 playerRotation;
    private Vector3 shootTarget;
    private int moveSpeed = 5;
    


    private void Awake()
    {
        
    }

    void Update()
    {
        //xMoveInput = Mathf.Lerp(xMoveInput, Input.GetAxis("Horizontal"), Time.deltaTime*10);
        //zMoveInput = Mathf.Lerp(zMoveInput, Input.GetAxis("Vertical"), Time.deltaTime*10);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        xMoveInput = Input.GetAxis("Horizontal");
        zMoveInput = Input.GetAxis("Vertical");

        playerVelocity = new Vector3(xMoveInput, 0, zMoveInput);
        playerVelocity = cam.transform.TransformDirection(playerVelocity);
        playerVelocity.y = 0;
        playerVelocity = playerVelocity.normalized;

        playerRotation = cam.transform.forward;
        playerRotation.y = 0;

        transform.rotation = Quaternion.LookRotation(playerRotation);

        playerRigid.velocity = playerVelocity * moveSpeed;


        Debug.DrawRay(cam.transform.position, cam.transform.forward * 15, Color.red);
        shootTarget = cam.transform.position + cam.transform.forward * 30;
        shootTarget = (shootTarget - transform.position).normalized;
        Debug.DrawRay(transform.position, shootTarget * 10, Color.red);



        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet,buleltStart.transform.position,Quaternion.LookRotation(shootTarget));
        }
    }
}
