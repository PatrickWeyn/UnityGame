using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_weapon : ScriptableObject
{
    public int mindmg;
    public int maxdmg;
    public Sprite art;
    public int pushback;
    public float cooldown;
}
