using Scripts.Hero.Ability;
using UnityEngine;

public class Abilities : ScriptableObject
{
    public new string name;
    public Sprite Icon;
    [TextArea(1, 4)]
    public string description;
    public SkillType skillType;
}
