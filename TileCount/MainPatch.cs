using HarmonyLib;
using UnityEngine;

namespace TileCount.MainPatch {
    public class Text : MonoBehaviour {
        public static string Content = "";
        
        void OnGUI() {
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
}