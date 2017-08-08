using Assets.Scripts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class NumberMapController : MonoBehaviour{
        public MapCell CellPrefabs;
        private MapCell[][] mapData;
        private Stack<MapCell> memories;
        public GameObject MemoryPrefabs, ActivePrefabs;

        void Awake() {
            memories = new Stack<MapCell>();
        }
        void Start() {
            
        }

        void Update() {

        }

        public void Prepare(int size) {
            if(mapData==null) {
                mapData = new MapCell[1][];
                mapData[0] = new MapCell[1];
                mapData[0][0] = SpawnMapCell();
            }

            if(mapData.Length == size)
                return;

            //TODO : Remove all prefabs to stack
            for(int i=0;i<mapData.Length;i++) {
                for(int j=0;j<mapData[i].Length;j++) {
                    if(mapData[i][j]!=null) {
                        memories.Push(mapData[i][j]);
                    }
                }
            }
            
            //TODO : set prefabs to map
            mapData = new MapCell[size][];
            for(int y = 0; y< size; y++) {
                mapData[y] = new MapCell[size];
                for(int x=0;x<size;x++) {
                    if(memories.Count>0) {
                        mapData[y][x] = memories.Pop();
                    }else {
                        mapData[y][x] = SpawnMapCell();
                    }
                    mapData[y][x].setPosition(x, y);
                }
            }
        }

        private MapCell SpawnMapCell() {
            MapCell cell = Instantiate<MapCell>(CellPrefabs);
            cell.transform.SetParent(this.ActivePrefabs.transform);
            cell.transform.localScale = Vector3.one;

            return cell;
        }

        public void setMap(int[][] arr) {
            int size = arr.Length;
            Prepare(size);  
            for(int y=0;y<size;y++) {
                for(int x=0;x<size;x++) {
                    this.mapData[y][x].textField.text = arr[y][x].ToString();
                }
            }      
        }

        public void Hide() {
            for(int y = 0; y < mapData.Length; y++) {
                for(int x = 0; x < mapData[y].Length; x++) {
                    this.mapData[y][x].gameObject.SetActive(false);
                }
            }
        }

        public void Show() {
            for(int y = 0; y < mapData.Length; y++) {
                for(int x = 0; x < mapData[y].Length; x++) {
                    this.mapData[y][x].gameObject.SetActive(true);
                }
            }
        }
    }
}
