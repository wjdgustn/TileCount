using HarmonyLib;
using UnityEngine;

namespace TileCount.MainPatch {
    public class Text : MonoBehaviour {
        public static string Content = "Wa sans!";

        void OnGUI() {
            GUIStyle style = GUI.skin.GetStyle("TileCount_text");
            style.fontSize = Main.Settings.TextSize;
            style.font = RDString.GetFontDataForLanguage(RDString.language).font;
            style.normal.textColor = Color.white;

            GUI.Label(new Rect(Main.Settings.PositionX + 10, Main.Settings.PositionY - 10, Screen.width, Screen.height), Content, "TileCount_text");
        }
    }

    [HarmonyPatch(typeof(scrController), "PlayerControl_Update")]

    internal static class ChangeText {
        private static void Prefix(scrController __instance) {
            if (!scrController.instance.paused && scrConductor.instance.isGameWorld) {
                var CurrentTile = (scrController.instance.currentSeqID);
                var TotalTile = (scrController.instance.lm.listFloors.Count - 1);
                var LeftTile = TotalTile - CurrentTile;
            
                Text.Content = "남은 타일 수 : " + LeftTile.ToString() + "개\n총 타일 수 : " + TotalTile.ToString() + "개";
            }
            else Text.Content = "플레이 중이 아님";
        }
    }
}