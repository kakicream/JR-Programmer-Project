using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    [System.Serializable] // Required for JsonUtility
    class SaveData
    {
        public Color TeamColor;
    }

    public static MainManager Instance;
    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Enables us to access the MainManager object from any other script
        LoadColor();
    }

    public void SaveColor()
    {
        SaveData data = new SaveData(); // new instance of the save data
        data.TeamColor = TeamColor; // and filled its team color class member with the TeamColor variable saved in the MainManager

        string json = JsonUtility.ToJson(data); // Transformed that instance to JSON with JsonUtility.ToJson
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        /* Special method "File.WriteAllText()" to write a string to a file 
         * 1st parameter : the path to the file                                                                                    
         * 2nd parameter : the text you want to write in that file(in this case, my JSON)                                                                                
         */
    }

    public void LoadColor() // Reversal of SaveColor()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // string variable for the file
        if (File.Exists(path)) // If the given path exists, 
        {
            string json = File.ReadAllText(path); // read the content with File.ReadAllText(path), then set it as the value of json
            SaveData data = JsonUtility.FromJson<SaveData>(json); // transform JsonUtility text to SaveData instance
            TeamColor = data.TeamColor; // set the TeamColor to the color saved in that SaveData
        }
    }
}

