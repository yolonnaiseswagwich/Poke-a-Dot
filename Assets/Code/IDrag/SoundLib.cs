using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SoundLib
{
    private static bool IsInit = false;
    //colors

    public const int Splash = 0;
    public const int Menu = 1;
    public const int Game = 2;
    public const int Click = 3;
    public const int Pause = 4;
    public const int Error = 5;
    public const int Dot = 6;
    public const int DotAlt = 7;
    public const int Endless = 8;
    public const int LifeLost = 9;
    public const int LifeGained = 10;
    private static Dictionary<int, AudioClip> SoundList = new Dictionary<int, AudioClip>();
    public static bool Init()
    {
        if (!IsInit)
        {

            AudioClip SoundToSave = Resources.Load("Music/Splash") as AudioClip;
            SoundList.Add(Splash, SoundToSave);
            SoundToSave = Resources.Load("Music/Menu") as AudioClip;
            SoundList.Add(Menu, SoundToSave);
            SoundToSave = Resources.Load("Music/Game") as AudioClip;
            SoundList.Add(Game, SoundToSave);
            SoundToSave = Resources.Load("Music/Click") as AudioClip;
            SoundList.Add(Click, SoundToSave);
            SoundToSave = Resources.Load("Music/Pause") as AudioClip;
            SoundList.Add(Pause, SoundToSave);
            SoundToSave = Resources.Load("Music/Error") as AudioClip;
            SoundList.Add(Error, SoundToSave);
            SoundToSave = Resources.Load("Music/Death") as AudioClip;
            SoundList.Add(Dot, SoundToSave);
            SoundToSave = Resources.Load("Music/SwitchDeath") as AudioClip;
            SoundList.Add(DotAlt, SoundToSave);
            SoundToSave = Resources.Load("Music/Endless") as AudioClip;
            SoundList.Add(Endless, SoundToSave);
            SoundToSave = Resources.Load("Music/LifeL") as AudioClip;
            SoundList.Add(LifeLost, SoundToSave);
            SoundToSave = Resources.Load("Music/LifeG") as AudioClip;
            SoundList.Add(LifeGained, SoundToSave);
            IsInit = true;
        }
        return IsInit;
    }
    public static AudioClip GetSound(int Key)
    {
        AudioClip Temp;
        SoundList.TryGetValue(Key, out Temp);
        return Temp;
    }
}
