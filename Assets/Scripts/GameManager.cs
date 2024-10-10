using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameConf GameConf { get; private set; }
    private void Awake()
    {
        Instance=this;
        GameConf = Resources.Load<GameConf>("GameConf");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
