using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    /// <summary>
    /// 用戶財務流水包括: 參與者充值(現金交給組織者),
    /// </summary>
   public class MemberFinancialDetail
    {
       public virtual Guid Id { get; set; }

       public virtual decimal Amount { get; set; }
       public virtual Enums.enumFinancialOperation OperationType { get; set; }
       public virtual DateTime OperationTime { get; set; }
       public virtual string Memo { get; set; }
       public virtual bool IsAudited { get; set; }
       /// <summary>
       /// 交給誰
       /// </summary>
       public virtual GOMemberShip ToWhom { get; set; }
      /// <summary>
       /// 與這比費用相關聯的活動
       /// </summary>
       public virtual Activity Activity { get; set; }
    }
}
