namespace CeneticEnergy
{
    public partial class Form1 : Form
    {
        PhysicalBody PB1;
        PhysicalBody PB2;
        public List<PhysicalBody> PBS;
        public float GEX,GIX;
        public float GEY,GIY;
        public Random randomGenerator;
        public float FPS;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            PB1 = new PhysicalBody(0, 1, 250);
            PB2 = new PhysicalBody(-5, 32, 500);*/
            PBS = new List<PhysicalBody>();
            FPS = 100f / UpDeta.Interval;
            //PBS.Add(new PhysicalBody(0,-1,500,500,s));
            randomGenerator = new Random();
        }

        private void UpDeta_Tick(object sender, EventArgs e)
        {
            // Colision 

            /*if (PhysicalBody.IsCollision(PBS[0], PBS[1])) PBS[0].pictureBox1.BackColor = Color.Aqua;
            else PBS[0].pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;*/

            // reflection 

            

            /*if (PB1.left <= 0)
            {
                PB1.v = MathF.Abs(PB1.v);
            }
            if (PB1.right >= Program.f1.Width)
            {
                PB1.v = -MathF.Abs(PB1.v);
            }
            if (PB2.left <= 0)
            {
                PB2.v = MathF.Abs(PB2.v);
            }
            if (PB2.right >= Program.f1.Width)
            {
                PB2.v = -MathF.Abs(PB2.v);
            }*/
            for (int i = 0; i < PBS.Count - 1; i++)
            {
                for (int l = i + 1; l < PBS.Count; l++)
                {
                    if (PhysicalBody.IsCollision(PBS[i], PBS[l]))
                    {
                        float modG1 = MathF.Abs(PBS[i].right - PBS[l].left);
                        float modG2 = MathF.Abs(PBS[i].left - PBS[l].right);
                        float modV1 = MathF.Abs(PBS[i].bottom - PBS[l].top);
                        float modV2 = MathF.Abs(PBS[i].top- PBS[l].bottom);
                        bool Vertical, Horizontal;
                        Horizontal = MathF.Min(modG2, modG1) <= MathF.Min(modV2, modV1);
                        Vertical = MathF.Min(modG2, modG1) >= MathF.Min(modV2, modV1);

                        if (Horizontal)
                        {
                            GEX = (PBS[i].vx * PBS[i].vx * PBS[i].mass + PBS[l].vx * PBS[l].vx * PBS[l].mass) / 2;
                            GIX = PBS[i].vx * PBS[i].mass + PBS[l].vx * PBS[l].mass;
                            float ax = PBS[l].mass * (PBS[i].mass + PBS[l].mass);
                            float bx = -2 * GIX * PBS[l].mass;
                            float cx = GIX * GIX - 2 * GEX * PBS[i].mass;
                            float DX = bx * bx - 4 * ax * cx;
                            float vx1 = (-bx + MathF.Sqrt(DX)) / (2 * ax), vx2 = (-bx - MathF.Sqrt(DX)) / (2 * ax);
                            float sx1 = (GIX - PBS[l].mass * vx1) / PBS[i].mass, sx2 = (GIX - PBS[l].mass * vx2) / PBS[i].mass;

                            float q1 = MathF.Abs(PBS[i].left - PBS[l].left + vx1 - sx1);
                            float q2 = MathF.Abs(PBS[i].left - PBS[l].left + vx2 - sx2);

                            if (q1 <= q2)
                            {
                                PBS[l].vx = vx1;
                                PBS[i].vx = sx1;
                            }
                            else
                            {
                                PBS[l].vx = vx2;
                                PBS[i].vx = sx2;
                            }

                            /*if (IsFirstCloser((float)vx2, (float)vx1, PBS[l].vx))
                            {
                                PBS[l].vx = (float)vx1;
                                PBS[i].vx = (float)((GIX - PBS[l].mass * vx1) / PBS[i].mass);
                            }
                            else
                            {
                                PBS[l].vx = (float)vx2;
                                PBS[i].vx = (float)((GIX - PBS[l].mass * vx2) / PBS[i].mass);
                            }*/
                        }
                        if (Vertical)
                        {
                            GEY = (PBS[i].vy * PBS[i].vy * PBS[i].mass + PBS[l].vy * PBS[l].vy * PBS[l].mass) / 2;
                            GIY = PBS[i].vy * PBS[i].mass + PBS[l].vy * PBS[l].mass;
                            float ay = PBS[l].mass * (PBS[i].mass + PBS[l].mass);
                            float by = -2 * GIY * PBS[l].mass;
                            float cy = GIY * GIY - 2 * GEY * PBS[i].mass;
                            float DY = by * by - 4 * ay * cy;
                            float vy1 = (-by + MathF.Sqrt(DY)) / (2 * ay), vy2 = (-by - MathF.Sqrt(DY)) / (2 * ay);
                            float sy1 = (GIY - PBS[l].mass * vy1) / PBS[i].mass, sy2 = (GIY - PBS[l].mass * vy2) / PBS[i].mass;

                            float q1 = MathF.Abs(PBS[i].bottom - PBS[l].bottom+ vy1 - sy1);
                            float q2 = MathF.Abs(PBS[i].bottom - PBS[l].bottom + vy2 - sy2);

                            if (q1 <= q2)
                            {
                                PBS[l].vy = vy1;
                                PBS[i].vy = sy1;
                            }
                            else
                            {
                                PBS[l].vy = vy2;
                                PBS[i].vy = sy2;
                            }

                            /*if (IsFirstCloser((float)vy2, (float)vy1, PBS[l].vy))
                            {
                                PBS[l].vy = (float)vy1;
                                PBS[i].vy = (float)((GIY - PBS[l].mass * vy1) / PBS[i].mass);
                            }
                            else
                            {
                                PBS[l].vy = (float)vy2;
                                PBS[i].vy = (float)((GIY - PBS[l].mass * vy2) / PBS[i].mass);
                            }*/
                        }
                    }
                }
            }
            
            foreach(var i in PBS)
            {
                if (i.left <= 0)
                {
                    i.vx = MathF.Abs(i.vx);
                }
                if (i.right >= Program.f1.Width)
                {
                    i.vx = -MathF.Abs(i.vx);
                }
                if (i.bottom <= 50)
                {
                    i.vy = MathF.Abs(i.vy);
                }
                if (i.top >= Program.f1.Height)
                {
                    i.vy = -MathF.Abs(i.vy);
                }
                // GO
                i.left += i.vx/FPS;
                i.bottom += i.vy/FPS;
            }

            // Interaction(ref PB1, ref PB2);

            // Go 
            /*PB1.left += PB1.v;
            PB2.left += PB2.v;*/

        }
        public bool IsFirstCloser(float First, float Second, float starting_point) => MathF.Abs(First - starting_point) < MathF.Abs(Second - starting_point);

