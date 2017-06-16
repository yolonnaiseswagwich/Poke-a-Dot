using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SpriteLib
{
    private static bool IsInit = false;
    //colors
    public const int Default = 0;
    public const int One = 1;
    public const int Two = 2;
    public const int Three = 3;
    public const int Splash = 4;
    public const int Back = 5;
    public const int Credits = 6;
    public const int GameModes = 7;
    public const int Achievments = 8;
    public const int Play = 9;
    public const int Settings = 10;
    public const int Store = 11;
    public const int HTP = 12;
    public const int PaD = 13;
    public const int CredOverlay = 14;
    public const int HTPOverlay = 15;
    public const int CorruptOverlay = 16;
    public const int SpeedInc = 17;
    public const int Pause = 18;
    public const int CircleBase = 19;
    public const int Reset = 20;
    public const int Settingsinfo = 21;
    public const int DC = 22;
    public const int Rate = 23;
    public const int Like = 24;
    public const int NA = 25;
    public const int SelectM = 26;
    public const int SelectL = 27;
    public const int Website = 28;
    public const int HTPS = 29;
    public const int HTPCS = 30;

    public const int Speed = 31;
    public const int Switch = 32;
    public const int Quick = 33;
    public const int Norm = 34;
    public const int Long = 35;
    public const int VLong = 36;
    public const int Next = 37;
    public const int Home = 38;
    public const int MusicOff = 39;
    public const int MusicOn = 40;
    public const int SoundOff = 41;
    public const int SoundOn = 42;
    public const int ColorBlindOff = 43;
    public const int ColorBlindOn = 44;


    public const int Circle0 = 100;
    public const int Circle1 = 101;
    public const int Circle2 = 102;
    public const int Circle3 = 103;
    public const int Circle4 = 104;
    public const int Circle5 = 105;
    public const int Circle6 = 106;
    public const int Circle7 = 107;
    public const int Circle8 = 108;

    public const int Blend0 = 501;
    public const int Blend1 = 502;
    public const int Blend2 = 503;
    public const int Blend3 = 504;
    public const int Blend4 = 505;
    public const int Blend5 = 506;
    public const int Blend6 = 507;
    public const int Blend7 = 508;
    public const int Blend8 = 509;
    public const int Blend9 = 510;
    public const int Blend10 = 511;
    public const int Blend11 = 512;
    public const int Blend12 = 513;

    public const int MaxTexDots = 7;
    private static Dictionary<int, Texture2D> SpriteList = new Dictionary<int, Texture2D>();
    public static bool Init()
    {
        if (!IsInit)
        {
            Texture2D TextureToSave = Resources.Load("Default") as Texture2D;
            SpriteList.Add(Default, TextureToSave);
            TextureToSave = Resources.Load("1") as Texture2D;
            SpriteList.Add(One, TextureToSave);
            TextureToSave = Resources.Load("2") as Texture2D;
            SpriteList.Add(Two, TextureToSave);
            TextureToSave = Resources.Load("3") as Texture2D;
            SpriteList.Add(Three, TextureToSave);
            TextureToSave = Resources.Load("Splash") as Texture2D;
            SpriteList.Add(Splash, TextureToSave);
            TextureToSave = Resources.Load("Back") as Texture2D;
            SpriteList.Add(Back, TextureToSave);
            TextureToSave = Resources.Load("Credits") as Texture2D;
            SpriteList.Add(Credits, TextureToSave);
            TextureToSave = Resources.Load("GameModes") as Texture2D;
            SpriteList.Add(GameModes, TextureToSave);
            TextureToSave = Resources.Load("Achievments") as Texture2D;
            SpriteList.Add(Achievments, TextureToSave);
            TextureToSave = Resources.Load("Play") as Texture2D;
            SpriteList.Add(Play, TextureToSave);
            TextureToSave = Resources.Load("Settings") as Texture2D;
            SpriteList.Add(Settings, TextureToSave);
            TextureToSave = Resources.Load("Store") as Texture2D;
            SpriteList.Add(Store, TextureToSave);
            TextureToSave = Resources.Load("HTP") as Texture2D;
            SpriteList.Add(HTP, TextureToSave);
            TextureToSave = Resources.Load("PaD") as Texture2D;
            SpriteList.Add(PaD, TextureToSave);
            TextureToSave = Resources.Load("CredOverlay") as Texture2D;
            SpriteList.Add(CredOverlay, TextureToSave);
            TextureToSave = Resources.Load("HTPOverlay") as Texture2D;
            SpriteList.Add(HTPOverlay, TextureToSave);
            TextureToSave = Resources.Load("Corrupt") as Texture2D;
            SpriteList.Add(CorruptOverlay, TextureToSave);
            TextureToSave = Resources.Load("IncreasedSpeed") as Texture2D;
            SpriteList.Add(SpeedInc, TextureToSave);
            TextureToSave = Resources.Load("Pause") as Texture2D;
            SpriteList.Add(Pause, TextureToSave);
            TextureToSave = Resources.Load("CircleBase") as Texture2D;
            SpriteList.Add(CircleBase, TextureToSave);
            TextureToSave = Resources.Load("Reset") as Texture2D;
            SpriteList.Add(Reset, TextureToSave);
            TextureToSave = Resources.Load("Settingsinfo") as Texture2D;
            SpriteList.Add(Settingsinfo, TextureToSave);
            TextureToSave = Resources.Load("DC") as Texture2D;
            SpriteList.Add(DC, TextureToSave);
            TextureToSave = Resources.Load("Rate") as Texture2D;
            SpriteList.Add(Rate, TextureToSave);
            TextureToSave = Resources.Load("Like") as Texture2D;
            SpriteList.Add(Like, TextureToSave);
            TextureToSave = Resources.Load("NA") as Texture2D;
            SpriteList.Add(NA, TextureToSave);
            TextureToSave = Resources.Load("Select") as Texture2D;
            SpriteList.Add(SelectM, TextureToSave);
            TextureToSave = Resources.Load("SelectL") as Texture2D;
            SpriteList.Add(SelectL, TextureToSave);
            TextureToSave = Resources.Load("Website") as Texture2D;
            SpriteList.Add(Website, TextureToSave);
            TextureToSave = Resources.Load("HTPS") as Texture2D;
            SpriteList.Add(HTPS, TextureToSave);
            TextureToSave = Resources.Load("HTPCS") as Texture2D;
            SpriteList.Add(HTPCS, TextureToSave);

            TextureToSave = Resources.Load("Speed") as Texture2D;
            SpriteList.Add(Speed, TextureToSave);
            TextureToSave = Resources.Load("Switch") as Texture2D;
            SpriteList.Add(Switch, TextureToSave);
            TextureToSave = Resources.Load("Quick") as Texture2D;
            SpriteList.Add(Quick, TextureToSave);
            TextureToSave = Resources.Load("Norm") as Texture2D;
            SpriteList.Add(Norm, TextureToSave);
            TextureToSave = Resources.Load("Long") as Texture2D;
            SpriteList.Add(Long, TextureToSave);
            TextureToSave = Resources.Load("VLong") as Texture2D;
            SpriteList.Add(VLong, TextureToSave);
            TextureToSave = Resources.Load("Next") as Texture2D;
            SpriteList.Add(Next, TextureToSave);
            TextureToSave = Resources.Load("Home") as Texture2D;
            SpriteList.Add(Home, TextureToSave);
            TextureToSave = Resources.Load("MusicOff") as Texture2D;
            SpriteList.Add(MusicOff, TextureToSave);
            TextureToSave = Resources.Load("MusicOn") as Texture2D;
            SpriteList.Add(MusicOn, TextureToSave);
            TextureToSave = Resources.Load("SoundOff") as Texture2D;
            SpriteList.Add(SoundOff, TextureToSave);
            TextureToSave = Resources.Load("SoundOn") as Texture2D;
            SpriteList.Add(SoundOn, TextureToSave);
            TextureToSave = Resources.Load("ColorBlindOff") as Texture2D;
            SpriteList.Add(ColorBlindOff, TextureToSave);
            TextureToSave = Resources.Load("ColorBlindOn") as Texture2D;
            SpriteList.Add(ColorBlindOn, TextureToSave);



            TextureToSave = Resources.Load("Circle0") as Texture2D;
            SpriteList.Add(Circle0, TextureToSave);
            TextureToSave = Resources.Load("Circle1") as Texture2D;
            SpriteList.Add(Circle1, TextureToSave);
            TextureToSave = Resources.Load("Circle2") as Texture2D;
            SpriteList.Add(Circle2, TextureToSave);
            TextureToSave = Resources.Load("Circle3") as Texture2D;
            SpriteList.Add(Circle3, TextureToSave);
            TextureToSave = Resources.Load("Circle4") as Texture2D;
            SpriteList.Add(Circle4, TextureToSave);
            TextureToSave = Resources.Load("Circle5") as Texture2D;
            SpriteList.Add(Circle5, TextureToSave);
            TextureToSave = Resources.Load("Circle6") as Texture2D;
            SpriteList.Add(Circle6, TextureToSave);
            TextureToSave = Resources.Load("Circle7") as Texture2D;
            SpriteList.Add(Circle7, TextureToSave);
            TextureToSave = Resources.Load("Circle8") as Texture2D;
            SpriteList.Add(Circle8, TextureToSave);

            TextureToSave = Resources.Load("Blend/0") as Texture2D;
            SpriteList.Add(Blend0, TextureToSave);
            TextureToSave = Resources.Load("Blend/1") as Texture2D;
            SpriteList.Add(Blend1, TextureToSave);
            TextureToSave = Resources.Load("Blend/2") as Texture2D;
            SpriteList.Add(Blend2, TextureToSave);
            TextureToSave = Resources.Load("Blend/3") as Texture2D;
            SpriteList.Add(Blend3, TextureToSave);
            TextureToSave = Resources.Load("Blend/4") as Texture2D;
            SpriteList.Add(Blend4, TextureToSave);
            TextureToSave = Resources.Load("Blend/5") as Texture2D;
            SpriteList.Add(Blend5, TextureToSave);
            TextureToSave = Resources.Load("Blend/6") as Texture2D;
            SpriteList.Add(Blend6, TextureToSave);
            TextureToSave = Resources.Load("Blend/7") as Texture2D;
            SpriteList.Add(Blend7, TextureToSave);
            TextureToSave = Resources.Load("Blend/8") as Texture2D;
            SpriteList.Add(Blend8, TextureToSave);
            TextureToSave = Resources.Load("Blend/9") as Texture2D;
            SpriteList.Add(Blend9, TextureToSave);
            TextureToSave = Resources.Load("Blend/10") as Texture2D;
            SpriteList.Add(Blend10, TextureToSave);
            TextureToSave = Resources.Load("Blend/11") as Texture2D;
            SpriteList.Add(Blend11, TextureToSave);
            TextureToSave = Resources.Load("Blend/12") as Texture2D;
            SpriteList.Add(Blend12, TextureToSave);
            IsInit = true;
        }
        return IsInit;
    }
    public static Texture2D GetTexture(int Key)
    {
        Texture2D Temp;
        SpriteList.TryGetValue(Key, out Temp);
        return Temp;
    }
    public static void SetColor(Color a_aColor, Texture2D a_Texture)
    {
        Color[] Pixels = a_Texture.GetPixels();
        for (int i = 0; i < Pixels.Length; ++i)
        {
            Pixels[i] *= a_aColor;
        }
        a_Texture.SetPixels(Pixels);
        a_Texture.Apply();
    }
    private static Color[] ChangeColor(Color a_cColor, Color[] a_aTexture)
    {
        for (int i = 0; i < a_aTexture.Length; ++i)
        {
            if (a_aTexture[i].a != 0)
            {
                a_aTexture[i].r = a_cColor.r;
                a_aTexture[i].g = a_cColor.g;
                a_aTexture[i].b = a_cColor.b;
            }
        }
        return a_aTexture;
    }


}
