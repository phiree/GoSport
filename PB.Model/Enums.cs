using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model.Enums
{
    /// <summary>
    /// 活動狀態
    /// </summary>
   public enum enumActivityState
    {
       NotPublished,//未發布
       Published,//已發布
       Aborted,//已終止
       Over//已結束
    }
   public enum enumActivityType
   {
       BasketBall,
       FootBall,
       Ktv,
       Hike
   }
   public enum enumFinancialOperation
   { 
       PrivateRecharge,//成員交款給組織者
       PrivatePay,//扣除成員活動費用
       //與 PrivateAdd同時發生 PublicCharge,//收取成員現金
       PublicPay,//支付場地費用
      //與 transfer同時發生 PublicReceive,//接收其他組織者的公共款項  
       PublicTranfer//將多出的公共款項轉移給其他用戶.
   }
    
}
