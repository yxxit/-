/**********************************************
 * 类作用：   系统初始化添加数据
 * 建立人：   宋凯歌
 * 建立时间： 2010/11/12
 ***********************************************/
using System;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using XBase.Model.Office.SystemManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Data.Office.SystemManager
{
    public class InitSystemDataDBHelper
    {
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// 
        public static bool AddInitSystemUserData(InitSystemDataModel model)
        {
            string strTime = DateTime.Now.ToString();
            string strOrderNo = "MR" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            //人员档案
            string hasEmployeeName = "select EmployeeName from officedba.EmployeeInfo where CompanyCD='" + model.CompanyCD + "' and EmployeeName in ('" + model.EmployeeName + "')";
            if (!IsHave(hasEmployeeName))
            {
                string strEmployeeInfo = "insert into officedba.EmployeeInfo(EmployeeNo,PYShort,CompanyCD,EmployeeName,Sex,QuarterID,TotalSeniority,Flag,DeptID,AdminLevelID) values('" + strOrderNo + "','" + model.HidEmployeeName + "','" + model.CompanyCD + "','" + model.EmployeeName + "','" + "1','0','0.0','1','0','0')";
                //ilist.Add(strEmployeeInfo);
                SqlHelper.ExecuteSql(strEmployeeInfo);
            }

            //新建用户信息
            string strEmployeeIDSQL = "select ID from officedba.EmployeeInfo where CompanyCD='" + model.CompanyCD + "' and Flag='1' and EmployeeName = '管理员'";
            DataTable DTEmployeeID = SqlHelper.ExecuteSql(strEmployeeIDSQL);
            string strEmployeeIDD = DTEmployeeID.Rows[0][0].ToString();
            string strCloseDate = DateTime.Now.AddDays(90).ToString();
            string hasUserID = "select UserID from officedba.UserInfo where  UserID in ('" + model.User + "')";
            if (!IsHave(hasUserID))
            {
                string strUserInfo = "insert into officedba.UserInfo(CompanyCD,UserID,password,EmployeeID,UsedStatus,LockFlag,OpenDate,CloseDate,ModifiedDate,ModifiedUserID,IsRoot,IsHardValidate) values('" + model.CompanyCD + "','"+model.User+"','"+model.PassWord+"','" + strEmployeeIDD + "','1','0','" + strTime + "','" + strCloseDate + "','" + strTime + "','" + model.UserID + "','0','0')";
                //ilist.Add(strUserInfo);
                SqlHelper.ExecuteSql(strUserInfo);
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 初始化添加基础数据
        /// <summary>
        /// 初始化添加基础数据
        /// </summary>

        public static bool AddInitSystemData(InitSystemDataModel model)
        {
            string strOrderNo = "MR" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            string strEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//登录编号
            string strTime = DateTime.Now.ToString();//当前时间
            #region 初始化基础数据
            IList<string> ilist = new List<string>();
            //组织机构
            string hasDeptName = "select DeptName from officedba.DeptInfo where CompanyCD='" + model.CompanyCD + "' and DeptName in ('" + model.DeptName + "')";
            if (!IsHave(hasDeptName))
            {
                string strDetpInfo = "insert into officedba.DeptInfo(CompanyCD,DeptNo,PYshort,DeptName,UsedStatus,saleflag,subflag) values('" + model.CompanyCD + "','" + strOrderNo + "','" + model.HidDeptName + "','" + model.DeptName + "','" + "1','0','0')" ;
                ilist.Add(strDetpInfo);
            }

            ////人员档案
            //string hasEmployeeName = "select EmployeeName from officedba.EmployeeInfo where CompanyCD='" + model.CompanyCD + "' and EmployeeName in ('" + model.EmployeeName + "')";
            //if (!IsHave(hasEmployeeName))
            //{
            //    string strEmployeeInfo = "insert into officedba.EmployeeInfo(EmployeeNo,PYShort,CompanyCD,EmployeeName,Sex,QuarterID,TotalSeniority,Flag,DeptID,AdminLevelID) values('" + strOrderNo + "','" + model.HidEmployeeName + "','" + model.CompanyCD + "','" + model.EmployeeName + "','" + "1','0','0.0','1','0','0')";
            //    //ilist.Add(strEmployeeInfo);
            //    SqlHelper.ExecuteSql(strEmployeeInfo);
            //}

            //默认角色（管理员）
            var strIDSQL = "select RoleID from officedba.RoleInfo where CompanyCD='" + model.CompanyCD + "' and IsRoot='1'";
            DataTable DTID = SqlHelper.ExecuteSql(strIDSQL);
            var strID = DTID.Rows[0][0].ToString();
            
            string hasRoleName = "select RoleName from officedba.RoleInfo where CompanyCD='" + model.CompanyCD + "' and RoleName in ('管理员') and IsRoot='0'";
            if (!IsHave(hasRoleName))
            {
                string strRoleInfo = "insert into officedba.RoleInfo(CompanyCD,RoleName,IsRoot,ModifiedDate,ModifiedUserID,SuperRoleID) values('" + model.CompanyCD + "','管理员','0','" + strTime + "','" + model.UserID + "','"+' ' + strID + "')";
                //ilist.Add(strRoleInfo);
                SqlHelper.ExecuteSql(strRoleInfo);
            }





            //默认角色关联（管理员与管理员）

            string strRowIDSQL = "select RoleID from officedba.RoleInfo where CompanyCD='" + model.CompanyCD + "' and RoleName = '管理员'";
            DataTable DTRowID = SqlHelper.ExecuteSql(strRowIDSQL);
            string strRowIDD = DTRowID.Rows[0][0].ToString();
            string hUserID = "select UserID from officedba.UserRole where CompanyCD='" + model.CompanyCD + "' and UserID in ('" + model.User + "')";
            if (!IsHave(hUserID))
            {
                string strUserRole = "insert into officedba.UserRole(CompanyCD,UserID,RoleID,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','"+model.User+"','" + strRowIDD + "','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strUserRole);
            }

            //管理员功能全部
            //DataTable DTidTable  = SqlHelper.ExecuteSql("SELECT  ModuleID, FunctionID FROM pubdba.ModuleFunction UNION SELECT ModuleID, NULL AS FunctionID FROM  pubdba.CompanyModule ");
            //string strRoleFunction = "insert into officedba.RoleFunction(CompanyCD,RoleID ,ModifiedDate,ModuleID,functionID,) select " + model.CompanyCD + "," + strRowIDD + ","+strTime+",ModuleID,functionID from " + DTidTable;
            //ilist.Add(strRoleFunction);
            string strRoleFunction = "insert into officedba.RoleFunction (CompanyCD,RoleID,ModifiedDate,ModuleID,functionID)" +
            " select '" + model.CompanyCD + "'," + strRowIDD + ",'"+strTime+"',ModuleID,functionID from (SELECT ModuleID, FunctionID FROM  pubdba.ModuleFunction WHERE ModuleID IN (SELECT  ModuleID" +
            " FROM pubdba.CompanyModule WHERE CompanyCD='"+model.CompanyCD+"') UNION SELECT  ModuleID, NULL AS FunctionID FROM  pubdba.CompanyModule WHERE CompanyCD='"+model.CompanyCD+"')AS B";
            ilist.Add(strRoleFunction);


            //默认仓库
            string hasStorageName = "select StorageName from officedba.StorageInfo where CompanyCD='" + model.CompanyCD + "' and StorageName in ('"+model.StorageName+"') and UsedStatus='1'";
            if (!IsHave(hasStorageName))
            {
                string strStorageInfo = "insert into officedba.StorageInfo(CompanyCD,StorageNo,StorageName,StorageType,UsedStatus,StorageAdmin) values('" + model.CompanyCD + "','" + strOrderNo + "','" + model.StorageName + "','0','1','" + strEmployeeID + "')";
                ilist.Add(strStorageInfo);
            }

            // 默认物品分类
            string hasCodeNameA = "select CodeName from officedba.CodeProductType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认成品分类') and TypeFlag='1'";
            string hasCodeNameB = "select CodeName from officedba.CodeProductType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认原材料分类') and TypeFlag='2'";
            string hasCodeNameC = "select CodeName from officedba.CodeProductType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认半成品分类') and TypeFlag='7'";
            if (!IsHave(hasCodeNameA))
            {
                string strTypeA = "insert into officedba.CodeProductType(CompanyCD,TypeFlag,CodeName,SupperID,path,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','1','默认成品分类','0','0','1','" + strTime + "','"+model.UserID+"')";
                ilist.Add(strTypeA);
            }
            if (!IsHave(hasCodeNameB))
            {
                string strTypeB = "insert into officedba.CodeProductType(CompanyCD,TypeFlag,CodeName,SupperID,path,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','2','默认原材料分类','0','0','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strTypeB);
            }
            if (!IsHave(hasCodeNameC))
            {
                string strTypeC = "insert into officedba.CodeProductType(CompanyCD,TypeFlag,CodeName,SupperID,path,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','7','默认半成品分类','0','0','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strTypeC);
            }

            //默认计量单位（个、台、件、只）
            string hasUnitNameA = "select CodeName from officedba.CodeUnitType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('个') ";
            string hasUnitNameB = "select CodeName from officedba.CodeUnitType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('台') ";
            string hasUnitNameC = "select CodeName from officedba.CodeUnitType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('件') ";
            string hasUnitNameD = "select CodeName from officedba.CodeUnitType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('只') ";
            if (!IsHave(hasUnitNameA))
            {
                string strUnitTypeA = "insert into officedba.CodeUnitType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','个','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strUnitTypeA);
            }
            if (!IsHave(hasUnitNameB))
            {
                string strUnitTypeB = "insert into officedba.CodeUnitType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','台','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strUnitTypeB);
            }
            if (!IsHave(hasUnitNameC))
            {
                string strUnitTypeC = "insert into officedba.CodeUnitType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','件','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strUnitTypeC);
            }
            if (!IsHave(hasUnitNameD))
            {
                string strUnitTypeD = "insert into officedba.CodeUnitType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','只','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strUnitTypeD);
            }

            //建立默认客户（常州市莱特网络技术有限公司）
            string hasCustName = "select CustName from officedba.CustInfo where CompanyCD='" + model.CompanyCD + "' and CustName in ('" + model.CustName + "')";
            if (!IsHave(hasCustName))
            {
                string strCustInfo = "insert into officedba.CustInfo(CompanyCD,BigType,CustType,CustClass,CustNo,CustName,CustShort,CreditGrade,Manager,AreaID,CountryID,LinkCycle,HotIs,HotHow,MeritGrade,RelaGrade,CompanyType,StaffCount,SetupMoney,CapitalScale,SaleroomY,";
                strCustInfo += "Profity,IsTax,TakeType,CarryType,BusiType,BillType,PayType,CurrencyType,CreditManage,MaxCredit,UsedStatus,Creator,CreatedDate,ModifiedDate,ModifiedUserID,MoneyType,CustTypeManage,CustTypeSell,CustTypeTime,MaxCreditDate,CanViewUser,CustBig) ";
                strCustInfo += "values('" + model.CompanyCD + "','1','0','0','" + strOrderNo + "','" + model.CustName + "','" + model.HidCustName + "','0','" + strEmployeeID + "','0','0','0','0','0','0','0','0','0','0.00','0.00','0.00','0.00','0','0','0','0','0','0','0','1','0.00','1','" + strEmployeeID + "','" + strTime + "','" + strTime + "','" + model.UserID + "','0','0','0','0','0',',,','1')";
                //ilist.Add(strCustInfo);
                SqlHelper.ExecuteSql(strCustInfo);

                //建立默认联系人（莱特客服，8657660）
                string strCustIDSQL = "select ID from officedba.CustInfo where CompanyCD='" + model.CompanyCD + "' order by ID desc";
                DataTable DTCustID = SqlHelper.ExecuteSql(strCustIDSQL);
                string strCustIDD = DTCustID.Rows[0][0].ToString();
                string strCustLinkMan = "insert into officedba.CustLinkMan(CustNo,CompanyCD,LinkManName,Sex,Important,WorkTel,LinkType,ModifiedDate,ModifiedUserID,CanViewUser,Creator,CreatedDate) values('" + strOrderNo + "','"+model.CompanyCD+"','"+model.LinkManName+"','1','0','" + model.WorkTel + "','0','"+strTime+"','"+model.UserID+"',',,','"+strEmployeeID+"','"+strTime+"')";
                ilist.Add(strCustLinkMan);

            }

            //建立默认供应商类别（默认类别） 前台可以修改
            string hasTypeName = "select TypeName from officedba.CodePublicType where CompanyCD='" + model.CompanyCD + "' and TypeName in ('" + model.TypeName + "')  and TypeFlag='7' and TypeCode='1'";
            if (!IsHave(hasTypeName))
            {
                string strCodePublicType = "insert into officedba.CodePublicType(CompanyCD,TypeFlag,TypeCode,TypeName,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','7','1','" + model.TypeName + "','" + "1','"+strTime+"','"+model.UserID+"')";
               // ilist.Add(strCodePublicType);
                SqlHelper.ExecuteSql(strCodePublicType);
            }
            //建立默认供应商联络期限7天
            string hasTypeNameB = "select TypeName from officedba.CodePublicType where CompanyCD='" + model.CompanyCD + "' and TypeName in ('5') and TypeCode in ('3')";
            if (!IsHave(hasTypeNameB))
            {
                string strCodePublicTypee = "insert into officedba.CodePublicType(CompanyCD,TypeFlag,TypeCode,TypeName,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','7','3','5','" + "1','" + strTime + "','" + model.UserID + "')";
                //ilist.Add(strCodePublicType);
                SqlHelper.ExecuteSql(strCodePublicTypee);
            }

            //建立默认供应商（常州市莱特网络技术有限公司） 前台可以修改

            //获取供应商类型
            
            string strTypeAA = "select  ID from officedba.CodePublicType where CompanyCD='" + model.CompanyCD + "' and TypeFlag = '7' and TypeCode='1'";
            DataTable DTTypeA = SqlHelper.ExecuteSql(strTypeAA);
            string TypeNameA = DTTypeA.Rows[0][0].ToString();

            //获得联络期限
            string strTypeBB = "select  ID from officedba.CodePublicType where CompanyCD='" + model.CompanyCD + "' and TypeFlag = '7' and TypeCode='3'";
            DataTable DTTypeB = SqlHelper.ExecuteSql(strTypeBB);
            string TypeNameB = DTTypeB.Rows[0][0].ToString();

            string hasProName = "select CustName from officedba.ProviderInfo where CompanyCD='" + model.CompanyCD + "' and CustName in ('" + model.ProName + "')";
            if (!IsHave(hasProName))
            {
                string strProviderInfo = "insert into officedba.ProviderInfo(CompanyCD,BigType,CustType,CustClass,CustNo,CustName,CreditGrade,Manager,AreaID,LinkCycle,HotIs,HotHow,MeritGrade,CompanyType,StaffCount,SetupMoney,CapitalScale,SaleroomY,ProfitY,isTax,CountryID,TakeType,CarryType,PayType,CurrencyType,UsedStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,AllowDefaultDays) ";
                strProviderInfo += "values('" + model.CompanyCD + "','2','" + TypeNameA + "','0','" + strOrderNo + "','" + model.ProName + "','0','0','0','" + TypeNameB + "','2','0','0','0','0','0.00','0.00','0.00','0.00','2','0','0','0','0','0','1','"+strEmployeeID+"','"+strTime+"','"+strTime+"','"+model.UserID+"','0')";
                ilist.Add(strProviderInfo);
            }
            //默认货币（人民币、美元、日元、欧元、英镑）
            string hasCurTypeA = "select CurrencyName from officedba.CurrencyTypeSetting where CompanyCD='" + model.CompanyCD + "' and CurrencyName in ('人民币') ";
            if (!IsHave(hasCurTypeA))
            {
                string strCurTypeA = "insert into officedba.CurrencyTypeSetting(CompanyCD,CurrencyName,CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate) values('" + model.CompanyCD + "','人民币','RMB','1','1.00','1','" + strTime + "','1','1.00')";
                ilist.Add(strCurTypeA);
            }
            else
            {
                string strCurTypeA = "update officedba.CurrencyTypeSetting set CompanyCD='" + model.CompanyCD + "',CurrencyName='人民币',CurrencySymbol='RMB',isMaster='1',ExchangeRate='1.00',ConvertWay='1',ChangeTime='" + strTime + "',UsedStatus='1',EndRate='1.00' where CompanyCD ='" + model.CompanyCD + "' and CurrencyName='人民币'";
                ilist.Add(strCurTypeA);
            }
            string hasCurTypeB = "select CurrencyName from officedba.CurrencyTypeSetting where CompanyCD='" + model.CompanyCD + "' and CurrencyName in ('美元') ";
            if (!IsHave(hasCurTypeB))
            {
                string strCurTypeB = "insert into officedba.CurrencyTypeSetting(CompanyCD,CurrencyName,CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate) values('" + model.CompanyCD + "','美元','USD','0','6.64','1','" + strTime + "','1','6.64')";
                ilist.Add(strCurTypeB);
            }
            else
            {
                string strCurTypeB = "update officedba.CurrencyTypeSetting set CompanyCD='" + model.CompanyCD + "',CurrencyName='美元',CurrencySymbol='USD',isMaster='0',ExchangeRate='6.64',ConvertWay='1',ChangeTime='" + strTime + "',UsedStatus='1',EndRate='6.64' where CompanyCD ='" + model.CompanyCD + "' and CurrencyName='美元'";
                ilist.Add(strCurTypeB);
            }
            string hasCurTypeC = "select CurrencyName from officedba.CurrencyTypeSetting where CompanyCD='" + model.CompanyCD + "' and CurrencyName in ('日元') ";
            if (!IsHave(hasCurTypeC))
            {
                string strCurTypeC = "insert into officedba.CurrencyTypeSetting(CompanyCD,CurrencyName,CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate) values('" + model.CompanyCD + "','日元','JPY','0','0.08','1','" + strTime + "','1','0.08')";
                ilist.Add(strCurTypeC);
            }
            string hasCurTypeD = "select CurrencyName from officedba.CurrencyTypeSetting where CompanyCD='" + model.CompanyCD + "' and CurrencyName in ('欧元') ";
            if (!IsHave(hasCurTypeD))
            {
                string strCurTypeD = "insert into officedba.CurrencyTypeSetting(CompanyCD,CurrencyName,CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate) values('" + model.CompanyCD + "','欧元','EUR','0','8.88','1','" + strTime + "','1','8.88')";
                ilist.Add(strCurTypeD);
            }
            string hasCurTypeE = "select CurrencyName from officedba.CurrencyTypeSetting where CompanyCD='" + model.CompanyCD + "' and CurrencyName in ('英镑') ";
            if (!IsHave(hasCurTypeE))
            {
                string strCurTypeE = "insert into officedba.CurrencyTypeSetting(CompanyCD,CurrencyName,CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate) values('" + model.CompanyCD + "','英镑','GBP','0','10.49','1','" + strTime + "','1','10.49')";
                ilist.Add(strCurTypeE);
            }
            //设备类别（默认）
            string hasCodeEqu = "select CodeName from officedba.CodeEquipmentType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认') ";
            if (!IsHave(hasCodeEqu))
            {
                string strCodeEqu = "insert into officedba.CodeEquipmentType(CompanyCD,TypeFlag,CodeName,SupperID,WarningLimit,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','1','默认','0','0','1','" + strTime + "','" + model.UserID + "')";
                //ilist.Add(strCodeEqu);
                SqlHelper.ExecuteSql(strCodeEqu);
            }
            //获得设备类别
            string strEquipmentTA = "select top 1  ID from officedba.CodeEquipmentType where CompanyCD='" + model.CompanyCD + "' and CodeName = '默认' ";
            DataTable DTEquipmentTA = SqlHelper.ExecuteSql(strEquipmentTA);
            string EquipmentTypeN = DTEquipmentTA.Rows[0][0].ToString();

            //添加默认设备
            string hasEquipName = "select EquipmentName from officedba.EquipmentInfo where CompanyCD='" + model.CompanyCD + "' and EquipmentName in ('默认设备')";
            if (!IsHave(hasEquipName))
            {
                string strEquipmentInfo = "insert into officedba.EquipmentInfo(EquipmentNo,CompanyCD,EquipmentName,BuyDate,Type,ExaminePeriod,CurrentUser,CurrentDepartment,FittingFlag,Unit,Money,Status,Creator,CreateDate,ModifiedDate,ModifiedUserID,EquipmentCount) ";
                strEquipmentInfo += "values('" + strOrderNo + "','" + model.CompanyCD + "','默认设备','" + strTime + "','" + EquipmentTypeN + "','1','0','0','0','949','100.00','0','" + strEmployeeID + "','" + strTime + "','" + strTime + "','" + model.UserID + "','1')";
                ilist.Add(strEquipmentInfo);
            }
            //竞争对手（默认）
            string hasCompanyTyp = "select CodeName from officedba.CodeCompanyType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认') ";
            if (!IsHave(hasCompanyTyp))
            {
                string strCompanyTyp = "insert into officedba.CodeCompanyType(CompanyCD,BigType,CodeName,SupperID,path,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','3','默认','0','0','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCompanyTyp);
            }

            //日常调整原因（默认）
            string hasReasonName = "select CodeName from officedba.CodeReasonType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认') and Flag='3' ";
            if (!IsHave(hasReasonName))
            {
                string strCodeReasonType = "insert into officedba.CodeReasonType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','默认','3','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCodeReasonType);
            }
            //采购退货原因（默认）
            string hasReasonNameB = "select CodeName from officedba.CodeReasonType where CompanyCD='" + model.CompanyCD + "' and CodeName in ('默认') and Flag='21' ";
            if (!IsHave(hasReasonNameB))
            {
                string strCodeReasonTypeB = "insert into officedba.CodeReasonType(CompanyCD,CodeName,Flag,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','默认','21','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCodeReasonTypeB);
            }

            //建立默认采购类别（默认类别）
            string hasPurcName = "select TypeName from officedba.CodePublicType where CompanyCD='" + model.CompanyCD + "' and TypeName in ('默认类别') and TypeFlag='7' and TypeCode='5' ";
            if (!IsHave(hasPurcName))
            {
                string strCodePurcType = "insert into officedba.CodePublicType(CompanyCD,TypeFlag,TypeCode,TypeName,UsedStatus,ModifiedDate,ModifiedUserID) values('" + model.CompanyCD + "','7','5','默认类别','" + "1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCodePurcType);
               // SqlHelper.ExecuteSql(strCodePurcType);
            }
            #endregion

            #region 自动编号设置
            #region 销售管理

            //销售报价单
            string hasXSBJ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('1')";
            if (!IsHave(hasXSBJ))
            {
                string strXSBJ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSBJ += "values('" + model.CompanyCD + "','5','1','自动编号','XSBJ','yyyyMMdd','4','0','XSBJ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSBJ);
            }

            //销售合同
            string hasXSHT = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('2')";
            if (!IsHave(hasXSHT))
            {
                string strXSHT = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSHT += "values('" + model.CompanyCD + "','5','2','自动编号','XSHT','yyyyMMdd','4','0','XSHT{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSHT);
            }
            //销售订单
            string hasXSDD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('3')";
            if (!IsHave(hasXSDD))
            {
                string strXSDD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSDD += "values('" + model.CompanyCD + "','5','3','自动编号','XSDD','yyyyMMdd','4','0','XSDD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSDD);
            }
            //销售发货单
            string hasXSFHD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('4')";
            if (!IsHave(hasXSFHD))
            {
                string strXSFHD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSFHD += "values('" + model.CompanyCD + "','5','4','自动编号','XSFHD','yyyyMMdd','4','0','XSFHD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSFHD);
            }
            //销售退货单
            string hasXSTHD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('5')";
            if (!IsHave(hasXSTHD))
            {
                string strXSTHD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSTHD += "values('" + model.CompanyCD + "','5','5','自动编号','XSTHD','yyyyMMdd','4','0','XSTHD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSTHD);
            }
            //委托代销结算单
            string hasWTDXJS = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('6')";
            if (!IsHave(hasWTDXJS))
            {
                string strWTDXJS = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strWTDXJS += "values('" + model.CompanyCD + "','5','6','自动编号','WTDXJS','yyyyMMdd','4','0','WTDXJS{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strWTDXJS);
            }
            //销售机会
            string hasXSJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('7')";
            if (!IsHave(hasXSJH))
            {
                string strXSJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXSJH += "values('" + model.CompanyCD + "','5','7','自动编号','XSJH','yyyyMMdd','4','0','XSJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXSJH);
            }
            //回款计划
            string hasHKJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('10')";
            if (!IsHave(hasHKJH))
            {
                string strHKJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strHKJH += "values('" + model.CompanyCD + "','5','10','自动编号','HKJH','yyyyMMdd','4','0','HKJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strHKJH);
            }
            //销售计划
            string hasXKJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('5') and ItemTypeID in ('8')";
            if (!IsHave(hasXKJH))
            {
                string strXKJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strXKJH += "values('" + model.CompanyCD + "','5','8','自动编号','XSJH','yyyyMMdd','4','0','XSJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strXKJH);
            }
            #endregion

            #region 采购管理

            //采购申请单
            string hasCGSQ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('1')";
            if (!IsHave(hasCGSQ))
            {
                string strCGSQ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGSQ += "values('" + model.CompanyCD + "','6','1','自动编号','CGSQ','yyyyMMdd','4','0','CGSQ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGSQ);
            }
            //采购计划
            string hasCGJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('2')";
            if (!IsHave(hasCGJH))
            {
                string strCGJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGJH += "values('" + model.CompanyCD + "','6','2','自动编号','CGJH','yyyyMMdd','4','0','CGJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGJH);
            }
            //采购询价单
            string hasCGXJ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('3')";
            if (!IsHave(hasCGXJ))
            {
                string strCGXJ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGXJ += "values('" + model.CompanyCD + "','6','3','自动编号','CGXJ','yyyyMMdd','4','0','CGXJ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGXJ);
            }
            //采购订单
            string hasCGDD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('4')";
            if (!IsHave(hasCGDD))
            {
                string strCGDD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGDD += "values('" + model.CompanyCD + "','6','4','自动编号','CGDD','yyyyMMdd','4','0','CGDD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGDD);
            }
            //采购到货通知单
            string hasCGDHTZ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('5')";
            if (!IsHave(hasCGDHTZ))
            {
                string strCGDHTZ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGDHTZ += "values('" + model.CompanyCD + "','6','5','自动编号','CGDHTZ','yyyyMMdd','4','0','CGDHTZ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGDHTZ);
            }
            //采购退货单
            string hasCGTH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('6')";
            if (!IsHave(hasCGTH))
            {
                string strCGTH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGTH += "values('" + model.CompanyCD + "','6','6','自动编号','CGTH','yyyyMMdd','4','0','CGTH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGTH);
            }
            //供应商联络单
            string hasGYSLL = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('7')";
            if (!IsHave(hasGYSLL))
            {
                string strGYSLL = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strGYSLL += "values('" + model.CompanyCD + "','6','7','自动编号','GYSLL','yyyyMMdd','4','0','GYSLL{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strGYSLL);
            }
            //采购合同
            string hasCGHT = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('8')";
            if (!IsHave(hasCGHT))
            {
                string strCGHT = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCGHT += "values('" + model.CompanyCD + "','6','8','自动编号','CGHT','yyyyMMdd','4','0','CGHT{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCGHT);
            }
            //供应商档案
            string hasGYSDA = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('6') and ItemTypeID in ('9')";
            if (!IsHave(hasGYSDA))
            {
                string strGYSDA = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strGYSDA += "values('" + model.CompanyCD + "','6','9','自动编号','GYSDA','yyyyMMdd','4','0','GYSDA{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strGYSDA);
            }
            #endregion

            #region 库存管理
            //入库单
            string hasRK = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('1')";
            if (!IsHave(hasRK))
            {
                string strRK = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strRK += "values('" + model.CompanyCD + "','8','1','自动编号','RK','yyyyMMdd','4','0','RK{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strRK);
            }
            //出库单
            string hasCK = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('2')";
            if (!IsHave(hasCK))
            {
                string strCK = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCK += "values('" + model.CompanyCD + "','8','2','自动编号','CK','yyyyMMdd','4','0','CK{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCK);
            }
            //借货申请单
            string hasJHSQ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('3')";
            if (!IsHave(hasJHSQ))
            {
                string strJHSQ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strJHSQ += "values('" + model.CompanyCD + "','8','3','自动编号','JHSQ','yyyyMMdd','4','0','JHSQ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strJHSQ);
            }
            //调拨单
            string hasTB = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('4')";
            if (!IsHave(hasTB))
            {
                string strTB = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strTB += "values('" + model.CompanyCD + "','8','4','自动编号','TB','yyyyMMdd','4','0','TB{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strTB);
            }
            //报损单
            string hasBS = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('5')";
            if (!IsHave(hasBS))
            {
                string strBS = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strBS += "values('" + model.CompanyCD + "','8','5','自动编号','BS','yyyyMMdd','4','0','BS{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strBS);
            }
            //盘点单
            string hasPD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('6')";
            if (!IsHave(hasPD))
            {
                string strPD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strPD += "values('" + model.CompanyCD + "','8','6','自动编号','PD','yyyyMMdd','4','0','PD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strPD);
            }
            //库存调整单
            string hasKCTZ = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('7')";
            if (!IsHave(hasKCTZ))
            {
                string strKCTZ = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strKCTZ += "values('" + model.CompanyCD + "','8','7','自动编号','KCTZ','yyyyMMdd','4','0','KCTZ{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strKCTZ);
            }
            //借货返还单
            string hasJHFH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('8') and ItemTypeID in ('10')";
            if (!IsHave(hasJHFH))
            {
                string strJHFH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strJHFH += "values('" + model.CompanyCD + "','8','10','自动编号','JHFH','yyyyMMdd','4','0','JHFH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strJHFH);
            }
            #endregion

            #region 生产管理
            //主生产计划
            string hasZSCJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('1')";
            if (!IsHave(hasZSCJH))
            {
                string strZSCJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strZSCJH += "values('" + model.CompanyCD + "','7','1','自动编号','ZSCJH','yyyyMMdd','4','0','ZSCJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strZSCJH);
            }
            //物料需求计划
            string hasWLXQJH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('2')";
            if (!IsHave(hasWLXQJH))
            {
                string strWLXQJH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strWLXQJH += "values('" + model.CompanyCD + "','7','2','自动编号','WLXQJH','yyyyMMdd','4','0','WLXQJH{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strWLXQJH);
            }
            //生产任务单
            string hasSCRW = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('3')";
            if (!IsHave(hasSCRW))
            {
                string strSCRW = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strSCRW += "values('" + model.CompanyCD + "','7','3','自动编号','SCRW','yyyyMMdd','4','0','SCRW{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strSCRW);
            }
            //物料清单
            string hasWLQD = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('5')";
            if (!IsHave(hasWLQD))
            {
                string strWLQD = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strWLQD += "values('" + model.CompanyCD + "','7','5','自动编号','WLQD','yyyyMMdd','4','0','WLQD{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strWLQD);
            }
            //生产任务汇报单
            string hasSCRWHB = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('6')";
            if (!IsHave(hasSCRWHB))
            {
                string strSCRWHB = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strSCRWHB += "values('" + model.CompanyCD + "','7','6','自动编号','SCRWHB','yyyyMMdd','4','0','SCRWHB{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strSCRWHB);
            }
            //领料单
            string hasLL = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('7')";
            if (!IsHave(hasLL))
            {
                string strLL = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strLL += "values('" + model.CompanyCD + "','7','7','自动编号','LL','yyyyMMdd','4','0','LL{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strLL);
            }
            //退料单
            string hasTL = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('7') and ItemTypeID in ('8')";
            if (!IsHave(hasTL))
            {
                string strTL = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strTL += "values('" + model.CompanyCD + "','7','8','自动编号','TL','yyyyMMdd','4','0','TL{yyyyMMdd}{NNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strTL);
            }
            #endregion

            #region 基础编号
            //客户
            string hasKH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('1')";
            if (!IsHave(hasKH))
            {
                string strKH = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strKH += "values('" + model.CompanyCD + "','0','1','自动编号','KH','4','0','KH{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strKH);
            }
            //供应商
            string hasGYS = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('2')";
            if (!IsHave(hasGYS))
            {
                string strGYS = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strGYS += "values('" + model.CompanyCD + "','0','2','自动编号','GYS','4','0','GYS{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strGYS);
            }
            //竞争对手
            string hasJZDS = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('3')";
            if (!IsHave(hasJZDS))
            {
                string strJZDS = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strJZDS += "values('" + model.CompanyCD + "','0','3','自动编号','JZDS','4','0','JZDS{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strJZDS);
            }
            //部门
            string hasBM = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('4')";
            if (!IsHave(hasBM))
            {
                string strBM = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strBM += "values('" + model.CompanyCD + "','0','4','自动编号','BM','4','0','BM{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strBM);
            }
            //人员
            string hasRY = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('5')";
            if (!IsHave(hasRY))
            {
                string strRY = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strRY += "values('" + model.CompanyCD + "','0','5','自动编号','RY','4','0','RY{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strRY);
            }
            //物品
            string hasWP = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('6')";
            if (!IsHave(hasWP))
            {
                string strWP = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strWP += "values('" + model.CompanyCD + "','0','6','自动编号','WP','4','0','WP{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strWP);
            }
            //设备
            string hasSB = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('7')";
            if (!IsHave(hasSB))
            {
                string strSB = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strSB += "values('" + model.CompanyCD + "','0','7','自动编号','SB','4','0','SB{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strSB);
            }
            //办公用品
            string hasBGYP = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('8')";
            if (!IsHave(hasBGYP))
            {
                string strBGYP = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strBGYP += "values('" + model.CompanyCD + "','0','8','自动编号','BGYP','4','0','BGYP{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strBGYP);
            }
            //往来单位
            string hasWLDW = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('9')";
            if (!IsHave(hasWLDW))
            {
                string strWLDW = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strWLDW += "values('" + model.CompanyCD + "','0','9','自动编号','WLDW','4','0','WLDW{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strWLDW);
            }
            //流程
            string hasLC = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('10')";
            if (!IsHave(hasLC))
            {
                string strLC = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strLC += "values('" + model.CompanyCD + "','0','10','自动编号','LC','4','0','LC{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strLC);
            }
            //仓库
            string hasCKH = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('11')";
            if (!IsHave(hasCKH))
            {
                string strCK = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strCK += "values('" + model.CompanyCD + "','0','11','自动编号','CK','4','0','CK{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strCK);
            }
            //岗位
            string hasGW = "select RulePrefix from officedba.ItemCodingRule where CompanyCD='" + model.CompanyCD + "' and CodingType in ('0') and ItemTypeID in ('12')";
            if (!IsHave(hasGW))
            {
                string strGW = "insert into officedba.ItemCodingRule(CompanyCD,CodingType,ItemTypeID,RuleName,RulePrefix,RuleNoLen,LastNo,RuleExample,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) ";
                strGW += "values('" + model.CompanyCD + "','0','12','自动编号','GW','4','0','GW{NNNN}','1','1','" + strTime + "','" + model.UserID + "')";
                ilist.Add(strGW);
            }
            #endregion
            #endregion
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                foreach (string str in ilist)
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, str, null);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        break;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            tran.Commit();
            string remark = ConstUtil.LOG_PROCESS_SUCCESS;;
            LogInfoModel logModel = InitLogInfo("系统初始化数据");
            logModel.Element = "系统初始化数据";
            //设置操作成功标识
            logModel.Remark = remark;
            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return true;

        }
        #endregion

        #region 判断某条SQL语句有无查询出数据的方法
        /// <summary>
        /// 判断某条SQL语句有无查询出数据的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool IsHave(string sql)
        {
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string wcno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = "21918";

            //设置操作日志类型 修改
            logModel.ObjectName = "系统初始化数据";
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            return logModel;
        }
        #endregion


    }
}