        Point BackPosMouse;
        float massLevel=8;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            BackPosMouse = new Point(e.X, Program.f1.Height - e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            PBS.Add(new PhysicalBody((e.X - BackPosMouse.X)*75 / FPS, ((Program.f1.Height - e.Y) - BackPosMouse.Y)*75 / FPS, BackPosMouse.X, BackPosMouse.Y, randomGenerator.NextSingle()*massLevel));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) massLevel += 1;
            if(e.KeyCode == Keys.Left) massLevel -= 1;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PBS.Count - 1; i++)
            {
                for (int l = i + 1; l < PBS.Count; l++)
                {
                    if (PhysicalBody.IsCollision(PBS[i], PBS[l]))
                    {
                        GEX = (PBS[i].vx * PBS[i].vx * PBS[i].mass + PBS[l].vx * PBS[l].vx * PBS[l].mass) / 2;
                        GIX = PBS[i].vx * PBS[i].mass + PBS[l].vx * PBS[l].mass;
                        double ax = PBS[l].mass * (PBS[i].mass + PBS[l].mass);
                        double bx = -2 * GIX * PBS[l].mass;
                        double cx = GIX * GIX - 2 * GEX * PBS[i].mass;
                        double DX = bx * bx - 4 * ax * cx;
                        double vx1 = (-bx + Math.Sqrt(DX)) / (2 * ax), vx2 = (-bx - Math.Sqrt(DX)) / (2 * ax);

                        if (IsFirstCloser((float)vx2, (float)vx1, PBS[l].vx))
                        {
                            PBS[l].vx = (float)vx1;
                            PBS[i].vx = (float)((GIX - PBS[l].mass * vx1) / PBS[i].mass);
                        }
                        else
                        {
                            PBS[l].vx = (float)vx2;
                            PBS[i].vx = (float)((GIX - PBS[l].mass * vx2) / PBS[i].mass);
                        }
                    }
                }
            }
        }

        public void Interaction(ref PhysicalBody PB1, ref PhysicalBody PB2)
        {
            if (PhysicalBody.IsCollision(PB1, PB2))
            {
                GEX = PB1.E + PB2.E;
                GIX = PB1.I + PB2.I;
                double a = PB2.mass * (PB1.mass + PB2.mass);
                double b = -2 * GIX * PB2.mass;
                double c = GIX * GIX - 2 * GEX * PB1.mass;
                double D = b * b - 4 * a * c;
                double v1 = (-b + Math.Sqrt(D)) / (2 * a), v2 = (-b - Math.Sqrt(D)) / (2 * a);

                if (IsFirstCloser((float)v2, (float)v1, PB2.v))
                {
                    PB2.v = (float)v1;
                    PB1.v = (float)((GIX - PB2.mass * v1) / PB1.mass);
                }
                else
                {
                    PB2.v = (float)v2;
                    PB1.v = (float)((GIX - PB2.mass * v2) / PB1.mass);
                }
            }
        }

    }
    public class PhysicalBody
    {
        public PictureBox pictureBox1;
        public float mass;
        public float v
        {
            get
            {
                return MathF.Sqrt(vx*vx+vy*vy);
            }
            set
            {
                float pv = MathF.Sqrt(vx * vx + vy * vy);
                vx *= value / pv;
                vy *= value / pv;
            }
        }
        public float vx, vy;
        public float w, h;
        public float left
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
                r = l + w;
                pictureBox1.Left = (int)value;
            }
        }
        private float l;
        
        public float right { get { return r; } }
        private float r;
        public float bottom
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                t = b + h;
                pictureBox1.Top = (int)(Program.f1.Height - h - value);
            }
        }
        private float b;
        public float top { get { return t; } }
        private float t;
        public float E
        {
            get { return (v*v*mass)/2; }
            set { v = MathF.Sqrt(2 * value / mass); }
        }
        public float I
        {
            get { return (mass * v); }
            set { v = value / mass; }
        }
        public PhysicalBody(float vx, float vy, float x, float y, float m)
        {
            mass = m;
            this.v = MathF.Sqrt(vx * vx + vy * vy);
            this.vx = vx;
            this.vy = vy;
            pictureBox1 = new System.Windows.Forms.PictureBox();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            pictureBox1.Location = new System.Drawing.Point((int)x, (int)(Program.f1.Height - y - 100 * MathF.Sqrt(mass)));
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size((int)(100 * MathF.Sqrt(mass)), (int)(100 * MathF.Sqrt(mass)));
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.Die);
            Program.f1.Controls.Add(pictureBox1);
            w = 100 * MathF.Sqrt(mass);
            h = 100 * MathF.Sqrt(mass);
            left = x;
            bottom = y;
        }
        public void Die(object sender, EventArgs e)
        {
            left = -10000;
            bottom = -10000;
            vx = 0;
            vy = 0;
        }

        public static bool IsCollision(PhysicalBody PB1, PhysicalBody PB2)
        {
            bool retX = false;
            bool retY = false;

            if (PB1.w >= PB2.w)
            {
                if ((PB1.left <= PB2.left && PB2.left <= PB1.right) || (PB1.left <= PB2.right && PB2.right <= PB1.right))
                {
                    retX = true;
                }
            }
            else
            {
                if ((PB2.left <= PB1.left && PB1.left <= PB2.right) || (PB2.left <= PB1.right && PB1.right <= PB2.right))
                {
                    retX = true;
                }
            }
            if (PB1.h >= PB2.h)
            {
                if ((PB1.bottom <= PB2.bottom && PB2.bottom <= PB1.top) || (PB1.bottom <= PB2.top && PB2.top <= PB1.top))
                {
                    retY = true;
                }
            }
            else
            {
                if ((PB2.bottom <= PB1.bottom && PB1.bottom <= PB2.top) || (PB2.bottom <= PB1.top && PB1.top <= PB2.top))
                {
                    retY = true;
                }
            }

            return retX && retY;
        }
    }
}