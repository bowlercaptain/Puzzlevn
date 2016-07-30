using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Jumpman : MonoBehaviour
{

    Rigidbody rb;
    public float jumpHeight;//this name is a lie
    public float runSpeed;//this name is also a lie

    bool currJumps = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }


    void Update()
    {
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal")*runSpeed, 0, 0));
        if (Input.GetButtonDown("Jump") && currJumps)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            currJumps = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.impulse.y > Mathf.Abs(col.impulse.x))//This assumes gravity points downwards but THAT'S FINE
        {
            print("jimp agaqionj");
            currJumps = true;
        }
    }
}
