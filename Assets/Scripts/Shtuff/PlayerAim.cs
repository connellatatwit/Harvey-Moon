using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Stats")]
    private float shootCd;
    private float shootTimer;

    [Header("Unity Stuff")]
    [SerializeField] Transform gun;
    [SerializeField] Transform gunTip;

    [SerializeField] SpriteRenderer render;
    [SerializeField] Transform cursorPos;
    [SerializeField] LayerMask gunLayer;

    [SerializeField] AudioSource shootSound; // TODO: Make the sound be dependant on the gunstats

    public GunInformation tempTestGun;
    public GunInformation tempTestGun2;
    public GunInformation tempTestGun3;
    private GunInformation gun1;
    private GunInformation gun2;
    private GunInformation currentGunInfo;

    private CameraControl cam;

    private bool canAim = true;


    [Header("New Weapon System Stuff")]
    [SerializeField] GameObject currentWeaponObj;
    private IWeapon currentWeapon;
    [SerializeField] GameObject newWeapon;
    [SerializeField] List<GameObject> weapons;
    private void Start()
    {
        cam = FindObjectOfType<CameraControl>();

        EquipGun(tempTestGun);
        //EquipGun(tempTestGun2);

        //InitPlayer();
        EquipNewWeapon(currentWeaponObj);

        Cursor.visible = false;
    }

    private void InitPlayer()
    {
        currentGunInfo = gun1;
        //InitEquippedGun();
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        HandleLook(mousePos);
        
        if (canAim)
        {
            if (Input.GetMouseButton(0))
            {
                if (shootTimer <= 0)
                {
                    //shootTimer = shootCd;
                    //HandleShoot(mousePos);
                    HandleWeapon(mousePos);
                }
            }
        }
 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SwapGun();
            EquipNewWeapon(newWeapon);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //EquipGroundGun();
        }

        shootTimer -= Time.deltaTime;
    }

    public void EquipGun(GunInformation gun)
    {
        if (gun1 == null)
            gun1 = gun;
        else if (gun2 == null)
            gun2 = gun;
        else
        {
            if(currentGunInfo == gun1)
            {
                gun1 = gun;
            }
            else if(currentGunInfo == gun2)
            {
                gun2 = gun;
            }
        }
        currentGunInfo = gun;
        InitEquippedGun();
    }
    public void EquipGroundGun()
    {
        Debug.Log("Equipping");
        //Find Gun On Ground
        Collider2D[] gunsOnGround = Physics2D.OverlapCircleAll(transform.position, .5f, gunLayer);

        foreach (Collider2D gun in gunsOnGround)
        {
            EquipGun(gun.GetComponent<GunOnGround>().PickUpGun(currentGunInfo));
        }
    }
    public void SwapGun()
    {
        if(currentGunInfo == gun1 && gun2 != null)
        {
            currentGunInfo = gun2;
        }
        else if (gun1 != null)
        {
            currentGunInfo = gun1;
        }
        InitEquippedGun();
    }
    void InitEquippedGun()
    {
        shootCd = currentGunInfo.FireCd();
        shootTimer = .2f;
        render.sprite = currentGunInfo.GetImage();
    }

    private void HandleLook(Vector3 mousePos)
    {
        cursorPos.position = mousePos;

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (angle < 20 || angle > 160)
        {
            render.sortingOrder = 1;    
        }
        else
        {
            render.sortingOrder = -1;
        }
        if (Mathf.Abs(angle) > 90)
            gun.transform.localScale = new Vector3(1, -1, 1);
        else
            gun.transform.localScale = new Vector3(1, 1, 1);

        gun.eulerAngles = new Vector3(0, 0, angle);
    }
    private void HandleShoot(Vector3 mousePos)
    {
        GameObject bullet = Instantiate(currentGunInfo.GetBullet(), transform.position, Quaternion.identity);
        bullet.transform.position = gunTip.transform.position;
        bullet.GetComponent<IBullet>().InitBullet(mousePos, currentGunInfo.GetDamage(), currentGunInfo.GetSpeed(), true, currentGunInfo.GetPierce()+1);

        shootSound.Play();
        cam.Shake((transform.position - gunTip.position).normalized, currentGunInfo.GetShakeStrength(), .05f);
    }
    private void HandleWeapon(Vector3 mousePos)
    {
        currentWeapon.Activate(mousePos);
        shootTimer = shootCd;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
    public void CanAim(bool aimin)
    {
        canAim = !aimin;
    }

    public void EquipNewWeapon(GameObject obj)
    {
        currentWeapon = obj.GetComponent<IWeapon>();
        currentWeaponObj = obj;
        render.sprite = currentWeapon.Image();
        shootCd = currentWeapon.CD;
    }
    public void NextWeapon(int pos)
    {
        EquipNewWeapon(weapons[pos]);
    }
}
