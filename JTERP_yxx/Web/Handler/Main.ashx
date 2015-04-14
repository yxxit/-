<%@ WebHandler Language="C#" Class="Main" %>

using System;
using System.Web;
using System.Data;
using XBase.Common;
using System.Data.SqlClient;
using XBase.Data;

public class Main : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.Params["sign"] != null)
        {
            if (context.Request.Params["sign"].ToString() == "GetContractCount")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + GetContractCount() + "}");
            }
            if (context.Request.Params["sign"].ToString() == "SearchCanHuiTongZhi")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + SearchCanHuiTongZhi() + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetBeiWangLuCount")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + GetBeiWangLuCount() + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetPowerdata")
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-08新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day,table) + ",PowerDate:" + Getdata(keyWord, ModifyDay, day,table) + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetUsedata")
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-08新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day, table) + ",Usedata:" + Getdata(keyWord, ModifyDay, day, table) + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetCertidata")
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-08新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day, table) + ",Certidata:" + Getdata(keyWord, ModifyDay, day, table) + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetWardata")
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-08新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day,table) + ",Wardata:" + Getdata(keyWord, ModifyDay, day,table) + "}");
            }
            if (context.Request.Params["sign"].ToString() == "GetGmspdata")
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-08新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day,table) + ",Gmspdata:" + Getdata(keyWord, ModifyDay, day,table) + "}");
            }

            if (context.Request.Params["sign"].ToString() == "GetMedFileDate")//2012-10-15新增方法，用于检查物品表中的信息
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-15新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverdueProdata(keyWord, day, table) + ",MedFileDate:" + GetProdata(keyWord, ModifyDay, day,table) + "}");
            }


            if (context.Request.Params["sign"].ToString() == "GetValidity")//2012-10-15新增方法，用于检查物品表中的信息
            {
                string day = context.Request.Params["day"].ToString();
                string ModifyDay = context.Request.Params["ModifyDay"].ToString();
                string keyWord = context.Request.Params["keyWord"].ToString();
                string table = context.Request.Params["table"].ToString();//2012-10-15新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
                context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverdueProdata(keyWord, day, table) + ",Validity:" + GetProdata(keyWord, ModifyDay, day, table) + "}");
            }

            //if (context.Request.Params["sign"].ToString() == " MedFileDate")//2012-10-15新增方法，用于检查物品表中的信息
            //{
            //    string day = context.Request.Params["day"].ToString();
            //    string ModifyDay = context.Request.Params["ModifyDay"].ToString();
            //    string keyWord = context.Request.Params["keyWord"].ToString();
            //    string table = context.Request.Params["table"].ToString();//2012-10-15新增参数，用于脚本方法的重载，通过该参数来指定需要查询的数据表的表名
            //    context.Response.Write("{sta:1,info:'',OverdueCount:" + GetOverduedata(keyWord, day, table) + ",MedFileDate:" + Getdata(keyWord, ModifyDay, day) + "}");
            //}
        }
        else
        {
            DataTable dtContract = GetContractList();
            if (dtContract == null)
            {
                context.Response.Write("{sta:0,info:'',totalCount:0}");
            }
            else
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + dtContract.Rows.Count + "}");
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    //--------------------------------------检测数据表中的证书相关信息 edit by DYG 2012-09-21-----------------------------------------------//
    private int GetOverduedata(string keyword,string day,string table)//检测已经过期的证书
    {
        //string table = "CustInfo";
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select  count("+keyword+") from officedba."+table+"  where "+keyword+" < '" + day + "' and CompanyCD=@CompanyCD";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);


        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }


    private int Getdata(string keyword,string modifyday,string day,string table)//检测即将过期的证书
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = "select  count(" + keyword + ") from officedba." + table + " where CompanyCD=@CompanyCD  and " + keyword + " between '" + day + "' and '" + modifyday + "'";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);


        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    private int GetOverdueProdata(string keyword, string day, string table)//检测已经过期的证书
    {
        //string table = "CustInfo";
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select  count(" + keyword + ") from officedba." + table + "  where " + keyword + " < '" + day + "' and CompanyCD=@CompanyCD AND UsedStatus = 1 AND CheckStatus =1 ";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);


        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }


    private int GetProdata(string keyword, string modifyday, string day, string table)//检测即将过期的证书
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = "select  count(" + keyword + ") from officedba." + table + " where CompanyCD=@CompanyCD AND UsedStatus = 1 AND CheckStatus =1  and " + keyword + " between '" + day + "' and '" + modifyday + "'";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);


        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    
    //-------------------------------------------------------------------------------------------------------------------------------------//    
    private int GetBeiWangLuCount()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string sql = " select count(*) from officedba.PersonalMemo where CompanyCD=@CompanyCD and  Memoer=@Memoer and Status=0";

        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@Memoer", userInfo.EmployeeID);

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    private int SearchCanHuiTongZhi()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select count(*) from officedba.meetinginfo where CompanyCD=@CompanyCD and CONVERT(varchar(100),StartDate, 23)>=CONVERT(varchar(100),getdate(), 23)and (MeetingStatus=2 or MeetingStatus=4) and JoinUser like @JoinUser";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@JoinUser", ",%" + userInfo.EmployeeID + "%,");

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    private int GetContractCount()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select  count(*) from officedba.EmployeeContract where  CompanyCD=@CompanyCD and  Reminder= @Reminder  and    DATEADD(day, -AheadDay, EndDate) <@day ";
        SqlParameter[] paras = new SqlParameter[3];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@Reminder", userInfo.EmployeeID);
        paras[2] = new SqlParameter("@day", DateTime.Now.ToString("yyyy-MM-dd"));

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }


    private DataTable GetContractList()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select * from officedba.EmployeeContract where  Reminder= " + userInfo.EmployeeID + " and    DATEADD(day, -AheadDay, EndDate) <'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
        return XBase.Data.DBHelper.SqlHelper.ExecuteSql(sql);
    }

}