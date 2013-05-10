using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PB.Model;
using PB.BLL;
using Rhino.Mocks;
using FizzWare.NBuilder;
using NUnit.Framework;
namespace PB.Test.DomainTest
{
    public class DomainTest
    {
        /// <summary>
        /// 用戶參與一次活動,計算每個用戶的費用
        /// </summary>
        public void UserJoinActivityTest()
        {
            IList<GOMemberShip> members = FizzWare.NBuilder.Builder<GOMemberShip>.CreateListOfSize(10)
                .Build();
            Activity act=Builder<Activity>.CreateNew()
                .With(x=>x.TotalCost=100)
                .With(x=>x.ServiceCharge=1)
                .With(x=>x.ServiceChargeForEach=true)
                .With(x=>x.ActivityState= Model.Enums.enumActivityState.Published)
                .Build()
                ;
            StringBuilder sb=new StringBuilder();
            string addMsg;
            for (int i=0;i<members.Count;i++)
            {
                
                act.AddMember(members[i],out addMsg);
                sb.AppendLine(addMsg);
              
                switch (i)
                {
                    case 0: Assert.AreEqual(101, act.CalculateCostForEachMember()); break;
                    case 3: Assert.AreEqual(104/4, act.CalculateCostForEachMember()); break;
                    case 4: Assert.AreEqual(105/5, act.CalculateCostForEachMember()); break;
                    case 7: Assert.AreEqual((decimal)108/8, act.CalculateCostForEachMember()); break;
                    case 9: Assert.AreEqual(110/10, act.CalculateCostForEachMember()); break;
                }
            }

        }

        /// <summary>
        /// 用戶交錢給組織者
        /// </summary>
        public void UserPayToOrganizer()
        {
             GOMemberShip memberFrom = Builder<GOMemberShip>.CreateNew()
                .With(x => x.PrivateBanlance = 12)
                .With(x => x.PublicBanlance = 13)
                .Build();
             GOMemberShip memberTo = Builder<GOMemberShip>.CreateNew()
                 .With(x => x.PrivateBanlance = 22)
               .With(x => x.PublicBanlance = 23)
               .Build();

            MemberFinancialDetail detail = Builder<MemberFinancialDetail>.CreateNew()
                .With(x => x.Amount = 10)
                .With(x => x.ToWhom = memberTo)
             
                .With(x=>x.OperationType= Model.Enums.enumFinancialOperation.PrivateAdd)
                .Build();
            memberFrom.AddDetail(detail);
            Assert.AreEqual(22,memberFrom.PrivateBanlance);
            Assert.AreEqual(13, memberFrom.PublicBanlance);
            Assert.AreEqual(22, memberTo.PrivateBanlance);
            Assert.AreEqual(33, memberTo.PublicBanlance);
        }
    }
}
