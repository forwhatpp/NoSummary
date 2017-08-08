using Assets.Scripts.GridMap;
using Assets.Scripts.GridMAp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class QuestionManager {
        int[][] _mapData;
        int size;

        public int[][] MapData {
            get {
                return _mapData;
            }
            private set {
                this._mapData = value;
            }
        }

        public const int ZERO = 0;

        public QuestionManager(int size) {
            this.size = size;
            this.MapData = new int[size][];
            for(int i=0;i<size;i++) {
                this.MapData[i] = new int[size];
            }
            GenerateAllNumber();
        }

        public int getQuestion() {
            int value=0;
            int startX = UnityEngine.Random.Range(0,size-1),startY = UnityEngine.Random.Range(0,size-1);
            int lenght = UnityEngine.Random.Range(3,size*size/3);

            List<IPositionProperties> selectedBlock = new List<IPositionProperties>();
            List<IPositionProperties> BFSList = new List<IPositionProperties>();
            BFSList.Add(new GridCell(startX,startY));

            while(selectedBlock.Count < lenght) {

                //TODO : Random Position In List :3
                int RandomIndex = UnityEngine.Random.Range(1, BFSList.Count) - 1;
                //Debug.Log("Random Index = "+RandomIndex + "Grid is null ? ["+(BFSList[RandomIndex]==null)+"]"+(BFSList!=null ? " data is ["+BFSList[RandomIndex].getPositionX()+","+ BFSList[RandomIndex].getPositionY() + "]!": ""));

                IPositionProperties position = BFSList[RandomIndex];
                BFSList.Remove(position);
                selectedBlock.Add(position);
                value += _mapData[position.getPositionY()][position.getPositionX()];

                foreach(IPositionProperties conectedPosition in GridCell.getConnectedPosition(position)) {
                    if(!GridCell.isOverflowFromArraySize(conectedPosition,this.size,this.size)
                        && BFSList.Find(item => item.getPositionX()==conectedPosition.getPositionX() && item.getPositionY() == conectedPosition.getPositionY())==null
                        && selectedBlock.Find(item => item.getPositionX() == conectedPosition.getPositionX() && item.getPositionY() == conectedPosition.getPositionY())==null
                        ) {
                        BFSList.Add(conectedPosition);
                    }
                }
            }
            value = UnityEngine.Random.Range(3,20);
            return value;
        }

        private void GenerateAllNumber() {
            int len = size;
            for(int y=0;y<len;y++) {
                for(int x=0;x<len;x++) {
                    if(MapData[y][x]==ZERO) {
                        MapData[y][x] = UnityEngine.Random.Range(1, 9);
                    }
                }
            }
        }

        public void GenerateNumberAtFixPosition(List<IPositionProperties> generatePositionList) {
            foreach(IPositionProperties position in generatePositionList) {
                try {
#if UNITY_EDITOR
                    Debug.Log("QuestionManager : Random new  <" + position.getPositionX() + "," + position.getPositionY() + ">");
#endif
                    MapData[position.getPositionY()][position.getPositionX()] = UnityEngine.Random.Range(1, 9);
                } catch(IndexOutOfRangeException e) {
#if UNITY_EDITOR
                    Debug.Log("QuestionManager : Cannot generate on position <" + position.getPositionX() + "," + position.getPositionY() + "> size is <" + size + ">"+e.ToString());
#endif
                }
            }
        }

        public void GenerateNumberAtFixPosition(List<Vector2> generatePositionList) {
            foreach(Vector2 position in generatePositionList) {
                try {
                    MapData[(int)position.y][(int)position.x] = UnityEngine.Random.Range(1, 9);
                } catch(IndexOutOfRangeException e) {
#if UNITY_EDITOR
                    Debug.Log("QuestionManager : Cannot generate on position <"+position.x+","+position.y+"> size is <"+size+">"+e.ToString());
#endif
                }
                //if(position.x >=0 && position.x < size && position.y >=0 && position.y <size) {

                //}
            }
        }
    }
}
