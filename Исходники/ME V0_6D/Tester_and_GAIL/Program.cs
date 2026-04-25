using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Geometry_2D;
using Geometry_3D;
using Collision;
using PhisModel;
using PhisAxeomes;
using System.Net.NetworkInformation;
using PhisGame;

namespace Tester_and_GAIL
{
    public class Game : GameWindow
    {
        public float pw1 = 1; public float pw2 =1;

        public int ScreanW, ScreanH;
        public List<Dot> IDots = new List<Dot>();
        public List<Line> ILins = new List<Line>();
        public List<Triangle> ITriangles = new List<Triangle>();
        public List<Dot> Ints = new List<Dot>();
        public List<Collider> Colliders = new List<Collider>();
        public int FocusIndex;

        public PhysTriangle T1;
        public PhysTriangle T2;

        public Collider Col1, Col2;

        public Dot C1 = new Dot(0,0), C2 = new Dot(0,0);

        public Dot Center = new Dot(0, 0);
        public Color CenterColor = Color.Black;

        float MaxSpeed = 0;

        float FPS;
        float argT;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings,nativeWindowSettings)
        {
            ScreanW = 1920;
            ScreanH = 1080;
            ScreanH = nativeWindowSettings.Size.Y;
            ScreanW = nativeWindowSettings.Size.X;
        }
        protected override void OnLoad()
        {
            base.OnLoad();
        }
        public Dot Speed = new Dot(0,0);
        public Dot Speed2 = new Dot(0,0);
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            argT += (float)args.Time;
            FPS++;
            if (argT >= 1)
            {
                Title = "FPS - " + FPS;
                FPS = 0;
                argT = 0;
            }

            GL.ClearColor(Color.Aqua);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            
            Center.Draw(10f, CenterColor);
            /*if (MouseState.IsButtonReleased(MouseButton.Left))
            {
                IDots.Add(new Dot((MousePosition.X * 2 - ScreanW) / ScreanW, ((ScreanH - MousePosition.Y) * 2 - ScreanH) / ScreanH));
                if(IDots.Count >= 3)
                {
                    Colliders.Add(new ColTriangle(IDots[0], IDots[1], IDots[2]));
                    IDots.Clear();
                }
            }
            if (MouseState.IsButtonDown(MouseButton.Right))
            {
                Dot Mous = new Dot((MousePosition.X * 2 - ScreanW) / ScreanW, ((ScreanH - MousePosition.Y) * 2 - ScreanH) / ScreanH);
                Colliders[FocusIndex].SpeedVector += (Mous - Colliders[FocusIndex].center) * (float)args.Time;
            }*/
            if (MouseState.IsButtonReleased(MouseButton.Left))
            {
                if (IDots.Count < 6)
                {
                    IDots.Add(new Dot((MousePosition.X * 2 - ScreanW) / ScreanW, ((ScreanH - MousePosition.Y) * 2 - ScreanH) / ScreanH));
                    /*if(IDots.Count >=2)
                    {
                        ILins.Add(new Line(IDots[IDots.Count - 1], IDots[IDots.Count - 2]));
                    }*/
                    if (IDots.Count % 3 == 0)
                    {
                        ITriangles.Add(new Triangle(IDots[IDots.Count - 1], IDots[IDots.Count - 2], IDots[IDots.Count - 3]));
                    }
                    /*if ((IDots.Count - 1) % 4 == 3 && IDots.Count >= 4)
                    {
                        ITriangles.Add(new Triangle(IDots[IDots.Count - 1], IDots[IDots.Count - 2], IDots[IDots.Count - 4]));
                    }*/
                    if(IDots.Count == 6)
                    {
                        Col1 = new ColTriangle(IDots[0], IDots[1], IDots[2]);
                        Col2 = new ColTriangle(IDots[3], IDots[4], IDots[5]);
                    }
                }
                if(IDots.Count == 6)
                {
                    T1 = new PhysTriangle(IDots[0], IDots[1], IDots[2], 1f);
                    T2 = new PhysTriangle(IDots[3], IDots[4], IDots[5], 1f);
                    //Speed = new Dot(100000000f, 0f);
                    /*T1.SpeedVector = Speed;
                    T2.SpeedVector = Speed2;*/
                }
            }
            if (MouseState.IsButtonDown(MouseButton.Left))
            {
                if (IDots.Count == 6)
                {
                    Dot Mous = new Dot((MousePosition.X * 2 - ScreanW) / ScreanW, ((ScreanH - MousePosition.Y) * 2 - ScreanH) / ScreanH);
                    Speed += (Mous - C1) * ((float)args.Time) * pw1;
                }
            }
            if (MouseState.IsButtonDown(MouseButton.Right))
            {
                if (IDots.Count == 6)
                {
                    Dot Mous = new Dot((MousePosition.X * 2 - ScreanW) / ScreanW, ((ScreanH - MousePosition.Y) * 2 - ScreanH) / ScreanH);
                    Speed2 += (Mous - C2) * ((float)args.Time) * pw2;
                }
            }
            /*if(IDots.Count <= 5)
            {
                foreach (var el in IDots)
                {
                    el.Draw(10f, Color.Red);
                }
                foreach (var el in ITriangles)
                {
                    el.Draw(Color.Yellow);
                }
            }*/
            foreach (var el in IDots)
            {
                el.Draw(10f, Color.Red);
            }
            foreach (var el in ILins)
            {

                el.Draw(5f, Color.White);
            }
            foreach (var el in ITriangles)
            {
                el.Draw(Color.Yellow);
            }
            /*foreach (var el in IDots)
            {
                el.Draw(10f, Color.Red);
            }
            if (Colliders.Count >= 1)
            {

                //TestOpti((float)args.Time);

                PhysFrame physFrame = new PhysFrame(Colliders.ToArray());
                physFrame.OnPhysFrame((float)args.Time);
                int i = 0;
                foreach (var el in Colliders)
                {
                    if(i!= FocusIndex)
                    {
                        el.Draw(Color.GreenYellow);
                        el.DrawVertex(10f, Color.Red);
                        el.center.Draw(10f, Color.HotPink);
                    }
                    else
                    {
                        el.Draw(Color.Green);
                        el.DrawVertex(10f, Color.IndianRed);
                        el.center.Draw(10f, Color.LightPink);
                    }
                    i++;
                }
                
                /*
                Colliders[FocusIndex].Draw(Color.HotPink);
                Colliders[FocusIndex].Scaling(Colliders[FocusIndex].center, 1f / (Colliders[FocusIndex].hitBox.Width * Colliders[FocusIndex].hitBox.Height)).Draw(Color.GreenYellow);*//*
            }*/
            
