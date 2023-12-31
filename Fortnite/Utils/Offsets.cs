﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Fortnite.Utils
{
    public static class Offsets
    {
        // constants
        public const float RECOIL_SCALE = 2.0f;

        // offsets
        // data
        public static int dwEntityList;
        public static int dwLocalPlayerPawn;
        public static int dwLocalPlayerController;
        public static int dwViewAngles;
        public static int dwViewMatrix;

        // client.dll
        // C_BasePlayerPawn
        public static int m_vOldOrigin;

        // C_BaseModelEntity
        public static int m_vecViewOffset;

        // C_CSPlayerPawn
        public static int m_aimPunchAngle;

        // C_BaseEntity
        public static int m_lifeState;
        public static int m_iTeamNum;
        public static int m_iHealth;
        public static int m_pGameSceneNode;

        // CGameSceneNode
        public static int m_bDormant;

        // C_CSPlayerPawnBase
        public static int m_ArmorValue;
        public static int m_iShotsFired;
        public static int m_iIDEntIndex;

        // CSkeletonInstance
        public static int m_modelState;

        // CBasePlayerController
        public static int m_hPawn;
        public static int m_iszPlayerName;

        // bones
        public static readonly Dictionary<string, int> BONES = new Dictionary<string, int>
        {
            { "head", 6 },
            { "neck_0", 5 },
            { "spine_1", 4 },
            { "spine_2", 2 },
            { "pelvis", 0 },
            { "arm_upper_L", 8 },
            { "arm_lower_L", 9 },
            { "hand_L", 10 },
            { "arm_upper_R", 13 },
            { "arm_lower_R", 14 },
            { "hand_R", 15 },
            { "leg_upper_L", 22 },
            { "leg_lower_L", 23 },
            { "ankle_L", 24 },
            { "leg_upper_R", 25 },
            { "leg_lower_R", 26 },
            { "ankle_R", 27 }
        };

        // reading
        public static void ReadOffsets(string runDir)
        {
            var path = runDir + "/offset_dumper/generated/";
            var offsetsText = File.ReadAllText(path + "offsets.json");
            var clientDllText = File.ReadAllText(path + "client.dll.json");
            var offsets = (JObject)JObject.Parse(offsetsText)["client_dll"]?["data"];
            dwEntityList = ParseOffset(offsets, "dwEntityList");
            dwLocalPlayerPawn = ParseOffset(offsets, "dwLocalPlayerPawn");
            dwLocalPlayerController = ParseOffset(offsets, "dwLocalPlayerController");
            dwViewAngles = ParseOffset(offsets, "dwViewAngles");
            dwViewMatrix = ParseOffset(offsets, "dwViewMatrix");

            var clientDll = JObject.Parse(clientDllText);
            var basePlayerPawn = (JObject)clientDll["C_BasePlayerPawn"]?["data"];
            m_vOldOrigin = ParseOffset(basePlayerPawn, "m_vOldOrigin");

            var baseModelEntity = (JObject)clientDll["C_BaseModelEntity"]?["data"];
            m_vecViewOffset = ParseOffset(baseModelEntity, "m_vecViewOffset");

            var csPlayerPawn = (JObject)clientDll["C_CSPlayerPawn"]?["data"];
            m_aimPunchAngle = ParseOffset(csPlayerPawn, "m_aimPunchAngle");

            var baseEntity = (JObject)clientDll["C_BaseEntity"]?["data"];
            m_lifeState = ParseOffset(baseEntity, "m_lifeState");
            m_iTeamNum = ParseOffset(baseEntity, "m_iTeamNum");
            m_iHealth = ParseOffset(baseEntity, "m_iHealth");
            m_pGameSceneNode = ParseOffset(baseEntity, "m_pGameSceneNode");

            var gameSceneNode = (JObject)clientDll["CGameSceneNode"]?["data"];
            m_bDormant = ParseOffset(gameSceneNode, "m_bDormant");

            var csPlayerPawnBase = (JObject)clientDll["C_CSPlayerPawnBase"]?["data"];
            m_ArmorValue = ParseOffset(csPlayerPawnBase, "m_ArmorValue");
            m_iShotsFired = ParseOffset(csPlayerPawnBase, "m_iShotsFired");
            m_iIDEntIndex = ParseOffset(csPlayerPawnBase, "m_iIDEntIndex");

            var skeletonInstance = (JObject)clientDll["CSkeletonInstance"]?["data"];
            m_modelState = ParseOffset(skeletonInstance, "m_modelState");

            var basePlayerController = (JObject)clientDll["CBasePlayerController"]?["data"];
            m_hPawn = ParseOffset(basePlayerController, "m_hPawn");
            m_iszPlayerName = ParseOffset(basePlayerController, "m_iszPlayerName");
        }

        private static int ParseOffset(this JObject offsetsJson, string name)
        {
            return (int)offsetsJson[name]?["value"];
        }
    }
}