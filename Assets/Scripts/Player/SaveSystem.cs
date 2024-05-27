using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SaveLevel(int level)
    {
        SaveInt(level, relativePathLevel);
    }

    public static int LoadLevel()
    {
        string path = Application.persistentDataPath + relativePathLevel;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            object data = formatter.Deserialize(stream);
            if (data is null)
            {
                return SceneManager.GetActiveScene().buildIndex + 1;
            }
            stream.Close();

            return (int) data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return SceneManager.GetActiveScene().buildIndex + 1;
        }
    }

    public static void SaveHealth(int health)
    {
        SaveInt(health, relativePathHealth);
    }

    public static int LoadHealth()
    {
        int defaultHealth = 3;
        string path = Application.persistentDataPath + relativePathHealth;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            object data = formatter.Deserialize(stream);
            if (data is null)
            {
                return defaultHealth;
            }
            stream.Close();

            return (int)data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return defaultHealth;
        }
    }

    private static string relativePathLevel = "/level.mysave";
    private static string relativePathHealth = "/health.mysave";

    private static void SaveInt(int n, string relativePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + relativePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, n);
        stream.Close();
    }
}
