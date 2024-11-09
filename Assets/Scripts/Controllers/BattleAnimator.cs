using UnityEngine;
using System.Collections;
using RPG;
public class BattleAnimator : MonoBehaviour
{

    public GameObject prefabDamage;
    public Canvas canvas;
    public GameObject skillAnimationObject, healAnimationObject, buffAnimationObject, debuffAnimationObject, defenseAnimationObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createDamageText(BattleMessage message, Transform spawnLocation)
    {
        //Debug.Log ("Message Value " + (int)message.value);
        int i = 0;



        float rndX = (message.receiver is EntityPlayer ? UnityEngine.Random.Range(-0.7f, 0.7f) : UnityEngine.Random.Range(-0.7f, 0.7f));
        //Debug.Log (message.receiver + "," + rndX);
        float rndY = UnityEngine.Random.Range(-0.7f, 0.7f);
        Transform trans = spawnLocation;
        if (trans != null)
        {
            if (message.type == BattleMessage.Type.Miss)
            {
                initDamageText(rndX, rndY, trans, 0, message);
            }
            else if (message.type == BattleMessage.Type.Critical || message.type == BattleMessage.Type.MPAttack || message.type == BattleMessage.Type.NormalAttack || message.type == BattleMessage.Type.Heal || message.type == BattleMessage.Type.MPHeal)
            {

                do
                {

                    initDamageText(rndX, rndY, trans, i, message);
                    i++;
                    message.value = (int)(message.value) / 10;
                } while (message.value > 0);
                if (message.type == BattleMessage.Type.Critical)
                    createCritical(rndX, rndY, trans, 0, message);
            }


        }


    }

    private void initDamageText(float rndX, float rndY, Transform trans, int i, BattleMessage message)
    {

       Vector3 pos = new Vector3(trans.position.x + rndX - 0.3f * i, trans.position.y + rndY, trans.position.z);
        //Debug.Log ("pos= " + pos);
        GameObject d = (GameObject)Instantiate(prefabDamage, pos, trans.rotation);
        d.transform.SetParent(canvas.transform);
        if (message.type == BattleMessage.Type.Miss)
        {
            //Debug.Log(message.type);
            d.transform.localScale = new Vector3(4f, 4f, 1.0f);
        }
        else
            d.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);

        d.transform.position = pos;
        //Debug.Log ("d.transform= " + d.transform.position);
        d.SendMessage("receiveDamageMessage", (int)(message.value) % 10);
        d.SendMessage("setType", message);
    }

    private void createCritical(float rndX, float rndY, Transform trans, int i, BattleMessage message)
    {
        Vector3 pos = new Vector3(trans.position.x + rndX - 3f * i, trans.position.y + rndY + 5f, trans.position.z);
        GameObject d = (GameObject)Instantiate(prefabDamage, pos, trans.rotation);
        d.SendMessage("createCritical");
    }

    public void createSkillAnimation(BattleMessage message, Transform spawnLocation)
    {

        //Debug.Log("message.SkillID" + message.SkillAnimationName);

        if (message.SkillAnimationName == "NormalAttack")
        {
            GameObject skillObj = (GameObject)Instantiate(skillAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, -5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play("S0");
            Debug.Log(skillObj.transform.position);
        }
        else if (message.type == BattleMessage.Type.Heal)
        {
            GameObject skillObj = (GameObject)Instantiate(healAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play("Heal");
        }
        else if (message.type == BattleMessage.Type.Buff)
        {
            GameObject skillObj = (GameObject)Instantiate(buffAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play("Buff");
        }
        else if (message.type == BattleMessage.Type.Debuff)
        {
            GameObject skillObj = (GameObject)Instantiate(debuffAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play("Debuff");
        }
        else if (message.type == BattleMessage.Type.Defense)
        {
            GameObject skillObj = (GameObject)Instantiate(defenseAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play("Defense");
        }
        else
        {
            GameObject skillPrefab = Resources.Load<GameObject>("SkillPrefab/" + message.SkillAnimationName);
            GameObject skillObj = null;
            if (skillPrefab != null)
                skillObj = (GameObject)Instantiate(skillPrefab, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            else
                skillObj = (GameObject)Instantiate(skillAnimationObject, new Vector3(spawnLocation.position.x, spawnLocation.position.y, 5.0f), Quaternion.identity);
            skillObj.transform.SetParent(canvas.transform);
            skillObj.transform.localScale = new Vector3(100f, 100f, 1.0f);
            skillObj.GetComponent<Animator>().Play(message.SkillAnimationName);
        }
    }

}
