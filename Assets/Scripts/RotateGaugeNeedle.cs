using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGaugeNeedle : MonoBehaviour
{

    public float zero_angle = 0f;
    public float max_angle = 0f;

    public float max_num;
    public float num;

    private void Update()
    {
        if (num > max_num) num = max_num;
        transform.eulerAngles = new Vector3(0, 0, GetRotationAngle());
    }


    private float GetRotationAngle()
    {
        float totalAngleSize = zero_angle - max_angle;
        float normalizedNum = num / max_num;

        return zero_angle - normalizedNum * totalAngleSize;
    }

}
