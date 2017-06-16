using UnityEngine;
using System.Collections;
public class Entity {
    protected Color MyColor;
    protected Texture2D m_aTexture; //this is the texture protected because we load on runtime.
    protected Rect m_aTexRect; //protected Tex Cords
    protected Rect m_aRect; //protected because this is the base of the objects (its the positon and size).
    protected Material Shaders;
    protected bool m_bShaked;
    protected float m_fLifeTimer;
    protected bool m_bBad;

    public virtual bool Init(float x, float y, int sx, int sy)
    {
        switch (GameGlobals.TexNum)
        {
            case 0:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle0);
                break;
            case 1:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle1);
                break;
            case 2:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle2);
                break;
            case 3:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle3);
                break;
            case 4:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle4);
                break;
            case 5:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle5);
                break;
            case 6:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle6);
                break;
            case 7:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle7);
                break;
            case 8:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle8);
                break;
        }
        Shaders = ShaderLib.GetShader(ShaderLib.DefaultColorTex);
        m_aRect = new Rect(x, y, sx, sy);
        MyColor = ColorLib.GetColor(ColorLib.Default);
        m_bShaked = false;
        return true;
    }
    public virtual bool Init(float x, float y, int sx, int sy, Color aColor)
    {
        switch (GameGlobals.TexNum)
        {
            case 0:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle0);
                break;
            case 1:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle1);
                break;
            case 2:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle2);
                break;
            case 3:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle3);
                break;
            case 4:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle4);
                break;
            case 5:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle5);
                break;
            case 6:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle6);
                break;
            case 7:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle7);
                break;
            case 8:
                m_aTexture = SpriteLib.GetTexture(SpriteLib.Circle8);
                break;
        }
        Shaders = ShaderLib.GetShader(ShaderLib.DefaultColorTex);
        m_aRect = new Rect(x, y, sx, sy);
        MyColor = aColor;
        m_bShaked = false;
        return true;
    }
    public virtual bool Update(bool Slow = false)
    {
        return true;
    }
    public virtual bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
    {
        Rect Temp = new Rect(m_aRect.x - m_aRect.width * 0.5f, m_aRect.y - m_aRect.height * 0.5f, m_aRect.width, m_aRect.height);
        if (Temp.y - Temp.height * 1.1f <= Screen.height * 1.1f && Temp.y + Temp.height * 1.1f >= 0 && Temp.x - Temp.width * 1.1f <= Screen.width && Temp.x + Temp.width * 1.1f >= 0)
        {
            if (aColor == new Color())
            {
                aColor = MyColor;
            }
            else
            {
                aColor *= MyColor;
            }
            //do shaderdataspecificstuff
            {
                Shaders.mainTexture = m_aTexture;
            }
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            //change it so that it draws in the right positon
            Vector2 aPos = GetPos();
            if (m_bShaked)
            {
                int iTemp = (int)(m_fLifeTimer - 4.0f);
                iTemp = (int)(iTemp * 2.5f);
                m_aRect.x += IDrag.Random.GetRandom((int)(Screen.width * -0.003f) * iTemp, (int)(Screen.width * 0.003f) * iTemp);
                m_aRect.y += IDrag.Random.GetRandom((int)(Screen.height * -0.003f) * iTemp, (int)(Screen.height * 0.003f) * iTemp);
            }
            IDrag.Shapes.CreateBox(IDrag.D2Camera.DrawPos(m_aRect), aColor);
            SetPos(aPos);
            GL.End();
            GL.PopMatrix();
            return true;
        }
        return false;
    }
    public void SetShader(int i)
    {
        Shaders = ShaderLib.GetShader(i);
    }
    public Texture2D GetTex()
    {
        return m_aTexture;
    }
    public void SetTex(int i)
    {
        m_aTexture = SpriteLib.GetTexture(i);
    }
    public virtual void SetPos(float x, float y)
    {
        m_aRect.x = x;
        m_aRect.y = y;
    }
    public virtual void SetPos(Vector2 aVec)
    {
        m_aRect.x = aVec.x;
        m_aRect.y = aVec.y;
    }
    public virtual Vector2 GetPos()
    {
        return new Vector2(m_aRect.x, m_aRect.y);
    }
    public virtual Vector2 GetSize()
    {
        return new Vector2(m_aRect.width, m_aRect.height);
    }
    public virtual void SetSize(float x, float y)
    {
        m_aRect.width = x;
        m_aRect.height = y;
    }
    public virtual void SetSize(Vector2 aVec)
    {
        m_aRect.width = aVec.x;
        m_aRect.height = aVec.y;
    }
    public virtual Color GetColor()
    {
        return MyColor;
    }
    public virtual void SetColor(Color aColor)
    {
        MyColor = aColor;
    }
    public bool GetBad()
    {
        return m_bBad;
    }
    //Here we draw the texture
}
