using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CardsData", menuName = "ScriptableObjects/CardsData", order = 1)]

public class CardsData : ScriptableObject
{
    public enum TYPES
    {
        //cardCategoryDict types
        CREATURE, SPELL, WEAPON,
        //creatureTypeDict types

        //spellTypeDict types

        //weaponTypeDict types
        SWORD, POLEARM, HEAVY, BOW, STAFF
    }

    public readonly Dictionary<TYPES, string> cardCategoryDict = new() { { TYPES.CREATURE, "Creature" }, { TYPES.SPELL, "Spell" }, { TYPES.WEAPON, "Weapon" } };
    public readonly Dictionary<TYPES, string> creatureTypeDict = new() { };
    public readonly Dictionary<TYPES, string> spellTypeDict = new() { };
    public readonly Dictionary<TYPES, string> weaponTypeDict = new()
    { { TYPES.SWORD, "Sword" }, { TYPES.POLEARM, "Polearm" }, { TYPES.HEAVY, "Heavy" }, { TYPES.BOW, "Bow" }, { TYPES.STAFF, "Staff" } };

    #region Creature Cards
    [Header("Creature Cards")]
    public Creature bird;
    #endregion

    #region Spell Cards
    [Header("Spell Cards")]
    public Spell blazeBall;
    #endregion

    #region Weapon Cards
    [Header("Weapon Cards")]
    public Weapon simpleSword;
    public Weapon beginnersBow;
    #endregion

}