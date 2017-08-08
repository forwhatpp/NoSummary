using Assets.Scripts.GridMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GridMAp {
    public class GridCell:IPositionProperties {
        int x, y;

        #region Direction
        public static readonly int[] DIR_VER = new int[] { 1, -1, 0 };
        public static readonly int[] DIR_HOR = new int[] { 0, 1, -1 };
        private int startX;
        private int startY;

        public GridCell(int x, int y) {
            this.x = x;
            this.y = y;
        }
        #endregion

        /// <summary>
        /// Check a grid cell is connected to b cell in 8diection or not
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool isConnected(IPositionProperties a,IPositionProperties b) {
            for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 3; j++) {
                    if(a.getPositionX() + DIR_VER[i] == b.getPositionX()
                        && a.getPositionY() + DIR_HOR[j] == b.getPositionY()) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<IPositionProperties> getConnectedPosition(IPositionProperties a) {
            List<IPositionProperties> connected = new List<IPositionProperties>();
            for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 3; j++) {
                    connected.Add(new GridCell(a.getPositionX() + DIR_VER[i], a.getPositionY() + DIR_HOR[j]));
                }
            }
            connected.RemoveAll( item => item.getPositionX()==a.getPositionX() && item.getPositionY() == a.getPositionY());
            return connected;
        }

        public static bool isOverflowFromArraySize(IPositionProperties a,int sizeX,int sizeY) {
            return (a.getPositionX() < 0 || a.getPositionX() >= sizeX) || (a.getPositionY() < 0 || a.getPositionY() >= sizeY);
        }

        public int getPositionX() {
            return this.x;
        }

        public int getPositionY() {
            return this.y;
        }
    }
}
