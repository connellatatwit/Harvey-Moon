using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 target, mousePos, refVal, shakeOffset;
    [SerializeField] float cameraDist = 3.5f;
    [SerializeField] float smoothTime = .2f;
    float zStart;
    float shakeMag, shakeTimeEnd;
    Vector3 shakeVector;
    bool shaking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.position;
        zStart = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = CameraPosition();
        shakeOffset = UpdateShake();
        target = UpdateTargetPos();
        UpdateCameraPosition();
    }

    Vector3 CameraPosition()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        ret *= 2;
        ret -= Vector2.one;
        float max = .9f;
        if(Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }
        return ret;
    }
    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = player.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }
    void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVal, smoothTime);
        transform.position = tempPos;
    }
    public void Shake(Vector3 direction, float mag, float length)
    {
        shaking = true;
        shakeVector = direction;
        shakeMag = mag;
        shakeTimeEnd = length + Time.time;
    }
    Vector3 UpdateShake()
    {
        if(!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            return Vector3.zero;
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }
}
