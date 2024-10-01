using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerHealth;
    public List<EnemyData> regularEnemies = new List<EnemyData>();
    public List<EnemyData> capEnemies = new List<EnemyData>();

    public List<string> equippedWeapons = new List<string>();
}

[System.Serializable]
public class EnemyData
{
    public Vector3 position;
    public Quaternion rotation;
    public float health;
}
