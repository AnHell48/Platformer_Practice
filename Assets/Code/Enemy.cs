using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    NPCBehaviors Behaviors;
    public GameObject target;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        Behaviors = gameObject.GetComponent<NPCBehaviors>();
        speed = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.localPosition, this.transform.localPosition) <= 15)
        {
            Behaviors.Follow(target, speed);

        }
    }
}
