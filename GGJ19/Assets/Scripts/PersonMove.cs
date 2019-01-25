using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMove : MonoBehaviour
{
    static private GameObject FinishObject;
    public Transform EndPos;
    private int RemunerationFroDeath { get; set; }
    public int ChanceToBecome { get; set; }
    public readonly int speed = 30;
    private float SpawnX;
    private float SpawnY;
    private float SpawnZ; 
    private int CheckForOverflow = 0;
    private readonly int RandomNumForSpawnMoreObjects = 10;

    // Start is called before the first frame update
    void Start()
    {
        FinishObject = GameObject.FindGameObjectWithTag("MyFinishObjct");
        EndPos = FinishObject.transform;
        SpawnX = this.transform.position.x;
        SpawnY = this.transform.position.y;
        SpawnZ = this.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, EndPos.position, Time.deltaTime * speed);
        
        if (RandomNumForSpawnMoreObjects == Random.Range(0, 100))
        {
            CheckForOverflow++;
            if (CheckForOverflow < 10)
            {
                Instantiate(this.gameObject, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
            }
        }
        if (this.transform.position == EndPos.position)
        {
            Destroy(this.gameObject);
            CheckForOverflow--;
        }
    }
}
