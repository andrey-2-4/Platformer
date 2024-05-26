using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SaveLevel(int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.mysave";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, level);
        stream.Close();
    }

    public static int LoadLevel()
    {
        string path = Application.persistentDataPath + "/level.mysave";
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
}
