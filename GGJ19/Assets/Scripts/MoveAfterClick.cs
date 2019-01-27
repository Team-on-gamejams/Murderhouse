using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterClick : MonoBehaviour
{
    public GameObject FirstObjc;
    public GameObject SecondObjc;
    private bool First = false;
    private bool Second = false;
    private PersonMove move;

    void Start()
    {
        move = GetComponent<PersonMove>();
        FirstObjc = GameObject.FindGameObjectWithTag("FirstPosToHouse");
        SecondObjc = GameObject.FindGameObjectWithTag("SecondPosToHouse");
    }

    void Update()
    {
        //if(this.gameObject.transform.position == FirstObjc.transform.position)
        //{
        //    Second = true;
        //}
        //if(First == true)
        //{
        //    MoveToFirstPos();
        //}
        //if(Second == true)
        //{
        //    MoveToSecondPos();
        //}
        MoveToSecondPos();
    }

    void OnMouseDown()
    {
       // First = true;
       // move.enabled = false;
    }

    void MoveToFirstPos()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, FirstObjc.transform.position, Time.deltaTime * Random.Range(10, 30));
    }
    
    void MoveToSecondPos()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, SecondObjc.transform.position, Time.deltaTime * Random.Range(10, 30));

    }
}
