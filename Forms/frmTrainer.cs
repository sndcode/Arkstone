using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Arkstone
{
    public partial class frmTrainer : Form
    {
        public frmTrainer()
        {
            InitializeComponent();
        }

        public static string ExeName = classOffsets.wow_processname;
        public static float X ;
        public static float Y ;
        public static float Z ;
        public static float Rotation ;
        public static int MapID;
        public static int GCursor;
        public static string Charname;
        public static float SpiderValue;
        public static float playerSpeed;
        public static float flySpeed;
        public static float fallSpeed;
        public static float falldamage;

        public static void m_ReadMemory()
        {
            //Read Process Memory and fill local variables with offsets and pointers.
             X            = classMemory.ReadFloat(classOffsets.tbc_coord_x);
             Y            = classMemory.ReadFloat(classOffsets.tbc_coord_y);
             Z            = classMemory.ReadFloat(classOffsets.tbc_coord_z);
             Rotation     = classMemory.ReadFloat(classOffsets.tbc_player_rotation);
             MapID        = classMemory.ReadInteger(classOffsets.tbc_coor_mapid,255);
             GCursor      = classMemory.ReadInteger(classOffsets.tbc_sys_cursor, 255);
             Charname     = classMemory.ReadString(classOffsets.tbc_player_name, 255);
            SpiderValue   = classMemory.ReadFloat(classOffsets.tbc_player_wallangle);
            fallSpeed     = classMemory.ReadFloat(classOffsets.tbc_player_fallspeed);
            //Pointer below
            falldamage    = classMemory.ReadFloat(classOffsets.tbc_player_falldamage_pointer);
            playerSpeed   = classMemory.ReadFloat(classOffsets.tbc_player_speed_pointer);
            flySpeed      = classMemory.ReadFloat(classOffsets.tbc_player_flyspeed_pointer);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            classMemory.WriteFloat(0x008C8398, 0.01f);
        }

        private void readPlayerbaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ReadMemory();
            //Print
            //richTextBox1.Clear();
            //richTextBox1.Text += "Playerbase : " + Environment.NewLine

            //    + "Rotation : " + Rotation.ToString()   + Environment.NewLine
            //    + "X : " + X.ToString()                 + Environment.NewLine
            //    + "Y : " + Y.ToString()                 + Environment.NewLine
            //    + "Z : " + Z.ToString()                 + Environment.NewLine
            //    + "MapID : " + MapID.ToString()         + Environment.NewLine
            //    + "Name : " + Charname.ToString()       + Environment.NewLine
            //    + "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            //Credits ownedcore mem editing section!
            0 = Nothing
            1 = Beasts
            2 = Dragonkin
            4 = Demons
            8 = Elementals
            16 = Giants
            32 = Undead
            64 = Humanoids
            132 = Misc
            255 = Everything
            */

            if (checkBox1.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0x3AC8 });
                classMemory.WriteBytes(pointer, new byte[] { 255 });
            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0x3AC8 });
                classMemory.WriteBytes(pointer, new byte[] { 0 });
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Read game memory every tick and refresh variables
            m_ReadMemory();
            this.Text = "WoW 2.4.3" + " - " + Charname;
            trackBar1.Value = Convert.ToInt32(playerSpeed);
            lblWalkspeed.Text = trackBar1.Value.ToString();
            trackBar2.Value = Convert.ToInt32(flySpeed);
            lblFlySpeed.Text = trackBar2.Value.ToString();
            trackBar3.Value = Convert.ToInt32(fallSpeed);
            lblFallSpeed.Text = trackBar3.Value.ToString();

            toolStripStatusLabel1.Text = "X: " + X.ToString();
            toolStripStatusLabel2.Text = "Y: " + Y.ToString();
            toolStripStatusLabel3.Text = "Z: " + Z.ToString();
            toolStripStatusLabel4.Text = "MID: " + MapID.ToString();
            toolStripStatusLabel5.Text = "Rot: " + Rotation.ToString();
        }

        private void frmTrainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void killWoWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Wow");
            proc[0].Kill();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WoW Hack for patch 2.4.3 \nMade by SND \nOffsets and reversing data : \nOwnedcore \nUC-Forum");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(classMemory.ReadInteger(classOffsets.tbc_player_wallangle, 255).ToString());
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = trackBar1.Value;
            int pointer = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xC70 });
            classMemory.WriteFloat(pointer, value);
            lblWalkspeed.Text = value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int value = trackBar2.Value;
            int pointer = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xC80 });
            classMemory.WriteFloat(pointer, value);
            lblFlySpeed.Text = value.ToString();
        }

        private void frmTrainer_Load(object sender, EventArgs e)
        {
            #if DEBUG
            groupBox2.Enabled = true;
            #else
            groupBox2.Enabled = false;       
            #endif

            m_ReadMemory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0x26CC });
            classMemory.WriteBytes(pointer, new byte[] { Convert.ToByte(textBox1.Text) });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0x26CC });
            textBox1.Text = pointer.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                classMemory.WriteDouble(0x0088D5E8, 2);
            }
            else
            {
                classMemory.WriteDouble(0x0088D5E8, 0.5);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
                classMemory.WriteBytes(pointer, new byte[] { 129 });
            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
                classMemory.WriteBytes(pointer, new byte[] { 128 });
                //Set to falling again otherwise player will walk on air
                int pointer_fall = classMemory.GetPointerAddress(classOffsets.tbc_player_base , new int[] { 0xC20 });
                classMemory.WriteDouble(pointer_fall, 1000);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
                classMemory.WriteBytes(pointer, new byte[] { 16 });
            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
                classMemory.WriteBytes(pointer, new byte[] { 128 });
            }
        }

        private void attachAntiAFKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                classAFK.launchAntiAfk();
            }).Start();

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(0x00DA563C, new int[] { 0x80 });
                classMemory.WriteBytes(pointer, new byte[] { 1 });
            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00DA563C, new int[] { 0x80 });
                classMemory.WriteBytes(pointer, new byte[] { 0 });
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            float value = trackBar3.Value;
            classMemory.WriteFloat(0x00BC4AF8, value);
            lblFallSpeed.Text = value.ToString();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xCA8 });
                classMemory.WriteFloat(pointer, 0f);
                //Also set player to walk straight on air to not fall thru ground
                int pointer2 = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
                classMemory.WriteBytes(pointer2, new byte[] { 128 });

            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xCA8 });
                classMemory.WriteFloat(pointer, 0.2777f);
                //Set to falling again otherwise player will walk on air
                int pointer_fall = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC20 });
                classMemory.WriteDouble(pointer_fall, 1000 );
            }
            
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xCB0 });
                classMemory.WriteFloat(pointer, 220f);
            }
            else
            {
                int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xCB0 });
                classMemory.WriteFloat(pointer, 1f);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                classMemory.WriteDouble(0x0088D5E8, 0.2);
            }
            else
            {
                classMemory.WriteDouble(0x0088D5E8, 0.5);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = X.ToString();
            textBox3.Text = Y.ToString();
            textBox4.Text = Z.ToString();
            textBox5.Text = MapID.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //IHNI
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                classMemory.WriteDouble(classOffsets.tbc_player_falldamage_pointer, 0.0);
            }
            else
            {
                classMemory.WriteDouble(classOffsets.tbc_player_falldamage_pointer, 824);
            }
        }
    }
}
