using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D z_collider2D;
    [SerializeField] ContactFilter2D z_filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    [SerializeField] GameObject hoverText;
    private bool hovered = false;

    protected virtual void Start()
    {
        z_collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        z_collider2D.OverlapCollider(z_filter, z_CollidedObjects);
        foreach (var item in z_CollidedObjects)
        {
            OnCollided(item.gameObject);
        }

        if (z_CollidedObjects.Count > 0)
            hovered = true;
        else
            hovered = false;

        hoverText.SetActive(hovered);
    }

    protected virtual void OnCollided(GameObject hitObj)
    {
        Debug.Log("Hit " + hitObj.name);
    }
}
