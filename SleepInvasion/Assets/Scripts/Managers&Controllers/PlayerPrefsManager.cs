using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPrefsKeys
{
    GameStarted,
    PlayerTransform,
    Level,
    InventoryItems,
    DestroyedItems,
    MayaStoneRingsIndex,
    LastDiaryPage,
    Chest1Unlocked,
    Chest3Unlocked,
    Door3Unlocked,
    Level1Process,
    Level2Process,
    Level3Process,
    Level4Process,
    FirstRaycast,
    FirstLockView,
    FirstInventory,
    FirstInspect,
    FirsMayaStone,
    FirstMagnifier,
    FirstShader,
    FirstDiary,
    MayaStoneUnlocked,
    DoorLocked,
    // Analytics
    GameTimer,
    LockTimer,
    MagnifierTimer,
    ShaderTimer,
    ShownDialogues,
    Sundial1Finished,
    Sundial2Finished,
    Sundial3Finished,
    Sundial4Finished
}

public struct SavedData
{
    public Transform PlayerTransform;
    public int Level;
}

public struct ItemsInfo
{
    public List<ItemInfo> Items;
}

public struct IntList
{
    public List<int> intList;
}

public class PlayerPrefsManager : MonoBehaviour
{
    private GameController _gameController;
    
    private void Start()
    {
        _gameController = GameController.Instance;
    }

    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public static void DeleteKey(PlayerPrefsKeys key)
    {
        PlayerPrefs.DeleteKey(key.ToString());
    }

    public static void SaveGame(int level = -100)
    {
        Vector3 playerPosition = GameController.Instance.GetPlayerTransform().position;
        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);
        if(level != -100)
            SetInt(PlayerPrefsKeys.Level, level);
    }
    
    public static SavedData LoadGame()
    {
        SavedData savedData = new SavedData();
        Vector3 playerPosition = GameController.Instance.GetPlayerTransform().position;
        savedData.Level = 0;
        savedData.PlayerTransform = GameController.Instance.GetPlayerTransform();  
        
        float x = PlayerPrefs.GetFloat("PlayerPositionX", playerPosition.x);
        float y = PlayerPrefs.GetFloat("PlayerPositionY", playerPosition.y);
        float z = PlayerPrefs.GetFloat("PlayerPositionZ", playerPosition.z);
        savedData.PlayerTransform.position = new Vector3(x, y, z);
        return savedData;
    }
    
    // public static void SaveGame(Transform transform, int level = -1)
    // {
    //     SetTransform(PlayerPrefsKeys.PlayerTransform, transform);
    //     if (level >= 0)
    //         SetInt(PlayerPrefsKeys.Level, level);
    // }
    //
    // public static SavedData LoadGame()
    // {
    //     SavedData savedData = new SavedData();
    //     savedData.PlayerTransform = new RectTransform();
    //     GetTransform(PlayerPrefsKeys.PlayerTransform, savedData.PlayerTransform);
    //     savedData.Level = 0;
    //     return savedData;
    // }

    public static void SetBool(PlayerPrefsKeys key, bool value)
    {
        PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
    }

    public static bool GetBool(PlayerPrefsKeys key, bool defaultValue = true)
    {
        int value = defaultValue ? 1 : 0;
        if (PlayerPrefs.HasKey(key.ToString()))
        {
            value = PlayerPrefs.GetInt(key.ToString());
        }
        else
        {
            PlayerPrefs.SetInt(key.ToString(), 0);
        }

        return value == 1 ? true : false;
    }

    public static void SetFloat(PlayerPrefsKeys key, float value)
    {
        PlayerPrefs.SetFloat(key.ToString(), value);
    }

    public static float GetFloat(PlayerPrefsKeys key, float defaultValue)
    {
        float value = defaultValue;
        if (PlayerPrefs.HasKey(key.ToString()))
        {
            value = PlayerPrefs.GetFloat(key.ToString());
        }
        else
        {
            SetFloat(key, defaultValue);
        }

        return value;
    }

    public static void SetInt(PlayerPrefsKeys key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }

    public static int GetInt(PlayerPrefsKeys key, int defaultValue)
    {
        int value = defaultValue;
        if (PlayerPrefs.HasKey(key.ToString()))
        {
            value = PlayerPrefs.GetInt(key.ToString());
        }
        else
        {
            SetInt(key, defaultValue);
        }

        return value;
    }

    public static void SetString(PlayerPrefsKeys key, string value)
    {
        PlayerPrefs.SetString(key.ToString(), value);
    }

    public static string GetString(PlayerPrefsKeys key, string defaultValue)
    {
        string value = defaultValue;
        if (PlayerPrefs.HasKey(key.ToString()))
        {
            value = PlayerPrefs.GetString(key.ToString());
        }
        else
        {
            SetString(key, defaultValue);
        }

        return value;
    }


    private static void SetVector3(string key, Vector3 value)
    {
        string x = key + "V3X";
        string y = key + "V3Y";
        string z = key + "V3Z";
        PlayerPrefs.SetFloat(x, value.x);
        PlayerPrefs.SetFloat(y, value.y);
        PlayerPrefs.SetFloat(z, value.z);
    }

    private static Vector3 GetVector3(string key)
    {
        Vector3 value;
        string x = key + "V3X";
        string y = key + "V3Y";
        string z = key + "V3Z";
        value.x = PlayerPrefs.GetFloat(x, 0);
        value.y = PlayerPrefs.GetFloat(y, 0);
        value.z = PlayerPrefs.GetFloat(z, 0);
        return value;
    }

    private static void SetTransform(PlayerPrefsKeys key, Transform value)
    {
        string position = key + "TP";
        string eulerAngles = key + "TE";
        // string scale = key + "TS";
        SetVector3(position, value.position);
        SetVector3(eulerAngles, value.eulerAngles);
        // SetVector3(scale, value.localScale);
    }
    
    private static void GetTransform(PlayerPrefsKeys key, Transform value)
    {
        string position = key + "TP";
        string eulerAngles = key + "TE";
        // string scale = key + "TS";
        value.transform.position = GetVector3(position);
        value.transform.eulerAngles = GetVector3(eulerAngles);
        // SetVector3(scale, value.localScale);
    }

    public static void SetIntList(PlayerPrefsKeys key, List<int> array)
    {
        List<int> intList = new List<int>();
        string itemsString = "";
        IntList list = new IntList
        {
            intList = array
        };
        itemsString = JsonUtility.ToJson(list);
        SetString(key, itemsString);
    }

    public static List<int> GetIntList(PlayerPrefsKeys key)
    {
        if (PlayerPrefs.HasKey(key.ToString()))
        {
            string itemsString = GetString(key, "");
            IntList intList = JsonUtility.FromJson<IntList>(itemsString);
            return intList.intList;
        }
        return new List<int>();
    }
}
