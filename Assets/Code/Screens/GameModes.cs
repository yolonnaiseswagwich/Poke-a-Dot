using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameModes : MonoBehaviour
{
    public AudioClip Clip;
    private Material Shaders;
    float ValueX;
    float ValueY;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Score.m_iColor = Color.white;
        Score.m_iCount = Score.m_iScore = 0;
        Score.m_fGameTime = 0;
        Shaders = ShaderLib.GetShader(ShaderLib.Julia);
        ValueX = ((Screen.width - Screen.height) * 0.5f);
        ValueY = ((Screen.height - Screen.width) * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    //relace later with my own buttons and put into update loop or make button class and draw it with post render.
    void OnGUI()
    {
        GUI.skin.button.normal.background = null;
        GUI.skin.button.active.background = null;
        GUI.skin.button.onHover.background = null;
        GUI.skin.button.hover.background = null;
        GUI.skin.button.onFocused.background = null;
        GUI.skin.button.focused.background = null;
        GUI.skin.button.normal.textColor = ColorLib.GetColor(ColorLib.WhiteGrays.White);
        GUI.skin.button.fontSize = 80;

        if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height * 0.125f), "Endless"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.Endless;
            SceneManager.LoadScene(6);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.125f, Screen.width, Screen.height * 0.125f), "Reverse"))
        {
            GameInfo.GameType = GameInfo.Reverse;
            SceneManager.LoadScene(7);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.25f, Screen.width, Screen.height * 0.125f), "Speed"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.Speed;
            SceneManager.LoadScene(8);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.375f, Screen.width, Screen.height * 0.125f), "Rush"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.Rush;
            SceneManager.LoadScene(9);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.5f, Screen.width, Screen.height * 0.125f), "Motorway"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.Motorway;
            SceneManager.LoadScene(10);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.625f, Screen.width, Screen.height * 0.125f), "Color Run"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.ColorRun;
            SceneManager.LoadScene(11);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.75f, Screen.width, Screen.height * 0.125f), "Color Switch"/*, GUIStyle.none*/))
        {
            GameInfo.GameType = GameInfo.ColorSwitch;
            SceneManager.LoadScene(12);
        }
        if (GUI.Button(new Rect(0, Screen.height * 0.875f, Screen.width, Screen.height * 0.125f), "Menu"/*, GUIStyle.none*/))
        {
            SceneManager.LoadScene(1);
        }
    }
    void OnPostRender()
    {
        Vector4 Variables1 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) * 0.02f + 0.37f, Mathf.Sin(Time.timeSinceLevelLoad * 1.22f) * 0.02f + 0.37f, 17.0f, 3.14159f);
        Vector4 Variables2 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 0.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 0.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.87f));

        Shaders.SetVector("MyVar", Variables1);
        Shaders.SetVector("MyVarb", Variables2);
        GL.PushMatrix();
        Shaders.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        if (Screen.width > Screen.height)
        {
            //GL.TexCoord2(0, 0);
            //GL.Vertex3((Screen.width - Screen.height) * 0.5f * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
            //GL.TexCoord2(0, 1);
            //GL.Vertex3((Screen.width - Screen.height) * 0.5f * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
            //GL.TexCoord2(1, 1);
            //GL.Vertex3(((Screen.width - Screen.height) * 0.5f + Screen.height) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
            //GL.TexCoord2(1, 0);
            //GL.Vertex3(((Screen.width - Screen.height) * 0.5f + Screen.height) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
            GL.TexCoord2(0, 0);
            GL.Vertex3(0, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        }
        else if (Screen.height > Screen.width)
        {
            //GL.TexCoord2(0, 0);
            //GL.Vertex3(0, (Screen.height - Screen.width) * 0.5f * IDrag.D2Camera.PixelSize.y, 0.1F);
            //GL.TexCoord2(0, 1);
            //GL.Vertex3(0, ((Screen.height - Screen.width) * 0.5f + Screen.width) * IDrag.D2Camera.PixelSize.y, 0.1F);
            //GL.TexCoord2(1, 1);
            //GL.Vertex3(1, ((Screen.height - Screen.width) * 0.5f + Screen.width) * IDrag.D2Camera.PixelSize.y, 0.1F);
            //GL.TexCoord2(1, 0);
            //GL.Vertex3(1, (Screen.height - Screen.width) * 0.5f * IDrag.D2Camera.PixelSize.y, 0.1F);
            GL.TexCoord2(0, 0);
            GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
            GL.TexCoord2(0, 1);
            GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
            GL.TexCoord2(1, 1);
            GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
            GL.TexCoord2(1, 0);
            GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
        }
        else
        {
            GL.TexCoord2(0, 0);
            GL.Vertex3(0, 0, 0.1F);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 1, 0.1F);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, 1, 0.1F);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, 0, 0.1F);
        }
        GL.End();
        GL.PopMatrix();

        ////do the click to continue
        //Color ColorVar = new Color(0.5f + (Mathf.Abs(Variables2.x) * 0.5f), Mathf.Abs(Variables2.y), Mathf.Abs(Variables2.z), 2.0f);
        //Shaders2.color = ColorVar;
        //GL.PushMatrix();
        //Shaders2.SetPass(0);
        //GL.LoadOrtho();
        //GL.Begin(GL.QUADS);
        ////change it so that it draws in the right positon
        //ColorVar.a = Mathf.Sin(Time.timeSinceLevelLoad * 4.79f) + Time.timeSinceLevelLoad - 0;
        //GL.Color(ColorVar);
        //GL.TexCoord2(0, 0);
        //GL.Vertex3(0, 0, 0.09F);
        //ColorVar.a = Mathf.Cos(Time.timeSinceLevelLoad * 3.79f) * 3.13f + Time.timeSinceLevelLoad - 3.0f;
        //GL.Color(ColorVar);
        //GL.TexCoord2(0, 1);
        //GL.Vertex3(0, 1, 0.09F);
        //ColorVar.a = Mathf.Cos(Time.timeSinceLevelLoad * 1.79f) * 2.43f + Time.timeSinceLevelLoad - 2;
        //GL.Color(ColorVar);
        //GL.TexCoord2(1, 1);
        //GL.Vertex3(1, 1, 0.09F);
        //ColorVar.a = Mathf.Sin(Time.timeSinceLevelLoad * 2.79f) * 1.13f + Time.timeSinceLevelLoad - 6.0f;
        //GL.Color(ColorVar);
        //GL.TexCoord2(1, 0);
        //GL.Vertex3(1, 0, 0.09F);
        //GL.End();
        //GL.PopMatrix();
    }
}
