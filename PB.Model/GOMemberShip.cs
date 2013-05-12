using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PB.Model
{
    public partial class GOMemberShip
    {
        public GOMemberShip()
        {
            PrivateBanlance = PublicBanlance = 0;
            FinancialDetalis = new List<MemberFinancialDetail>();
        }
        public virtual Guid Id { get; set; }

        public virtual string LoginName { get; set; }
        public virtual string Password { get; set; }
        public virtual string NickName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }

    }
}
