using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class AirVRServerExampleCan : MonoBehaviour {
    public float lifetime;

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }

    void Start() {
        StartCoroutine(DestroySelf());
    }

    public void Throw(Vector3 velocity, Vector3 torque) {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce(velocity, ForceMode.VelocityChange);
        rigid.AddTorque(torque, ForceMode.VelocityChange);
    }
}
