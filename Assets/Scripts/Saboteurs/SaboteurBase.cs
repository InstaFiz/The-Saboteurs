/*
 * Scriptable Object for the Saboteurs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Saboteur", menuName = "Saboteur/Create new Saboteur")]

public class SaboteurBase : ScriptableObject
{
    [SerializeField] string name;
    
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] SaboteurType type; // Saboteurs will be mono-typed. If we want, we can make a type1 + type2 combination later
    
    // Base Stats
    [SerializeField] int maxHP; // Rename? Presence
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;
}

public enum SaboteurType
{
    None,
    Avoider,
    Controller,
    HyperAchiever,
    HyperRational,
    HyperVigilant,
    Pleaser,
    Restless,
    Stickler,
    Victim,
    Judge
}