            if (IDots.Count >= 6)
            {
                /*
                 * New ME 0.6E
                 *
                Speed *= (float)args.Time;
                Speed2 *= (float)args.Time;

                Col1.SpeedVector = Speed; Col2.SpeedVector = Speed2;

                C1 = Col1.center; C2 = Col2.center;
                PhysFrame physFrame = new PhysFrame(new Collider[] { Col1, Col2 });
                physFrame.OnPhysFrame((float)args.Time);
                Col1.DrawVertex(10f, Color.Red);
                Col1.Draw(Color.YellowGreen);
                C1.Draw(10f, Color.Black);

                Col2.DrawVertex(10f, Color.Red);
                Col2.Draw(Color.Yellow);
                C2.Draw(10f, Color.Black);

                Speed = Col1.SpeedVector; Speed2 = Col2.SpeedVector;

                Speed /= (float)args.Time;
                Speed2 /= (float)args.Time;
                /**/
                C1 = T1.center; C2 = T2.center;
                T1.center.Draw(10f, Color.Black);
                T2.center.Draw(10f, Color.Black);

                MaxSpeed = MathF.Max(MaxSpeed, Speed.GetLong());
                Speed *= (float)args.Time;
                Speed2 *= (float)args.Time;
                if (Speed != new Dot(0, 0))
                {
                    T1 = new PhysTriangle(IDots[0], IDots[1], IDots[2], 1f);
                    T2 = new PhysTriangle(IDots[3], IDots[4], IDots[5], 1f);

                    T1.GiperBox();
                    T2.GiperBox();
                    
                    T1.SpeedVector = Speed;
                    T2.SpeedVector = Speed2;

                    GiperDot GI = ColAction.FOP(T1, T2, out bool Is1t2, out int sideI);
                    Console.WriteLine(GI);
                    
                    IDots[0] += Speed * GI.t;
                    IDots[1] += Speed * GI.t;
                    IDots[2] += Speed * GI.t;

                    IDots[3] += Speed2 * GI.t;
                    IDots[4] += Speed2 * GI.t;
                    IDots[5] += Speed2 * GI.t;

                    ITriangles[0] = new Triangle(IDots[0], IDots[1], IDots[2]);
                    ITriangles[1] = new Triangle(IDots[3], IDots[4], IDots[5]);

                    Console.WriteLine($"полу экранов в секунду: {MaxSpeed}");
                    Console.WriteLine($"m/s: {MaxSpeed / 2 * 0.6118f}");
                    Console.WriteLine($"km/h: {MaxSpeed / 2 * 0.6118f * 3.6f}");
                    //Console.WriteLine(ColAction.CollisBox(T1.hitBox, T2.hitBox));
                    if (!float.IsNaN(GI.x))
                    {
                        T1.ColCache = new ColCache(1, new ColDot(GI.x, GI.y, GI.t, Is1t2, sideI));
                        T2.ColCache = new ColCache(0, new ColDot(GI.x, GI.y, GI.t, Is1t2, sideI));
                        PushHit.PushHitFunction(T1,T2,out Speed, out Speed2);
                    }
                    if (T1.hitBox.Left <= -1) T1.SpeedVector.x = MathF.Abs(T1.SpeedVector.x);
                    if (T1.hitBox.Right >= 1) T1.SpeedVector.x = -MathF.Abs(T1.SpeedVector.x);
                    if (T1.hitBox.Top <= -1) T1.SpeedVector.y = MathF.Abs(T1.SpeedVector.y);
                    if (T1.hitBox.Bottom >= 1) T1.SpeedVector.y = -MathF.Abs(T1.SpeedVector.y);


                    if (T2.hitBox.Left <= -1) T2.SpeedVector.x = MathF.Abs(T2.SpeedVector.x);
                    if (T2.hitBox.Right >= 1) T2.SpeedVector.x = -MathF.Abs(T2.SpeedVector.x);
                    if (T2.hitBox.Top <= -1) T2.SpeedVector.y = MathF.Abs(T2.SpeedVector.y);
                    if (T2.hitBox.Bottom >= 1) T2.SpeedVector.y = -MathF.Abs(T2.SpeedVector.y);
                }
                Speed /= (float)args.Time;
                Speed2 /= (float)args.Time;

                /*Line Side = T1.triangles[0].Sids[0];
                Dot Delta = Side.End - Side.Beg;
                int Chetvert = (Delta.x >= 0 && Delta.y >= 0) ? 1 : (Delta.x >= 0) ? 2 : (Delta.y >= 0) ? 3 : 4;
                Chetvert++;
                float k = Delta.y / Delta.x;
                float u = (Chetvert == 5 || Chetvert == 3) ? -MathF.Sqrt((1 + k * k) / (1 + 1 / (k * k))) * Delta.x : MathF.Sqrt((1 + k * k) / (1 + 1 / (k * k))) * Delta.x;
                float v = -u / k;
                Dot Normal = new Dot(u, v);
                Normal /= -Normal.GetLong();
                if (ILins.Count == 0)
                    ILins.Add(new Line(Side.Beg, Side.Beg + Normal));
                else ILins[0] = new Line(Side.Beg, Side.Beg + Normal);*/
                //Console.WriteLine(Delta.ToString() + "\n " + Normal.ToString());  
                /*Console.Write((!float.IsNaN(GI.x)) ? GI.x : "");  
                if (!float.IsNaN(GI.x))
                {
                    Speed = new Dot(0,0);
                    Speed2 = new Dot(0, 0);
                    /*
                    PushHit.PushHitFunction(T1, T2, out Dot retSpeed1, out Dot retSpeed2);
                    Speed = retSpeed1;
                    Speed2 = retSpeed2;*/
             }
                /*
            }*/


