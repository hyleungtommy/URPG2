using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class SkillSpecial : Skill
    {
        public override string GetSkillType()
        {
            return "Special Skill";
        }
    }
}