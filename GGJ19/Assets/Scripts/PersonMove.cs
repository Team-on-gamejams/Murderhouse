using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMove : MonoBehaviour
{
    private const int Min = 0;
    static private GameObject FinishObject;
    public Transform EndPos;

    public int RemunerationFroDeath { get; set; }
    public int ChanceToBecome { get; set; }
    public int speed = 30;


    void Start()
    {
        FinishObject = GameObject.FindGameObjectWithTag("MyFinishObjct");
        EndPos = FinishObject.transform;
        RemunerationFroDeath = Random.Range(Min, 100);
        ChanceToBecome = Random.Range(Min, 10);
    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, EndPos.position, Time.deltaTime * speed);

        if (this.transform.position == EndPos.position)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetMouseButtonDown(Min))
        {
            Debug.Log("++++");
        }
    }
}
