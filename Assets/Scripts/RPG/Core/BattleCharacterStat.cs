namespace RPG
{
    public class BattleCharacterStat
    {
        public int strength { get; set; }
        public int mana { get; set; }
        public int stamina { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }

        public BattleCharacterStat()
        {
        }

        public BasicStat toBasicStat()
        {
            return new BasicStat(
                stamina * 24 + strength * 2,
                stamina * 12 + mana * 5,
                strength * 4,
                stamina * 1 + strength * 1,
                mana * 5,
                stamina * 1 + mana * 2,
                agility,
                dexterity
            );
        }
    }
}