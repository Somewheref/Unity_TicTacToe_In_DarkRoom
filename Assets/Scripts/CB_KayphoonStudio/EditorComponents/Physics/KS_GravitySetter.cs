using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_GravitySetter : MonoBehaviour
{
    public float gravity = -9.81f;

    private void Start() {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}
