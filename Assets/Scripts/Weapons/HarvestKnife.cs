using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestKnife : MonoBehaviour, IWeapon
{
    [SerializeField] AudioSource shootSound;
    [SerializeField] Sprite image;
    [SerializeField] Animator anim;
    [Header("Collider Stuff")]
    [SerializeField] Collider2D colliderz;
    [SerializeField] ContactFilter2D z_filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>();

    [Header("Stats")]
    [SerializeField] int damage;
    [SerializeField] float cd;

    public float CD => cd;

    //private CameraControl cam;
    private void Start()
    {
        z_filter.SetLayerMask(z_filter.layerMask);
    }
    public void Activate(Vector3 mousePos)
    {
        anim.SetTrigger("Slash");
        colliderz.OverlapCollider(z_filter, z_CollidedObjects);
        foreach (Collider2D item in z_CollidedObjects)
        {
            item.GetComponent<Enemy>().TakeDamage(damage);
        }
        shootSound.Play();
        //cam.Shake((transform.position - gunTip.position).normalized, currentGunInfo.GetShakeStrength(), .05f);
    }

    public void Slash()
    {

    }
    public Sprite Image()
    {
        return image;
    }
}
