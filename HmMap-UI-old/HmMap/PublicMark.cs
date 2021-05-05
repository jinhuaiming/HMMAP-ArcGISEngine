using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmMap
{
   public class PublicMark
    {
        public enum ButtonFunction
        {
            roam = 1,//漫游；
            measure = 2,//测量；
            distinguish = 3,//识别；
            TrackCircle = 4,//按圆选择；
            TrackLine = 5,//按线路选择;
            TrackRectangle = 6,//按矩形选择;
            TrackPolygon = 7,//按多边形选择；
            enlarge = 8,//局部放大功能
            narrow = 9,//局部缩小功能
            DrawPoint = 10,//绘制点要素
            Drawpolyline=11,//绘制线要素
            DrawPolygon =12//绘制面要素
        }
    }
}
