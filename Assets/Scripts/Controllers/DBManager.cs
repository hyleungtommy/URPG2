using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class DBManager : MonoBehaviour
{
    [SerializeField]
    public List<Item>Items;
    public static DBManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
