using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class Player : MonoBehaviour{
        public static readonly string key_score = "Score",key_hiscore="HiScore", key_level = "Level";

        public static int Score{
            get {
                return PlayerPrefs.GetInt(Player.key_score);
            }
            set {
                PlayerPrefs.SetInt(Player.key_score, value);
            }
        }

        public static int HiScore {
            get {
                return PlayerPrefs.GetInt(Player.key_hiscore);
            }
            set {
                PlayerPrefs.SetInt(Player.key_hiscore, value);
            }
        }
    }
}
