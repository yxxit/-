using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using System.IO;
using XBase.Data.Office.HumanManager;
using XBase.Business.Office.PurchaseManager;
using System.Text.RegularExpressions;

public partial class Pages_Office_PurchaseManager_ProviderInfo_Import : System.Web.UI.Page
{
    UserInfoUtil userinfo = new UserInfoUtil();
    public static DataSet ds = new DataSet();
    public string errorstr = string.Empty; //错误串
    public static int wrong = 0;
    public static int right = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
     userinfo=(UserInfoUtil)SessionUtil.Session["UserInfo"];
    }

    protected void initvalidate()
    {
        //this.lbl_validateend.Visible = false;
        lbl_jg.Text = string.Empty;

        this.lbl_result.Text = string.Empty;
        errorstr = string.Empty;

        if (Session["newfile"] != null)
        {
            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        }
    }
    #region 上传文件
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        #region 上传文件
        //上传文件

        right = 0;
        wrong = 0;
        this.tab_end.Visible = false;
        if (upExcelFile.PostedFile.FileName.Length < 1)
        {
            this.lbl_result.Text = "请选择要上传的Excel文件!";
            return;
        }
        else
        {
            this.initvalidate();
        }
        string filename = upExcelFile.PostedFile.FileName;
        string subfile = filename.Substring(filename.LastIndexOf(".") + 1);
        if (subfile.ToUpper() != "XLS" && subfile.ToUpper() != "XLSX")
        {
            this.lbl_result.Text = "导入文件格式错误,必须为XLS,XLSX格式!";
            //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "导入文件格式错误");
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
        }
        else
        {
            try
            {
                //获取企业上传文件路径 
                string upfilepath = ProductInfoBus.GetCompanyUpFilePath(userinfo.CompanyCD);//得到格式如:"D:\zhou"
                //获取企业并构造企业上传文件名称
                Session["newfile"] = DateTime.Now.ToString("yyyyMMddhhmmss") + filename.Substring(filename.LastIndexOf("\\") + 1);
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());

                #region
                try
                {
                    //ds = ProductInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);//导入临时表
                    ds = ProductInfoBus.ReadExcel_ds(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                string RetVal = ds.Tables[0].Rows[0]["序号"].ToString() + ds.Tables[0].Rows[0]["供应商编号"].ToString() +
                                                ds.Tables[0].Rows[0]["供应商名称"].ToString() + ds.Tables[0].Rows[0]["供应商简称"].ToString() +
                                                ds.Tables[0].Rows[0]["供应商类别"].ToString() + 
                                                ds.Tables[0].Rows[0]["人员编号"].ToString() + ds.Tables[0].Rows[0]["分管采购员"].ToString() +
                                                ds.Tables[0].Rows[0]["热点供应商"].ToString() + ds.Tables[0].Rows[0]["联络期限(天)"].ToString() +
                                                ds.Tables[0].Rows[0]["区域"].ToString() +
                                                ds.Tables[0].Rows[0]["省"].ToString() + ds.Tables[0].Rows[0]["市"].ToString() +
                                                ds.Tables[0].Rows[0]["联系人"].ToString() + ds.Tables[0].Rows[0]["电话"].ToString() +
                                                ds.Tables[0].Rows[0]["手机"].ToString() + ds.Tables[0].Rows[0]["传真"].ToString() +
                                                ds.Tables[0].Rows[0]["在线咨询"].ToString() + ds.Tables[0].Rows[0]["公司网址"].ToString() +
                                                ds.Tables[0].Rows[0]["邮编"].ToString() + ds.Tables[0].Rows[0]["电子邮件"].ToString() +
                                                ds.Tables[0].Rows[0]["发货地址"].ToString() + ds.Tables[0].Rows[0]["经营范围"].ToString() +
                                                ds.Tables[0].Rows[0]["单位性质"].ToString() + ds.Tables[0].Rows[0]["成立时间"].ToString() +
                                                ds.Tables[0].Rows[0]["注册资本(万元)"].ToString() + ds.Tables[0].Rows[0]["资产规模(万元)"].ToString() +
                                                ds.Tables[0].Rows[0]["年销售额(万元)"].ToString() + ds.Tables[0].Rows[0]["员工总数(个)"].ToString() +
                                                ds.Tables[0].Rows[0]["法人代表"].ToString() + ds.Tables[0].Rows[0]["注册地址"].ToString();

                                DataTable dt = new DataTable();
                                dt = ds.Tables[0].Clone();

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {


                                    DataRow dr = ds.Tables[0].NewRow();
                                    string tem = StrReplace(ds.Tables[0].Rows[i]["序号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["序号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["供应商编号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["供应商编号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["供应商名称"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["供应商名称"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["供应商简称"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["供应商简称"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["供应商类别"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["供应商类别"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["人员编号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["人员编号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["分管采购员"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["分管采购员"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["热点供应商"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["热点供应商"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["联络期限(天)"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["联络期限(天)"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["区域"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["区域"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["省"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["省"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["市"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["市"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["联系人"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["联系人"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["电话"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["电话"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["手机"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["手机"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["传真"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["传真"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["在线咨询"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["在线咨询"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["公司网址"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["公司网址"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["邮编"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["邮编"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["电子邮件"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["电子邮件"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["发货地址"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["发货地址"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["经营范围"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["经营范围"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["单位性质"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["单位性质"] = tem;
                                    }

                                    tem = StrReplace(ds.Tables[0].Rows[i]["成立时间"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["成立时间"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["注册资本(万元)"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["注册资本(万元)"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["资产规模(万元)"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["资产规模(万元)"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["年销售额(万元)"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["年销售额(万元)"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["员工总数(个)"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["员工总数(个)"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["法人代表"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["法人代表"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["注册地址"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["注册地址"] = tem;
                                    }
                                  
                                    dt.Rows.Add(dr.ItemArray);

                                }


                                ds.Tables.RemoveAt(0);
                                ds.Tables.Add(dt.DefaultView.ToTable());
                                for (int i = ds.Tables[0].Rows.Count - 1; i > 0; i--)
                                {
                                    bool flag = false;
                                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                    {
                                        if (ds.Tables[0].Rows[i][j].ToString() != "" && ds.Tables[0].Rows[i][j].ToString() != null)
                                        {
                                            flag = true;
                                        }

                                    }
                                    int a = 0;
                                    if (!flag)
                                    {
                                        ds.Tables[0].Rows.RemoveAt(i);

                                    }

                                }


                            }
                            catch (Exception ex)
                            {
                                this.lbl_result.Text = "数据读取失败,可能原因：Excel模板格式不正确";
                                //initvalidate();
                                this.tr_result.Visible = false;
                                //this.lbl_validateend.Visible = false;
                                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                                return;
                            }

                            this.lbl_result.Text = "Excel文件上传成功，请点击“批量导入”导入文件！";
                            //this.setup1.Enabled = true;
                            //this.setup2.Enabled = false;
                            //this.setup3.Enabled = false;
                            //this.setup4.Enabled = false;
                            //this.setup5.Enabled = false;
                            //this.setup6.Enabled = false;
                            //this.setup7.Enabled = false;
                            //this.setup8.Enabled = false;
                            this.tr_result.Visible = false;
                            this.btn_input.Enabled = true;

                            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                        }
                        else
                        {
                            initvalidate();
                            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                            this.lbl_result.Text = "您导入的Excel表没有数据!";
                        }
                    }
                    else
                    {
                        initvalidate();
                        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                        this.lbl_result.Text = "您导入的Excel表没有数据!";
                    }
                }
                catch (Exception ex)
                {
                    //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "数据读取失败");
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                    initvalidate();
                    this.lbl_result.Text = "数据读取失败,原因：" + ex.Message.ToString();
                    //this.lbl_result.Text = "数据读取失败!";
                }
                #endregion
            }
            catch (Exception ex)
            {
                //this.setup1.Enabled = false;
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                this.lbl_result.Text = "数据读取失败,原因：" + ex.Message;
            }
        }
        #endregion
    }
    #endregion

    #region 批量导入
    protected void btn_input_Click(object sender, EventArgs e)
    {
        string err_flag = "";
        btn_input.Enabled = false;
        try
        {
            if (userinfo.EmployeeID.ToString().Length < 1 || userinfo.UserID.Length < 1)
            {
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";
            }
            else
            {
                if (!ds.Tables[0].Columns.Contains("del"))
                {
                    ds.Tables[0].Columns.Add("del");
                }
                if (!ds.Tables[0].Columns.Contains("错误"))
                {
                    ds.Tables[0].Columns.Add("错误");
                }
                DataTable dt = ds.Tables[0];
                //if (CustInfoBus.InsertCustInfoRecord(dt, userinfo.CompanyCD,userinfo.EmployeeID.ToString(),userinfo.UserID))
                //{
                //    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "客户档案导入成功！");
                //    this.lbl_jg.Text = "Excel数据导入成功";
                //}

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strErr = DetermineLegality(dt.Rows[i]);
                    if (string.IsNullOrEmpty(strErr))
                    {
                        if (ProviderInfoBus.InsertProviderInfoRecord(dt.Rows[i], userinfo.CompanyCD, userinfo.EmployeeID.ToString(), userinfo.UserID))
                        {
                            dt.Rows[i]["del"] = "1";
                            right++;
                        }
                    }
                    else
                    {

                        if (i % 2 == 1)
                        {
                            errorstr += "<font color='#000093'>";
                        }
                        if (i % 2 == 0)
                        {
                            errorstr += "<font color='#003D79'>";
                        }
                        dt.Rows[i]["del"] = "0";
                        dt.Rows[i]["错误"] = strErr;
                        errorstr += "供应商编号：";
                        errorstr += dt.Rows[i]["供应商编号"];
                        errorstr += "&nbsp;&nbsp;&nbsp;&nbsp;";
                        errorstr += "供应商名称：";
                        errorstr += dt.Rows[i]["供应商名称"];
                        errorstr += "&nbsp;&nbsp;&nbsp;&nbsp;";


                        errorstr += "错误信息：";
                        errorstr += strErr;
                        errorstr += "<br>";
                        errorstr += "</font>";
                    }
                }
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["del"].ToString() == "1")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }
                ds.Tables.RemoveAt(0);
                ds.Tables.Add(dt.DefaultView.ToTable());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    wrong = Convert.ToInt32(ds.Tables[0].Rows.Count);

                    if (wrong > 0)
                    {
                        Err_Excel.Visible = true;
                        Err_Excel.Enabled = true;
                        this.lbl_info.Visible = true;
                        this.tr_result.Visible = true;

                    }

                    this.lbl_jg.Text = "<font  color='#CE0000' size='+2'  >Excel数据导入成功" + right + "条，导入失败" + wrong + "条！</font>";
                    this.lbl_info.Text = "<font  color='#CE0000' size='+1'  >请点击‘失败数据导出’查看相关错误，修改后再导入！</font>";
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), right, 0, "供应商档案导入成功" + right + "条,导入失败" + wrong + "条！<br>错误信息：<br>" + errorstr + "");
                }
                else
                {
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), right, 1, "供应商档案导入成功" + right + "条！");
                    this.lbl_jg.Text = "<font  color='#CE0000' size='+2'  >Excel数据" + right + "条全部导入成功!</font>";
                    this.lbl_info.Visible = false;
                    this.Err_Excel.Enabled = false;
                    if (Session["newfile"] != null)
                    {
                        ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                    }
                }

                this.tab_end.Visible = true;
                //btn_input.Enabled = false;
            }

        }

        catch (Exception ex)
        {
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            btn_input.Enabled = true;
            this.tab_end.Visible = true;
            this.lbl_jg.Text = ex.Message;
        }
    }
    #endregion

    #region 去掉字符串中特定ASC码字符
    /// <summary>
    /// 去掉字符串中特定ASC码字符
    /// </summary>
    /// <param name="stri">字符串</param>
    /// <param name="asc">特定字符ASCⅡ</param>
    /// <returns></returns>
    public string StrReplace(string stri, int asc)
    {
        string temp = "";
        CharEnumerator CEnumerator = stri.GetEnumerator();

        while (CEnumerator.MoveNext())
        {

            byte[] array = new byte[1];

            array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());

            int asciicode = (short)(array[0]);

            if (asciicode != asc)
            {

                temp += CEnumerator.Current.ToString();

            }

        }
        return temp;
    }
    #endregion

    #region 判断数据是否合法
    public string DetermineLegality(DataRow dr)
    {
        bool bSpecial = true;
        string strErr = "";
        string strErrs = "";
        strErr = CheckSpecialCharacters(dr);//特殊字符校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
            bSpecial = false;
        }
        strErr = Space_Check(dr);//空值校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        if (bSpecial)
        {
            strErr = DuplicateCheck(dr);//判断往来单位编号、名称是否存在
            if (!string.IsNullOrEmpty(strErr))
            {
                strErrs += strErr;
            }
        }
        //strErr = LengthCheck(dr);//信息项长度校验
        //if (!string.IsNullOrEmpty(strErr))
        //{
        //    strErrs += strErr;
        //}
        strErr = Date_Check(dr);//日期格式验证
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }

        strErr = Code_Set_Check(dr);//人员与人员编号匹配校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        strErr = CompanyType_Check(dr);//单位校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        strErr = Area_Check(dr);//区域校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }

        strErr = Num_F_Check(dr);//验证数字格式
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }

        strErr = CustType_Check(dr);//供应商类别校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }

        strErr = LinkCycle_Check(dr);//联络期限校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        strErr = HotIs_Check(dr);//热点供应商校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        return strErrs;
    }

    #endregion

    #region 字符串特殊字符显示

    public static string ShowCheckString(string source)
    {
        string StrInfo = "";
        string[] Strcheck = { "^", "'", "\\", "<", ">", "%", "?", "/", "+", "[", "]", "*", "$" };
        foreach (string str in Strcheck)
        {
            if (source.Contains(str))
            {
                StrInfo += str + ",";
            }
        }
        if (StrInfo.Length > 0)
        {
            StrInfo = StrInfo.Substring(0, StrInfo.Length - 1);
        }
        return StrInfo;
    }

    #endregion

    #region 特殊字符校验
    public string CheckSpecialCharacters(DataRow dr)
    {
        string strErr = "";
        if (checkString(dr["供应商名称"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["供应商名称"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：";
                strErr += "供应商名称含有特殊字符(" + StrInfo + ")  ";
            }
            else
            {
                strErr += "供应商名称含有特殊字符(" + StrInfo + ") ";
            }

        }
        if (checkString(dr["供应商编号"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["供应商编号"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：";
                strErr += "供应商编号含有特殊字符(" + StrInfo + ")  ";
            }
            else
            {
                strErr += "供应商编号含有特殊字符(" + StrInfo + ") ";
            }
        }
        if (checkString(dr["分管采购员"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["分管采购员"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：";
                strErr += "分管采购员含有特殊字符(" + StrInfo + ")  ";
            }
            else
            {
                strErr += "分管采购员含有特殊字符(" + StrInfo + ") ";
            }
        }
        if (checkString(dr["人员编号"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["人员编号"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：";
                strErr += "人员编号含有特殊字符(" + StrInfo + ")  ";
            }
            else
            {
                strErr += "人员编号含有特殊字符(" + StrInfo + ") ";
            }
        }
        if (checkString(dr["供应商类别"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["供应商类别"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：";
                strErr += "供应商类别含有特殊字符(" + StrInfo + ")  ";
            }
            else
            {
                strErr += "供应商类别含有特殊字符(" + StrInfo + ") ";
            }
        }
        return strErr;
    }

    #endregion

    #region Excel空值校验
    protected string Space_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        string suberrorstrs = string.Empty;

        string[] code = { "序号", "供应商编号", "供应商名称","供应商类别","热点供应商","联络期限(天)" };

        for (int k = 0; k < code.Length; k++)
        {
            if (dr[code[k]].ToString().Trim().Length < 1)
            {
                suberrorstr += "" + code[k] + "  值不能为空值,导入操作失败!";
            }
        }


        if (suberrorstr != "" && suberrorstr != null)
        {
            suberrorstrs = "空值校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
        }
        else
        {
            this.tr_result.Visible = false;
        }
        return suberrorstrs;
    }
    #endregion

    #region 长度判断
    //public string LengthCheck(DataRow dr)
    //{
    //    //3.长度判断
    //    string suberrorstr = string.Empty;
    //    string strErr = string.Empty;
    //    string[] code = { "供应商编号", "供应商名称", "供应商简称", "供应商类别", "人员编号", "分管采购员", "热点供应商", "联络期限(天)", "区域", "省", "市", "联系人", "电话", "手机", "传真", "在线咨询", "公司网址", "邮编", "电子邮件", "发货地址", "经营范围", "单位性质", "成立时间", "注册资本(万元)", "资产规模(万元)", "年销售额(万元)", "员工总数(个)", "法人代表", "注册地址" };
    //    int[] codelen = { 50, 100, 50, 12, 50, 50, 1, 12, 12, 50, 50, 50, 50, 50, 50, 100, 100, 20, 50, 100, 200, 50, 10, 12, 12, 12, 12, 20, 100 };

    //    for (int k = 0; k < code.Length; k++)
    //    {
    //        if (dr[code[k]].ToString().Trim().Length > codelen[k])
    //        {
    //            strErr += "" + code[k] + " 数据长度过长,导入操作失败!";
    //        }
    //    }
    //    return strErr;

    //}
    #endregion

    #region 判断往来单位编号、名称是否存在
    protected string DuplicateCheck(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号
        int k = 0;
        int total = 0;//总错误记录


        if (dr["供应商编号"].ToString().Length > 0)
        {
            string[] arr = new string[] { "CustNo", "CompanyCD" };
            string[] CustNos = new string[] { dr["供应商编号"].ToString().Trim(), userinfo.CompanyCD };

            bool NoHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.ProviderInfo", arr, CustNos);
            if (NoHas)
            {
                suberrorstr += "供应商编号与数据库中记录重复,导入操作失败!";
                j++;
            }
        }
        if (dr["供应商名称"].ToString().Length > 0)
        {
            string[] arr = new string[] { "CustName", "CompanyCD" };
            string[] CustNames = new string[] { dr["供应商名称"].ToString().Trim(), userinfo.CompanyCD };

            bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.ProviderInfo", arr, CustNames);
            if (NameHas)
            {
                suberrorstr += "供应商名称与数据库中记录重复,导入操作失败!";
                k++;
            }
        }

        if (j > 0 || k > 0)
        {
            suberrorstrs += "是否存在校验错误列表(供应商编号,名称)：";
            suberrorstrs += suberrorstr;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);
            this.tr_result.Visible = true;

        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }

    #endregion

    #region 日期时间格式校验
    protected string Date_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        string[] code = { "成立时间" };

        for (int k = 0; k < code.Length; k++)
        {
            try
            {
                if (dr[code[k]].ToString().Length > 0)
                {
                    DateTime.Parse(dr[code[k]].ToString().Trim());
                }
            }
            catch
            {
                suberrorstr += " " + code[k] + " 格式错误,导入操作失败!";
                j++;
            }
        }

        if (j > 0)
        {
            suberrorstrs = "日期时间格式校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }
    #endregion

    #region 人员与人员编号匹配校验
    protected string Code_Set_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;

        int j = 0;//定义错误列表序号

        DataColumn Manager = new DataColumn();
        if (!ds.Tables[0].Columns.Contains("Manager"))
        {
            ds.Tables[0].Columns.Add("Manager");
        }
        if (dr["人员编号"].ToString().Trim().Length < 1 && dr["分管采购员"].ToString().Trim().Length > 0 || dr["分管采购员"].ToString().Trim().Length < 1 && dr["人员编号"].ToString().Trim().Length >0)
        {
            suberrorstrs += "人员编号和分管采购员要么都为空值，要么都输入。";
        }

        if (dr["人员编号"].ToString().Trim().Length > 0 && dr["分管采购员"].ToString().Trim().Length > 0)
        {
            DataTable dt = EmployeeInfoDBHelper.IsHaveEmployeesByEmployeeNOAndName(dr["分管采购员"].ToString().Trim(), dr["人员编号"].ToString().Trim(), userinfo.CompanyCD);
            if (dt.Rows.Count < 1)
            {
                suberrorstr += "分管采购员与对应的人员编号不匹配,导入操作失败!";
                j++;
            }
            else
            {
                dr["Manager"] = dt.Rows[0]["ID"].ToString();
            }
        }

        if (j > 0)
        {
            suberrorstrs = "分管采购员与人员编号匹配式校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }
    #endregion

    #region 单位性质校验（1事业，2企业，3社团，4自然人，5其他）
    protected string CompanyType_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        DataColumn CompanyType = new DataColumn();
        if (!ds.Tables[0].Columns.Contains("CompanyType"))
        {
            ds.Tables[0].Columns.Add("CompanyType");
        }


        if (dr["单位性质"].ToString().Trim().Length > 0)
        {
            //单位性质（1事业，2企业，3社团，4自然人，5其他）
            switch (dr["单位性质"].ToString())
            {
                case "事业":
                    dr["CompanyType"] = "1";
                    break;
                case "企业":
                    dr["CompanyType"] = "2";
                    break;
                case "社团":
                    dr["CompanyType"] = "3";
                    break;
                case "自然人":
                    dr["CompanyType"] = "4";
                    break;
                case "其他":
                    dr["CompanyType"] = "5";
                    break;
                default:
                    suberrorstr += "单位性质不存在,导入操作失败!";
                    j++;
                    break;
            }
        }

        if (j > 0)
        {
            suberrorstrs = "单位性质校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }
    #endregion

    #region 区域校验
    protected string Area_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn AreaID = new DataColumn();

        if (!ds.Tables[0].Columns.Contains("AreaID"))
        {
            ds.Tables[0].Columns.Add("AreaID");
        }


        if (dr["区域"].ToString().Trim().Length > 0)
        {
            DataTable dt = ProviderInfoBus.CheckArea(dr["区域"].ToString().Trim(), userinfo.CompanyCD);

            if (dt.Rows.Count < 1)
            {
                suberrorstr += "区域与系统的区域不匹配,导入操作失败!";
                j++;
            }
            else
            {
                dr["AreaID"] = dt.Rows[0]["ID"].ToString();
            }
        }

        if (j > 0)
        {
            suberrorstrs = "与系统的区域匹配式校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

            this.btn_input.Enabled = true;
            //lbl_validateend.Visible = true;
        }
        return suberrorstrs;
    }
    #endregion

    #region 供应商类别校验
    protected string CustType_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn CustType = new DataColumn();

        if (!ds.Tables[0].Columns.Contains("CustType"))
        {
            ds.Tables[0].Columns.Add("CustType");
        }


        if (dr["供应商类别"].ToString().Trim().Length > 0)
        {
            DataTable dt = ProviderInfoBus.CheckCustType(dr["供应商类别"].ToString().Trim(), userinfo.CompanyCD);

            if (dt.Rows.Count < 1)
            {
                suberrorstr += "供应商类别与系统的供应商类别不匹配,导入操作失败!";
                j++;
            }
            else
            {
                dr["CustType"] = dt.Rows[0]["ID"].ToString();
            }
        }

        if (j > 0)
        {
            suberrorstrs = "与系统的供应商类别匹配时校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
           }
        }
        else
        {
            this.tr_result.Visible = false;

            this.btn_input.Enabled = true;
        }
        return suberrorstrs;
    }
    #endregion

    #region 热点供应商校验（1是，2否）
    protected string HotIs_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        DataColumn HotIs = new DataColumn();
        if (!ds.Tables[0].Columns.Contains("HotIs"))
        {
            ds.Tables[0].Columns.Add("HotIs");
        }


        if (dr["热点供应商"].ToString().Trim().Length > 0)
        {
            //热点供应商（1是，2否）
            switch (dr["热点供应商"].ToString())
            {
                case "是":
                    dr["HotIs"] = "1";
                    break;
                case "否":
                    dr["HotIs"] = "2";
                    break;
                default:
                    suberrorstr += "热点供应商填写不正确,导入操作失败!";
                    j++;
                    break;
            }
        }

        if (j > 0)
        {
            suberrorstrs = "热点供应商校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }
    #endregion

    #region 联络期限校验
    protected string LinkCycle_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn LinkCycle = new DataColumn();

        if (!ds.Tables[0].Columns.Contains("LinkCycle"))
        {
            ds.Tables[0].Columns.Add("LinkCycle");
        }


        if (dr["联络期限(天)"].ToString().Trim().Length > 0)
        {
            DataTable dt = ProviderInfoBus.CheckLinkCycle(dr["联络期限(天)"].ToString().Trim(), userinfo.CompanyCD);

            if (dt.Rows.Count < 1)
            {
                suberrorstr += "联络期限(天)与系统的联络期限(天)不匹配,导入操作失败!";
                j++;
            }
            else
            {
                dr["LinkCycle"] = dt.Rows[0]["ID"].ToString();
            }
        }

        if (j > 0)
        {
            suberrorstrs = "与系统的联络期限匹配时校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
        }
        else
        {
            this.tr_result.Visible = false;

            this.btn_input.Enabled = true;
        }
        return suberrorstrs;
    }
    #endregion
    #region 数字格式校验
    protected string Num_F_Check(DataRow dr)
    {
        string suberrorstr = string.Empty;
        string suberrorstrs = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        string[] code = { "注册资本(万元)", "资产规模(万元)", "年销售额(万元)", "员工总数(个)" };

        for (int k = 0; k < code.Length; k++)
        {
            try
            {
                if (dr[code[k]].ToString().Length > 0)
                {
                    if (k == 3)
                    {
                        Convert.ToInt32(dr[code[k]].ToString().Trim());
                    }
                    else
                    {
                        Convert.ToDecimal(dr[code[k]].ToString().Trim());
                        //decimal.Round(decimal.Parse(ds.Tables[0].Rows[i][code[k]].ToString().Trim()), 2);
                    }
                }
            }
            catch
            {
                suberrorstr += " " + code[k] + " 格式错误,导入操作失败!<br>";
                j++;
            }
        }

        if (j > 0)
        {
            suberrorstrs = "数字格式校验错误：";
            suberrorstrs += suberrorstr;
            this.tr_result.Visible = true;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, suberrorstrs);

            }
        }
        else
        {
            this.tr_result.Visible = false;

        }
        return suberrorstrs;
    }
    #endregion
    #region 验证特殊字符
    public static bool checkString(string source)
    {
        //Regex regExp = new Regex("[/^[a-zA-Z0-9_-()[]{}.]+$/g]");
        Regex regExp = new Regex(@"^[^'\\<>%?/]*$");
        return !regExp.IsMatch(source);
    }

    public static bool chenkHave(string source)
    {
        Regex regExp = new Regex(@"^[a-zA-Z0-9-()_\[\]{}\.\\]+$");
        return !regExp.IsMatch(source);
    }
    #endregion

    protected void Err_Excel_Click(object sender, EventArgs e)
    {
        Err_Excel.Enabled = false;

        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        //方法
        OutputToExecl.ExportToTable_Err(this, dt,
                new string[] { "错误", "序号", "供应商编号", "供应商名称", "供应商简称", "人员编号", "分管采购员", "区域", "省", "市", "联系人", "电话", "手机", "传真", "在线咨询", "公司网址", "邮编", "电子邮件", "发货地址", "经营范围", "单位性质", "成立时间", "注册资本(万元)", "资产规模(万元)", "年销售额(万元)", "员工总数(个)", "法人代表", "注册地址" },
                new string[] { "错误", "序号", "供应商编号", "供应商名称", "供应商简称", "人员编号", "分管采购员", "区域", "省", "市", "联系人", "电话", "手机", "传真", "在线咨询", "公司网址", "邮编", "电子邮件", "发货地址", "经营范围", "单位性质", "成立时间", "注册资本(万元)", "资产规模(万元)", "年销售额(万元)", "员工总数(个)", "法人代表", "注册地址" },
                "供应商档案导入失败列表", "操作提示：请按照错误提示编辑好后，另存为Excel 标准格式。注意要在导入前把本行删除！");


        lbl_jg.Text = "导出完成！";
    }
  
}
