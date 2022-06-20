using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed, runSpeed, mouseX,mouseY, mouseXSensitivity, mouseYSensitivity, maxHealth;
    public GameObject cam;
    CapsuleCollider capCol;
    Quaternion camRot;
    Vector3 offset;
    Score score;
    HealthManager playersHealth;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Score>();
        //playersHealth = GetComponent<HealthManager>();
        maxHealth = 20f;
        playersHealth = new HealthManager(maxHealth);
        capCol = this.GetComponent<CapsuleCollider>();
        speed = 5.0f;
        runSpeed = speed  + 3f;
        mouseXSensitivity = mouseYSensitivity = 2f;
        offset =  cam.transform.position - this.transform.position;

        cam.transform.position = this.transform.position;
        cam.transform.rotation = this.transform.localRotation;
        ////cam.transform.localPosition = new Vector3(0, 2, -8);
        ////cam.transform.SetParent(this.transform);
        //camRot = cam.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse Y") * mouseXSensitivity;
        mouseY = Input.GetAxis("Mouse X") * mouseYSensitivity;

        camRot *= Quaternion.Euler(0,mouseY, 0);
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            this.transform.GetComponent<Rigidbody>().AddForce(0, 300, 0);//(Vector3.up * 10 , ForceMode.Impulse); 
        }
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    speed += 3f; 
        //}

        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
     
        //camera
        //distance between camera and player
        Vector3 newCamPos = this.transform.position + offset;
        //camera follow player
        cam.transform.position = Vector3.Slerp(cam.transform.position, newCamPos, 1f);
        // camera(camera pivot) rotation with mouse sideways
        cam.transform.Rotate(0, mouseY, 0);

        this.transform.localRotation = cam.transform.localRotation;//Rotate(new Vector3(0,cam.transform.localRotation.y,0));
    }

    private bool IsGrounded()
    {
        RaycastHit hitInfo;
        //get the middle of the capsule and set the sphere down and a bit under the capsule.
        if (Physics.SphereCast(this.transform.position, capCol.radius, Vector3.down, out hitInfo, (capCol.height / 2f) - capCol.radius + 0.01f))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision col)
    {
        switch(col.gameObject.layer)
        {
            case 6:
                //moving platform
                if(col.gameObject.CompareTag("movablePlarform") && col.gameObject.GetComponent<MovePlatforms>().movementType != MovePlatforms.MovementType.UpDown)
                    this.transform.parent = col.gameObject.transform;
                break;
            case 7:
                Debug.Log("EXIT");
                break;
            case 8:
                PickUpCollectable(col.gameObject);
                break;
            case 9:
                EnemyEncounter(col.gameObject);
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        switch (col.gameObject.layer)
        {
            case 6:
                //moving platform
                if (col.gameObject.CompareTag("movablePlarform"))
                    this.transform.parent = null;
                break;
            default:
                break;
        }
    }

    private void PickUpCollectable(GameObject collectable)
    {
        Destroy(collectable);
        score.AddPoints(1);
        /*TODO
         * activate particles
         */
    }

    private void EnemyEncounter(GameObject enemy)
    {
        playersHealth.TakeDamage(4f);
        Debug.Log(playersHealth.checkHealth);
    }

}
