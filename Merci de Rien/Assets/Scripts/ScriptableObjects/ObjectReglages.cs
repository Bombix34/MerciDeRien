using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MDR/Objet Reglages")]
public class ObjectReglages : ScriptableObject
{
    public bool IsPortable = false;
    public bool IsBreakingThings = false;
    public bool IsBreaking = false;
    public bool IsHurting = false;
    public bool IsFilling = false;

    public bool IsGivingPatouneOnBreak = false;
}
