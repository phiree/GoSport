using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    /// <summary>
    /// 活动对应的地址.
    /// </summary>
   public  class BaseGround
    {
       public virtual Guid Id { get; set; }
       public virtual string Name { get; set; }
       public virtual string Address { get; set; }
       public virtual string Coordinate { get; set; }
       public virtual string Descrption { get; set; }
       
    }
}
