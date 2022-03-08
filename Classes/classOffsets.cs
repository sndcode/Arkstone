using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkstone
{
    class classOffsets
    {

        //Patch 2.4.3 Offsets and pointers
        //Credits ownedcore mem editing section!
        public static int tbc_player_base = 0x00E29D28;
        public static int tbc_coord_x = 0x00E18DF4;
        public static int tbc_coord_y = 0x00E18DF8;
        public static int tbc_coord_z = 0x00E18DFC;
        public static int tbc_coor_mapid = 0x00E18DB4;
        public static int tbc_player_rotation = 0x00E18E24;
        public static int tbc_player_name = 0x00D43348;
        public static int tbc_player_fallspeed = 0x00BC4AF8;
        public static int tbc_sys_cursor = 0x00CF5750;
        public static int tbc_player_wallangle = 0x008C8398;
        public static string wow_processname = "Wow";

        //Pointer
        public static int tbc_player_falldamage_pointer 
            = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC5C });
        public static int tbc_player_speed_pointer 
            = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC70 });
        public static int tbc_player_flyspeed_pointer 
            = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC80 });
        
    }
}
