using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    private Transform player;
    private float baseCamSize;
    private bool mouseFollow = true;
    Vector3 target, mousePos, refVal, shakeOffset;
    [SerializeField] float cameraDist = 3.5f;
    [SerializeField] float smoothTime = .2f;
    float zStart;
    float shakeMag, shakeTimeEnd;
    Vector3 shakeVector;
    bool shaking;

    [SerializeField] Transform testPos;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Player").transform;
        player = currentTarget;
        target = currentTarget.position;
        zStart = transform.position.z;
        cam = GetComponent<Camera>();
        baseCamSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = CameraPosition();
        shakeOffset = UpdateShake();
        if (mouseFollow)
            target = UpdateTargetPos();
        else
            target = UpdateTargetPos2();
        UpdateCameraPosition();

        if (Input.GetKeyDown(KeyCode.P))
        {
            NewTarget(testPos, 7, false);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetCam();
        }
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
        Vector3 ret = currentTarget.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }
    Vector3 UpdateTargetPos2()
    {
        Vector3 ret = currentTarget.position;
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

    public void NewTarget(Transform newTarget, int size, bool camFollow)
    {
        currentTarget = newTarget;
        cam.orthographicSize = size;
        mouseFollow = camFollow;
    }
    public void ResetCam()
    {
        currentTarget = player;
        cam.orthographicSize = baseCamSize;
        mouseFollow = true;
    }
}
