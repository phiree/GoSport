using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PB.Model.Enums;
namespace PB.Model
{
    /// <summary>
    /// 活動類
    /// </summary>
    public class Activity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 舉辦地
        /// </summary>
        public virtual BaseGround Ground { get; set; }
        
        /// <summary>
        /// 總共花費
        /// </summary>
        public virtual decimal TotalCost { get; set; }
        /// <summary>
        /// 活動類型
        /// </summary>
        public virtual enumActivityType ActivityType { get; set; }

        public virtual enumActivityState ActivityState { get; set; }
        /// <summary>
        /// 活動詳情介紹
        /// </summary>
        public virtual string Details { get; set; }
        //圖片:每種類型的默認圖標
        public virtual DateTime BeginTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        /// <summary>
        /// 活動創建時間
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 發起者
        /// </summary>
        public virtual GOMemberShip Initiator { get; set; }
        public virtual IList<ActivityMember> Participants { get; set; }
        public Activity()
        {
            Participants = new List<ActivityMember>();
            CreateTime = DateTime.Now;
            ActivityState = enumActivityState.NotPublished;
        }
        /// <summary>
        /// 是否允許資金不足的成員參加
        /// </summary>
        public bool AllowNegative { get; set; }
        public virtual decimal CalculateCostForEachMember()
        {
            if (Participants.Count == 0)
                return 0;
            decimal TotalServiceCharge = 0;
            if (ServiceChargeForEach)
            {
                TotalServiceCharge = Participants.Count * ServiceCharge;
            }
            else
            {
                TotalServiceCharge = ServiceCharge;
            }

            return (TotalCost + TotalServiceCharge) / Participants.Count;
        }
        /// <summary>
        /// 額外收取的服務費.贏利點.服務費與組織者共享.
        /// </summary>
        public virtual int ServiceCharge { get; set; }
        /// <summary>
        /// 服務費是按參與者人數計算
        /// </summary>
        public virtual bool ServiceChargeForEach { get; set; }
        
        

        public virtual bool AddMember(GOMemberShip member,out string errMsg)
        {
            decimal balance = member.PrivateBanlance - CalculateCostForEachMember();
            if (!AllowNegative && balance < 0)
            {
                errMsg = "活動要求參與用戶有足夠餘額";
            }

            bool result = true;
            errMsg = string.Empty;
            if (ActivityState != Enums.enumActivityState.Published)
            {
                errMsg = "活動處於非活動狀態,不能加入";
                return false;
            }
            ActivityMember am = new ActivityMember();
            am.Activity = this;
            am.JoinTime = DateTime.Now;
            am.Member = member;
            Participants.Add(am);
            return result;
        }
        
    }
}
