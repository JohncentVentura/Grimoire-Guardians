using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsSO", menuName = "ScriptableObjects/CardsSO", order = 1)]
public class CardsSO : ScriptableObject
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
    public CreatureCard bird;
    #endregion

    #region Spell Cards
    [Header("Spell Cards")]
    public SpellCard blazeBall;
    #endregion

    #region Weapon Cards
    [Header("Weapon Cards")]
    public WeaponCard simpleSword;
    public WeaponCard beginnersBow;
    #endregion

}