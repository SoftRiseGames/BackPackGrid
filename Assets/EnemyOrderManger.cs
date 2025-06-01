using UnityEngine;
using System.Collections.Generic;
public class EnemyOrderManger : MonoBehaviour
{
    public List<Enemy> enemyList;

    private void Start()
    {
        for(int i = 0; i<enemyList.Count; i++)
        {
            enemyList[i].Order = i;
        }
    }
}
