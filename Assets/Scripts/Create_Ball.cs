using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Ball : MonoBehaviour
{
    public GameObject ball;
    public Transform target;
    public Transform shotStart;

    [SerializeField]
    float shotPower;

    System.DateTime dt;
    System.DateTime dt_old;
    // Start is called before the first frame update
    void Start()
    {
        dt = System.DateTime.Now;
        dt_old = dt;
    }

    // Update is called once per frame
    void Update()
    {
        dt = System.DateTime.Now;

        if (dt_old.Second != dt.Second)
        {
            GameObject instanceBall=Instantiate(ball, shotStart.position, Quaternion.identity);

            Vector3 vec= target.position - transform.position;
            float endPower = Random.Range(shotPower/1.2f,shotPower);
            Vector3 shotTarget = vec.normalized * endPower + new Vector3(0, 1f, Random.Range(-.5f,.5f));
            instanceBall.GetComponent<Rigidbody>().velocity= shotTarget;
            Destroy(instanceBall, 10f);

            dt_old = dt;
        }
    }
}
