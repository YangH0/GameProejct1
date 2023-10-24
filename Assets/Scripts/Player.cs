using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigid;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletStart;
    [SerializeField] TraitAttack traitAttack;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject fireBullet;
    [SerializeField] GameObject IceBullet;
    [SerializeField] GameObject WindBullet;
    [SerializeField] GameObject ElecBullet;
    [SerializeField] GameObject o_Shield;
    [SerializeField] GameObject o_ShieldImpact;
    private GameObject attackObj;

    public float shieldCoolTime;
    public float shieldDamage;

    List<GameObject> bulletList = new List<GameObject>();


    private Vector3 playerVelocity;
    private Vector3 playerRotation;
    private Vector3 shootTarget;

    private float xMoveInput;
    private float zMoveInput;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    private float curAttackTime;
    public float maxAttackTime;
    public float maxHp;
    public float hp;
    private float exp = 0;
    public float expMulti;
    public float maxExp;
    public float ExpLevelUp;
    public float autoAttackDamage;

    private int attackType=0;


    private bool bIsRun;
    private bool bIsShield = false;
    private bool bIsShieldImpact = false;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hp = maxHp;
        uiManager.SetMaxHpUI(maxHp);
        uiManager.SetHpUI(hp);
        uiManager.SetExpUI(exp);
        uiManager.SetMaxExpUI(maxExp);
        attackObj = bullet;
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
        if (Input.GetMouseButton(0) && !bIsRun &&curAttackTime > maxAttackTime)
        {
            GameObject newObj = null;
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].activeSelf)
                {
                    bulletList[i].SetActive(true);
                    newObj = bulletList[i];
                    break;
                }
            }
            if(newObj == null)
            {
                newObj = Instantiate(attackObj);
                bulletList.Add(newObj);
            }

            if(attackType == 0)
            {
                newObj.transform.position = bulletStart.transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(shootTarget);

                Bullet bul = newObj.GetComponent<Bullet>();
                bul.damage = autoAttackDamage;
            }
            else if(attackType == 1)
            {
                newObj.transform.position = bulletStart.transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(shootTarget);

                ExplosiveBullet bul = newObj.GetComponent<ExplosiveBullet>();
                bul.damage = autoAttackDamage;
            }
            else if(attackType == 2)
            {
                newObj.transform.position = bulletStart.transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(shootTarget);

                Explosive bul = newObj.GetComponent<Explosive>();
                bul.damage = autoAttackDamage*1.3f;
                StartCoroutine(IceSword(newObj));
            }
            else if(attackType == 3)
            {
                newObj.transform.position = bulletStart.transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(shootTarget);

                PiercingBullet bul = newObj.GetComponent<PiercingBullet>();
                bul.damage = autoAttackDamage;
            }
            else if(attackType == 4)
            {
                newObj.transform.position = bulletStart.transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(shootTarget);

                Bullet bul = newObj.GetComponent<Bullet>();
                bul.damage = autoAttackDamage;
            }
            
            curAttackTime = 0;
        }
    }

    public void ChangeAutoAttack(int num)
    {
        for(int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].SetActive(false);
        }
        bulletList.Clear();
        switch (num)
        {
            case 1:
                attackObj = fireBullet;
                attackType = 1;
                break;
            case 2:
                attackObj = IceBullet;
                attackType = 2;
                break;
            case 3:
                attackObj = WindBullet;
                attackType = 3;
                break;
            case 4:
                attackObj = ElecBullet;
                attackType = 4;
                break;

        }
    }

    private void SetAim()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 15, Color.red);
        shootTarget = cam.transform.position + cam.transform.forward * 50;
        shootTarget = (shootTarget - transform.position).normalized;
        Debug.DrawRay(transform.position, shootTarget * 10, Color.red);
    }

    private void Move()
    {
        xMoveInput = Input.GetAxisRaw("Horizontal");
        zMoveInput = Input.GetAxisRaw("Vertical");
        CheckRun();

        playerVelocity = new Vector3(xMoveInput, 0, zMoveInput);
        playerVelocity = cam.transform.TransformDirection(playerVelocity);
        playerVelocity.y = 0;
        playerVelocity = playerVelocity.normalized;
        if(!bIsRun)
            playerRigid.velocity = playerVelocity * walkSpeed;
        else
            playerRigid.velocity = playerVelocity * runSpeed;
    }

    private void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bIsRun = true;
        }
        else
        {
            bIsRun = false;
        }
    }

    private void SetRotation()
    {
        playerRotation = cam.transform.forward;
        playerRotation.y = 0;

        transform.rotation = Quaternion.LookRotation(playerRotation);
    }

    public void GetDamage(float dmg)
    {
        if (bIsShield)
        {
            if (bIsShieldImpact) return;

            StartCoroutine(ShieldImpact());
            return;
        }
        hp -= dmg;
        uiManager.SetHpUI(hp);
        Debug.Log(hp);
        if(hp < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void GetExp(float m_exp)
    {
        exp += (m_exp*(expMulti*0.01f+1));

        if (exp >= maxExp) //·¹º§¾÷
        {
            exp -= maxExp;
            maxExp *= ExpLevelUp;
            uiManager.SetMaxExpUI(maxExp);
            uiManager.LevelUp();
        }
        uiManager.SetExpUI(exp);

    }

    private IEnumerator IceSword(GameObject obj)
    {
        yield return new WaitForSeconds(0.8f);
        obj.SetActive(false);
    }

    public IEnumerator Shield()
    {
        yield return new WaitForSeconds(shieldCoolTime);
        o_Shield.SetActive(true);
        bIsShield = true;
    }
    private IEnumerator ShieldImpact()
    {
        Explosive s = o_ShieldImpact.GetComponent<Explosive>();
        s.damage = shieldDamage;
        bIsShieldImpact = true;
        o_ShieldImpact.SetActive(true);
        yield return new WaitForSeconds(1f);
        o_Shield.SetActive(false);
        bIsShield = false;
        bIsShieldImpact = false;
        o_ShieldImpact.SetActive(false);
        StartCoroutine(Shield());
    }
}
