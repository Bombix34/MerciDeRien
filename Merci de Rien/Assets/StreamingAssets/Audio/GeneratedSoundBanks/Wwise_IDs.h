/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ENV_AMB_BEACH_PLAY = 173647080U;
        static const AkUniqueID ENV_AMB_EXT_PLAY = 1275933152U;
        static const AkUniqueID ENV_AMB_INT_PLAY = 2306680942U;
        static const AkUniqueID ENV_DOOR_CLOSE_PLAY = 96362093U;
        static const AkUniqueID ENV_DOOR_OPEN_PLAY = 1881369153U;
        static const AkUniqueID ENV_FIRE_PLAY = 2989807018U;
        static const AkUniqueID ENV_FIRE_STOP = 99041748U;
        static const AkUniqueID ENV_POT_BREAK_PLAY = 2141874203U;
        static const AkUniqueID ENV_POT_PUT_DOWN_PLAY = 2760972584U;
        static const AkUniqueID GAME_FAKE_FIGHT = 1100552122U;
        static const AkUniqueID GAME_LAUNCH = 2732978883U;
        static const AkUniqueID GAME_OVER = 1432716332U;
        static const AkUniqueID GAME_START = 733168346U;
        static const AkUniqueID MC_ATTACK_PLAY = 538908709U;
        static const AkUniqueID MC_IDLE_ANIM_PLAY = 87855283U;
        static const AkUniqueID MC_PICK_BIG_ITEM_PLAY = 2287292191U;
        static const AkUniqueID MC_PICK_ITEM_PLAY = 2246829044U;
        static const AkUniqueID MC_TALK_PLAY = 2826582739U;
        static const AkUniqueID MC_TALK_STOP = 1956601529U;
        static const AkUniqueID MC_THROW_PLAY = 1796000097U;
        static const AkUniqueID MC_WALK_PLAY = 1011903932U;
        static const AkUniqueID MUSIC_CHANGE_MORBID = 1416541639U;
        static const AkUniqueID NPC_TALK_PLAY = 787023996U;
        static const AkUniqueID NPC_TALK_STOP = 3835799014U;
        static const AkUniqueID NPC_WALK_PLAY = 2227517075U;
        static const AkUniqueID NPC_WALK_STOP = 1357639289U;
        static const AkUniqueID TEST = 3157003241U;
        static const AkUniqueID UI_CAMERA_DEZOOM_PLAY = 4008763347U;
        static const AkUniqueID UI_CAMERA_ZOOM_PLAY = 2038710544U;
        static const AkUniqueID UI_CANCEL_PLAY = 658987741U;
        static const AkUniqueID UI_ITEM_HIGHLIGHT_PLAY = 1277176383U;
        static const AkUniqueID UI_PAUSE_MENU_PLAY = 2578355359U;
        static const AkUniqueID UI_SELECT_CONFIRM_PLAY = 401277688U;
        static const AkUniqueID UI_SELECT_HOVER_PLAY = 4033886834U;
    } // namespace EVENTS

    namespace SWITCHES
    {
        namespace DAY_CYCLE
        {
            static const AkUniqueID GROUP = 4021126918U;

            namespace SWITCH
            {
                static const AkUniqueID EVENING = 2905060079U;
                static const AkUniqueID MORNING = 1924633667U;
                static const AkUniqueID NIGHT = 1011622525U;
                static const AkUniqueID NOON = 765672767U;
            } // namespace SWITCH
        } // namespace DAY_CYCLE

        namespace FLOOR_TYPE
        {
            static const AkUniqueID GROUP = 4168292868U;

            namespace SWITCH
            {
                static const AkUniqueID _GRAVEL = 3363833453U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID MUD = 712897245U;
                static const AkUniqueID ROCK = 2144363834U;
                static const AkUniqueID SAND = 803837735U;
                static const AkUniqueID WATER = 2654748154U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace FLOOR_TYPE

        namespace MUSIC_SWITCH
        {
            static const AkUniqueID GROUP = 2724869341U;

            namespace SWITCH
            {
                static const AkUniqueID BATTLE = 2937832959U;
                static const AkUniqueID VILLAGE = 3945572659U;
            } // namespace SWITCH
        } // namespace MUSIC_SWITCH

        namespace VILLAGE_KARMA_SWITCH
        {
            static const AkUniqueID GROUP = 3887908721U;

            namespace SWITCH
            {
                static const AkUniqueID BAD = 513390134U;
                static const AkUniqueID GOOD = 668632890U;
            } // namespace SWITCH
        } // namespace VILLAGE_KARMA_SWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DEBUG_ZIK_SPEED = 501502913U;
        static const AkUniqueID GAME_TIME = 1870090125U;
        static const AkUniqueID VILLAGE_KARMA = 3200455562U;
        static const AkUniqueID VOLUME_MASTER = 3695994288U;
        static const AkUniqueID VOLUME_MUSIC = 3891337659U;
        static const AkUniqueID VOLUME_SFX = 3673881719U;
        static const AkUniqueID VOLUME_VOICES = 3190188375U;
        static const AkUniqueID WATER_DISTANCE = 1642165204U;
        static const AkUniqueID WATER_FAR_DISTANCE = 1920428182U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID TEST_FOOTSTEP = 2649590838U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIANCE = 2981377429U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID VOICES = 3313685232U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
