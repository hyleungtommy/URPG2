using UnityEngine;
using System.Collections;
using RPG;
public class BattleAnimator : MonoBehaviour
{

    public GameObject prefabDamage;
    public Canvas canvas;
    public GameObject skillAnimationObject, healAnimationObject, buffAnimationObject, debuffAnimationObject, defenseAnimationObject;

    public void CreateDamageText(BattleMessage message, Transform spawnLocation)
    {
        int i = 0;
        float rndX = message.receiver is EntityPlayer ? Random.Range(-0.7f, 0.7f) : Random.Range(-0.7f, 0.7f);
        float rndY = Random.Range(-0.7f, 0.7f);
        Transform trans = spawnLocation;
        if (trans != null)
        {
            if (message.type == BattleMessage.Type.Miss)
            {
                InitDamageText(rndX, rndY, trans, 0, message);
            }
            else if (message.type == BattleMessage.Type.Critical || message.type == BattleMessage.Type.MPAttack || message.type == BattleMessage.Type.NormalAttack || message.type == BattleMessage.Type.Heal || message.type == BattleMessage.Type.MPHeal)
            {
                do
                {
                    InitDamageText(rndX, rndY, trans, i, message);
                    i++;
                    message.value = (int)message.value / 10;
                } while (message.value > 0);
                if (message.type == BattleMessage.Type.Critical)
                    CreateCritical(rndX, rndY, trans, 0);
            }
        }
    }

    private void InitDamageText(float rndX, float rndY, Transform trans, int i, BattleMessage message)
    {

        Vector3 pos = new Vector3(trans.position.x + rndX - 0.3f * i, trans.position.y + rndY, trans.position.z);
        GameObject d = Instantiate(prefabDamage, pos, trans.rotation);
        d.transform.SetParent(canvas.transform);
        if (message.type == BattleMessage.Type.Miss)
        {
            d.transform.localScale = new Vector3(4f, 4f, 1.0f);
        }
        else
        {
            d.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
        }

        d.transform.position = pos;
        d.SendMessage("receiveDamageMessage", (int)message.value % 10);
        d.SendMessage("setType", message);
    }

    private void CreateCritical(float rndX, float rndY, Transform trans, int i)
    {
        Vector3 pos = new Vector3(trans.position.x + rndX - 3f * i, trans.position.y + rndY + 5f, trans.position.z);
        GameObject d = Instantiate(prefabDamage, pos, trans.rotation);
        d.SendMessage("createCritical");
    }

    public void CreateSkillAnimation(BattleMessage message, Transform spawnLocation)
    {
        if (message.SkillAnimationName == "NormalAttack")
        {
            CreateSkillObject(skillAnimationObject, spawnLocation, "S0");
        }
        else if (message.type == BattleMessage.Type.Heal)
        {
            CreateSkillObject(healAnimationObject, spawnLocation, "Heal");
        }
        else if (message.type == BattleMessage.Type.Buff)
        {
            CreateSkillObject(buffAnimationObject, spawnLocation, "Buff");
        }
        else if (message.type == BattleMessage.Type.Debuff)
        {
            CreateSkillObject(debuffAnimationObject, spawnLocation, "Debuff");
        }
        else if (message.type == BattleMessage.Type.Defense)
        {
            CreateSkillObject(defenseAnimationObject, spawnLocation, "Defense");
        }
        else
        {
            GameObject skillPrefab = Resources.Load<GameObject>("SkillPrefab/" + message.SkillAnimationName);
            if (skillPrefab == null)
            {
                CreateSkillObject(skillAnimationObject, spawnLocation, "S0");
            }
            else
            {
                CreateSkillObject(skillPrefab, spawnLocation, message.SkillAnimationName);
            }
        }
    }

    private void CreateSkillObject(GameObject skillObject, Transform spawnLocation, string animationStateName)
    {
        GameObject skillObj = Instantiate(skillObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
        skillObj.transform.SetParent(canvas.transform);
        skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
        skillObj.GetComponent<Animator>().Play(animationStateName);
    }

}
