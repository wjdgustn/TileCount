using HarmonyLib;
using UnityEngine;

namespace TileCount.MainPatch {
    public class Text : MonoBehaviour {
        public static string Content = "";
        
        void OnGUI() {
            if (Main.Settings.TextShadow > 0) {
                GUIStyle shadow = new GUIStyle();
                shadow.fontSize = Main.Settings.TextSize;
                shadow.font = RDString.GetFontDataForLanguage(SystemLanguage.Korean).font;
                shadow.normal.textColor = Color.black.WithAlpha((float) Main.Settings.TextShadow / 100);
                
                GUI.Label(new Rect(Main.Settings.PositionX + 12, Main.Settings.PositionY - 8, Screen.width, Screen.height), Content, shadow);
            }
            
            GUIStyle style = new GUIStyle();
            style.fontSize = Main.Settings.TextSize;
            style.font = RDString.GetFontDataForLanguage(SystemLanguage.Korean).font;
            style.normal.textColor = Color.white;

            GUI.Label(new Rect(Main.Settings.PositionX + 10, Main.Settings.PositionY - 10, Screen.width, Screen.height), Content, style);
        }
    }

    [HarmonyPatch(typeof(scrController), "PlayerControl_Update")]

    internal static class ChangeText {
        private static void Prefix(scrController __instance) {
            if (!scrController.instance.paused && scrConductor.instance.isGameWorld) {
                var CurrentTile = (scrController.instance.currentSeqID);
                var TotalTile = (scrController.instance.lm.listFloors.Count - 1);
                var LeftTile = TotalTile - CurrentTile;
            
                Text.Content = Main.Settings.TextTemplate
                    .Replace("<CurrentTile>", CurrentTile.ToString())
                    .Replace("<LeftTile>", LeftTile.ToString())
                    .Replace("<TotalTile>", TotalTile.ToString());
            }
            else Text.Content = Main.Settings.NotPlayingText;
        }
    }

    [HarmonyPatch(typeof(scrController), "FailAction")]

    internal static class ChangeTextOnFail {
        private static void Prefix(scrController __instance) {
            Text.Content = Main.Settings.NotPlayingText;
        }
    }
}