using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    /// <summary>
    /// 场所.包含多个活动场地
    /// </summary>
   public class Arena
    {
       public Arena()
       {
           Grounds = new List<BaseGround>();
       }
       public virtual Guid Id { get; set; }

       public IList<BaseGround> Grounds { get; set; }
    }
}
