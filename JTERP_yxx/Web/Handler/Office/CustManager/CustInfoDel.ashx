<%@ WebHandler Language="C#" Class="CustInfoDel" %>

using System;
using System.Web;
using System.Collections;
using XBase.Business.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;
using System.Data;

public class CustInfoDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string allcustid = context.Request.Params["allcustid"].ToString().Trim(); //客户编号
        string AllCustNO = context.Request.Params["AllCustNO"].ToString().Trim(); //客户编号
        string[] al = allcustid.Split(',');
        string[] AllNo = AllCustNO.Split(',');

        JsonClass jc;
        
        //执行删除
        //CustInfoBus.DelCustInfo(al);
        //bool isNotHave = false;
        //for (int i = 0; i < al.Length; i++)
        //{
        //    string CustIDD = al[i];
        //    bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("V_QuoteCustInfo", "CustID", CustIDD);
        //    if (isAlready == true)
        //    {
        //        isNotHave = true;
        //    }
        //}
        //bool LinkManHas = CustInfoBus.GetLinkManByCustNo(AllNo,al);
        bool IsHave = CustInfoBus.GetCustInfoByID(CompanyCD,al, AllNo);
        if (IsHave)
        {
            jc = new JsonClass("success", "", 2);
        }
        else
        {
            if (CustInfoBus.isdel(al))
            {
                if (CustInfoBus.DelCustInfo(al) > 0)
                    jc = new JsonClass("success", "", 1);
                else
                    jc = new JsonClass("faile", "", 0);
            }
            else
            {
                jc = new JsonClass("success", "", 2);
            }       
        }
        context.Response.Write(jc);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}