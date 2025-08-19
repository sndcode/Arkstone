using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Timer = System.Threading.Timer;

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
             MapID        = classMemory.ReadInt(classOffsets.tbc_coor_mapid);
             GCursor      = classMemory.ReadInt(classOffsets.tbc_sys_cursor);
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
            groupBox1.Enabled = true;
            //classMemory.WriteFloat(0x008C8398, 0.01f);
        }

        private void readPlayerbaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ReadMemory();
            MessageBox.Show("");

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
            try
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
            }catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }

           
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
            MessageBox.Show("WoW Hack for patch 2.4.3 " +
                "\nMade by SND " +
                "\nOffsets and reversing data : " +
                "\nOwnedcore " +
                "\nUC-Forum");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(classMemory.ReadInt(classOffsets.tbc_player_wallangle).ToString());
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
            groupBox4.Enabled = false;

            m_ReadMemory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0x26CC });
            classMemory.WriteBytes(pointer, new byte[] { Convert.ToByte(textBox1.Text) });
        }
      
        private void button4_Click(object sender, EventArgs e)
        {
            int basePtr = 0x00E29D28;           // Basisadresse oder Moduladresse + Offset
            int[] offsets = { 0x26CC };         // deine Offsets
            byte value = classMemory.ReadByteFromPointer(basePtr, offsets);
            textBox1.Text = value.ToString();
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

        private Timer freezeTimer;
        private int pointer;

        public void ToggleFreeze(bool enabled)
        {
            pointer = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xC23 });

            if (enabled)
            {
                freezeTimer = new Timer(_ =>
                {
                    classMemory.WriteBytes(pointer, new byte[] { 129 });
                }, null, 0, 1000); // Alle 1000ms (1 Sekunde) Wert setzen
            }
            else
            {
                freezeTimer?.Dispose();
                classMemory.WriteBytes(pointer, new byte[] { 129 });

                int pointer_fall = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xC20 });
                classMemory.WriteDouble(pointer_fall, 1000);
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //OG Method - Works on most emulators but not on custom cores.
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
                int pointer_fall = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xC20 });
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
            txt_x.Text = X.ToString();
            txt_y.Text = Y.ToString();
            txt_z.Text = Z.ToString();
            txt_mapID.Text = MapID.ToString();
            txt_rotation.Text = Rotation.ToString();
        }
        // Spielerkoordinaten setzen
        void SetPlayerPosition(float x, float y, float z, int mid , float rtv)
        {
            //Set MapID
            classMemory.WriteInt(classOffsets.tbc_coor_mapid, mid);
            //Set X Y Z 
            int player_x = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF0 });
            classMemory.WriteFloat(player_x, x);
            int player_y = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF4 });
            classMemory.WriteFloat(player_y, y);
            int player_z = classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF8 });
            classMemory.WriteFloat(player_z, z);
            //Set Rotation
            classMemory.WriteFloat(classOffsets.tbc_player_rotation, rtv);
            //Moveflag change
            int pointer = classMemory.GetPointerAddress(0x00E29D28, new int[] { 0xC23 });
            classMemory.WriteBytes(pointer, new byte[] { 128 });
            Thread.Sleep(500);
            classMemory.WriteBytes(pointer, new byte[] { 16 });
        }
        private void button5_Click(object sender, EventArgs e)
        {
            float newx              = float.Parse(txt_x.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            float newy              = float.Parse(txt_y.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            float newz              = float.Parse(txt_z.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            float newRotation       = float.Parse(txt_rotation.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            int mid                 = Convert.ToInt32(txt_mapID.Text);

            SetPlayerPosition(newx, newy, newz,mid, newRotation);

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

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                groupBox4.Enabled = true;
            }
            else if (checkBox10.Checked == false)
            {
                groupBox4.Enabled = false;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            double x = classMemory.ReadFloat(classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF0 }));
            double y = classMemory.ReadFloat(classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF4 }));
            double z = classMemory.ReadFloat(classMemory.GetPointerAddress(classOffsets.tbc_player_base, new int[] { 0xBF8 }));

            // Array mit den Daten erstellen, mit invariant culture
            string[] data = new string[]
            {
                x.ToString(CultureInfo.InvariantCulture),
                y.ToString(CultureInfo.InvariantCulture),
                z.ToString(CultureInfo.InvariantCulture),
                MapID.ToString(),
                Rotation.ToString(CultureInfo.InvariantCulture)
            };

            string locationName = txt_locName.Text;
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Application.StartupPath + "\\teleport_locations\\");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // Dateipfad neben Programm
            string filePath = folderPath + locationName + ".txt";

            // Datei schreiben mit Semikolon als Trennzeichen
            File.WriteAllText(filePath, string.Join(";", data));

            MessageBox.Show($"Daten für {locationName} gespeichert!");
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ofd.Filter = "Text files (*.txt)|*.txt";
                ofd.Title = "Wähle eine gespeicherte Position";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string content = File.ReadAllText(ofd.FileName);
                    string[] parts = content.Split(';');

                    if (parts.Length >= 5)
                    {
                        txt_x.Text = parts[0];
                        txt_y.Text = parts[1];
                        txt_z.Text = parts[2];
                        txt_mapID.Text = parts[3];
                        txt_rotation.Text = parts[4];
                        txt_locName.Text = Path.GetFileName(ofd.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Ungültiges Format der Datei!");
                    }
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //int pointer = classMemory.GetPointerAddress(0x00DA563C, new int[] { 0x1 });
            //classMemory.WriteBytes(pointer, new byte[] { 0 });
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                ToggleFreeze(true);
            }
            else
            {
                ToggleFreeze(false);
            }
        }

        private void readHacksValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alwaysOnTOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TopMost != true)
                TopMost = true;
            else
                TopMost = false;
        }
    }
}
