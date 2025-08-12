using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkstone
{
    class classOffsets
    {

        // Basispointer des Spielers im Speicher (fixer Adressbereich)
        public static int tbc_player_base = 0x00E29D28;

        // Koordinaten (X, Y, Z) des Spielers im Speicher (direkte Adressen)
        public static int tbc_coord_x = 0x00E18DF4;
        public static int tbc_coord_y = 0x00E18DF8;
        public static int tbc_coord_z = 0x00E18DFC;

        // Map ID des Spielers
        public static int tbc_coor_mapid = 0x00E18DB4;

        // Rotation des Spielers (Winkel)
        public static int tbc_player_rotation = 0x00E18E24;

        // Spielername im Speicher (String)
        public static int tbc_player_name = 0x00D43348;

        // Fallspeed des Spielers (für Physik)
        public static int tbc_player_fallspeed = 0x00BC4AF8;

        // System Cursor (für Mauszeiger-Position)
        public static int tbc_sys_cursor = 0x00CF5750;

        // Wandwinkel (Spieler Ausrichtung an Wänden)
        public static int tbc_player_wallangle = 0x008C8398;

        // Prozessname des WoW-Clients
        public static string wow_processname = "Wow";


        // Beispiel Pointer-Adressen, die via Base + Offsets gelesen werden:
        public static int tbc_player_falldamage_pointer = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC5C });
        public static int tbc_player_speed_pointer = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC70 });
        public static int tbc_player_flyspeed_pointer = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC80 });
        public static int tbc_player_pointer_x = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC28 });
        public static int tbc_player_pointer_y = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC2C });
        public static int tbc_player_pointer_z = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC60 });
        public static int tbc_player_pointer_jumpstate = classMemory.GetPointerAddress(tbc_player_base, new int[] { 0xC23 });
        /*
         * 00E29D28 is the base address
        008C8398 MC angle default value 0.6427 (float)

        Most movement related offsets *grey ones have a decent use*

        C00 points to vertical orientation, no default value (float)
        C20 points to movement state(hard) 0 default value (float,2 byte) {bitmask}
        C23 points to movement state(easy) 128 default value (4 bytes)
        C28 points to starting X point, X coord default value (float)
        C2C points to starting Y point, Y coord default value (float)
        C30 points to height in water, no default value (float)
        C34 points to orientation point, no default value (float) *point at which you start*
        C38 points to V orientation point, no default value (float) *point at which you start*
        C3C points to odd movement thing, no default value (double)
        C40 points to forward movement angle, no default value (float)
        C44 points to forward movement angle, no default value (float)
        C48 points to turning movement angle, no default value (float)
        C4C points to turning movement angle, no default value (float)
        C50 points to turning movement angle, no default value (float)
        C54 points to allowed to turn while moving, no default value (float) *test*
        C5C points to jump state, 824 default value (4 byte)
        C60 points to starting Z point, Z coord, default (float) *jump starting position*
        C68 points to current speed, no default value (float) *effects all other speeds also while moving!*
        C6C points to walk speed 2.5 default value (float)
        C70 points to run(forward) 7 default value (float)
        C74 points to run(backward) 4.5 default value (float)
        C78 points to swim(forward) 4.72222185134888 default value (Float)
        C7C points to swim(backward) 2.5 default value (float)
        C80 points to flying speed 7 default value (float) *changes forward and backward*
        C84 points to flying speed(backward) 4.5 default value (float)
        C88 points to turning speed, 3.14 default value (float)
        C8C points to jump height, -7.955547 default value *after jump* (float)


        location
        BF0 points to X coord, no default value (float)
        BF4 points to Y coord, no default value (float)
        BF8 points to Z coord, no default value (float)
        BFC points to orientation, no default value (float)


        MISC
        3AC8 points to hunter tracking, 0 default value (byte)
        9C points to player scale, 1 default value (float)
        28E4 points to emote state, 0 default value (4 byte)
        26CC points to player faction, no default value (4 byte)
        F40 points to casting spell, 0 default value (4 byte)


        00DA563C address
        80 points to can mount, no default value (byte)



        here are also some notes i took on it.

        movement state

        normal
        128

        whisp * walk on water*
        16


        floating (levitate)
        64

        flyhack (can land)
        1

        flyhack (can't land *swim-like*)
        2

        (flyhack *theres alot of different ones heres another, swim-like*)
        130

        (flyhack actual gm-like value)
        129

        80 (floaty dead?)

        (dead) *walk on water*
        144

        slow fall
        160


        hunter tracking

        Nothing
        0

        Beasts
        1

        Dragonkin
        2

        Demons
        4

        Elementals
        8

        Giants
        16

        Undead
        32

        Humanoids
        64

        Misc
        132

        Everything
        255



        emote state

        None 0
        Talk 1
        Bow 2
        Wave 3
        Cheer 4
        Exclamation 5
        Question 6
        Eat 7
        Emote State Dance 10
        Laugh 11
        Emote State Sleep 12
        Emote State Sit 13
        Rude 14
        Roar 15
        Kneel 16
        Kiss 17
        Cry 18
        Chicken 19
        Beg 20
        Applouad 21
        Shout 22
        Flex 23
        Shy 24
        Point 25
        Wound 33
        Kick 60
        Stun 64
        Dead 65
        Salute 66
        Kneel 68
        Dance 94
        LiftOff 254
        Yes 273
        No 274
        Train 275
        Land 293
        MountSpecial 377
        Talk 378
        Fishing 379
        Fishing 380
        Loot 381
        JumpLandRun 394
        JumpLand 395
        Jumpstart 399
        DanceSpecial (Human Only) 400
        DanceSpecial (Human Only) 401
        Execlaim 412
        Sit Chair 415
        0x0046E0D0 -> CreateCurMgr
        0x005DC6F0 -> SellItem
        0x006D0BF5 -> NewWardenPatch
        0x004A6690 -> SelectUnit
        0x00C896BC -> CInputControl
        0x005343A0 -> CInputControl::SetFlags
        0x00647418 -> ModelEdit
        0x00615127 -> NameplatePatch
        0x00613960 -> CanAttack
        0x00610C00 -> GetUnitRelation
        0x006425A0 -> UpdateGameTime
        0x00642689 -> TimeSetPatch
        0x00641707 -> TimeSetPatch2
        0x007B9DE0 -> SetFacing
        0x0060D9A0 -> GetUnitType
        0x0046FFDB -> GlueXML_SignaturePatch
        0x0046B610 -> GetObjectByGUID
        0x0074A160 -> Lua_FuncPatch
        0x0048DA51 -> TranslatePatch
        0x0052E704 -> GlueXML_RenamePatch
        0x0049DBB2 -> Lua_Patch
        0x005FA050 -> GetItemIDByName
        0x005F8A50 -> UseItem
        0x005BCBB0 -> Checksum
        0x00574FF0 -> Base_DBCache
        0x00591600 -> DBCache::GetInfoBlockByID
        0x00707850 -> BroadcastEvent
        0x008C839B -> MountainClimbPatch
        0x005E5130 -> ObjectTracking
        0x005E50A0 -> UnitTracking
        0x0046FFDE -> FrameXML_SignaturePatch
        0x00E11AB8 -> TimePtr
        0x00E1DBCC -> EventBase
        0x005DC315 -> Repop
        0x00573C90 -> RegisterBase_ClientDB
        0x004745A0 -> GetRow_ClientDB
        0x0049059B -> Language_Restriction
        0x005DC790 -> BuyItem
        0x004AF580 -> AFK jump
        0x007478D0 -> InputEvent
        0x004AB5B0 -> GetCamera
        0x00622520 -> ObjectUpdate
        0x007BA4C0 -> NoFallDamage
        0x00706C80 -> Lua_Dostring
        0x007059B0 -> Lua_Register
        0x00401AE0 -> Lua_Reload
        0x0072DAE0 -> lua_gettop
        0x0072DF40 -> lua_tonumber
        0x0072DF80 -> lua_tointeger
        0x0072DFF0 -> lua_tostring
        0x0072E120 -> lua_touserdata
        0x0072DFC0 -> lua_toboolean
        0x0072E1A0 -> lua_pushnumber
        0x0072E1D0 -> lua_pushinteger
        0x0072E200 -> lua_pushstring
        0x0072E3B0 -> lua_pushboolean
        0x0072E2F0 -> lua_pushcclosure
        0x0072E180 -> lua_pushnil
        0x0072E7E0 -> lua_setfield
        0x0072F710 -> lua_getfield
        0x0072DC80 -> lua_replace

                   */



        /*----------------------------------
WoW Offset Dumper 0.1
by kynox

Credits:
bobbysing, Patrick, Dominik, Azorbix
-----------------------------------*/

        //        // Objects
        //#define createObjectManager 0x0046E0D0
        //#define ClntObjMgrGetActivePlr 0x00402F40
        //#define ClntObjMgrGetActivePlrGuid 0x00469DD0
        //#define ClntObjMgrObjectPtr 0x0046B610
        //#define ClntObjMgrEnumObjects 0x0046B3F0

        //        // Descriptors
        //#define s_objectDescriptors 0x00B95890
        //#define s_itemDescriptors 0x00B95930
        //#define s_containerDescriptors 0x00B958F4
        //#define s_unitDescriptors 0x00B95A48
        //#define s_playerDescriptors 0x00B96128
        //#define s_gameobjectDescriptors 0x00B97410
        //#define s_dynamicobjDescriptors 0x00B97550
        //#define s_corpseDescriptors 0x00B97608

        //        // Console
        //#define consoleColourTable 0x00BA59BC
        //#define consoleAddCommand 0x0063F140
        //#define consoleAddLine 0x0063BEE0
        //#define consoleCommandHandler 0x00000000

        //        // Misc
        //#define GxDevicePtr 0x00C71C24
        //#define entryPointOffset 0x00E1F830
        //#define endPointOffset 0x00E1F834
        //#define crcCheck 0x005CB130

        //#define s_GxDevWindow 0x00E1F894
        //#define g_clientConnection 0x00D43318
        //#define WorldFrame__GetCamera 0x004AB5B0
        //#define UnitModel__GetModel 0x006075C0
        //#define ObjectModel__MinDisplayID 0x00BA00CC
        //#define ObjectModel__MaxDisplayID 0x00BA00C8
        //#define ObjectModel__ModelList 0x00BA00D8
        //#define ObjectModel__UpdateModel 0x00622520
        //#define OsGetAsyncTimeMs 0x00749850




    }
}
