using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
namespace IDrag
{
    public class Globals
    {
        public const float PI2 = 6.283185307179586476925286766559f;//2 PI
        public const float PI2DIV = 0.15915494309189533576888376337251f;//Times by this to divide by 2 PI
        public const float PI = 3.1415926535897932384626433832795f;//3.14 pi der
        public const float PIDIV = 0.31830988618379067153776752674503f;//use this to divide by PI
    }
    //public class D2Camera // 2d Camera
    //{
    //    public static Vector2 PixelSize;
    //    public static bool Calibrate()
    //    {
    //        PixelSize = new Vector2(1.0f / Screen.width, 1.0f / Screen.height);
    //        return true;
    //    }
    //    public static Rect DrawPos(Rect ObjectRect)
    //    {
    //        return new Rect((ObjectRect.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - ObjectRect.height * 0.5f) * PixelSize.y, (ObjectRect.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - ObjectRect.height * 0.5f) * PixelSize.y);
    //    }
    //}
    public class D2Camera // 2d Camera
    {
        public static Vector2 PixelSize;
        static Vector2 CameraPos;
        public static bool Calibrate()
        {
            PixelSize = new Vector2(1.0f / Screen.width, 1.0f / Screen.height);
            return true;
        }
        public static bool Update(Vector2 PlayerPos)
        {
            CameraPos = PlayerPos - new Vector2(Screen.width, Screen.height) * 0.5f;
            return true;
        }
        public static Vector2 GetPosRel(Rect ObjectRect)
        {
            return new Vector2(ObjectRect.x - CameraPos.x, ObjectRect.y - CameraPos.y);
        }
        public static Rect DrawPos(Rect ObjectRect)
        {
            return new Rect((ObjectRect.x - CameraPos.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - CameraPos.y - ObjectRect.height * 0.5f) * PixelSize.y, (ObjectRect.x - CameraPos.x + ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - CameraPos.y + ObjectRect.height * 0.5f) * PixelSize.y);
        }
    }
    public class Random
    {
        private static int m_iNumber = 0;
        public static void SetSeed(int a_iNumber)
        {
            m_iNumber = a_iNumber;
        }
        public static int GetRandom(int low, int high)
        {
            m_iNumber = (m_iNumber * (int)22695477 + (int)1) % (int)0x7FFFFFFF;
            if (m_iNumber < 0) // check to see if its negetive if it is make it possitive
                return ((m_iNumber * -1) % (high + 1 - low)) + low;
            return (m_iNumber % (high + 1 - low)) + low; // we h - l = total range then you add the low to put it into perspective
        }
    }

    public class Math
    {
        public static float Sqr(float z)
        {
            return z * z;
        }
        public static float Sum(float y, int z)
        {
            float x = 0;
            for (int i = 1; i <= z; ++i)
            {
                x += y * i;
            }
            return x;
        }
        public static float Sqrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.tmp = 0;
            u.f = z;
            u.tmp -= 1 << 23; /* Subtract 2^m. */
            u.tmp >>= 1; /* Divide by 2. */
            u.tmp += 1 << 29; /* Add ((b + 1) / 2) * 2^m. */
            return u.f;
        }
        [StructLayout(LayoutKind.Explicit)]
        private struct FloatIntUnion
        {
            [FieldOffset(0)]
            public float f;

            [FieldOffset(0)]
            public int tmp;
        }
    }
    public class Shapes
    {
        public static void CreateLine(Rect aRect, Color aColor = new Color())
        {
            GL.Begin(GL.LINES);
            GL.Color(aColor);
            GL.TexCoord2(0, 0);
            GL.Vertex3(aRect.x, aRect.y, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 1);
            GL.Vertex3(aRect.width, aRect.height, 0.1F);
        }
        public static void CreateLine(Vector2 Pos1, Vector2 Pos2, Color aColor1 = new Color(), Color aColor2 = new Color())
        {
            GL.Begin(GL.LINES);
            GL.Color(aColor1);
            GL.TexCoord2(0, 0);
            GL.Vertex3(Pos1.x * D2Camera.PixelSize.x, Pos1.y * D2Camera.PixelSize.y, 0.1F);
            GL.Color(aColor2);
            GL.TexCoord2(1, 1);
            GL.Vertex3(Pos2.x * D2Camera.PixelSize.x, Pos2.y * D2Camera.PixelSize.y, 0.1F);
        }
        public static void CreateBox(Rect aRect, Color aColor = new Color())
        {
            GL.Begin(GL.QUADS);
            GL.Color(aColor);
            GL.TexCoord2(0, 0);
            GL.Vertex3(aRect.x, aRect.y, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(0, 1);
            GL.Vertex3(aRect.x, aRect.height, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 1);
            GL.Vertex3(aRect.width, aRect.height, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 0);
            GL.Vertex3(aRect.width, aRect.y, 0.1F);
        }

    }
}