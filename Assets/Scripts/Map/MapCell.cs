using Assets.Scripts.GridMap;
using Assets.Scripts.GridMAp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Map {
    public class MapCell : MonoBehaviour,IPositionProperties {

        [Header("Block")]
        public Image TargetImage;
        public Image EffectImage;

        public Color Color_NormalState;
        public Color Color_SelectedState;
        public Color Color_ErrorState;

        public Animator Animator;


        public Text textField {
            get {
                return this.transform.FindChild("Text").GetComponent<Text>();
            }
        }
        #region Button State
        private static MapCell lastSelectedCell;
        private static List<MapCell> selectedCellList = new List<MapCell>();
        public static bool onPointingDown = false;
        #endregion
        
        private bool onThisSelected = false;
        private int positionX, positionY;

        public int getPositionX() {
            return this.positionX;
        }

        public int getPositionY() {
            return this.positionY;
        }

        private void OnCellSelected() {
            this.changeColor(Color_SelectedState);
            if(!this.onThisSelected) {
                this.onThisSelected = true;
                MapCell.lastSelectedCell = this;
                selectedCellList.Add(this);
                //this.EffectImage.gameObject.SetActive(true);
            }
        }

        private void OnCellCancelSelected() {
            this.changeColor(Color_NormalState);
            this.onThisSelected = false;
        }


        public void changeColor(Color color) {

            var btnColors = this.TargetImage.color;
            this.TargetImage.color = color;
        }

        public static void SubmitAnswer() {
            

            GameManager.SubmitAnswer(MapCell.selectedCellList);
            MapCell.selectedCellList.Clear();
            //Debug.Log("Answer = " + ans);
        }

        public void setPosition(int x, int y) {
            this.positionX = x;
            this.positionY = y;
        }

        private bool isConnected() {
            return GridCell.isConnected(this,MapCell.lastSelectedCell);
        }

        //Overide Method
        public void OnPointerDown() {
            //base.OnPointerDown(eventData);
            
            
            MapCell.onPointingDown = true;
            OnCellSelected();
            Debug.Log("Pointer down wow wow");
        }

        public void OnPointerUp() {
            //base.OnPointerUp(eventData);
            Debug.Log("Pointer up ... sad");
            MapCell.onPointingDown = false;
            foreach(MapCell m in FindObjectsOfType<MapCell>()) {
                m.OnCellCancelSelected();
            }
            MapCell.SubmitAnswer();
        }

        public void OnPointerEnter() {
            //base.OnPointerEnter(eventData);
            if(onPointingDown) {
                if(isConnected()) { OnCellSelected();
                    Debug.Log("Point Drag and Enter me XD");
                }
            }
        }

        public void Spin() {
            this.Animator.Play("Spin");
        }
        
    }
}
