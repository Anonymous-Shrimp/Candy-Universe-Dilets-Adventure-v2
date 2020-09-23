using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformQuest
{
    public class Cam : MonoBehaviour
    {

        public Transform target;

        public bool lookAt = false;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            if (lookAt) { transform.LookAt(target); }
        }

    }
}
