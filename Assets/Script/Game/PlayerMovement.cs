using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;
    public float movementSpeed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       if (PV.IsMine) {
           BasicRotation();
           BasicMovement();
       }
    }

    void BasicMovement() {
         if (Input.GetKey(KeyCode.Z)){
            myCC.Move(transform.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D)){
            myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S)){
            myCC.Move(-1 * transform.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q)){
            myCC.Move(-1 * transform.right * Time.deltaTime * movementSpeed);
        }
    }
    void BasicRotation() {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
