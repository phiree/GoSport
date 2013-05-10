using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    /// <summary>
    ///   场地信息
    /// 有些场地没有 父级的 PlayGround 如,徒步旅游的地点 路线等.
    /// 但是,具体的活动是在 场所内的 某个场地举行的,它也应该有地点/坐标等属性.
    /// </summary>
    public class Ground
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 四号场地,302房间
        /// </summary>
        public string Name { get; set; }
        public GroundType GroundType { get; set; }
       // public PlayGround PlayGround { get; set; }
    }
    public enum GroundType
    {
        BasketBall = 1,
        FootBall,
        Badminton,
        TableTennis,
        MountainClimb

    }
}
