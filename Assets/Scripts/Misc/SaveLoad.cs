using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

public class SaveLoad : MonoBehaviour
{
    private string saveFilePath;
    private string encryptionKey = "your-encryption-key"; // Original key, will be adjusted

    public PlayerController playerController;
   // public List<EnemyAI> enemies;
   // public List<CapEnemy> capEnemies;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savegame.json";

        // Automatically find all enemies with the EnemyAI and CapEnemy components
        //enemies = new List<EnemyAI>(FindObjectsOfType<EnemyAI>());
        //capEnemies = new List<CapEnemy>(FindObjectsOfType<CapEnemy>());
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            playerPosition = playerController.transform.position,
            playerRotation = playerController.transform.rotation,
            //playerHealth = playerController.GetComponent<PlayerHealth>().currentHealth
        };

        //foreach (var enemy in enemies)
        //{
        //    EnemyData enemyData = new EnemyData
        //    {
        //        position = enemy.transform.position,
        //        rotation = enemy.transform.rotation,
        //        health = enemy.GetComponent<EnemyHealth>().currentHealth
        //    };
        //    data.regularEnemies.Add(enemyData);
        //}

        //foreach (var capEnemy in capEnemies)
        //{
        //    EnemyData capEnemyData = new EnemyData
        //    {
        //        position = capEnemy.transform.position,
        //        rotation = capEnemy.transform.rotation,
        //        health = capEnemy.GetComponent<EnemyHealth>().currentHealth
        //    };
        //    data.capEnemies.Add(capEnemyData);
        //}

        string json = JsonUtility.ToJson(data, true);

        // Encrypt the JSON before saving it
        string encryptedJson = Encrypt(json, encryptionKey);

        File.WriteAllText(saveFilePath, encryptedJson);
        Debug.Log("Game Saved (Encrypted)");
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string encryptedJson = File.ReadAllText(saveFilePath);

            // Decrypt the JSON after loading it
            string json = Decrypt(encryptedJson, encryptionKey);
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Ensure the PlayerController is not overriding the position on load
            playerController.enabled = false;  // Disable PlayerController to prevent movement during load

            CharacterController charController = playerController.GetComponent<CharacterController>();
            if (charController != null)
            {
                charController.enabled = false;
            }

            playerController.transform.position = data.playerPosition;
            playerController.transform.rotation = data.playerRotation;
           // playerController.GetComponent<PlayerHealth>().currentHealth = data.playerHealth;

            Debug.Log("Loaded Player Position: " + data.playerPosition);
            Debug.Log("Loaded Player Rotation: " + data.playerRotation);

            if (charController != null)
            {
                charController.enabled = true;
            }

            playerController.enabled = true;

            // Load regular enemies
            //for (int i = 0; i < data.regularEnemies.Count; i++)
            //{
            //    if (i < enemies.Count)
            //    {
            //        enemies[i].transform.position = data.regularEnemies[i].position;
            //        enemies[i].transform.rotation = data.regularEnemies[i].rotation;
            //        enemies[i].GetComponent<EnemyHealth>().currentHealth = data.regularEnemies[i].health;

            //        Debug.Log("Loaded Enemy " + i + " Position: " + data.regularEnemies[i].position);
            //        Debug.Log("Loaded Enemy " + i + " Rotation: " + data.regularEnemies[i].rotation);
            //    }
            //}

            // Load cap enemies
            //for (int i = 0; i < data.capEnemies.Count; i++)
            //{
            //    if (i < capEnemies.Count)
            //    {
            //        capEnemies[i].transform.position = data.capEnemies[i].position;
            //        capEnemies[i].transform.rotation = data.capEnemies[i].rotation;
            //        capEnemies[i].GetComponent<EnemyHealth>().currentHealth = data.capEnemies[i].health;

            //        Debug.Log("Loaded CapEnemy " + i + " Position: " + data.capEnemies[i].position);
            //        Debug.Log("Loaded CapEnemy " + i + " Rotation: " + data.capEnemies[i].rotation);
            //    }
            //}

            Debug.Log("Game Loaded (Decrypted)");
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }

    // Adjust the encryption key length to 16, 24, or 32 bytes
    private string AdjustKey(string key)
    {
        if (key.Length < 32)
        {
            key = key.PadRight(32, '0');  // Pad the key with '0' if it's less than 32 characters
        }
        else if (key.Length > 32)
        {
            key = key.Substring(0, 32);   // Truncate the key if it's longer than 32 characters
        }
        return key;
    }

    private string Encrypt(string plainText, string key)
    {
        key = AdjustKey(key);  // Adjust the key to ensure it's a valid length
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.GenerateIV(); // Generates a random Initialization Vector (IV)
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            {
                // Write IV to the beginning of the stream
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private string Decrypt(string cipherText, string key)
    {
        key = AdjustKey(key);  // Adjust the key to ensure it's a valid length
        byte[] fullCipher = Convert.FromBase64String(cipherText);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;

            byte[] iv = new byte[16];
            Array.Copy(fullCipher, iv, iv.Length);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
