using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    enum Cherectre : byte
    {
        Cat = 0,
        Dog,
        Code,
        Design,
        Nature,
        Technics,
        Anime,
        Serials,
        Action,
        Drama,
        Mems,
        serfing,
        PhysicalWork,
        MentalWork,
        Windows,
        Mac,
        Meme,
        Youtube,
        Sprot,
        Alcohol
    }

    List<Cherectre> cherectres = new List<Cherectre>();

    private float RemunerationFroDeath { get; set; }
    public float ChanceToBecome { get; set; }
    private float speed = 5.0f;
    //private string Name { get; set; }

    void Start()
    {
        RandomCharactiristics();
    }

    void Update()
    {
        Move();
    }

    private void RandomCharactiristics()
    {
        float TempRnd;
        for (int i = 0; i < (int)Cherectre.Alcohol; i++)
        {
            TempRnd = Random.Range(-100, 100);
            cherectres.Add((Cherectre)TempRnd);
        }
        ChanceToBecome = Random.Range(0, 100);
        RemunerationFroDeath = Random.Range(0, 100);
    }

    private void Move()
    {
        //transofr.
    }

}
