using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharp.Controllers
{
   public class _07HW4Controller : Controller
   {
     const string eng = "ABCDEFGHJKLMNPQRSTUVXYWZIO"; //宣告為常數 
     public ActionResult Index()
     {
       return View();
     }

     [HttpPost]
     public ActionResult Index(string id)
     {
       //主程式只做一件事:呼叫函數
       string result = "";
       if (!LengthCheck(ref id))
         result = "格式有誤";
       else if(!LetterCheck(ref id))
         result = "格式有誤";
       else if(!GenderCheck(ref id))
         result = "格式有誤";
       else if(!NumberCheck(ref id))
         result = "格式有誤";
       else if(!RuleCheck(ref id))
         result = "此身分證字號不正確";
       else
         result = "身分證字號合法";
       ViewBag.Result = result;
       ViewBag.id = id;
       return View();
      }


       //判斷是否為 10 碼
       bool LengthCheck(ref string id)
       {
         if (id.Length==10)
           return true;
         else return false;
       }


        //判斷第一碼是否為 A-Z
       bool LetterCheck(ref string id)
       {
         //A123456789
         string w = id.Substring(0, 1); 

         if (eng.Contains(w))
           return true;
         return false;
       }

        //判斷第二碼是否為 1 或 2
       bool GenderCheck(ref string id)
       {
         string gender=id.Substring(1,1);

           if (gender == "1" || gender == "2")
             return true;

         return false;
       }

        //判斷第 3-10 碼是否為 0-9
       bool NumberCheck(ref string id)
       {
         try
         {
           for (int i = 2; i < 10; i++)
           {
             Convert.ToInt16(id.Substring(i, 1));
           }
           return true;
         }
         catch
         {
           return false;
         }
       }

        //判斷檢查碼是否符合邏輯
       bool RuleCheck(ref string id)
       {
         //第一階段:把英文換成 2 碼數字
         string w = id.Substring(0, 1);
         //C123456789
         int intEng = eng.IndexOf(w) + 10; //index=2, 2+10=12
         //12123456789

         //第二階段:依規則計算 11 碼數字總合
         //目前 intEng:12
         int n1 = intEng / 10; //n1=1
         int n2 = intEng % 10; //n2=2
         int[] a = new int[9];
         for(int i = 0; i < a.Length; i++)
         {
           a[i] = Convert.ToInt16( id.Substring(i + 1, 1));
         }
         //平鋪直述的寫法
         int sum =n1*1+n2*9+a[0]*8 + a[1] * 7 + a[2] * 6 + a[3] * 5 + a[4] * 4 + a[5] * 3 + a[6] * 2 + a[7] + a[8];

         //第三階段:判斷是否為合法身分證
         if (sum % 10 == 0)
           return true;
         return false;
        }

   }
}






