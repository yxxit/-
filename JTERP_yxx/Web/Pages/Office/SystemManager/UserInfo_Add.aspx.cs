/**********************************************
 * 类作用：   用户管理
 * 建立人：   吴志强
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
public partial class Pages_Office_SystemManager_UserInfo_Add : BasePage
{
    /// <summary>
    /// 类名：UserInfo_Add
    /// 描述：用户管理处理
    /// 
    /// 作者：陶春
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///

    protected void Page_Load(object sender, EventArgs e)
    {
        //lblMessage.Text = string.Empty;
        if (!Page.IsPostBack)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int UserNum = UserInfoBus.GetUserNum(companyCD);
            //BindDrpList(companyCD);
            hidden_companycd.Value = companyCD;
            hidModuleID.Value = ConstUtil.Menu_SerchUserInfo;
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddUserInfo, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
                btnback.Visible = true;
            }
            else
            {
                btnback.Visible = false;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["SetLicenseDisabledForSaaS"] == "true")
            {
                string cpuid = XBase.Common.LicenseValidator.GetCUPInfo();
                string[] t = XBase.Common.LicenseValidator.getlicense();
                if (t[0] == "false")
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Write("授权信息验证失败,请联系管理员！");
                    HttpContext.Current.Response.End();
                    return;
                }
                else
                {


                    try
                    {
                        if (UserNum >= Convert.ToInt32(t[9]))
                        {

                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.Write("您的授权用户数已到,请联系管理员！");
                            HttpContext.Current.Response.End();
                            return;
                        }

                    }
                    catch
                    {
                        //HttpContext.Current.Response.Clear();
                        //HttpContext.Current.Response.Write("授权信息验证失败,请联系管理员！");
                        //HttpContext.Current.Response.End();
                        //return;
                    }


                }

                return;
            }
            //判断公司是否启用USBKEY
            IsCompanyOpen.Value = UserInfoBus.IsOpenValidateByCompany(UserInfo.CompanyCD) ? "1" : "0";


        }

    }
    /// <summary>
    /// 绑定员工姓名
    /// </summary>
    //private void BindDrpList(string companyCD)
    //{
    //    //zyy修改，获取员工信息，绑定员工姓名和对应的公司名称，以区分是本公司员工还是客户
    //   // DataTable dt_employee = UserInfoBus.GetEmployeeInfo(companyCD);
    //    DataTable dt_employee = UserInfoBus.GetEmployeeInfoDiff(companyCD);
    //    //DataTable dt_company = UserInfoBus.GetCompanyName(companyCD);
    //    if (dt_employee != null && dt_employee.Rows.Count > 0)
    //    {
    //        EmployeeID.Items.Clear();
    //        foreach (DataRow Row in dt_employee.Rows)
    //        {
    //            ListItem Employee = new ListItem();
    //           // Employee.Text = Row["EmployeeName"].ToString() + "_" + Row["EmployeeNo"].ToString();
    //            if (!string.IsNullOrEmpty(Row["CustName"].ToString()))
    //            {
    //                Employee.Text = Row["EmployeeName"].ToString() + "_" + Row["CustName"].ToString();
    //            }
    //            else 
    //            {
    //                    Employee.Text = Row["EmployeeName"].ToString() + "_本公司" ;
    //            }
    //            Employee.Value = Row["ID"].ToString();
    //            EmployeeID.Items.Add(Employee);
    //        }
    //    }
    //}

}


