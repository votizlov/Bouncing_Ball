using System;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public class LeaderboardGUI:MonoBehaviour
    {
        [SerializeField] private GameObject textLabelPrefab;
        [SerializeField] private Transform textElementsRoot;
            private string _nameInput = "";
            private string _scoreInput = "0";

            private void OnEnable()
            {
                for (int i = 0; i < textElementsRoot.childCount; i++)
                {
                    Destroy(textElementsRoot.GetChild(i).gameObject);
                }
                
                for (int i = 0; i < Leaderboard.EntryCount; ++i) {
                    var entry = Leaderboard.GetEntry(i);
                    var text = Instantiate(textLabelPrefab, textElementsRoot).GetComponent<Text>();
                    text.text = "Name: " + entry.name + ", Score: " + entry.score;
                }
            }

            private void OnGUI() {
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
 
                // Display high scores!
                for (int i = 0; i < Leaderboard.EntryCount; ++i) {
                    var entry = Leaderboard.GetEntry(i);
                    GUILayout.Label("Name: " + entry.name + ", Score: " + entry.score);
                }
 
                
                // Interface for reporting test scores.
                GUILayout.Space(10);
 
                _nameInput = GUILayout.TextField(_nameInput);
                _scoreInput = GUILayout.TextField(_scoreInput);
 
                if (GUILayout.Button("Record")) {
                    int score;
                    int.TryParse(_scoreInput, out score);
 
                    Leaderboard.Record(_nameInput, score);
 
                    // Reset for next input.
                    _nameInput = "";
                    _scoreInput = "0";
                }
 
                GUILayout.EndArea();
            }
        
    }
}