            /*
            if(IDots.Count >= 8)
            {
                C1.center.Draw(10f, Color.Black);
                C2.center.Draw(10f, Color.Black);
                
                Speed *= (float)args.Time;
                Speed2 *= (float)args.Time;
                if (Speed != new Dot(0, 0))
                {
                    C1 = new Collider(new Triangle[] { ITriangles[0], ITriangles[1] });
                    C2 = new Collider(new Triangle[] { ITriangles[2], ITriangles[3] });

                    C1.SpeedVector = Speed;
                    C2.SpeedVector = Speed2;

                    GiperDot GI = ColAction.FOP(C1, C2, out bool Is1t2, out int sideI);
                    Console.WriteLine(GI + "\n" + Is1t2);
                    IDots[0] += Speed * GI.t;
                    IDots[1] += Speed * GI.t;
                    IDots[2] += Speed * GI.t;
                    IDots[3] += Speed * GI.t;

                    IDots[4] += Speed2 * GI.t;
                    IDots[5] += Speed2 * GI.t;
                    IDots[6] += Speed2 * GI.t;
                    IDots[7] += Speed2 * GI.t;

                    ITriangles[0] = new Triangle(IDots[0], IDots[1], IDots[2]);
                    ITriangles[1] = new Triangle(IDots[0], IDots[2], IDots[3]);

                    ITriangles[2] = new Triangle(IDots[4], IDots[5], IDots[6]);
                    ITriangles[3] = new Triangle(IDots[4], IDots[6], IDots[7]);
                    if (!float.IsNaN(GI.x))
                    {
                        Speed *= -1;
                        Speed2 *= -1;
                    }
                }
                Speed /= (float)args.Time;
                Speed2 /= (float)args.Time;
            }*/
            /*if(IDots.Count >= 6)
            {
                Speed *= 0.001f;
                Speed2 *= 0.001f;
                //Console.WriteLine(Speed);
                if (Speed != new Dot(0, 0))
                {
                    GiperDot GInter = GiperAction.FOP(new GTriangle(ITriangles[0], Speed), new GTriangle(ITriangles[1], Speed2));

                    //Console.WriteLine(GInter);
                    IDots[0] += Speed * GInter.t;
                    IDots[1] += Speed * GInter.t;
                    IDots[2] += Speed * GInter.t;
                }
                    
                ITriangles[0] = new Triangle(IDots[0], IDots[1], IDots[2]);
            }
            /*if (IDots.Count >= 4)
            {
                Dot Delta = (IDots[3] - IDots[2]);
                float k = Delta.y / Delta.x;
                float u = MathF.Sqrt((1 + k * k) / (1 + 1 / (k * k))) * Delta.x;
                float v = -u / k;
                Speed2 = new Dot(u, v) * 0.00001f;
                Speed2 = new Dot(Delta.x, Delta.y) * 0.00001f;
                Speed *= 0.001f;
                if(Speed.x != 0 || Speed.y != 0)
                {
                    BP_Plane BP1 = new BP_Plane(ILins[0].Beg, ILins[0].End - ILins[0].Beg, Speed);
                    BP_Plane BP2 = new BP_Plane(ILins[1].Beg, ILins[1].End - ILins[1].Beg, Speed2);
                    GiperDot Int = GiperAction.Intersection(BP1, BP2);
                    if(!float.IsNaN(Int.t))
                    {
                        IDots[0] += Speed * Int.t;
                        IDots[1] += Speed * Int.t;
                        IDots[2] += Speed2 * Int.t;
                        IDots[3] += Speed2 * Int.t;
                        ILins[0] = new Line(IDots[0], IDots[1]);
                        ILins[1] = new Line(IDots[2], IDots[3]);
                    }
                    /*
                    Dot Spd = Speed * 0.001f;
                    BP_Plane G = new BP_Plane(ILins[0].Beg, ILins[0].End - ILins[0].Beg, Spd);
                    GStraight S = new GStraight(new GiperDot(IDots[2], 0), new GiperDot(IDots[2], 1));
                    GiperDot Int = GiperAction.Intersection(S, G);
                    Console.WriteLine(Int.ToString());
                    
                    if ((Int.t < 1 && Int.t > 0) && G.Shodow.Belong(Int.ToDot2D()))
                        Spd = Speed * 0.001f * Int.t;
                    IDots[0] += Spd;
                    IDots[1] += Spd;
                    ILins[0] = new Line(IDots[0], IDots[1]);
                    Console.WriteLine(args.Time.ToString());
                    /*
                    if((Int.t<1 && Int.t>0)&&G.Shodow.Belong(Int.ToDot2D()))
                        CenterColor = Color.Green;
                    else CenterColor = Color.Red;
                }
                else
                {
                    IDots[2] += Speed2;
                    IDots[3] += Speed2;
                    ILins[1] = new Line(IDots[2], IDots[3]);
                }
            }*/
            //Speed = new Dot(0, 0);
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            /*if (KeyboardState.IsKeyReleased(Keys.W)) Speed += new Dot(0, 250f);
            if (KeyboardState.IsKeyReleased(Keys.S)) Speed += new Dot(0, -250f);
            if (KeyboardState.IsKeyReleased(Keys.D)) Speed += new Dot(250f, 0f);
            if (KeyboardState.IsKeyReleased(Keys.A)) Speed += new Dot(-250f, 0f);*/
        }
        protected override void OnMaximized(MaximizedEventArgs e)
        {
            base.OnMaximized(e);   
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            ScreanW = e.Width;
            ScreanH = e.Height;
            base.OnResize(e);
        }
    }
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("ME V:0.6D");

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(1920, 1080),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Maximized,
                StartVisible = true,
                StartFocused = true,
                Title = "NameGame",

                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            using (Game game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                Console.WriteLine(GL.GetString(StringName.Version));
                Console.WriteLine(GL.GetString(StringName.Vendor));
                Console.WriteLine(GL.GetString(StringName.Renderer));
                Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
                game.Run();
            }
        }
    }
}
