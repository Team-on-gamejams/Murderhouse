using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Person : MonoBehaviour
{
    enum Cherectre : float
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

    static Random rnd = new Random();
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
        for (int i = 0; i < Cherectre.Alcohol; i++)
        {
            TempRnd = rnd.Next(-100, 100);
            cherectres.Add(TempRnd);
        }
        ChanceToBecome = rnd.Next(0, 100);
        RemunerationFroDeath = rnd.Next(0, 100);
    }

    private void Move()
    {
        //transofr.
    }

}
