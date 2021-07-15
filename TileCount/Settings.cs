using UnityEngine;
using UnityModManagerNet;

namespace TileCount {
    public class MainSettings : UnityModManager.ModSettings, IDrawable {
        [Draw("")] public string TextTemplate = "현재 타일 : <CurrentTile>개\n남은 타일 수 : <LeftTile>개\n총 타일 수 : <TotalTile>개";
        [Draw("")] public string NotPlayingText = "플레이 중이 아님";
        [Draw("위치 X좌표")] public int PositionX = 0;
        [Draw("위치 Y좌표")] public int PositionY = 0;
        [Draw("글자 크기")] public int TextSize = 50;
        [Draw("텍스트 그림자 진하기(1~100)", Min = 0, Max = 100)] public int TextShadow = 50;

        public override void Save(UnityModManager.ModEntry modEntry) {
            Save(this, modEntry);
        }
        
        public void OnChange() {
            
        }
        
        public void OnGUI(UnityModManager.ModEntry modEntry) {
            GUILayout.Label("텍스트(Text)");
            TextTemplate = GUILayout.TextArea(TextTemplate);
            
            GUILayout.Label("플레이 중이 아닐 때 텍스트(Not Playing Text)");
            NotPlayingText = GUILayout.TextArea(NotPlayingText);
            
            Main.Settings.Draw(modEntry);
        }

        public void OnSaveGUI(UnityModManager.ModEntry modEntry) {
            Main.Settings.Save(modEntry);
        }
    }
}