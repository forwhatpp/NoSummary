using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class Timer : MonoBehaviour{
        public GameObject timer;
        public Image Clock;
        public Text textTime;
        public double setTime,currentTime;
        private bool isOnCount;
        private Action callBackAfterFinish;

        public void SetTime(double time) {
            this.currentTime = this.setTime = time;
        }

        public void Pause() {
            this.isOnCount = false;
        }
        
        void Update() {

            this.textTime.text = ((int)this.currentTime).ToString();
            //TODO : time couting;
            if(isOnCount) {
                this.currentTime -= Time.deltaTime;
                SetTextColor();

                if(this.currentTime <= 0) {
                    this.isOnCount = false;
                    if(this.callBackAfterFinish!=null) {
                        callBackAfterFinish();
                    }
                }
            }
            
        }

        private void SetTextColor() {
            var color = this.textTime.color;
            if(this.currentTime <= 10) {
                color = new Color(1, 88f / 255, 88f / 255);
            }else if(this.currentTime <= 20) {
                color = new Color(1,1,88f/255);
            }else {
                color = new Color(0, 1, 88f / 255);
            }
            this.textTime.color = color;
        }

        public void AddExtraTime(double value) {
            //this.currentTime += value;
            this.currentTime = Math.Min(this.currentTime + value,120);
        }

        public void StartTimer() {
            this.currentTime = this.setTime;
            ResumeTimer();
        }

        public void ResumeTimer() {
            this.isOnCount = true;
            this.textTime.text = ((int)this.currentTime).ToString();
        }

        public void StartTimer(double time) {
            SetTime(time);
            StartTimer();
        }

        public void StartTimer(double time,Action callback) {
            this.callBackAfterFinish = callback;
            StartTimer(time);
        }
    }
}
