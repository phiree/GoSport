using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    /// <summary>
    /// 財務狀況
    /// </summary>
   public partial class GOMemberShip
    {
      
     
       public virtual IList<MemberFinancialDetail> FinancialDetalis { get; set; }
       /// <summary>
       /// 私人款項餘額.(交錢給組織者,扣除活動費用)
       /// </summary>
       public virtual decimal PrivateBanlance { get; set; }
       /// <summary>
       /// 公共款項餘額(收取成員現金, 支付場地費用,多餘資金轉交給其他組織者,接收其他組織者的現金)
       /// </summary>
       public virtual decimal PublicBanlance { get; set; }
       /// <summary>
       /// 財務活動:充值,或者花費, 收錢, 支付場地費用,轉交給其他成員
       /// 更改banlance.
       /// </summary>
       /// <param name="detail"></param>
       /// <param name="AllowNegative">允許資金不足的人成員參加</param>
       /// <returns></returns>
       public virtual void  AddDetail(MemberFinancialDetail detail)
       {
           FinancialDetalis.Add(detail);
           switch (detail.OperationType)
           {
               case Enums.enumFinancialOperation.PrivateAdd: 
                   //本人賬戶的私人餘額 和 收款者的公共 餘額 均增加
                  this.PrivateBanlance += detail.Amount;
                   detail.ToWhom.PublicBanlance += detail.Amount;
                   break;
               case Enums.enumFinancialOperation.PrivateMinus: 
                   //私人餘額減少
                  this.PrivateBanlance -= detail.Amount;
                   break;
               case Enums.enumFinancialOperation.PublicPay:
                   this.PublicBanlance -= detail.Amount;
                   break;
              
               case Enums.enumFinancialOperation.PublicTranfer:
                   this.PublicBanlance -= detail.Amount;
                   detail.ToWhom.PublicBanlance += detail.Amount;
                   break;
               default:
                   throw new Exception("未知的財務操作");
                  
           }
          
       }
    }
}
