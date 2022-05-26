using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS FOR PLAYABLE UNITS

[Serializable]
public class UnitClass
{
    public int hp;
    public int hpmax;
    public int s1;
    public int s1max;
    public int s2;
    public int s2max;
    public string name;

    public enum FACTION { MTF, FDN, DCS, CI, GOC, SCP, CIV, MCD, CBG }
    /*
     * MTF - Mobile Task Force
     * FDN - Foundation (Campaign AI Class/Security Forces)
     * DCS - D-Class
     * CI - Chaos Insurgency
     * GOC - Global Occult Coalition
     * SCP - SCP Anomalies
     * CIV - Civilian
     * MCD - Marshall Carter and Dark
     * CBG - Church of the Broken God
     */
    public FACTION unitFaction = FACTION.MTF;
    
}
