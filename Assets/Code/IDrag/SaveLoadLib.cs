using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
class SaveLoadLib
{
    public static bool Load()
    {
#if !UNITY_WEB
        FileInfo aFile = new FileInfo(Application.persistentDataPath + "\\Settings.idf");
#endif
        string Data = "";
#if !UNITY_WEB
        if (aFile.Exists) {
            StreamReader k = aFile.OpenText();
            Data = k.ReadToEnd();
            k.Close();
#endif
        } else {
#if UNITY_EDITOR
            Debug.Log("No File Detected");
#endif
            return false;
        }
        StringReader r = new StringReader(Data);
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Reds.IndianRed && i <= ColorLib.Reds.Orange){
                GameGlobals.iRed = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Red");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iRed = ColorLib.Reds.Crismon;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Red");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iRed = ColorLib.Reds.Crismon;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Yellows.Yellow && i <= ColorLib.Yellows.GoldenRod)
            {
                GameGlobals.iYellow = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Yellow");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iYellow = ColorLib.Yellows.Yellow;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Yellow");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iYellow = ColorLib.Yellows.Yellow;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Browns.Peru && i <= ColorLib.Browns.Brown)
            {
                GameGlobals.iBrown = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Brown");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iBrown = ColorLib.Browns.SaddleBrown;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Brown");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iBrown = ColorLib.Browns.SaddleBrown;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Greens.LimeGreen && i <= ColorLib.Greens.Green)
            {
                GameGlobals.iGreen = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Green");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iGreen = ColorLib.Greens.Lime;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Green");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iGreen = ColorLib.Greens.Lime;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Blues.Cyan && i <= ColorLib.Blues.MidnightBlue)
            {
                GameGlobals.iBlue = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Blue");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iBlue = ColorLib.Blues.DarkTurqoise;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Blue");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iBlue = ColorLib.Blues.DarkTurqoise;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.Purples.Pink && i <= ColorLib.Purples.Indigo)
            {
                GameGlobals.iPurple = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Purple");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iPurple = ColorLib.Purples.HotPink;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Purple");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iPurple = ColorLib.Purples.HotPink;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.WhiteGrays.White && i <= ColorLib.BlackGrays.Black)
            {
                GameGlobals.iWhiteGray = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting White/Black");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting White/Black");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= ColorLib.WhiteGrays.White && i <= ColorLib.BlackGrays.Black)
            {
                GameGlobals.iBackGround = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Background");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Background");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 100)
            {
                GameGlobals.MusicVolume = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Music Volume");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.MusicVolume = 100;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Music Volume");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.MusicVolume = 100;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 100)
            {
                GameGlobals.SoundVolume = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Sound Volume");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.SoundVolume = 100;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Sound Volume");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.SoundVolume = 100;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= SpriteLib.MaxTexDots)
            {
                GameGlobals.TexNum = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Texture");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.TexNum = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Texture");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.TexNum = 0;
        }
        ////
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 100)
            {
                GameGlobals.ChallengeLevelSpeed = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting ChallengeLevelSpeed");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.ChallengeLevelSpeed = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting ChallengeLevelSpeed");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.ChallengeLevelSpeed = 0;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 100)
            {
                GameGlobals.ChallengeLevelSwitch = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting ChallengeLevelSwitch");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.ChallengeLevelSwitch = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting ChallengeLevelSwitch");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.ChallengeLevelSwitch = 0;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= (System.DateTime.IsLeapYear(System.DateTime.Now.Year)?366:365))
            {
                GameGlobals.Day = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Day");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.Day = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Day");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.Day = 0;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 1074)
            {
                GameGlobals.Dots = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Dots");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.Dots = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Dots");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.Dots = 0;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0)
            {
                GameGlobals.Best = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Best");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.Best = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Best");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.Best = 0;
        }
        r.Close();
        if (GameGlobals.Corrupt)
        {
#if !UNITY_WEB
            SaveLoadLib.Save();
#endif
        }
        return true;
    }
    public static bool Save()
    {
        StringWriter w = new StringWriter();
        w.WriteLine(GameGlobals.iRed.ToString());
        w.WriteLine(GameGlobals.iYellow.ToString());
        w.WriteLine(GameGlobals.iBrown.ToString());
        w.WriteLine(GameGlobals.iGreen.ToString());
        w.WriteLine(GameGlobals.iBlue.ToString());
        w.WriteLine(GameGlobals.iPurple.ToString());
        w.WriteLine(GameGlobals.iWhiteGray.ToString());
        w.WriteLine(GameGlobals.iBackGround.ToString());
        w.WriteLine(GameGlobals.MusicVolume.ToString());
        w.WriteLine(GameGlobals.SoundVolume.ToString());
        w.WriteLine(GameGlobals.TexNum.ToString());
        w.WriteLine(GameGlobals.ChallengeLevelSpeed.ToString());
        w.WriteLine(GameGlobals.ChallengeLevelSwitch.ToString());
        w.WriteLine(GameGlobals.Day);
        w.WriteLine(GameGlobals.Dots);
        w.WriteLine(GameGlobals.Best);
        string Data = w.ToString();
#if !UNITY_WEB
        FileInfo aFile = new FileInfo(Application.persistentDataPath + "\\Settings.idf");
        StreamWriter k;
        if (aFile.Exists)
        {
            aFile.Delete();
        }
        k = aFile.CreateText();
        k.Write(Data);
        k.Close();
#endif
        return true;
    }
}