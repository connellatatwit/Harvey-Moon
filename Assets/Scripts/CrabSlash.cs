using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSlash : MonoBehaviour
{
    [SerializeField] Collider2D hitBox;
    [SerializeField] ContactFilter2D z_filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    private void Start()
    {
        z_filter.SetLayerMask(z_filter.layerMask);
    }
    public void Slash()
    {
        hitBox.OverlapCollider(z_filter, z_CollidedObjects);
        foreach (var item in z_CollidedObjects)
        {
            OnCollided(item.gameObject);
        }
    }
    public void OnCollided(GameObject obj)
    {
        obj.GetComponent<PlayerMovement>().Stun(1f);
    }
}
