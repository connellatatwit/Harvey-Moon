using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStats ps;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] List<Sprite> headSprites;
    private Rigidbody2D rb;

    private bool moonUp = true;

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        moonUp = true;
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
        if (moonUp)
        {
            Vector2 dir;
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            dir = new Vector2(x * ps.Speed, y * ps.Speed);

            rb.velocity = dir;
        }
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

    public void MoonDown(bool moonDown)
    {
        moonUp = !moonDown;
        rb.velocity = Vector2.zero;
    }
}
