using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
   public class ActivityMember
   {
       public virtual Guid Id { get; set; }
       public virtual GOMemberShip Member { get; set; }
       public virtual Activity Activity { get; set; }
       public virtual DateTime JoinTime { get; set; }

    }
}
