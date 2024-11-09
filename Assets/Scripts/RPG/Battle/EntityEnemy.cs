using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class EntityEnemy : Entity
    {

        private Entity[] players;
        private float[] hateMeter;
        public int dropEXP { get; }
        public int dropMoney { get; }
        public BattleScene scene { set; get; }
        public int strengthLv { set; get; }
        public string fullName{get{
            //return Constant.enemyStrengthString[strengthLv] + base.name;
            return base.name;
        }}
        public EntityEnemy(string name, BasicStat stat, Sprite img, int dropEXP, int dropMoney) : base(name, stat, img)
        {
            this.dropEXP = dropEXP;
            this.dropMoney = dropMoney;
        }

        public override void TakeAction(IFunctionable functionable)
        {
            // Pick target
            Entity mostHated = null;
            float maxHate = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null && players[i].currhp > 0 && hateMeter[i] > maxHate)
                {
                    maxHate = hateMeter[i];
                    mostHated = players[i];
                }
            }
            if (mostHated == null)
            {// randomly pick oppa
                Entity attackTarget = players[UnityEngine.Random.Range(0, players.Length)];
                while (attackTarget.currhp <= 0)
                {
                    attackTarget = players[UnityEngine.Random.Range(0, players.Length)];
                }
                SetOpponent(attackTarget);
            }
            else
            {// pick most hated
                SetOpponent(mostHated);
            }


            List<BattleMessage> bundle;
            /*
			Debug.Log ("skillset:" + skillset);
			if (skillset!= null && skillset.Length > 0) {
				int pickedSkillNo = Utility.getRandomIndexFrom (skillWeights, Utility.calculateSum (skillWeights));
				GeneralSkill pickedSkill = skillset [pickedSkillNo];

				if (pickedSkill.MP > CurrMP) {
					Debug.Log ("mp not enough");
					bundle = useNormalAttack ();
				}
				if (!pickedSkill.isAttackSkill ()) {
					if (pickedSkill.AOE) {
						setOpponent (this);
					} else {
						setOpponent (this);
					}
				} else {
					if (pickedSkill.AOE) {
						setOpponent (players);
					}
				}

				Debug.Log (Name + " use skill " + pickedSkill.Name);
				bundle = pickedSkill.use (this, Opponent);
				CurrATB = 0;
			} else {
				bundle = useNormalAttack ();
			}

			Observer.createFloatingText (bundle);
			Debug.Log ("e taske action");
			*/
            bundle = UseNormalAttack();
            scene.createFloatingText(bundle);
        }

        public override void OnReceiveDamage(Entity attacker, float damage)
        {
            base.OnReceiveDamage(attacker, damage);
            hateMeter[attacker.id] += Mathf.Log10(damage);
        }

        public void setupHateMeter(Entity[] players)
        {
            this.players = players;
            hateMeter = new float[players.Length];
            for (int i = 0; i < hateMeter.Length; i++)
                hateMeter[i] = 0f;
        }

        public void decoy(EntityPlayer player, int decoyValue)
        {
            for (int i = 0; i < hateMeter.Length; i++)
            {
                if (players[i].name.Equals(player.name))
                {
                    hateMeter[i] += decoyValue;
                    Debug.Log("Decoy " + hateMeter[i]);
                }
            }
        }
    }
}