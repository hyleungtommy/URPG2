using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Entity
    {
        public int id { get; set; }
        public string name { get; set; }
        public Sprite img { get; set; }
        public float currhp { get; set; }
        public float currmp { get; set; }
        public float curratb { get; set; }
        private BasicStat _stat;
        public BasicStat stat
        {
            get
            {
                //return _stat.multiply(buffState.getBasicStat());
                return _stat;
            }
            set
            {
                _stat = value;
            }
        }
        public bool isDefensing { get; set; }
        float atbSpeed;
        protected Entity[] opponent;
        public bool reflectiveDefense { get; set; }
        public float defenseModifier { get; set; }
        // public BuffState buffState { get; set; }
        // public ElementalTemplate elementResistance {get; set;}
        // public ElementalTemplate elementalDamage {get; set;}
        public Entity(string name, BasicStat stat, Sprite img)
        {
            this.name = name;
            this._stat = stat;
            this.img = img;
            currhp = stat.HP;
            currmp = stat.MP;
            curratb = 0f;
            atbSpeed = 0f;
            isDefensing = false;
            defenseModifier = 1.0f;
            // buffState = new BuffState();
            // elementResistance = new ElementalTemplate();
            // elementalDamage = new ElementalTemplate();
        }

        public void SetOpponent(Entity entity)
        {
            opponent = new Entity[1];
            opponent[0] = entity;
        }

        public void SetOpponent(Entity[] entity)
        {
            opponent = entity;
        }

        public bool Tick(float opponentAvgAGI)
        {
            if (currhp > 0)
            {

                atbSpeed = UnityEngine.Random.Range((stat.AGI / opponentAvgAGI) * 2f, (stat.AGI / opponentAvgAGI) * 4.0f) * (isDefensing ? 0.5f : 1f);
                if (atbSpeed > 6.0f)
                    atbSpeed = 6.0f;
                if (atbSpeed < 1.0f)
                    atbSpeed = 1.0f;
                curratb += atbSpeed;
                //Debug.Log (name + "" + currATB);
                return (curratb >= 100.0f);
            }
            return false;
        }

        // public bool isStunned(){
        //     return buffState.isStunned();
        // }

        public virtual void TakeAction(IFunctionable functionable)
        {

        }

        public virtual void OnReceiveDamage(Entity attacker, float damage)
        {

        }

        public virtual void PassRound()
        {
            //buffState.passRound(this);
            isDefensing = false;
            reflectiveDefense = false;
        }

        public List<BattleMessage> UseNormalAttack()
        {

            List<BattleMessage> msgs = new List<BattleMessage>();
            for (int j = 0; j < opponent.Length; j++)
            {

                BattleMessage atkMsg = new BattleMessage();
                atkMsg.sender = this;
                atkMsg.receiver = opponent[j];
                atkMsg.SkillAnimationName = "NormalAttack";

                // float attackModifier = ModifierFromBuffHelper.getAttackModifierFromSpecialBuff(this, name);
                // if(this is EntityPlayer && (this as EntityPlayer).hasPassiveSkill("Battle Will") && (this.currhp/this.stat.HP) <= 0.25f){
                //     SkillPassive passiveSkill = (this as EntityPlayer).getPassiveSkill("Battle Will");
                //     attackModifier = attackModifier + passiveSkill.mod;
                // }
                float attackModifier = 1f;

                float attackPower = (stat.ATK * 1 * Random.Range(0.9f, 1.1f) * attackModifier) - (opponent[j].isDefensing ? opponent[j].stat.DEF * opponent[j].defenseModifier : opponent[j].stat.DEF);
                // * ModifierFromBuffHelper.getTargetDefenseModifierFromSpecialBuff(opponent[j]);
                // int elementalAttackPower = Util.CalculateElementalDamage(this.elementalDamage, opponent[j].elementResistance, attackPower);
                // attackPower += elementalAttackPower;
                
                if (attackPower <= 0f) attackPower = 1f;
                float hitChance = stat.DEX / (opponent[j].stat.AGI * 2f);
                if (hitChance > 1.0f) hitChance = 1.0f;
                else if (hitChance <= 0.1f) hitChance = 0.1f;

                if (Random.Range(0.0f, 1.0f) > hitChance)
                    atkMsg.type = BattleMessage.Type.Miss;
                else
                {
                    bool crititcal = false;
                    float critChance = Mathf.Log(stat.DEX / opponent[j].stat.AGI);
                    if (critChance < 0.05f)
                        critChance = 0.05f;
                    if (Random.Range(0.0f, 1.0f) <= critChance)
                    {
                        crititcal = true;
                        attackPower *= (stat.DEX / opponent[j].stat.DEX) * 2;
                    }

                    // if(opponent[j] is EntityPlayer && (opponent[j] as EntityPlayer).hasPassiveSkill("Potentiality") && attackPower >= opponent[j].stat.HP/2 && attackPower >= opponent[j].currhp && opponent[j].currhp > 1f){
                    //     attackPower = (int)(opponent[j].currhp - 1);
                    // }

                    opponent[j].currhp -= attackPower;
                    opponent[j].OnReceiveDamage(this, attackPower);
                    if (crititcal)
                        atkMsg.type = BattleMessage.Type.Critical;
                    else
                        atkMsg.type = BattleMessage.Type.NormalAttack;
                    if (opponent[j].currhp < 0)
                        opponent[j].currhp = 0;

                    atkMsg.value = attackPower;

                    if (opponent[j].isDefensing && opponent[j].reflectiveDefense)
                    {
                        currhp -= attackPower * 0.1f;
                        BattleMessage refDefMessage = new BattleMessage();
                        refDefMessage.sender = refDefMessage.receiver = this;
                        refDefMessage.value = attackPower * 0.1f;
                        refDefMessage.type = BattleMessage.Type.NormalAttack;
                        msgs.Add(refDefMessage);
                    }

                }
                msgs.Add(atkMsg);
            }
            curratb = 0;


            //Debug.Log (msgs.Count);

            return msgs;
        }

    }
}