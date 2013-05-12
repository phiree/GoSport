using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PB.Model;
using PB.Model.Enums;
namespace PB.BLL
{
   public class BLLActivity:BLLBase<Activity>
    {
       /// <summary>
       /// 創建活動/應該由誰創建
       /// </summary>
       /// <returns></returns>
       public bool CreateActivity(string strActivityType,out string errMsg)
       {
           errMsg = string.Empty;
           Activity activity = new Activity();
           enumActivityType activityType;
           if (!Enum.TryParse<PB.Model.Enums.enumActivityType>(strActivityType, out activityType))
           {
               errMsg = "活動類型有誤";
               return false;
           }
           return true;
       }
    }
}
