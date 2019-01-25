using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder : MonoBehaviour {
	//Стати представлені статою і протилежною їй статою
	public enum Stat : sbyte {
		Cat,
		Dog,

		Code,
		Design,

		Nature,
		Technics,

		PhysicalWork,
		MentalWork,

		Windows,
		Mac,

		Sprot,
		Alcohol,

		//Anime,
		//Serials,
		//Action,
		//Drama,
		//Mems,
		//serfing,
		//Meme,
		//Youtube,

		LAST_STAT
	}

	sbyte[] stats;

	void Start() {
		stats = new sbyte[(int)Stat.LAST_STAT];

		for(int i = 0; i < (int)Stat.LAST_STAT; i += 2){
			sbyte r = (sbyte)Random.Range(-100, 101);
			stats[i] = r;
			stats[i + 1] = (sbyte)-r;
		}
	}

	public sbyte GetStatValue(Stat stat){
		return stats[(int)stat];
	}

	public override string ToString() {
		string rez = "";

		for (int i = 0; i < (int)Stat.LAST_STAT; ++i) 
			rez += ((Stat)(i)).ToString() + ": " + stats[i] + "\n";

		return rez;
	}
}
