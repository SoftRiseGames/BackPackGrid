using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Attack();
    }

    void Attack()
    {
        
        for(int i = 0; i<enemies.Count; i++)
        {
            enemies[i].GetComponent<IEnemy>().AttackOnTour();
        }
        
    }
}
