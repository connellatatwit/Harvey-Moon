using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOnGround : MonoBehaviour
{
    [SerializeField] float pickUpSpeed;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GunInformation gunInfo;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
    }

    public void SetSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    public GunInformation PickUpGun(GunInformation oldGun)
    {
        GunInformation tempgun = GunInformation.DeepCopy(oldGun);
        oldGun = gunInfo;
        gunInfo = tempgun;

        SetInformation();

        return oldGun;
    }

    public void SetInformation()
    {
        renderer.sprite = gunInfo.GetImage();
        float rand = Random.Range(0, 361);
        renderer.gameObject.transform.eulerAngles = new Vector3(0, 0, rand);

        float randX = Random.Range(-10, 11);
        float randY = Random.Range(-10, 11);
        randX = randX / 10;
        randY = randY / 10;
        
        rb.velocity = new Vector2(randX, randY) * pickUpSpeed;
    }

    public void SetNewInformation(GunInformation info)
    {
        gunInfo = info;
    }
}
