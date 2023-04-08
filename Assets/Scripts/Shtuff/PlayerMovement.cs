using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] List<Sprite> headSprites;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        HandleLook(mousePos);
    }
    private void FixedUpdate()
    {
        Vector2 dir;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        dir = new Vector2(x * speed, y * speed);

        rb.velocity = dir;

    }

    public void HandleLook(Vector3 mousePos)
    {
        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (angle > 0)
        {
            //Debug.Log("Angle: " + angle);
            if (angle <= 30 && angle >= -30) // Right
            {
                renderer.sprite = headSprites[0];
            }
            else if (angle > 30 && angle < 60) // Up Right
            {
                renderer.sprite = headSprites[1];
            }
            else if (angle >= 60 && angle <= 120) // Up
            {
                renderer.sprite = headSprites[2];
            }
            else if (angle > 120 && angle < 150) // Up left
            {
                renderer.sprite = headSprites[3];
            }
            else if (angle > 150) // Left
            {
                renderer.sprite = headSprites[4];
            }
        }
        else
        {
            if (angle <= 30 && angle >= -30) // Right
            {
                renderer.sprite = headSprites[0];
            }
            else if (angle < -30 && angle > -60) // Up Right
            {
                renderer.sprite = headSprites[7];
            }
            else if (angle <= -60 && angle >= -120) // Up
            {
                renderer.sprite = headSprites[6];
            }
            else if (angle < -120 && angle > -150) // Up left
            {
                renderer.sprite = headSprites[5];
            }
            else if (angle < -150) // Left
            {
                renderer.sprite = headSprites[4];
            }
        }
    }
}
