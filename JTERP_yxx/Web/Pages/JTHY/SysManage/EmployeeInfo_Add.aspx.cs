/**********************************************
 * 类作用：   人员信息维护处理
 * 建立人：   吴志强
 * 建立时间： 2009/03/10
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_EmployeeInfo_Add : BasePage
{
    /// <summary>
    /// 类名：EmployeeInfo_Add
    /// 描述：人员信息处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示处理
        if(!IsPostBack){

            //获取系统当前日期
            //hdSystemDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            string employeeID = Request.QueryString["ID"];
            string SearchType = Request.QueryString["type"];

            #region 分类初期表示
            hidSysteDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            ////职称 分类标识
            //ddlPosition.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlPosition.TypeCode = ConstUtil.CODE_TYPE_POSITION;
            //ddlPosition.IsInsertSelect = true;
            //岗位
            //DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            //ddlQuarter_ddlCodeType.DataSource = dtQuarter;           
            //ddlQuarter_ddlCodeType.DataValueField = "ID";
            //ddlQuarter_ddlCodeType.DataTextField = "QuarterName";
            //ddlQuarter_ddlCodeType.DataBind();
            //ListItem Item = new ListItem();
            //Item.Value = "0";
            //Item.Text = "--请选择--";
            //ddlQuarter_ddlCodeType.Items.Insert(0, Item);
            
            ////婚姻状况
            //ddlMarriage.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlMarriage.TypeCode = ConstUtil.CODE_TYPE_MARRIAGE;
            //ddlMarriage.IsInsertSelect = true;
            //学历
            //ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            //ddlCulture.IsInsertSelect = true;
            ////专业
            //ddlProfessional.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlProfessional.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            //ddlProfessional.IsInsertSelect = true;
            ////政治面貌
            //ddlLandscape.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlLandscape.TypeCode = ConstUtil.CODE_TYPE_LANDSACAPE;
            //ddlLandscape.IsInsertSelect = true;
            ////宗教信仰
            //ddlReligion.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlReligion.TypeCode = ConstUtil.CODE_TYPE_RELIGION;
            //ddlReligion.IsInsertSelect = true;
            //民族
            //ddlNational.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlNational.TypeCode = ConstUtil.CODE_TYPE_NATIONAL;
            //ddlNational.IsInsertSelect = true;
            ////国籍
            //ddlCountry.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlCountry.TypeCode = ConstUtil.CODE_TYPE_COUNTRY;
            //ddlCountry.IsInsertSelect = true;
            ////外语语种1
            //ddlLanguage1.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlLanguage1.TypeCode = ConstUtil.CODE_TYPE_LANGUAGE;
            //ddlLanguage1.IsInsertSelect = true;
            ////外语语种2
            //ddlLanguage2.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlLanguage2.TypeCode = ConstUtil.CODE_TYPE_LANGUAGE;
            //ddlLanguage2.IsInsertSelect = true;
            ////外语语种3
            //ddlLanguage3.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlLanguage3.TypeCode = ConstUtil.CODE_TYPE_LANGUAGE;
            //ddlLanguage3.IsInsertSelect = true;

            //岗位职等
            //ddlAdminLevelID.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlAdminLevelID.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            //ddlAdminLevelID.IsInsertSelect = true;

            #endregion

            #region 返回页面以及查询条件处理
            //模板列表模块ID
            hidWorkModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_WORK;//在职人员
            hidLeaveModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_LEAVE;//离职人员
            hidReserveModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_RESERVE;//人才储备
            hidInterviewModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTINTERVIEW_EDIT;//面试
            hidWaitModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ENTER;//待入职
            hidInitSysModuleID.Value = ConstUtil.MODULE_ID_INIT_SYSTEM;//系统管理初始化向导
            hidInitHumanModuleID.Value = ConstUtil.MODULE_ID_INIT_HUMAN;//人力资源初始化向导


            //获取请求参数            
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {

                if (Request.QueryString["TypeFlag"] != null && Request.QueryString["TypeFlag"] == "2")  //个人信息
                {
                    btnBack.Visible = false;
                    this.btnSave.Visible = false;
                    this.btnCont.Visible = false;

                    employeeID  = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//用户ID
                    SearchType = "";
                }
                else
                {

                    //返回按钮可见
                    btnBack.Visible = true;
                    //获取列表的查询条件
                    string searchCondition = requestParam.Substring(firstIndex);
                    //string SearchType = Request.QueryString["type"];
                    //if (SearchType != "Continue")
                    //{
                    //迁移页面
                    hidFromPage.Value = Request.QueryString["FromPage"];
                    //去除参数
                    searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD, string.Empty);
                    searchCondition = searchCondition.Replace("&type=Continue", string.Empty);
                    searchCondition = searchCondition.Replace("&FromPage=" + hidFromPage.Value, string.Empty);
                    //设置检索条件
                    hidSearchCondition.Value = searchCondition;
                }
                   
                //}
                //else
                //{
 
                //}
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }

            //if (Request.QueryString["type"] != null)
            //{
            //    //返回按钮不可见
            //    btnBack.Visible = false;
            //}
            #endregion

            #region 页面内容初期设置
            //获取人员编号
            //string employeeID = Request.QueryString["ID"];//张？
            hidEmployeeID.Value = employeeID;
            //string SearchType = Request.QueryString["type"];

            //编号初期处理
            codruleEmployNo.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            codruleEmployNo.ItemTypeID = ConstUtil.CODING_RULE_EMPLOYEE_NO;

            //employeeID = "38";
            //人员编号为空，为新建模式
            if (string.IsNullOrEmpty(employeeID) || SearchType == "Continue")
            {
                //编号初期处理
                //codruleEmployNo.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
                //codruleEmployNo.ItemTypeID = ConstUtil.CODING_RULE_EMPLOYEE_NO;
                //设置人员编号不可见
                divEmployeeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");

                //创建人
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                ////设置用户名
                //txtCreateUserName.Text = userInfo.EmployeeName;
                ////创建日期
                //txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ////工作履历表格
                //divWorkInfo.InnerHtml = CreateWorkTable() + EndTable();
                ////学习履历表格
                //divStudyInfo.InnerHtml = CreateStudyTable() + EndTable();
                ////技能信息表格
                //divSkillInfo.InnerHtml = CreateSkillTable() + EndTable();
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
            }
            else
            {
                //设置标题
                divTitle.InnerText = "人员档案";
                //获取并设置人员信息
                InitEmployeeInfo(employeeID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
            }
            #endregion
                        
        }
    }
    #endregion

    #region 根据人员ID，获取人员信息，并设置到页面显示
    /// <summary>
    /// 根据人员编号，获取人员信息，并设置到页面显示
    /// </summary>
    /// <param name="employeeNo">人员编号</param>
    private void InitEmployeeInfo(string employeeID)
    {
        //查询数据
        EmployeeInfoModel employModel = EmployeeInfoBus.GetEmployeeInfoWithID(int.Parse(employeeID));

        //设置人员基本信息
        InitEmployeeBaseInfo(employModel);

   
    }    
    #endregion

    #region 设置人员基本信息
    /// <summary>
    /// 设置人员基本信息
    /// </summary>
    /// <param name="employModel">人员信息</param>
    private void InitEmployeeBaseInfo(EmployeeInfoModel employModel)
    {
        //设置人员编号
        divEmployeeNo.InnerText = employModel.EmployeeNo;
        divEmployeeNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");
        //姓名
        txtEmployeeName.Value = employModel.EmployeeName;
        //工号
        txtEmployeeNum.Text = employModel.EmployeeNum;
        //拼音缩写
        txtPYShort.Text = employModel.PYShort;
        //曾用名
        //txtUsedName.Text = employModel.UsedName;
        ////英文名
        //txtNameEn.Text = employModel.NameEn;
        //人员分类
        string flag = employModel.Flag;
        ddlFlag.Value = flag;
        //人才储备时
        //if ("2".Equals(flag))
        //{
        //    //标题设置
        //    divJobTitle.InnerHtml = "应聘职务";
        //    //职称控件不显示
        //    divPosition.Attributes.Add("style", "display:none;");
        //    //应聘职务控件显示
        //    divPositionTitle.Attributes.Add("style", "display:block;");
        //    //应聘职务值设置
        //txtPositionTitle.Text = employModel.PositionTitle;
        //    //所在岗位标题设置清除
        //    //divQuarter.InnerHtml = "";
        //    //所在岗位控件不显示
        //    divQuarterValue.Attributes.Add("style", "display:none;");

        //}
        //else
        //{
        //    //职称
        //ddlPosition.SelectedValue = employModel.PositionID;
        //    divPosition.Attributes.Add("style", "display:block;");
        //    txtPositionTitle.Visible = false;
        //    //所在岗位
        ddlQuarter_ddlCodeType.Text = employModel.QuarterID.ToString().Trim() == "0" ? "" : employModel.QuarterID;
        //    divQuarterValue.Attributes.Add("style", "display:block;");
        //    divQuarter.Attributes.Add("style", "display:block;");
        //}

        //身份证
        //txtCardID.Text = employModel.CardID;
        ////社保卡号
        //txtSafeguardCard.Text = employModel.SafeguardCard;
        //性别
        ddlSex.SelectedValue = employModel.Sex;
        //出身年月日
        txtBirth.Text = employModel.Birth;
        //婚姻状况
        //ddlMarriage.SelectedValue = employModel.MarriageStatus;
        ////籍贯
        //txtOrigin.Text = employModel.Origin;
        //联系电话
        txtTelephone.Text = employModel.Telephone;
        //手机号码
        txtMobile.Text = employModel.Mobile;
        //电子邮件
        txtEMail.Text = employModel.EMail;
        
        //其他联系方式
        //txtOtherContact.Text = employModel.OtherContact;
        ////家庭住址
        //txtHomeAddress.Text = employModel.HomeAddress;
        ////健康状况
        //ddlHealth.SelectedValue = employModel.HealthStatus;
        ////学历
        //ddlCulture.SelectedValue = employModel.CultureLevel;
        ////毕业院校
        //txtSchool.Text = employModel.GraduateSchool;
        ////专业
        //ddlProfessional.SelectedValue = employModel.Professional;
        ////政治面貌
        //ddlLandscape.SelectedValue = employModel.Landscape;
        ////宗教信仰
        //ddlReligion.SelectedValue = employModel.Religion;
        //民族
        //ddlNational.SelectedValue = employModel.National;
        //户口
        //txtAccount.Text = employModel.Account;
        ////户口性质
        //ddlAccountNature.SelectedValue = employModel.AccountNature;
        ////国籍
        //ddlCountry.SelectedValue = employModel.CountryID;
        ////身高
        //txtHeight.Text = employModel.Height;
        ////体重
        //txtWeight.Text = employModel.Weight;
        ////视力
        //txtSight.Text = employModel.Sight;
        ////最高学位
        //txtDegree.Text = employModel.Degree;
        ////证件类型
        //txtDocuType.Text = employModel.DocuType;
        ////特长
        //txtFeatures.Text = employModel.Features;
        ////计算机水平
        //txtComputerLevel.Text = employModel.ComputerLevel;
        ////参加工作时间
        //txtWorkTime.Text = employModel.WorkTime;
        //if (txtWorkTime.Text.Trim() != "")
        //{
        //    DateTime oldDate = Convert.ToDateTime(txtWorkTime.Text.Trim());
        //    DateTime newDate = DateTime.Now;
            
        //    TimeSpan ts = newDate - oldDate;           
        //    int differenceInDays = ts.Days/365 + 1;   
        //    //总工龄
        //    txtTotalSeniority.Text = differenceInDays.ToString();//employModel.TotalSeniority.ToString();

        //}
        
        //外语语种1
        //ddlLanguage1.SelectedValue = employModel.ForeignLanguage1;
        ////外语语种2
        //ddlLanguage2.SelectedValue = employModel.ForeignLanguage2;
        ////外语语种3
        //ddlLanguage3.SelectedValue = employModel.ForeignLanguage3;
        ////外语水平1
        //ddlLanguageLevel1.SelectedValue = employModel.ForeignLanguageLevel1;
        ////外语水平2
        //ddlLanguageLevel2.SelectedValue = employModel.ForeignLanguageLevel2;
        ////外语水平3
        //ddlLanguageLevel3.SelectedValue = employModel.ForeignLanguageLevel3;
        //创建人
        //txtCreateUserName.Text = employModel.CreateUserName;
        ////创建时间
        //txtCreateDate.Text = employModel.CreateDate.ToString("yyyy-MM-dd");
        //txtProfessionalDes.Text = employModel.ProfessionalDes;


        ////简历
        //string resumeUrl = employModel.Resume;

        //ddlAdminLevelID.SelectedValue = employModel.AdminLevelID.ToString();
        DeptName.Value = employModel.DeptName;
        //hdDeptID.Value = employModel.DeptID.ToString();
        ////txtEnterDate.Value = Convert.ToDateTime(employModel.EnterDate.ToString()).ToShortDateString();
        txtEnterDate.Value = employModel.EnterDate.ToString() == "0001-1-1 0:00:00" ? "" : Convert.ToDateTime(employModel.EnterDate).ToShortDateString();


        ////简历存在的时候 
        //if (!string.IsNullOrEmpty(resumeUrl))
        //{
        //    hfResume.Value = resumeUrl;
        //    hfPageResume.Value = resumeUrl;
        //    //上传简历不显示
        //    divUploadResume.Attributes.Add("style", "display:none;");
        //    //简历处理显示
        //    divDealResume.Attributes.Add("style", "display:block;");
        //    int j = resumeUrl.LastIndexOf("\\") + 1;
        //    spanAttachmentName.InnerHtml = resumeUrl.Substring(j, resumeUrl.Length - j);
           
        //}
        //else
        //{
        //    //简历处理不显示
        //    divDealResume.Attributes.Add("style", "display:none;");
        //    //上传简历显示
        //    divUploadResume.Attributes.Add("style", "display:block;");
        //}
        ////相片
        //string photoURL = employModel.PhotoURL;
        //if (string.IsNullOrEmpty(photoURL))
        //{
        //    imgPhoto.Src = "~/Images/Pic/Pic_Nopic.jpg";
        //}
        //else
        //{
        //    imgPhoto.Src = "~/Images/Photo/" + photoURL;
        //    hfPhotoUrl.Value = photoURL;
        //    hfPagePhotoUrl.Value = photoURL;
        //}
    }
    #endregion

    

    #region 返回表格的结束符

    /// <summary>
    /// 返回表格的结束符
    /// </summary>
    /// <returns></returns>
    private string EndTable()
    {
        return "</table>";
    }
    #endregion    
}
