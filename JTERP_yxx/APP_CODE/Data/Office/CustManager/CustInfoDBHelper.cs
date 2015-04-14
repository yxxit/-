/**********************************************
 * 类作用：   客户信息数据层处理
 * 建立人：   张玉圆
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Configuration;


namespace XBase.Data.Office.CustManager
{
    public class CustInfoDBHelper
    {
        #region 办公模式
        #region 获取扩展属性
        public static DataTable GetExtAttrValue(string strKey, string CustNo, string CompanyCD)
        {
            //DataTable dt = new DataTable();
            string strSql = "select " + strKey + " from officedba.CustInfo where CompanyCD = @CompanyCD AND CustNo = @CustNo ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            arr.Add(new SqlParameter("@CustNo", CustNo));
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }
        /// <summary>
        /// 打印需要
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="CustNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetExtAttrValueReport(string strKey, string CustNo, string CompanyCD)
        {
            //DataTable dt = new DataTable();
            string strSql = "select " + strKey + "  from officedba.CustInfo where CompanyCD = @CompanyCD AND CustNo = @CustNo ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            arr.Add(new SqlParameter("@CustNo", CustNo));
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        //private static void GetExtAttrCmd(CustInfoModel model, Hashtable htExtAttr, SqlCommand cmd)
        //{
        //    try
        //    {
        //        string strSql = string.Empty;

        //        strSql = "UPDATE officedba.CustInfo set ";
        //        foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
        //        {
        //            strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
        //            cmd.Parameters.Add("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
        //        }
        //        int iLength = strSql.Length - 1;
        //        strSql = strSql.Substring(0, iLength);
        //        strSql += " where CompanyCD = @CompanyCD  AND CustNo = @CustNo";
        //        cmd.Parameters.Add("@CompanyCD", model.CompanyCD);
        //        cmd.Parameters.Add("@CustNo", model.CustNo);
        //        cmd.CommandText = strSql;
        //    }
        //    catch (Exception d)
        //    { }
        //}
        #endregion 

        #region 根据客户ID获取客户详细信息的方法
        /// <summary>
        /// 根据客户ID获取客户详细信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="custid">客户ID</param>
        /// <returns></returns>
        public static DataTable GetCustInfoByID(string CompanyCD, int custid,string CustBig,string CustNo)
        {
            try
            {                
                string sql = "";
                if (CustBig == "2")
                {
                    #region 带联系人的客户信息
                    sql = "select ci.ID,ci.BigType,ci.CompanyCD,ci.CustNo,ci.CustName,ci.CustNam,ci.CustClass,cct.CodeName CustClassName,ci.CustType,ci.MaxCredit," +
                        " ci.CustTypeSell,ci.CustTypeTime ,ci.CustTypeManage ,ci.CreditGrade,ci.CustShort,ci.CustNote,ci.CountryID," +
                        " ci.AreaID,ci.ReceiveAddress,ci.BusiType,ci.Manager,em.EmployeeName ManagerName,ci.LinkCycle,ci.CreditManage," +
                          "CONVERT(varchar(100), ci.FirstBuyDate, 23) FirstBuyDate ," +
                                     "CONVERT(varchar(100), ci.StopDate, 23) StopDate ," +
                        " ci.MaxCreditDate,ci.PayType,ci.RelaGrade,ci.UsedStatus,ci.Creator,ei.EmployeeName CreatorName,ci.Mobile,ci.email," +
                        " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,ci.ModifiedUserID,CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate," +
                        " ci.CustBig,ci.CustNum,cl.Sex,cl.LinkType,cl.PaperNum,Convert(varchar(20),cl.Birthday,23) Birthday,cl.WorkTel,cl.Handset,cl.Fax," +
                        " cl.Position,cl.Age,cl.MailAddress,cl.Post,cl.HomeTown,cl.NationalID,cl.CultureLevel,cl.Professional,cl.IncomeYear,cl.FuoodDrink," +
                        " cl.LoveMusic,cl.LoveColor,cl.LoveSmoke,cl.LoveDrink,cl.LoveTea,cl.LoveBook,cl.LoveSport,cl.LoveClothes,cl.Cosmetic,cl.Nature," +
                        " cl.Appearance,cl.AdoutBody,cl.AboutFamily,cl.Car,cl.CanViewUser,cl.CanViewUserName " +
                        " from officedba.CustInfo ci " +
                        " left join officedba.CustLinkMan cl on cl.CustNo = ci.CustNo and cl.CompanyCD = ci.CompanyCD " +
                        "                   and cl.id=(select min(id) from officedba.CustLinkMan where CustNo = ci.CustNo) " +
                        " left join officedba.EmployeeInfo ei on ei.id = ci.Creator" +
                        " left join officedba.EmployeeInfo em on em.id = ci.Manager" +
                        " left join officedba.CodeCompanyType cct on cct.id = ci.CustClass " +
                        " where ci.id=@id";
                    #endregion
                }
                else
                {
                    #region 拼写SQL语句
                    sql = "SELECT ci.ID," +
                                    "ci.CompanyCD," +
                                    "ci.CustTypeManage ," +
                                    "ci.CustTypeSell ," +
                                    "ci.CustTypeTime ," +
                                    "CONVERT(varchar(100), ci.FirstBuyDate, 23) FirstBuyDate ," +
                                     "CONVERT(varchar(100), ci.StopDate, 23) StopDate ," +
                                    "ci.BigType," +
                                    "ci.CustType," +
                                    "ci.CustClass,cct.CodeName CustClassName," +
                                    "ci.CustNo," +
                                    "ci.CustName," +
                                    "ci.CustNam," +
                                    "ci.CustShort," +
                                    "ci.CreditGrade," +
                                    "ci.CustNote," +
                                    "ci.Manager," +
                                    "em.EmployeeName ManagerName," +
                                    "ci.AreaID," +
                                    "ci.CountryID," +
                                    "ci.Province," +
                                    "ci.City," +
                                    "ci.Tel," +
                                    "ci.Fax," +
                                    "ci.OnLine," +
                                    "ci.WebSite," +
                                    "ci.Post," +
                                    "ci.LinkCycle," +
                                    "ci.HotIs," +
                                    "ci.HotHow," +
                                    "ci.MeritGrade," +
                                    "ci.RelaGrade," +
                                    "ci.Relation," +
                                    "ci.CompanyType," +
                                    "ci.StaffCount," +
                                    "ci.Source," +
                                    "ci.Phase," +
                                    "ci.CustSupe," +
                                    "ci.Trade," +
                                    "CONVERT(varchar(100), ci.SetupDate, 23) SetupDate," +
                                    "ci.ArtiPerson," +
                                    "ci.SetupMoney," +
                                    "ci.SetupAddress," +
                                    "ci.CapitalScale," +
                                    "ci.SaleroomY," +
                                    "ci.ProfitY," +
                                    "ci.TaxCD," +
                                    "ci.BusiNumber," +
                                    "ci.IsTax," +
                                    "ci.SellArea," +
                                    "ci.SellMode," +
                                    "ci.ReceiveAddress," +
                                    "ci.ContactName," +
                                    "ci.Mobile," +
                                    "ci.email," +
                                    "ci.TakeType," +
                                    "ci.CarryType," +
                                    "ci.BusiType," +
                                    "ci.BillType," +
                                    "ci.PayType," +
                                    "ci.MoneyType," +
                                    "ci.CurrencyType," +
                                    "ci.MaxCredit," +
                                    "ci.MaxCreditDate," +
                                    "ci.CreditManage," +
                                    "ci.OpenBank," +
                                    "ci.AccountMan," +
                                    "ci.AccountNum," +
                                    "ci.Remark," +
                                    "ci.UsedStatus," +
                                    "ci.CanViewUser," +
                                    "ci.CanViewUserName," +
                                    "ci.Creator," +
                                    "ci.CompanyValues, " +
                                    "ci.CatchWord," +
                                    "ci.ManageValues, " +
                                    "ci.Potential, " +
                                    "ci.Problem, " +
                                    "ci.Advantages, " +
                                    "ci.TradePosition ," +
                                    "ci.Competition, " +
                                    "ci.Collaborator, " +
                                    "ci.ManagePlan, " +
                                    "ci.Collaborate, " +
                                    "ci.Corptype," +
                                    "ci.BillUnit," +  //开票单位
                                    "CONVERT(varchar(100), ci.Usedata, 23) Usedata," +
                                    "CONVERT(varchar(100), ci.Certidata, 23) Certidata," +//新添加20120724
                                    "CONVERT(varchar(100), ci.Wardata, 23) Wardata," +
                                    "CONVERT(varchar(100), ci.Powerdata, 23) Powerdata," +
                                    "CONVERT(varchar(100), ci.Gmspdata, 23) Gmspdata," +
                                    "ci.Pagestatus," +
                                    "ci.Category, " +
                                    "ei.EmployeeName CreatorName," + 
                                    "CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate," +
                                    "CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate," +
                                    "ci.ModifiedUserID " +
                                " from " +
                                   " officedba.CustInfo ci" +
                                   " left join officedba.EmployeeInfo ei on ei.id = ci.Creator" +
                                   " left join officedba.EmployeeInfo em on em.id = ci.Manager" +
                                   " left join officedba.CodeCompanyType cct on cct.id = ci.CustClass " +
                                  " where " +
                                        "ci.id=@id ";//and ci.CompanyCD=@CompanyCD";
                    #endregion
                }

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", custid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 根据客户ID获取客户中关于联系人信息
        /// <summary>
        /// 根据客户ID获取客户中关于联系人信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="custid">客户ID</param>
        /// <returns></returns>
        public static DataTable GetCustLinkByID(string CompanyCD, string LinkName, string CustNo)
        {
            try
            {
                SqlParameter[] LinkParam = new SqlParameter[3];
                LinkParam[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                LinkParam[1] = SqlHelper.GetParameter("@LinkName", LinkName);
                LinkParam[2] = SqlHelper.GetParameter("@CustNo", CustNo);

                string LinkSql = "select id from officedba.CustLinkMan where LinkManName = @LinkName and CompanyCD = @CompanyCD and CustNo = @CustNo";
                DataTable LinkDt = SqlHelper.ExecuteSql(LinkSql, LinkParam);

                if (LinkDt.Rows.Count == 0)
                {
                    string sql = "select id,ContactName,isnull(Tel,'') Tel,isnull(Mobile,'') Mobile,isnull(Fax,'') Fax,isnull(Post,'') Post,isnull(email,'') email from officedba.CustInfo " +
                             "where ContactName = @LinkName and CompanyCD = @CompanyCD and CustNo =@CustNo";

                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                    param[1] = SqlHelper.GetParameter("@LinkName", LinkName);
                    param[2] = SqlHelper.GetParameter("@CustNo", CustNo);

                    return SqlHelper.ExecuteSql(sql, param);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 获取在职员工姓名和ID
        /// <summary>
        /// 获取在职员工姓名和ID
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>员工ID、姓名结果集合</returns>
        public static DataTable GetCustManager(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                   "Info.id,Info.EmployeeName " +
                               "from " +
                                   "officedba.Employeeinfo Info,officedba.Employeejob Job " +
                               "where " +
                                   "Job.EmployeesID = Info.id " +
                               "and " +
                                   "job.CompanyCD = Info.CompanyCD " +
                                "and job.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 获取币种的方法
        /// <summary>
        /// 获取币种的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>币种集合</returns>
        public static DataTable GetCustCurrencyType(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                    "CONVERT(varchar(50),id)+','+CONVERT(varchar(50),ExchangeRate) IDCurrencyName,id,CurrencyName,ExchangeRate " +
                              " from " +
                                    "officedba.CurrencyTypeSetting" +
                              " where " +
                                    "CompanyCD = @CompanyCD and UsedStatus = 1";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 获取客户分类的方法
        /// <summary>
        /// 获取客户分类的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>客户分类结果集</returns>
        public static DataTable GetCustClass(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                    "id,CodeName,SupperID" +
                              " from " +
                                    "officedba.CodeCompanyType" +
                              " where " +
                                    "BigType=1 and CompanyCD=@CompanyCD and UsedStatus=1";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 添加客户信息的方法
        /// <summary>
        /// 添加客户信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息</param>
        /// <returns>bool值</returns>
        public static bool CustInfoAdd(CustInfoModel CustInfoModel)//,LinkManModel LinkManM, Hashtable htExtAttr)
        {
            //----------------------------金泰恒业 20140331 刘锦旗------------------------//
            ArrayList lstCmd = new ArrayList();
            try
            {
                #region 拼写SQL语句
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.CustInfo");
                sql.AppendLine("(CompanyCD");
                //sql.AppendLine(",CustTypeManage");
                //sql.AppendLine(",CustTypeSell");
                //sql.AppendLine(",CustTypeTime");
                //sql.AppendLine(",CustType");
                //sql.AppendLine(",CustClass ");
                sql.AppendLine(",CustNo ");
                sql.AppendLine(",CustName ");
                sql.AppendLine(",CustNam ");
                sql.AppendLine(",CustShort ");
                //sql.AppendLine(",CreditGrade ");
                sql.AppendLine(",Manager ");
                sql.AppendLine(",AreaID ");
                sql.AppendLine(",CustNote ");
                //sql.AppendLine(",LinkCycle ");
                //sql.AppendLine(",HotIs ");
                //sql.AppendLine(",HotHow ");
                //sql.AppendLine(",MeritGrade ");
                //sql.AppendLine(",RelaGrade ");
                //sql.AppendLine(",Relation ");
                //sql.AppendLine(",CompanyType ");
                //sql.AppendLine(",StaffCount ");
                //sql.AppendLine(",Source ");
                //sql.AppendLine(",Phase ");
                //sql.AppendLine(",CustSupe ");
                //新添加20120720
                //sql.AppendLine(",Corptype ");//企业类型
                //sql.AppendLine(",Usedata");
                //sql.AppendLine(",Certidata ");
                //sql.AppendLine(",Wardata ");
                //sql.AppendLine(",Powerdata ");
                sql.AppendLine(",Pagestatus ");//页面状态
                //sql.AppendLine(",Gmspdata ");
                //sql.AppendLine(",Category ");//所属分类

                //sql.AppendLine(",Trade ");
                //sql.AppendLine(",SetupDate ");
                //sql.AppendLine(",ArtiPerson ");
                //sql.AppendLine(",SetupMoney ");
                sql.AppendLine(",SetupAddress ");
                //sql.AppendLine(",CapitalScale ");
                //sql.AppendLine(",SaleroomY ");
                //sql.AppendLine(",ProfitY ");
                sql.AppendLine(",TaxCD ");
                //sql.AppendLine(",BusiNumber ");
                //sql.AppendLine(",IsTax ");
                //sql.AppendLine(",SellMode ");
                //sql.AppendLine(",SellArea ");
                //sql.AppendLine(",CountryID ");
                //sql.AppendLine(",Province ");
                //sql.AppendLine(",City ");
                sql.AppendLine(",Tel ");
                sql.AppendLine(",ContactName ");
                sql.AppendLine(",Mobile ");
                sql.AppendLine(",ReceiveAddress ");
                sql.AppendLine(",MaxCredit ");
                sql.AppendLine(",UsedStatus ");

                sql.AppendLine(",CanViewUser ");
                sql.AppendLine(",CanViewUserName ");

                sql.AppendLine(",Creator ");
                sql.AppendLine(",CreatedDate ");
                //sql.AppendLine(",FirstBuyDate ");
                sql.AppendLine(",OpenBank ");
                sql.AppendLine(",AccountMan ");
                sql.AppendLine(",AccountNum ");
                sql.AppendLine(",ModifiedUserID ");
                sql.AppendLine(",Remark");
                sql.AppendLine(",BillUnit  ) ");
                sql.AppendLine(" values ");
                sql.AppendLine("(@CompanyCD");
                //sql.AppendLine(",@CustTypeManage");
                //sql.AppendLine(",@CustTypeSell");
                //sql.AppendLine(",@CustTypeTime");
                //sql.AppendLine(",@CustType");
                //sql.AppendLine(",@CustClass ");
                sql.AppendLine(",@CustNo ");
                sql.AppendLine(",@CustName ");
                sql.AppendLine(",@CustNam ");
                sql.AppendLine(",@CustShort ");
                //sql.AppendLine(",@CreditGrade ");
                sql.AppendLine(",@Manager ");
                sql.AppendLine(",@AreaID ");
                sql.AppendLine(",@CustNote ");
                //sql.AppendLine(",@LinkCycle ");
                //sql.AppendLine(",@HotIs ");
                //sql.AppendLine(",@HotHow ");
                //sql.AppendLine(",@MeritGrade ");
                //sql.AppendLine(",@RelaGrade ");
                //sql.AppendLine(",@Relation ");
                //sql.AppendLine(",@CompanyType ");
                //sql.AppendLine(",@StaffCount ");
                //sql.AppendLine(",@Source ");
                //sql.AppendLine(",@Phase ");
                ////sql.AppendLine(",@CustSupe ");
                ////新添加20120720 zhuang
                //sql.AppendLine(",@Corptype ");//企业类型
                //sql.AppendLine(",@Usedata");
                //sql.AppendLine(",@Certidata ");
                //sql.AppendLine(",@Wardata ");
                //sql.AppendLine(",@Powerdata ");
                sql.AppendLine(",@Pagestatus ");//页面状态
                //sql.AppendLine(",@Gmspdata ");
                //sql.AppendLine(",@Category ");//所属分类

                //sql.AppendLine(",@Trade ");
                //sql.AppendLine(",@SetupDate ");
                //sql.AppendLine(",@ArtiPerson ");
                //sql.AppendLine(",@SetupMoney ");
                sql.AppendLine(",@SetupAddress ");
                //sql.AppendLine(",@CapitalScale ");
                //sql.AppendLine(",@SaleroomY ");
                //sql.AppendLine(",@ProfitY ");
                sql.AppendLine(",@TaxCD ");
                //sql.AppendLine(",@BusiNumber ");
                //sql.AppendLine(",@IsTax ");
                //sql.AppendLine(",@SellMode ");
                //sql.AppendLine(",@SellArea ");
                //sql.AppendLine(",@CountryID ");
                //sql.AppendLine(",@Province ");
                //sql.AppendLine(",@City ");
                sql.AppendLine(",@Tel ");
                sql.AppendLine(",@ContactName ");
                sql.AppendLine(",@Mobile ");
                sql.AppendLine(",@ReceiveAddress ");
                //sql.AppendLine(",@WebSite ");
                //sql.AppendLine(",@Post ");
                //sql.AppendLine(",@email ");
                //sql.AppendLine(",@Fax ");
                //sql.AppendLine(",@OnLine ");
                //sql.AppendLine(",@TakeType ");
                //sql.AppendLine(",@CarryType ");
                //sql.AppendLine(",@BusiType ");
                //sql.AppendLine(",@BillType ");
                //sql.AppendLine(",@PayType ");
                //sql.AppendLine(",@MoneyType ");
                //sql.AppendLine(",@CurrencyType ");
                //sql.AppendLine(",@CreditManage ");
                sql.AppendLine(",@MaxCredit ");
                //sql.AppendLine(",@MaxCreditDate ");
                sql.AppendLine(",@UsedStatus ");

                sql.AppendLine(",@CanViewUser ");
                sql.AppendLine(",@CanViewUserName ");

                sql.AppendLine(",@Creator ");
                sql.AppendLine(",@CreatedDate ");
                //sql.AppendLine(",@FirstBuyDate ");
                sql.AppendLine(",@OpenBank ");
                sql.AppendLine(",@AccountMan ");
                sql.AppendLine(",@AccountNum ");
                sql.AppendLine(",@ModifiedUserID ");

                //sql.AppendLine(",@CompanyValues ");
                //sql.AppendLine(",@CatchWord ");
                //sql.AppendLine(",@ManageValues ");
                //sql.AppendLine(",@Potential ");
                //sql.AppendLine(",@Problem ");
                //sql.AppendLine(",@Advantages ");
                //sql.AppendLine(",@TradePosition ");
                //sql.AppendLine(",@Competition ");
                //sql.AppendLine(",@Collaborator ");
                //sql.AppendLine(",@ManagePlan ");
                //sql.AppendLine(",@Collaborate ");
                //sql.AppendLine(",@CustBig ");
                //sql.AppendLine(",@CustNum ");
                sql.AppendLine(",@Remark ");// ,@stopdate,@BillUnit ) ");
                sql.AppendLine(",@BillUnit ) ");
                #endregion

                #region 设置参数
                SqlCommand mycomm = new SqlCommand();
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustType", CustInfoModel.CustType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustClass", CustInfoModel.CustClass));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustName", CustInfoModel.CustName));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNam", CustInfoModel.CustNam));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustShort", CustInfoModel.CustShort));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CreditGrade", CustInfoModel.CreditGrade));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Manager", CustInfoModel.Seller));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AreaID", CustInfoModel.AreaID));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNote", CustInfoModel.CustNote));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@LinkCycle", CustInfoModel.LinkCycle));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@HotIs", CustInfoModel.HotIs));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@HotHow", CustInfoModel.HotHow));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MeritGrade", CustInfoModel.MeritGrade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@RelaGrade", CustInfoModel.RelaGrade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Relation", CustInfoModel.Relation));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyType", CustInfoModel.CompanyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@StaffCount", CustInfoModel.StaffCount));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Source", CustInfoModel.Source));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Phase", CustInfoModel.Phase));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustSupe", CustInfoModel.CustSupe));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Trade", CustInfoModel.Trade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupDate", CustInfoModel.SetupDate == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.SetupDate.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ArtiPerson", CustInfoModel.ArtiPerson));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupMoney", CustInfoModel.SetupMoney));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupAddress", CustInfoModel.SetupAddress));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CapitalScale", CustInfoModel.CapitalScale));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SaleroomY", CustInfoModel.SaleroomY));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ProfitY", CustInfoModel.ProfitY));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@TaxCD", CustInfoModel.TaxCD));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BusiNumber", CustInfoModel.BusiNumber));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@IsTax", CustInfoModel.IsTax));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SellMode", CustInfoModel.SellMode));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SellArea", CustInfoModel.SellArea));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CountryID", CustInfoModel.CountryID));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Province", CustInfoModel.Province));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@City", CustInfoModel.City));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Tel", CustInfoModel.Tel));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ContactName", CustInfoModel.ContactName));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Mobile", CustInfoModel.Mobile));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ReceiveAddress", CustInfoModel.ReceiveAddress));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@WebSite", CustInfoModel.WebSite));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Post", CustInfoModel.Post));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@email", CustInfoModel.email));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Fax", CustInfoModel.Fax));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@OnLine", CustInfoModel.OnLine));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@TakeType", CustInfoModel.TakeType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CarryType", CustInfoModel.CarryType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BusiType", CustInfoModel.BusiType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BillType", CustInfoModel.BillType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@PayType", CustInfoModel.PayType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", CustInfoModel.MoneyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CustInfoModel.CurrencyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CreditManage", CustInfoModel.CreditManage));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@MaxCredit", CustInfoModel.MaxCredit));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MaxCreditDate", CustInfoModel.MaxCreditDate));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", CustInfoModel.UsedStatus));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Creator", CustInfoModel.Creator));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CreatedDate", CustInfoModel.CreatedDate == null
                                                       ? SqlDateTime.Null
                                                       : SqlDateTime.Parse(CustInfoModel.CreatedDate.ToString())));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@OpenBank", CustInfoModel.OpenBank));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", CustInfoModel.AccountMan));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", CustInfoModel.AccountNum));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustBig", CustInfoModel.CustBig));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNum", CustInfoModel.CustNum));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Remark", CustInfoModel.Remark));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeManage", CustInfoModel.CustTypeManage));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeSell", CustInfoModel.CustTypeSell));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeTime", CustInfoModel.CustTypeTime));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@FirstBuyDate", CustInfoModel.FirstBuyDate == null
                //                                       ? SqlDateTime.Null
                //                                       : SqlDateTime.Parse(CustInfoModel.FirstBuyDate.ToString())));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", CustInfoModel.ModifiedUserID));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", CustInfoModel.CanViewUser));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", CustInfoModel.CanViewUserName));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyValues", CustInfoModel.CompanyValues));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CatchWord", CustInfoModel.CatchWord));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ManageValues", CustInfoModel.ManageValues));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Potential", CustInfoModel.Potential));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Problem", CustInfoModel.Problem));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Advantages", CustInfoModel.Advantages));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@TradePosition", CustInfoModel.TradePosition));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Competition", CustInfoModel.Competition));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Collaborator", CustInfoModel.Collaborator));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ManagePlan", CustInfoModel.ManagePlan));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Collaborate", CustInfoModel.Collaborate));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@stopdate", CustInfoModel.StopDate == null
                //                                     ? SqlDateTime.Null
                //                                     : SqlDateTime.Parse(CustInfoModel.StopDate.ToString())));
                ////新添加20120723
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Corptype", CustInfoModel.Corptype));//企业类型
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Pagestatus", CustInfoModel.Pagestatus));//页面状态
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Usedata", CustInfoModel.Usedata == null
                //                        ? SqlDateTime.Null
                //                       : SqlDateTime.Parse(CustInfoModel.Usedata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Certidata", CustInfoModel.Certidata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Certidata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Wardata", CustInfoModel.Wardata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Wardata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Powerdata", CustInfoModel.Powerdata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Powerdata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Gmspdata", CustInfoModel.Gmspdata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Gmspdata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Category", CustInfoModel.Category));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@BillUnit", CustInfoModel.BillUnit));  //开票单位
                mycomm.CommandText = sql.ToString();
                #endregion
                #region 附件
                try
                {

                    if (!String.IsNullOrEmpty(CustInfoModel.AnnFileName))
                    {
                        string[] annFileName = CustInfoModel.AnnFileName.Split(',');
                        //string[] annRemark = model.AnnRemark.Split(',');
                        string[] annRemark = CustInfoModel.AnnRemark.Split(',');
                        string[] annUpDateTime = CustInfoModel.UpDateTime.Split(',');
                        string[] annAddr = CustInfoModel.AnnAddr.Split(',');
                        if (annFileName.Length >= 1)
                        {
                            for (int i = 0; i < annFileName.Length - 1; i++)
                            {
                                System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                cmdsql.AppendLine("INSERT INTO officedba.Annex    ");
                                cmdsql.AppendLine("           (CompanyCD                    ");
                                cmdsql.AppendLine("           ,ParentId                    ");
                                cmdsql.AppendLine("           ,AnnAddr                   ");
                                cmdsql.AppendLine("           ,annFileName                        ");
                                cmdsql.AppendLine("           ,upDatetime                    ");
                                cmdsql.AppendLine("           ,annRemark,ModuleType)                  ");
                                cmdsql.AppendLine("     VALUES                              ");
                                cmdsql.AppendLine("           (@CompanyCD                   ");
                                cmdsql.AppendLine("           ,@ParentId                   ");
                                cmdsql.AppendLine("           ,@AnnAddr                  ");
                                cmdsql.AppendLine("           ,@AnnFileName                       ");
                                cmdsql.AppendLine("           ,@UpDateTime                   ");
                                cmdsql.AppendLine("           ,@AnnRemark,@ModuleType)                 ");

                                SqlCommand comms = new SqlCommand();
                                comms.CommandText = cmdsql.ToString();
                                comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ParentId", CustInfoModel.CustNo));
                                comms.Parameters.Add(SqlHelper.GetParameter("@AnnAddr", annAddr[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@AnnFileName", annFileName[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@UpDateTime", annUpDateTime[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@AnnRemark", annRemark[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ModuleType", ConstUtil.MODULE_ID_CUST_INFO_ADD));
                                lstCmd.Add(comms);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
                
                

                lstCmd.Add(mycomm);//基本信息
               
                //if (htExtAttr != null)//扩展信息
                //{
                //    SqlCommand cmd = new SqlCommand();
                //    GetExtAttrCmd(CustInfoModel, htExtAttr, cmd);
                //    lstCmd.Add(cmd);
                //}


                //if (CustInfoModel.CustBig == "2")//当选择会员客户时,插入信息到联系人表
                //{
                //    SqlCommand cmdLinkMan = new SqlCommand();
                //    StringBuilder sqlLinkMan = new StringBuilder();
                //    sqlLinkMan.AppendLine(" insert into officedba.CustLinkMan ");
                //    sqlLinkMan.AppendLine(" ( CompanyCD,CustNo,LinkManName,Sex,LinkType,PaperNum,Birthday,WorkTel,Handset,Fax,Position, ");
                //    sqlLinkMan.AppendLine("   Important, ");
                //    sqlLinkMan.AppendLine("   Age,Post,MailAddress,HomeTown,NationalID,CultureLevel,Professional,IncomeYear,FuoodDrink, ");
                //    sqlLinkMan.AppendLine("   LoveMusic,LoveColor,LoveSmoke,LoveDrink,LoveTea,LoveBook,LoveSport,LoveClothes,Cosmetic, ");
                //    sqlLinkMan.AppendLine("   Nature,Appearance,AdoutBody,AboutFamily,Car,CanViewUser,CanViewUserName,Creator,CreatedDate) values ");
                //    sqlLinkMan.AppendLine(" ( @CompanyCD,@CustNo,@LinkManName,@Sex,@LinkType,@PaperNum,@Birthday,@WorkTel,@Handset,@Fax,@Position, ");
                //    sqlLinkMan.AppendLine("    '0',");
                //    sqlLinkMan.AppendLine("   @Age,@Post,@MailAddress,@HomeTown,@NationalID,@CultureLevel,@Professional,@IncomeYear,@FuoodDrink, ");
                //    sqlLinkMan.AppendLine("   @LoveMusic,@LoveColor,@LoveSmoke,@LoveDrink,@LoveTea,@LoveBook,@LoveSport,@LoveClothes,@Cosmetic, ");
                //    sqlLinkMan.AppendLine("   @Nature,@Appearance,@AdoutBody,@AboutFamily,@Car,@CanViewUser,@CanViewUserName,@Creator,@CreatedDate ) ");

                //    #region 设置联系人参数
                //    SqlCommand commLinkMan = new SqlCommand();
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LinkManName", CustInfoModel.CustName));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Sex", LinkManM.Sex));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LinkType", LinkManM.LinkType));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@PaperNum", LinkManM.PaperNum));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Birthday", LinkManM.Birthday == null ? SqlDateTime.Null : SqlDateTime.Parse(LinkManM.Birthday.ToString())));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@WorkTel", LinkManM.WorkTel));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Handset", LinkManM.Handset));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Fax", LinkManM.Fax));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Position", LinkManM.Position));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Age", LinkManM.Age));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Post", LinkManM.Post));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@MailAddress", LinkManM.MailAddress));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@HomeTown", LinkManM.HomeTown));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@NationalID", LinkManM.NationalID));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CultureLevel", LinkManM.CultureLevel));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Professional", LinkManM.Professional));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@IncomeYear", LinkManM.IncomeYear));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@FuoodDrink", LinkManM.FuoodDrink));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveMusic", LinkManM.LoveMusic));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveColor", LinkManM.LoveColor));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveSmoke", LinkManM.LoveSmoke));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveDrink", LinkManM.LoveDrink));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveTea", LinkManM.LoveTea));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveBook", LinkManM.LoveBook));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveSport", LinkManM.LoveSport));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveClothes", LinkManM.LoveClothes));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Cosmetic", LinkManM.Cosmetic));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Nature", LinkManM.Nature));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Appearance", LinkManM.Appearance));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@AdoutBody", LinkManM.AdoutBody));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@AboutFamily", LinkManM.AboutFamily));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Car", LinkManM.Car));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", CustInfoModel.CanViewUser));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", CustInfoModel.CanViewUserName));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Creator", CustInfoModel.Creator));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CreatedDate", CustInfoModel.CreatedDate == null
                //                                       ? SqlDateTime.Null
                //                                       : SqlDateTime.Parse(CustInfoModel.CreatedDate.ToString())));
                //    commLinkMan.CommandText = sqlLinkMan.ToString();

                //    #endregion

                //    lstCmd.Add(commLinkMan);

                //}

                //执行登陆操作
                bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

                return isSucc;
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return false;
            }
        }
        #endregion

        #region 同时添加客户信息及联系人列表信息的方法
        /// <summary>
        /// 同时添加客户信息及联系人列表信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <param name="LinkManlist">联系人列表信息流</param>
        /// <returns>bool值</returns>
        public static bool AddCustAndLinkMan(CustInfoModel CustInfoModel, string LinkManlist)
        {
            try
            {
                #region 拼写添加客户信息SQL语句
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.CustInfo");
                sql.AppendLine("(CompanyCD");
                sql.AppendLine(",CustType");
                sql.AppendLine(",CustClass ");
                sql.AppendLine(",CustNo ");
                sql.AppendLine(",CustName ");
                sql.AppendLine(",CustNam ");
                sql.AppendLine(",CustShort ");
                sql.AppendLine(",CreditGrade ");
                sql.AppendLine(",Manager ");
                sql.AppendLine(",AreaID ");
                sql.AppendLine(",CustNote ");
                sql.AppendLine(",LinkCycle ");
                sql.AppendLine(",HotIs ");
                sql.AppendLine(",HotHow ");
                sql.AppendLine(",HotType ");
                sql.AppendLine(",MeritGrade ");
                sql.AppendLine(",RelaGrade ");
                sql.AppendLine(",Relation ");
                sql.AppendLine(",CompanyType ");
                sql.AppendLine(",StaffCount ");
                sql.AppendLine(",Source ");
                sql.AppendLine(",Phase ");
                //sql.AppendLine(",CustSupe ");
                sql.AppendLine(",Trade ");
                sql.AppendLine(",SetupDate ");
                sql.AppendLine(",ArtiPerson ");
                sql.AppendLine(",SetupMoney ");
                sql.AppendLine(",SetupAddress ");
                sql.AppendLine(",CapitalScale ");
                sql.AppendLine(",SaleroomY ");
                sql.AppendLine(",ProfitY ");
                sql.AppendLine(",TaxCD ");
                sql.AppendLine(",BusiNumber ");
                sql.AppendLine(",IsTax ");
                sql.AppendLine(",SellMode ");
                sql.AppendLine(",SellArea ");
                sql.AppendLine(",CountryID ");
                sql.AppendLine(",Province ");
                sql.AppendLine(",City ");
                sql.AppendLine(",Tel ");
                sql.AppendLine(",ContactName ");
                sql.AppendLine(",Mobile ");
                sql.AppendLine(",ReceiveAddress ");
                sql.AppendLine(",WebSite ");
                sql.AppendLine(",Post ");
                sql.AppendLine(",email ");
                sql.AppendLine(",Fax ");
                sql.AppendLine(",OnLine ");
                sql.AppendLine(",TakeType ");
                sql.AppendLine(",CarryType ");
                sql.AppendLine(",BusiType ");
                sql.AppendLine(",BillType ");
                sql.AppendLine(",PayType ");
                sql.AppendLine(",MoneyType ");
                sql.AppendLine(",CurrencyType ");
                sql.AppendLine(",CreditManage ");
                sql.AppendLine(",MaxCredit ");
                sql.AppendLine(",MaxCreditDate ");
                //sql.AppendLine(",UpdateCredit ");
                sql.AppendLine(",UsedStatus ");
                sql.AppendLine(",Creator ");
                sql.AppendLine(",CreatedDate ");
                sql.AppendLine(",OpenBank ");
                sql.AppendLine(",AccountMan ");
                sql.AppendLine(",AccountNum ");
                sql.AppendLine(",Remark) ");
                sql.AppendLine(" values ");
                //新添加20120723
                sql.AppendLine(",Corptype ");//企业类型
                sql.AppendLine(",Usedata");
                sql.AppendLine(",Certidata ");
                sql.AppendLine(",Wardata ");
                sql.AppendLine(",Powerdata ");
                sql.AppendLine(",Pagestatus ");//页面状态
                sql.AppendLine(",Gmspdata ");
                sql.AppendLine(",Category ");//所属分类

                sql.AppendLine("(@CompanyCD");
                sql.AppendLine(",@CustType");
                sql.AppendLine(",@CustClass ");
                sql.AppendLine(",@CustNo ");
                sql.AppendLine(",@CustName ");
                sql.AppendLine(",@CustNam ");
                sql.AppendLine(",@CustShort ");
                sql.AppendLine(",@CreditGrade ");
                sql.AppendLine(",@Manager ");
                sql.AppendLine(",@AreaID ");
                sql.AppendLine(",@CustNote ");
                sql.AppendLine(",@LinkCycle ");
                sql.AppendLine(",@HotIs ");
                sql.AppendLine(",@HotHow ");
                sql.AppendLine(",@HotType ");
                sql.AppendLine(",@MeritGrade ");
                sql.AppendLine(",@RelaGrade ");
                sql.AppendLine(",@Relation ");
                sql.AppendLine(",@CompanyType ");
                sql.AppendLine(",@StaffCount ");
                sql.AppendLine(",@Source ");
                sql.AppendLine(",@Phase ");
                //sql.AppendLine(",@CustSupe ");
                sql.AppendLine(",@Trade ");
                sql.AppendLine(",@SetupDate ");
                sql.AppendLine(",@ArtiPerson ");
                sql.AppendLine(",@SetupMoney ");
                sql.AppendLine(",@SetupAddress ");
                sql.AppendLine(",@CapitalScale ");
                sql.AppendLine(",@SaleroomY ");
                sql.AppendLine(",@ProfitY ");
                sql.AppendLine(",@TaxCD ");
                sql.AppendLine(",@BusiNumber ");
                sql.AppendLine(",@IsTax ");
                sql.AppendLine(",@SellMode ");
                sql.AppendLine(",@SellArea ");
                sql.AppendLine(",@CountryID ");
                sql.AppendLine(",@Province ");
                sql.AppendLine(",@City ");
                sql.AppendLine(",@Tel ");
                sql.AppendLine(",@ContactName ");
                sql.AppendLine(",@Mobile ");
                sql.AppendLine(",@ReceiveAddress ");
                sql.AppendLine(",@WebSite ");
                sql.AppendLine(",@Post ");
                sql.AppendLine(",@email ");
                sql.AppendLine(",@Fax ");
                sql.AppendLine(",@OnLine ");
                sql.AppendLine(",@TakeType ");
                sql.AppendLine(",@CarryType ");
                sql.AppendLine(",@BusiType ");
                sql.AppendLine(",@BillType ");
                sql.AppendLine(",@PayType ");
                sql.AppendLine(",@MoneyType ");
                sql.AppendLine(",@CurrencyType ");
                sql.AppendLine(",@CreditManage ");
                sql.AppendLine(",@MaxCredit ");
                sql.AppendLine(",@MaxCreditDate ");
                //sql.AppendLine(",@UpdateCredit ");
                sql.AppendLine(",@UsedStatus ");
                sql.AppendLine(",@Creator ");
                sql.AppendLine(",@CreatedDate ");
                sql.AppendLine(",@OpenBank ");
                sql.AppendLine(",@AccountMan ");
                sql.AppendLine(",@AccountNum ");
                sql.AppendLine(",@Remark) ");
                //新添加20120723
                sql.AppendLine(",@Corptype ");//企业类型 
                sql.AppendLine(",@Usedata");
                sql.AppendLine(",@Certidata ");
                sql.AppendLine(",@Wardata ");
                sql.AppendLine(",@Powerdata ");
                sql.AppendLine(",@Pagestatus ");//页面状态
                sql.AppendLine(",@Gmspdata ");
                sql.AppendLine(",@Category ");//所属分类 

                #endregion

                #region 设置添加客户信息参数
                SqlParameter[] param = new SqlParameter[73];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustType", CustInfoModel.CustType);
                param[2] = SqlHelper.GetParameter("@CustClass", CustInfoModel.CustClass);
                param[3] = SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo);
                param[4] = SqlHelper.GetParameter("@CustName", CustInfoModel.CustName);
                param[5] = SqlHelper.GetParameter("@CustNam", CustInfoModel.CustNam);
                param[6] = SqlHelper.GetParameter("@CustShort", CustInfoModel.CustShort);
                param[7] = SqlHelper.GetParameter("@CreditGrade", CustInfoModel.CreditGrade);
                param[8] = SqlHelper.GetParameter("@Manager", CustInfoModel.Seller);
                param[9] = SqlHelper.GetParameter("@AreaID", CustInfoModel.AreaID);
                param[10] = SqlHelper.GetParameter("@CustNote", CustInfoModel.CustNote);
                param[11] = SqlHelper.GetParameter("@LinkCycle", CustInfoModel.LinkCycle);
                param[12] = SqlHelper.GetParameter("@HotIs", CustInfoModel.HotIs);
                param[13] = SqlHelper.GetParameter("@HotHow", CustInfoModel.HotHow);
                param[14] = SqlHelper.GetParameter("@HotType", CustInfoModel.HotType);
                param[15] = SqlHelper.GetParameter("@MeritGrade", CustInfoModel.MeritGrade);
                param[16] = SqlHelper.GetParameter("@RelaGrade", CustInfoModel.RelaGrade);
                param[17] = SqlHelper.GetParameter("@Relation", CustInfoModel.Relation);
                param[18] = SqlHelper.GetParameter("@CompanyType", CustInfoModel.CompanyType);
                param[19] = SqlHelper.GetParameter("@StaffCount", CustInfoModel.StaffCount);
                param[20] = SqlHelper.GetParameter("@Source", CustInfoModel.Source);
                param[21] = SqlHelper.GetParameter("@Phase", CustInfoModel.Phase);
                //param[22] = SqlHelper.GetParameter("@CustSupe", CustInfoModel.CustSupe);
                param[23] = SqlHelper.GetParameter("@Trade", CustInfoModel.Trade);
                param[24] = SqlHelper.GetParameter("@SetupDate", CustInfoModel.SetupDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(CustInfoModel.SetupDate.ToString()));
                param[25] = SqlHelper.GetParameter("@ArtiPerson", CustInfoModel.ArtiPerson);
                param[26] = SqlHelper.GetParameter("@SetupMoney", CustInfoModel.SetupMoney);
                param[27] = SqlHelper.GetParameter("@SetupAddress", CustInfoModel.SetupAddress);
                param[28] = SqlHelper.GetParameter("@CapitalScale", CustInfoModel.CapitalScale);
                param[29] = SqlHelper.GetParameter("@SaleroomY", CustInfoModel.SaleroomY);
                param[30] = SqlHelper.GetParameter("@ProfitY", CustInfoModel.ProfitY);
                param[31] = SqlHelper.GetParameter("@TaxCD", CustInfoModel.TaxCD);
                param[32] = SqlHelper.GetParameter("@BusiNumber", CustInfoModel.BusiNumber);
                param[33] = SqlHelper.GetParameter("@IsTax", CustInfoModel.IsTax);
                param[34] = SqlHelper.GetParameter("@SellMode", CustInfoModel.SellMode);
                param[35] = SqlHelper.GetParameter("@SellArea", CustInfoModel.SellArea);
                param[36] = SqlHelper.GetParameter("@CountryID", CustInfoModel.CountryID);
                param[37] = SqlHelper.GetParameter("@Province", CustInfoModel.Province);
                param[38] = SqlHelper.GetParameter("@City", CustInfoModel.City);
                param[39] = SqlHelper.GetParameter("@Tel", CustInfoModel.Tel);
                param[40] = SqlHelper.GetParameter("@ContactName", CustInfoModel.ContactName);
                param[41] = SqlHelper.GetParameter("@Mobile", CustInfoModel.Mobile);
                param[42] = SqlHelper.GetParameter("@ReceiveAddress", CustInfoModel.ReceiveAddress);
                param[43] = SqlHelper.GetParameter("@WebSite", CustInfoModel.WebSite);
                param[44] = SqlHelper.GetParameter("@Post", CustInfoModel.Post);
                param[45] = SqlHelper.GetParameter("@email", CustInfoModel.email);
                param[46] = SqlHelper.GetParameter("@Fax", CustInfoModel.Fax);
                param[47] = SqlHelper.GetParameter("@OnLine", CustInfoModel.OnLine);
                param[48] = SqlHelper.GetParameter("@TakeType", CustInfoModel.TakeType);
                param[49] = SqlHelper.GetParameter("@CarryType", CustInfoModel.CarryType);
                param[50] = SqlHelper.GetParameter("@BusiType", CustInfoModel.BusiType);
                param[51] = SqlHelper.GetParameter("@BillType", CustInfoModel.BillType);
                param[52] = SqlHelper.GetParameter("@PayType", CustInfoModel.PayType);
                param[53] = SqlHelper.GetParameter("@MoneyType", CustInfoModel.MoneyType);

                param[54] = SqlHelper.GetParameter("@CurrencyType", CustInfoModel.CurrencyType);
                param[55] = SqlHelper.GetParameter("@CreditManage", CustInfoModel.CreditManage);
                param[56] = SqlHelper.GetParameter("@MaxCredit", CustInfoModel.MaxCredit);
                param[57] = SqlHelper.GetParameter("@MaxCreditDate", CustInfoModel.MaxCreditDate);
                param[58] = SqlHelper.GetParameter("@UsedStatus", CustInfoModel.UsedStatus);
                param[59] = SqlHelper.GetParameter("@Creator", CustInfoModel.Creator);
                param[60] = SqlHelper.GetParameter("@CreatedDate", CustInfoModel.CreatedDate == null
                                                       ? SqlDateTime.Null
                                                       : SqlDateTime.Parse(CustInfoModel.CreatedDate.ToString()));
                param[61] = SqlHelper.GetParameter("@OpenBank", CustInfoModel.OpenBank);
                param[62] = SqlHelper.GetParameter("@AccountMan", CustInfoModel.AccountMan);
                param[63] = SqlHelper.GetParameter("@AccountNum", CustInfoModel.AccountNum);
                param[64] = SqlHelper.GetParameter("@Remark", CustInfoModel.Remark);
                //新添加20120723
                param[65] = SqlHelper.GetParameter("@Corptype", CustInfoModel.Corptype);
                param[66] = SqlHelper.GetParameter("@Usedata", CustInfoModel.Usedata == null
                                       ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Usedata.ToString()));
                param[67] = SqlHelper.GetParameter("@Certidata", CustInfoModel.Certidata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Certidata.ToString()));
                param[68] = SqlHelper.GetParameter("@Wardata", CustInfoModel.Wardata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Wardata.ToString()));
                param[69] = SqlHelper.GetParameter("@Powerdata", CustInfoModel.Powerdata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Powerdata.ToString()));
                param[70] = SqlHelper.GetParameter("@Pagestatus", CustInfoModel.Pagestatus);
                param[71] = SqlHelper.GetParameter("@Gmspdata", CustInfoModel.Gmspdata == null
                                       ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Gmspdata.ToString()));
                param[72] = SqlHelper.GetParameter("@Category", CustInfoModel.Category);//所属分类

                #endregion

                LinkManModel LinkManM = new LinkManModel();
                string[] strlinkman = LinkManlist.Split('|'); //把联系人列表流分隔成数组
                SqlCommand[] comms = new SqlCommand[strlinkman.Length]; //申明cmd数组

                SqlCommand cmdcust = new SqlCommand(sql.ToString());  //未执行的客户信息添加命令
                cmdcust.Parameters.AddRange(param);
                comms[0] = cmdcust; //把未执行的客户信息添加命令给cmd数组第一项                

                string recorditems = "";
                string[] linkmanfield = null;

                for (int i = 1; i < strlinkman.Length; i++) //循环数组
                {
                    recorditems = strlinkman[i].ToString();//取到每一条记录:[序号,联系人姓名,手机,工作电话,职位,负责业务]
                    linkmanfield = recorditems.Split(','); //把每条记录分隔到字段

                    string fieldxh = linkmanfield[0].ToString();//序号
                    string fieldname = linkmanfield[1].ToString();//联系人姓名
                    string fieldhandset = linkmanfield[2].ToString();//手机
                    string fieldworktel = linkmanfield[3].ToString();//工作电话
                    string fieldposition = linkmanfield[4].ToString();//职务
                    string fieldoperation = linkmanfield[5].ToString();//负责业务

                    LinkManM.CompanyCD = CustInfoModel.CompanyCD; //联系人信息赋予一个LinkManM(联系人Model对象实例)
                    LinkManM.CustNo = CustInfoModel.CustNo;
                    LinkManM.LinkManName = fieldname;
                    LinkManM.Handset = fieldhandset;
                    LinkManM.WorkTel = fieldworktel;
                    LinkManM.Position = fieldposition;
                    LinkManM.Operation = fieldoperation;
                    LinkManM.ModifiedDate = DateTime.Now;
                    LinkManM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//Session读取当前用户ID

                    #region 拼写添加联系人信息sql语句
                    StringBuilder sqllinkman = new StringBuilder();
                    sqllinkman.AppendLine("INSERT INTO officedba.CustLinkMan");
                    sqllinkman.AppendLine("(CompanyCD");
                    sqllinkman.AppendLine(",CustNo     ");
                    sqllinkman.AppendLine(",LinkManName");
                    sqllinkman.AppendLine(",Position   ");
                    sqllinkman.AppendLine(",Operation  ");
                    sqllinkman.AppendLine(",WorkTel    ");
                    sqllinkman.AppendLine(",Handset    ");
                    sqllinkman.AppendLine(",ModifiedDate");
                    sqllinkman.AppendLine(",ModifiedUserID)");
                    sqllinkman.AppendLine(" values ");
                    sqllinkman.AppendLine("(@CompanyCD");
                    sqllinkman.AppendLine(",@CustNo     ");
                    sqllinkman.AppendLine(",@LinkManName");
                    sqllinkman.AppendLine(",@Position   ");
                    sqllinkman.AppendLine(",@Operation  ");
                    sqllinkman.AppendLine(",@WorkTel    ");
                    sqllinkman.AppendLine(",@Handset    ");
                    sqllinkman.AppendLine(",@ModifiedDate");
                    sqllinkman.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramlinkman = new SqlParameter[9];
                    paramlinkman[0] = SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD);
                    paramlinkman[1] = SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo);
                    paramlinkman[2] = SqlHelper.GetParameter("@LinkManName", LinkManM.LinkManName);
                    paramlinkman[3] = SqlHelper.GetParameter("@Position", LinkManM.Position);
                    paramlinkman[4] = SqlHelper.GetParameter("@Operation", LinkManM.Operation);
                    paramlinkman[5] = SqlHelper.GetParameter("@WorkTel", LinkManM.WorkTel);
                    paramlinkman[6] = SqlHelper.GetParameter("@Handset", LinkManM.Handset);
                    paramlinkman[7] = SqlHelper.GetParameter("@ModifiedDate", LinkManM.ModifiedDate);
                    paramlinkman[8] = SqlHelper.GetParameter("@ModifiedUserID", LinkManM.ModifiedUserID);
                    #endregion

                    SqlCommand cmdlinkman = new SqlCommand(sqllinkman.ToString());  //未执行的联系人信息添加命令
                    cmdlinkman.Parameters.AddRange(paramlinkman);
                    comms[i] = cmdlinkman; //把未执行的联系人信息添加命令给cmd数组
                }

                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return false;
            }
        }
        #endregion

        #region 客户档案附件信息
        /// <summary>
        /// 获取客户档案附件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetAdvisoryPriceAttachInfo(CustInfoModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select a.ParentId as InqNo,a.annFileName as AnnFileName,a.AnnAddr,a.upDatetime as UpDateTime,a.annRemark as AnnRemark ");
            //detSql.AppendLine("	   ,b.DocSavePath as AnnAdd");
            detSql.AppendLine("from officedba.Annex a");
            //detSql.AppendLine("left join pubdba.companyOpenServ b  on a.CompanyCD = b.CompanyCD");
            detSql.AppendLine("where a.CompanyCD=@CompanyCD  and ModuleType=@ModuleType and a.ParentId=(select CustNo from officedba.CustInfo where ID = @ID)");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@InqNo", model.InqNo.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModuleType", ConstUtil.MODULE_ID_CUST_INFO_ADD));

            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据客户编号修改客户信息的方法
        /// <summary>
        /// 根据客户编号修改客户信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <returns>bool值</returns>
        public static bool UpdateCustInfo(CustInfoModel CustInfoModel)//, LinkManModel LinkManM, Hashtable htExtAttr)
        {
            ArrayList lstCmd = new ArrayList();
            try
            {
                #region 拼写修改客户信息SQL语句
                StringBuilder sqlcust = new StringBuilder();
                sqlcust.AppendLine("UPDATE officedba.CustInfo set ");
                //sqlcust.AppendLine("CompanyCD=           @CompanyCD, ");
                //sqlcust.AppendLine("CustTypeManage =           @CustTypeManage, ");
                //sqlcust.AppendLine("CustTypeSell =           @CustTypeSell, ");
                //sqlcust.AppendLine("CustTypeTime =           @CustTypeTime, ");
                //sqlcust.AppendLine("CustType=            @CustType, ");
                //sqlcust.AppendLine("CustClass =          @CustClass , ");
                sqlcust.AppendLine("CustName =           @CustName , ");
                sqlcust.AppendLine("CustNam =            @CustNam , ");
                sqlcust.AppendLine("CustShort =          @CustShort , ");
                //sqlcust.AppendLine("CreditGrade =        @CreditGrade , ");
                sqlcust.AppendLine("Manager =            @Manager , ");
                sqlcust.AppendLine("AreaID =             @AreaID , ");
                sqlcust.AppendLine("CustNote =           @CustNote , ");
                //sqlcust.AppendLine("LinkCycle =          @LinkCycle , ");
                //sqlcust.AppendLine("HotIs =              @HotIs , ");
                //sqlcust.AppendLine("HotHow =             @HotHow , ");
                //sqlcust.AppendLine("MeritGrade =         @MeritGrade , ");
                //sqlcust.AppendLine("RelaGrade =          @RelaGrade , ");
                //sqlcust.AppendLine("Relation =           @Relation , ");
                //sqlcust.AppendLine("CompanyType =        @CompanyType , ");
                //sqlcust.AppendLine("StaffCount =         @StaffCount , ");
                //sqlcust.AppendLine("Source =             @Source , ");
                //sqlcust.AppendLine("Phase =              @Phase , ");
                //sqlcust.AppendLine("CustSupe =           @CustSupe , ");
                //sqlcust.AppendLine("Trade =              @Trade , ");
                //sqlcust.AppendLine("SetupDate =          @SetupDate , ");
                //sqlcust.AppendLine("ArtiPerson =         @ArtiPerson , ");
                //sqlcust.AppendLine("SetupMoney =         @SetupMoney , ");
                sqlcust.AppendLine("SetupAddress =       @SetupAddress , ");
                //sqlcust.AppendLine("CapitalScale =       @CapitalScale , ");
                //sqlcust.AppendLine("SaleroomY =          @SaleroomY , ");
                //sqlcust.AppendLine("ProfitY =            @ProfitY , ");
                sqlcust.AppendLine("TaxCD =              @TaxCD , ");
                //sqlcust.AppendLine("BusiNumber =         @BusiNumber , ");
                //sqlcust.AppendLine("IsTax =              @IsTax , ");
                //sqlcust.AppendLine("SellMode =           @SellMode , ");
                //sqlcust.AppendLine("SellArea =           @SellArea , ");
                //sqlcust.AppendLine("CountryID =          @CountryID , ");
                //sqlcust.AppendLine("Province =           @Province , ");
                //sqlcust.AppendLine("City =               @City , ");
                sqlcust.AppendLine("Tel =                @Tel , ");
                sqlcust.AppendLine("ContactName =        @ContactName , ");
                sqlcust.AppendLine("Mobile =             @Mobile , ");
                sqlcust.AppendLine("ReceiveAddress =     @ReceiveAddress , ");
                //sqlcust.AppendLine("WebSite =            @WebSite , ");
                //sqlcust.AppendLine("Post =               @Post , ");
                //sqlcust.AppendLine("email =              @email , ");
                //sqlcust.AppendLine("Fax =                @Fax , ");
                //sqlcust.AppendLine("OnLine =             @OnLine , ");
                //sqlcust.AppendLine("TakeType =           @TakeType , ");
                //sqlcust.AppendLine("CarryType =          @CarryType , ");
                //sqlcust.AppendLine("BusiType =           @BusiType , ");
                //sqlcust.AppendLine("BillType =           @BillType , ");
                //sqlcust.AppendLine("PayType =            @PayType , ");
                //sqlcust.AppendLine("MoneyType =          @MoneyType , ");
                //sqlcust.AppendLine("CurrencyType =       @CurrencyType , ");
                //sqlcust.AppendLine("CreditManage =       @CreditManage , ");
                sqlcust.AppendLine("MaxCredit =          @MaxCredit , ");
                //sqlcust.AppendLine("MaxCreditDate =      @MaxCreditDate , ");
                sqlcust.AppendLine("UsedStatus =         @UsedStatus , ");
                sqlcust.AppendLine("CanViewUser =         @CanViewUser , ");
                sqlcust.AppendLine("CanViewUserName =         @CanViewUserName , ");
                //sqlcust.AppendLine("FirstBuyDate =         @FirstBuyDate , ");
                sqlcust.AppendLine("CreatedDate =        @CreatedDate , ");
                sqlcust.AppendLine("OpenBank =             @OpenBank, ");
                sqlcust.AppendLine("AccountMan =             @AccountMan, ");
                sqlcust.AppendLine("AccountNum =             @AccountNum, ");
                //sqlcust.AppendLine("CustBig =            @CustBig, ");
                //sqlcust.AppendLine("CustNum =            @CustNum, ");
                sqlcust.AppendLine("Remark =             @Remark, ");
                sqlcust.AppendLine("ModifiedDate =       @ModifiedDate, ");
                //sqlcust.AppendLine("CompanyValues =      @CompanyValues , ");
                //sqlcust.AppendLine("CatchWord =         @CatchWord , ");
                //sqlcust.AppendLine("ManageValues =         @ManageValues , ");
                //sqlcust.AppendLine("Potential =         @Potential , ");
                //sqlcust.AppendLine("Problem =         @Problem , ");
                //sqlcust.AppendLine("Advantages =        @Advantages , ");
                //sqlcust.AppendLine("TradePosition =             @TradePosition, ");
                //sqlcust.AppendLine("Competition =             @Competition, ");
                //sqlcust.AppendLine("Collaborator =             @Collaborator, ");
                //sqlcust.AppendLine("ManagePlan =             @ManagePlan, ");
                //sqlcust.AppendLine("Collaborate =       @Collaborate, ");
                sqlcust.AppendLine("ModifiedUserID =     @ModifiedUserID, ");
                //新添加20120723
                //sqlcust.AppendLine("Corptype =         @Corptype , ");//企业类型
                //sqlcust.AppendLine("Usedata =           @Usedata , ");
                //sqlcust.AppendLine("Certidata =           @Certidata , ");
                //sqlcust.AppendLine("Wardata =           @Wardata , ");
                //sqlcust.AppendLine("Powerdata =           @Powerdata , ");
                sqlcust.AppendLine("Pagestatus =         @Pagestatus , ");//页面状态
                //sqlcust.AppendLine("Gmspdata =           @Gmspdata , ");
                //sqlcust.AppendLine("Category =         @Category , ");//所属分类
                //sqlcust.AppendLine("StopDate =         @stopdate,  ");
                sqlcust.AppendLine("BillUnit =         @BillUnit  ");  //开票单位

                
                sqlcust.AppendLine(" WHERE ");
                sqlcust.AppendLine("CustNo = @CustNo and CompanyCD = @CompanyCD");
                

                #endregion

                #region 设置修改客户信息参数
                SqlCommand mycomm = new SqlCommand();
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustType", CustInfoModel.CustType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustClass", CustInfoModel.CustClass));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustName", CustInfoModel.CustName));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNam", CustInfoModel.CustNam));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustShort", CustInfoModel.CustShort));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CreditGrade", CustInfoModel.CreditGrade));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Manager", CustInfoModel.Seller));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AreaID", CustInfoModel.AreaID));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNote", CustInfoModel.CustNote));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@LinkCycle", CustInfoModel.LinkCycle));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@HotIs", CustInfoModel.HotIs));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@HotHow", CustInfoModel.HotHow));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MeritGrade", CustInfoModel.MeritGrade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@RelaGrade", CustInfoModel.RelaGrade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Relation", CustInfoModel.Relation));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyType", CustInfoModel.CompanyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@StaffCount", CustInfoModel.StaffCount));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Source", CustInfoModel.Source));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Phase", CustInfoModel.Phase));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustSupe", CustInfoModel.CustSupe));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Trade", CustInfoModel.Trade));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupDate", CustInfoModel.SetupDate == null
                                        //? SqlDateTime.Null
                                        //: SqlDateTime.Parse(CustInfoModel.SetupDate.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ArtiPerson", CustInfoModel.ArtiPerson));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupMoney", CustInfoModel.SetupMoney));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@SetupAddress", CustInfoModel.SetupAddress));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CapitalScale", CustInfoModel.CapitalScale));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SaleroomY", CustInfoModel.SaleroomY));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ProfitY", CustInfoModel.ProfitY));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@TaxCD", CustInfoModel.TaxCD));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BusiNumber", CustInfoModel.BusiNumber));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@IsTax", CustInfoModel.IsTax));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SellMode", CustInfoModel.SellMode));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@SellArea", CustInfoModel.SellArea));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CountryID", CustInfoModel.CountryID));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Province", CustInfoModel.Province));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@City", CustInfoModel.City));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Tel", CustInfoModel.Tel));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ContactName", CustInfoModel.ContactName));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Mobile", CustInfoModel.Mobile));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ReceiveAddress", CustInfoModel.ReceiveAddress));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@WebSite", CustInfoModel.WebSite));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Post", CustInfoModel.Post));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@email", CustInfoModel.email));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Fax", CustInfoModel.Fax));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@OnLine", CustInfoModel.OnLine));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@TakeType", CustInfoModel.TakeType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CarryType", CustInfoModel.CarryType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BusiType", CustInfoModel.BusiType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@BillType", CustInfoModel.BillType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@PayType", CustInfoModel.PayType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", CustInfoModel.MoneyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CustInfoModel.CurrencyType));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CreditManage", CustInfoModel.CreditManage));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@MaxCredit", CustInfoModel.MaxCredit));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@MaxCreditDate", CustInfoModel.MaxCreditDate));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", CustInfoModel.UsedStatus));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Creator", CustInfoModel.Creator));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CreatedDate", CustInfoModel.CreatedDate == null
                                                       ? SqlDateTime.Null
                                                       : SqlDateTime.Parse(CustInfoModel.CreatedDate.ToString())));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@OpenBank", CustInfoModel.OpenBank));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", CustInfoModel.AccountMan));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", CustInfoModel.AccountNum));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustBig", CustInfoModel.CustBig));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustNum", CustInfoModel.CustNum));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Remark", CustInfoModel.Remark));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeManage", CustInfoModel.CustTypeManage));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeSell", CustInfoModel.CustTypeSell));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CustTypeTime", CustInfoModel.CustTypeTime));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@FirstBuyDate", CustInfoModel.FirstBuyDate == null
                //                                       ? SqlDateTime.Null
                //                                       : SqlDateTime.Parse(CustInfoModel.FirstBuyDate.ToString())));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", CustInfoModel.CanViewUser));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", CustInfoModel.CanViewUserName));

                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyValues", CustInfoModel.CompanyValues));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@CatchWord", CustInfoModel.CatchWord));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ManageValues", CustInfoModel.ManageValues));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Potential", CustInfoModel.Potential));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Problem", CustInfoModel.Problem));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Advantages", CustInfoModel.Advantages));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@TradePosition", CustInfoModel.TradePosition));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Competition", CustInfoModel.Competition));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Collaborator", CustInfoModel.Collaborator));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@ManagePlan", CustInfoModel.ManagePlan));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Collaborate", CustInfoModel.Collaborate));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@stopdate", CustInfoModel.StopDate == null
                //                                   ? SqlDateTime.Null
                //                                   : SqlDateTime.Parse(CustInfoModel.StopDate.ToString())));
                ////新添加20120723
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Corptype", CustInfoModel.Corptype));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Usedata", CustInfoModel.Usedata == null
                //                                   ? SqlDateTime.Null
                //                                   : SqlDateTime.Parse(CustInfoModel.Usedata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Certidata", CustInfoModel.Certidata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Certidata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Wardata", CustInfoModel.Wardata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Wardata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Powerdata", CustInfoModel.Powerdata == null
                //                        ? SqlDateTime.Null
                //                        : SqlDateTime.Parse(CustInfoModel.Powerdata.ToString())));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@Pagestatus", CustInfoModel.Pagestatus));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Gmspdata", CustInfoModel.Gmspdata == null
                //                                   ? SqlDateTime.Null
                //                                   : SqlDateTime.Parse(CustInfoModel.Gmspdata.ToString())));
                //mycomm.Parameters.Add(SqlHelper.GetParameter("@Category", CustInfoModel.Category));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@BillUnit", CustInfoModel.BillUnit));  //开票单位

                mycomm.CommandText = sqlcust.ToString();
                #endregion
                #region 先删除附件信息
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("Delete From officedba.Annex where CompanyCD=@CompanyCD and ParentId=@InqNo and ModuleType=@ModuleType");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@InqNo", CustInfoModel.CustNo));
                commDel.Parameters.Add(SqlHelper.GetParameter("@ModuleType", ConstUtil.MODULE_ID_CUST_INFO_ADD));
                lstCmd.Add(commDel);
                #endregion

                #region 插入附件
                if (!String.IsNullOrEmpty(CustInfoModel.AnnFileName))
                {
                    string[] annFileName = CustInfoModel.AnnFileName.Split(',');
                    string[] annRemark = CustInfoModel.AnnRemark.Split(',');
                    string[] annUpDateTime = CustInfoModel.UpDateTime.Split(',');
                    string[] annAddr = CustInfoModel.AnnAddr.Split(',');
                    if (annFileName.Length >= 1)
                    {
                        for (int i = 0; i < annFileName.Length - 1; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.Annex    ");
                            cmdsql.AppendLine("           (CompanyCD                    ");
                            cmdsql.AppendLine("           ,ParentId                    ");
                            cmdsql.AppendLine("           ,AnnAddr                   ");
                            cmdsql.AppendLine("           ,annFileName                        ");
                            cmdsql.AppendLine("           ,upDatetime                    ");
                            cmdsql.AppendLine("           ,annRemark,ModuleType)                  ");
                            cmdsql.AppendLine("     VALUES                              ");
                            cmdsql.AppendLine("           (@CompanyCD                   ");
                            cmdsql.AppendLine("           ,@ParentId                   ");
                            cmdsql.AppendLine("           ,@AnnAddr                  ");
                            cmdsql.AppendLine("           ,@AnnFileName                       ");
                            cmdsql.AppendLine("           ,@UpDateTime                   ");
                            cmdsql.AppendLine("           ,@AnnRemark,@ModuleType)                 ");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ParentId", CustInfoModel.CustNo));
                            comms.Parameters.Add(SqlHelper.GetParameter("@AnnAddr", annAddr[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@AnnFileName", annFileName[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UpDateTime", annUpDateTime[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@AnnRemark", annRemark[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ModuleType", ConstUtil.MODULE_ID_CUST_INFO_ADD));
                            lstCmd.Add(comms);
                        }
                    }
                }
                #endregion

                
                

                lstCmd.Add(mycomm);//修改客户基本信息
                //if (htExtAttr != null)
                //{
                //    SqlCommand cmd = new SqlCommand();
                //    GetExtAttrCmd(CustInfoModel, htExtAttr, cmd);
                //    lstCmd.Add(cmd);//修改客户扩展信息
                //}

                //if (CustInfoModel.CustBig == "2")//当选择会员客户时,修改信息到联系人表
                //{
                //    SqlCommand cmdLinkMan = new SqlCommand();
                //    StringBuilder sqlLinkMan = new StringBuilder();
                //    sqlLinkMan.AppendLine(" update officedba.CustLinkMan ");
                //    sqlLinkMan.AppendLine(" set LinkManName=@LinkManName,Sex=@Sex,LinkType=@LinkType,PaperNum=@PaperNum,");
                //    sqlLinkMan.AppendLine(" Birthday=@Birthday,WorkTel=@WorkTel,Handset=@Handset,Fax=@Fax,Position=@Position, ");
                //    sqlLinkMan.AppendLine("   Age= @Age,Post=@Post,MailAddress=@MailAddress,HomeTown=@HomeTown,NationalID=@NationalID,CultureLevel=@CultureLevel,");
                //    sqlLinkMan.AppendLine(" Professional=@Professional,IncomeYear=@IncomeYear,FuoodDrink=@FuoodDrink, ");
                //    sqlLinkMan.AppendLine("   LoveMusic=@LoveMusic,LoveColor=@LoveColor,LoveSmoke=@LoveSmoke,LoveDrink=@LoveDrink,LoveTea=@LoveTea,");
                //    sqlLinkMan.AppendLine(" LoveBook=@LoveBook,LoveSport=@LoveSport,LoveClothes=@LoveClothes,Cosmetic=@Cosmetic, ");
                //    sqlLinkMan.AppendLine("   Nature=@Nature,Appearance=@Appearance,AdoutBody=@AdoutBody,AboutFamily=@AboutFamily,Car=@Car,");
                //    sqlLinkMan.AppendLine(" CanViewUser=@CanViewUser,CanViewUserName=@CanViewUserName ");                    
                //    sqlLinkMan.AppendLine(" WHERE ");
                //    sqlLinkMan.AppendLine(" CustNo = @CustNo and CompanyCD = @CompanyCD ");
                //    sqlLinkMan.AppendLine(" and id=(select min(c.id)id from officedba.CustLinkMan c ");
                //    sqlLinkMan.AppendLine(" 			where c.CompanyCD = @CompanyCD and c.custno = @CustNo )");

                //    #region 设置联系人参数
                //    SqlCommand commLinkMan = new SqlCommand();
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LinkManName", CustInfoModel.CustName));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Sex", LinkManM.Sex));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LinkType", LinkManM.LinkType));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@PaperNum", LinkManM.PaperNum));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Birthday", LinkManM.Birthday == null ? SqlDateTime.Null : SqlDateTime.Parse(LinkManM.Birthday.ToString())));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@WorkTel", LinkManM.WorkTel));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Handset", LinkManM.Handset));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Fax", LinkManM.Fax));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Position", LinkManM.Position));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Age", LinkManM.Age));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Post", LinkManM.Post));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@MailAddress", LinkManM.MailAddress));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@HomeTown", LinkManM.HomeTown));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@NationalID", LinkManM.NationalID));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CultureLevel", LinkManM.CultureLevel));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Professional", LinkManM.Professional));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@IncomeYear", LinkManM.IncomeYear));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@FuoodDrink", LinkManM.FuoodDrink));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveMusic", LinkManM.LoveMusic));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveColor", LinkManM.LoveColor));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveSmoke", LinkManM.LoveSmoke));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveDrink", LinkManM.LoveDrink));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveTea", LinkManM.LoveTea));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveBook", LinkManM.LoveBook));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveSport", LinkManM.LoveSport));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@LoveClothes", LinkManM.LoveClothes));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Cosmetic", LinkManM.Cosmetic));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Nature", LinkManM.Nature));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Appearance", LinkManM.Appearance));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@AdoutBody", LinkManM.AdoutBody));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@AboutFamily", LinkManM.AboutFamily));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@Car", LinkManM.Car));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", CustInfoModel.CanViewUser));
                //    commLinkMan.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", CustInfoModel.CanViewUserName));
                //    commLinkMan.CommandText = sqlLinkMan.ToString();

                //    #endregion

                //    lstCmd.Add(commLinkMan);

                //}

                //执行登陆操作
                bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

                return isSucc;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 同时修改客户信息及联系人列表信息的方法
        /// <summary>
        /// 同时修改客户信息及联系人列表信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <param name="LinkManlist">联系人列表信息流</param>
        /// <returns>bool值</returns>
        public static bool UpdateCustAndLinkMan(CustInfoModel CustInfoModel, string LinkManlist)
        {
            try
            {
                #region 拼写修改客户信息SQL语句
                StringBuilder sqlcust = new StringBuilder();
                sqlcust.AppendLine("UPDATE officedba.CustInfo set ");
                sqlcust.AppendLine("CompanyCD=           @CompanyCD, ");
                sqlcust.AppendLine("CustType=            @CustType, ");
                sqlcust.AppendLine("CustClass =          @CustClass , ");
                //sqlcust.AppendLine("CustNo =             @CustNo , ");        
                sqlcust.AppendLine("CustName =           @CustName , ");
                sqlcust.AppendLine("CustNam =            @CustNam , ");
                sqlcust.AppendLine("CustShort =          @CustShort , ");
                sqlcust.AppendLine("CreditGrade =        @CreditGrade , ");
                sqlcust.AppendLine("Manager =            @Manager , ");
                sqlcust.AppendLine("AreaID =             @AreaID , ");
                sqlcust.AppendLine("CustNote =           @CustNote , ");
                sqlcust.AppendLine("LinkCycle =          @LinkCycle , ");
                sqlcust.AppendLine("HotIs =              @HotIs , ");
                sqlcust.AppendLine("HotHow =             @HotHow , ");
                sqlcust.AppendLine("HotType =            @HotType , ");
                sqlcust.AppendLine("MeritGrade =         @MeritGrade , ");
                sqlcust.AppendLine("RelaGrade =          @RelaGrade , ");
                sqlcust.AppendLine("Relation =           @Relation , ");
                sqlcust.AppendLine("CompanyType =        @CompanyType , ");
                sqlcust.AppendLine("StaffCount =         @StaffCount , ");
                sqlcust.AppendLine("Source =             @Source , ");
                sqlcust.AppendLine("Phase =              @Phase , ");
                //sqlcust.AppendLine("CustSupe =           @CustSupe , ");
                sqlcust.AppendLine("Trade =              @Trade , ");
                sqlcust.AppendLine("SetupDate =          @SetupDate , ");
                sqlcust.AppendLine("ArtiPerson =         @ArtiPerson , ");
                sqlcust.AppendLine("SetupMoney =         @SetupMoney , ");
                sqlcust.AppendLine("SetupAddress =       @SetupAddress , ");
                sqlcust.AppendLine("CapitalScale =       @CapitalScale , ");
                sqlcust.AppendLine("SaleroomY =          @SaleroomY , ");
                sqlcust.AppendLine("ProfitY =            @ProfitY , ");
                sqlcust.AppendLine("TaxCD =              @TaxCD , ");
                sqlcust.AppendLine("BusiNumber =         @BusiNumber , ");
                sqlcust.AppendLine("IsTax =              @IsTax , ");
                sqlcust.AppendLine("SellMode =           @SellMode , ");
                sqlcust.AppendLine("SellArea =           @SellArea , ");
                sqlcust.AppendLine("CountryID =          @CountryID , ");
                sqlcust.AppendLine("Province =           @Province , ");
                sqlcust.AppendLine("City =               @City , ");
                sqlcust.AppendLine("Tel =                @Tel , ");
                sqlcust.AppendLine("ContactName =        @ContactName , ");
                sqlcust.AppendLine("Mobile =             @Mobile , ");
                sqlcust.AppendLine("ReceiveAddress =     @ReceiveAddress , ");
                sqlcust.AppendLine("WebSite =            @WebSite , ");
                sqlcust.AppendLine("Post =               @Post , ");
                sqlcust.AppendLine("email =              @email , ");
                sqlcust.AppendLine("Fax =                @Fax , ");
                sqlcust.AppendLine("OnLine =             @OnLine , ");
                sqlcust.AppendLine("TakeType =           @TakeType , ");
                sqlcust.AppendLine("CarryType =          @CarryType , ");
                sqlcust.AppendLine("BusiType =           @BusiType , ");
                sqlcust.AppendLine("BillType =           @BillType , ");
                sqlcust.AppendLine("PayType =            @PayType , ");
                sqlcust.AppendLine("MoneyType =          @MoneyType , ");
                sqlcust.AppendLine("CurrencyType =       @CurrencyType , ");
                sqlcust.AppendLine("CreditManage =       @CreditManage , ");
                sqlcust.AppendLine("MaxCredit =          @MaxCredit , ");
                sqlcust.AppendLine("MaxCreditDate =      @MaxCreditDate , ");
                //sqlcust.AppendLine("UpdateCredit =       @UpdateCredit , ");
                sqlcust.AppendLine("UsedStatus =         @UsedStatus , ");
                sqlcust.AppendLine("Creator =            @Creator , ");
                sqlcust.AppendLine("CreatedDate =        @CreatedDate , ");
                sqlcust.AppendLine("OpenBank =        @OpenBank , ");
                sqlcust.AppendLine("AccountMan =        @AccountMan , ");
                sqlcust.AppendLine("AccountNum =        @AccountNum , ");
                sqlcust.AppendLine("Remark =            @Remark, ");
                sqlcust.AppendLine("CustBig =            @CustBig, ");
                sqlcust.AppendLine("CustNum =            @CustNum, ");
                sqlcust.AppendLine("ModifiedDate =       @ModifiedDate, ");
                //新添加20120723
                sqlcust.AppendLine("Corptype =           @Corptype , ");
                sqlcust.AppendLine("Usedata =           @Usedata , ");
                sqlcust.AppendLine("Certidata =           @Certidata , ");
                sqlcust.AppendLine("Wardata =           @Wardata , ");
                sqlcust.AppendLine("Powerdata =           @Powerdata , ");
                sqlcust.AppendLine("Pagestatus =         @Pagestatus , ");//页面状态
                sqlcust.AppendLine("Gmspdata =           @Gmspdata , ");
                sqlcust.AppendLine("Category =           @Category , ");
                sqlcust.AppendLine("ModifiedUserID =     @ModifiedUserID ");
               
                sqlcust.AppendLine(" WHERE ");
                sqlcust.AppendLine("CustNo = @CustNo ");
                

                #endregion

                #region 设置修改客户信息参数
                SqlParameter[] param = new SqlParameter[77];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustType", CustInfoModel.CustType);
                param[2] = SqlHelper.GetParameter("@CustClass", CustInfoModel.CustClass);
                param[3] = SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo);
                param[4] = SqlHelper.GetParameter("@CustName", CustInfoModel.CustName);
                param[5] = SqlHelper.GetParameter("@CustNam", CustInfoModel.CustNam);
                param[6] = SqlHelper.GetParameter("@CustShort", CustInfoModel.CustShort);
                param[7] = SqlHelper.GetParameter("@CreditGrade", CustInfoModel.CreditGrade);
                param[8] = SqlHelper.GetParameter("@Manager", CustInfoModel.Seller);
                param[9] = SqlHelper.GetParameter("@AreaID", CustInfoModel.AreaID);
                param[10] = SqlHelper.GetParameter("@CustNote", CustInfoModel.CustNote);
                param[11] = SqlHelper.GetParameter("@LinkCycle", CustInfoModel.LinkCycle);
                param[12] = SqlHelper.GetParameter("@HotIs", CustInfoModel.HotIs);
                param[13] = SqlHelper.GetParameter("@HotHow", CustInfoModel.HotHow);
                param[14] = SqlHelper.GetParameter("@HotType", CustInfoModel.HotType);
                param[15] = SqlHelper.GetParameter("@MeritGrade", CustInfoModel.MeritGrade);
                param[16] = SqlHelper.GetParameter("@RelaGrade", CustInfoModel.RelaGrade);
                param[17] = SqlHelper.GetParameter("@Relation", CustInfoModel.Relation);
                param[18] = SqlHelper.GetParameter("@CompanyType", CustInfoModel.CompanyType);
                param[19] = SqlHelper.GetParameter("@StaffCount", CustInfoModel.StaffCount);
                param[20] = SqlHelper.GetParameter("@Source", CustInfoModel.Source);
                param[21] = SqlHelper.GetParameter("@Phase", CustInfoModel.Phase);
                //param[22] = SqlHelper.GetParameter("@CustSupe", CustInfoModel.CustSupe);
                param[23] = SqlHelper.GetParameter("@Trade", CustInfoModel.Trade);
                param[24] = SqlHelper.GetParameter("@SetupDate", CustInfoModel.SetupDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustInfoModel.SetupDate.ToString()));
                param[25] = SqlHelper.GetParameter("@ArtiPerson", CustInfoModel.ArtiPerson);
                param[26] = SqlHelper.GetParameter("@SetupMoney", CustInfoModel.SetupMoney);
                param[27] = SqlHelper.GetParameter("@SetupAddress", CustInfoModel.SetupAddress);
                param[28] = SqlHelper.GetParameter("@CapitalScale", CustInfoModel.CapitalScale);
                param[29] = SqlHelper.GetParameter("@SaleroomY", CustInfoModel.SaleroomY);
                param[30] = SqlHelper.GetParameter("@ProfitY", CustInfoModel.ProfitY);
                param[31] = SqlHelper.GetParameter("@TaxCD", CustInfoModel.TaxCD);
                param[32] = SqlHelper.GetParameter("@BusiNumber", CustInfoModel.BusiNumber);
                param[33] = SqlHelper.GetParameter("@IsTax", CustInfoModel.IsTax);
                param[34] = SqlHelper.GetParameter("@SellMode", CustInfoModel.SellMode);
                param[35] = SqlHelper.GetParameter("@SellArea", CustInfoModel.SellArea);
                param[36] = SqlHelper.GetParameter("@CountryID", CustInfoModel.CountryID);
                param[37] = SqlHelper.GetParameter("@Province", CustInfoModel.Province);
                param[38] = SqlHelper.GetParameter("@City", CustInfoModel.City);
                param[39] = SqlHelper.GetParameter("@Tel", CustInfoModel.Tel);
                param[40] = SqlHelper.GetParameter("@ContactName", CustInfoModel.ContactName);
                param[41] = SqlHelper.GetParameter("@Mobile", CustInfoModel.Mobile);
                param[42] = SqlHelper.GetParameter("@ReceiveAddress", CustInfoModel.ReceiveAddress);
                param[43] = SqlHelper.GetParameter("@WebSite", CustInfoModel.WebSite);
                param[44] = SqlHelper.GetParameter("@Post", CustInfoModel.Post);
                param[45] = SqlHelper.GetParameter("@email", CustInfoModel.email);
                param[46] = SqlHelper.GetParameter("@Fax", CustInfoModel.Fax);
                param[47] = SqlHelper.GetParameter("@OnLine", CustInfoModel.OnLine);
                param[48] = SqlHelper.GetParameter("@TakeType", CustInfoModel.TakeType);
                param[49] = SqlHelper.GetParameter("@CarryType", CustInfoModel.CarryType);
                param[50] = SqlHelper.GetParameter("@BusiType", CustInfoModel.BusiType);
                param[51] = SqlHelper.GetParameter("@BillType", CustInfoModel.BillType);
                param[52] = SqlHelper.GetParameter("@PayType", CustInfoModel.PayType);
                param[53] = SqlHelper.GetParameter("@MoneyType", CustInfoModel.MoneyType);
                param[54] = SqlHelper.GetParameter("@CurrencyType", CustInfoModel.CurrencyType);
                param[55] = SqlHelper.GetParameter("@CreditManage", CustInfoModel.CreditManage);
                param[56] = SqlHelper.GetParameter("@MaxCredit", CustInfoModel.MaxCredit);
                param[57] = SqlHelper.GetParameter("@MaxCreditDate", CustInfoModel.MaxCreditDate);
                param[58] = SqlHelper.GetParameter("@UsedStatus", CustInfoModel.UsedStatus);
                param[59] = SqlHelper.GetParameter("@Creator", CustInfoModel.Creator);
                param[60] = SqlHelper.GetParameter("@CreatedDate", CustInfoModel.CreatedDate == null
                                                       ? SqlDateTime.Null
                                                       : SqlDateTime.Parse(CustInfoModel.CreatedDate.ToString()));
                param[61] = SqlHelper.GetParameter("@OpenBank", CustInfoModel.OpenBank);
                param[62] = SqlHelper.GetParameter("@AccountMan", CustInfoModel.AccountMan);
                param[63] = SqlHelper.GetParameter("@AccountNum", CustInfoModel.AccountNum);
                param[64] = SqlHelper.GetParameter("@Remark", CustInfoModel.Remark);
                param[65] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
                param[66] = SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                param[67] = SqlHelper.GetParameter("@CustBig", CustInfoModel.CustBig);
                param[68] = SqlHelper.GetParameter("@CustNum", CustInfoModel.CustNum);
                //新添加20120723
                param[69] = SqlHelper.GetParameter("@Corptype", CustInfoModel.Corptype);
                param[70] = SqlHelper.GetParameter("@Usedata", CustInfoModel.Usedata == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustInfoModel.Usedata.ToString()));
                param[71] = SqlHelper.GetParameter("@Certidata", CustInfoModel.Certidata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Certidata.ToString()));
                param[72] = SqlHelper.GetParameter("@Wardata", CustInfoModel.Wardata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Wardata.ToString()));
                param[73] = SqlHelper.GetParameter("@Powerdata", CustInfoModel.Powerdata == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(CustInfoModel.Powerdata.ToString()));
                param[74] = SqlHelper.GetParameter("@Pagestatus", CustInfoModel.Pagestatus);
                param[75] = SqlHelper.GetParameter("@Gmspdata", CustInfoModel.Gmspdata == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustInfoModel.Gmspdata.ToString()));
                param[76] = SqlHelper.GetParameter("@Category", CustInfoModel.Category);
                #endregion

                LinkManModel LinkManM = new LinkManModel();
                string[] strlinkman = LinkManlist.Split('|'); //把联系人列表流分隔成数组
                SqlCommand[] comms = new SqlCommand[strlinkman.Length + 1]; ; //申明cmd数组

                SqlCommand cmdcust = new SqlCommand(sqlcust.ToString());  //未执行的客户信息修改命令
                cmdcust.Parameters.AddRange(param);
                comms[0] = cmdcust; //把未执行的客户信息修改命令给cmd数组第一项

                //如果联系人信息不为空，执行删除及从表修改
                if (LinkManlist != "")
                {
                    //对于从表联系人信息先删除再添加
                    SqlCommand cmdlinkmandelete = new SqlCommand("delete officedba.CustLinkMan where CustNo = '" + CustInfoModel.CustNo + "'");
                    comms[1] = cmdlinkmandelete;
                    string recorditems = "";
                    string[] linkmanfield = null;

                    for (int i = 1; i < strlinkman.Length; i++) //循环数组
                    {
                        recorditems = strlinkman[i].ToString();//取到每一条记录:[序号,联系人姓名,手机,工作电话,职位,负责业务]
                        linkmanfield = recorditems.Split(','); //把每条记录分隔到字段

                        string fieldxh = linkmanfield[0].ToString();//序号
                        string fieldname = linkmanfield[1].ToString();//联系人姓名
                        string fieldhandset = linkmanfield[2].ToString();//手机
                        string fieldworktel = linkmanfield[3].ToString();//工作电话
                        string fieldposition = linkmanfield[4].ToString();//职务
                        string fieldoperation = linkmanfield[5].ToString();//负责业务

                        LinkManM.CompanyCD = CustInfoModel.CompanyCD; //联系人信息赋予一个LinkManM(联系人Model对象实例)
                        LinkManM.CustNo = CustInfoModel.CustNo;
                        LinkManM.LinkManName = fieldname;
                        LinkManM.Handset = fieldhandset;
                        LinkManM.WorkTel = fieldworktel;
                        LinkManM.Position = fieldposition;
                        LinkManM.Operation = fieldoperation;
                        LinkManM.ModifiedDate = DateTime.Now;
                        LinkManM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//Session读取

                        #region 拼写添加联系人信息sql语句
                        StringBuilder sqllinkman = new StringBuilder();
                        sqllinkman.AppendLine("INSERT INTO officedba.CustLinkMan");
                        sqllinkman.AppendLine("(CompanyCD");
                        sqllinkman.AppendLine(",CustNo     ");
                        sqllinkman.AppendLine(",LinkManName");
                        sqllinkman.AppendLine(",Position   ");
                        sqllinkman.AppendLine(",Operation  ");
                        sqllinkman.AppendLine(",WorkTel    ");
                        sqllinkman.AppendLine(",Handset    ");
                        sqllinkman.AppendLine(",ModifiedDate");
                        sqllinkman.AppendLine(",ModifiedUserID)");
                        sqllinkman.AppendLine(" values ");
                        sqllinkman.AppendLine("(@CompanyCD");
                        sqllinkman.AppendLine(",@CustNo     ");
                        sqllinkman.AppendLine(",@LinkManName");
                        sqllinkman.AppendLine(",@Position   ");
                        sqllinkman.AppendLine(",@Operation  ");
                        sqllinkman.AppendLine(",@WorkTel    ");
                        sqllinkman.AppendLine(",@Handset    ");
                        sqllinkman.AppendLine(",@ModifiedDate");
                        sqllinkman.AppendLine(",@ModifiedUserID)");

                        SqlParameter[] paramlinkman = new SqlParameter[9];
                        paramlinkman[0] = SqlHelper.GetParameter("@CompanyCD", CustInfoModel.CompanyCD);
                        paramlinkman[1] = SqlHelper.GetParameter("@CustNo", CustInfoModel.CustNo);
                        paramlinkman[2] = SqlHelper.GetParameter("@LinkManName", LinkManM.LinkManName);
                        paramlinkman[3] = SqlHelper.GetParameter("@Position", LinkManM.Position);
                        paramlinkman[4] = SqlHelper.GetParameter("@Operation", LinkManM.Operation);
                        paramlinkman[5] = SqlHelper.GetParameter("@WorkTel", LinkManM.WorkTel);
                        paramlinkman[6] = SqlHelper.GetParameter("@Handset", LinkManM.Handset);
                        paramlinkman[7] = SqlHelper.GetParameter("@ModifiedDate", LinkManM.ModifiedDate);
                        paramlinkman[8] = SqlHelper.GetParameter("@ModifiedUserID", LinkManM.ModifiedUserID);
                        #endregion

                        SqlCommand cmdlinkman = new SqlCommand(sqllinkman.ToString());  //未执行的联系人信息添加命令
                        cmdlinkman.Parameters.AddRange(paramlinkman);
                        comms[i + 1] = cmdlinkman; //把未执行的联系人信息添加命令给cmd数组
                    }
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 根据查询条件获取客户列表信息的方法
        /// <summary>
        /// 根据查询条件获取客户列表信息的方法
        /// </summary>
        /// <param name="CustModel">查询条件</param>
        /// <param name="Creator">登陆人</param>
        /// <returns>客户列表结果集</returns>
      public static DataTable GetCustInfoBycondition(CustInfoModel CustModel, string Manager, int pageIndex, int pageCount, string ord, ref int TotalCount)
         //  public static DataTable GetCustInfoBycondition(CustInfoModel CustModel, string Manager, string CanUserID, string CreatedBegin, string CreatedEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id,ci.CustBig, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.ReceiveAddress, " +
                                   " (case ci.CustBig when '2' then '会员' else '企业' end)CustBigName, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                                   " ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " ci.RelaGrade,isnull(eic.EmployeeName,'') Creator," +
                                    "CONVERT(varchar(100), ci.Usedata, 23) Usedata,CONVERT(varchar(100), ci.Certidata, 23) Certidata,CONVERT(varchar(100), ci.Wardata, 23) Wardata,CONVERT(varchar(100), ci.Powerdata, 23) Powerdata," +
                                   "CONVERT(varchar(100), ci.Gmspdata, 23) Gmspdata,"+
                                   "  CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate, CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate,ci.Corptype,ci.Category,ci.Pagestatus,ci.UsedStatus " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CustModel.CompanyCD + "'";
                //sql += " AND  (  ";
                //XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
                //DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                    
                //    if (dt.Rows[0]["RoleRange"].ToString() == "1")
                //    {
                //        sql += " (ci.Creator IN  ";
                //        sql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                //        sql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                //    }
                //    if (dt.Rows[0]["RoleRange"].ToString() == "2")
                //    {
                //        sql += " (ci.Creator IN  ";
                //        sql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                //        sql += "  WHERE a.ID=b.ID) )) or ";
                //    }
                //    if (dt.Rows[0]["RoleRange"].ToString() == "3")
                //    {
                //        sql += " (ci.Creator IN  ";
                //        sql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                //        sql += "  WHERE a.ID=b.ID))) or ";
                //    }
                //}


                //sql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+ci.CanViewUser+',')>0 )";
                //sql += " or (ci.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (ci.Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
              

                if (CustModel.CustNo != "")
                    sql += " and ci.CustNo like '%" + CustModel.CustNo + "%'";
                if (CustModel.CustName != "")
                    sql += " and ci.CustName like '%" + CustModel.CustName + "%'";
                if (Manager != "")
                    sql += " and ei.EmployeeName like '%" + Manager + "%'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                //return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据创建人获取客户ID、编号、简称的方法
        /// <summary>
        /// 根据创建人获取客户ID、编号、简称的方法
      /// 20121226 修改 添加Gmspdata查询字段
        /// </summary>
        /// <param name="Creator">创建人</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>客户列表结果集</returns>
        public static DataTable GetCustName(CustInfoModel CustModel,string CanUserID, string CompanyCD)
        {
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            string sql = "";
            try
            {
                 
                    // 以下部分，是根据莱特CMR系统进行获取数据
                    sql = "select top 200 ci.id,ci.CustNo,ci.CustName,isnull(ci.CustShort,'') CustShort,isnull(ci.AreaID,0) AreaID,isnull(cp.TypeName,'') TypeName,isnull(ci.Tel,'') Tel,";
                    sql += " isnull(ci.ReceiveAddress,'') ReceiveAddress, ";
                    sql += " isnull(ci.Gmspdata,'') as Gmspdata ,ISNULL(ci.ContactName,'') as cCusPerson, ISNULL(ci.Mobile,'') as cCusHand,isnull(ci.billunit,'') billunit ";
                    sql += "   from  officedba.CustInfo ci ";
                    sql += "  left join officedba.CodePublicType cp on cp.id = ci.AreaID ";
                    sql += "   where ci.UsedStatus <> 0 ";
                    sql += "    and ci.CompanyCD = '" + CompanyCD + "'";
                    //sql += " AND  (   ";

                    //DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    if (dt.Rows[0]["RoleRange"].ToString() == "1")
                    //    {
                    //        sql += " (ci.Creator IN  ";
                    //        sql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                    //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                    //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    //        sql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " ))   or ";
                    //    }
                    //    if (dt.Rows[0]["RoleRange"].ToString() == "2")
                    //    {
                    //        sql += " (ci.Creator IN  ";
                    //        sql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                    //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    //        sql += "  WHERE a.ID=b.ID) ))  or ";
                    //    }
                    //    if (dt.Rows[0]["RoleRange"].ToString() == "3")
                    //    {
                    //        sql += " (ci.Creator IN  ";
                    //        sql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                    //        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                    //        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    //        sql += "  WHERE a.ID=b.ID)))  or ";
                    //    }
                    //}
                    //sql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+ci.CanViewUser+',')>0 )";
                    //sql += " or (ci.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (ci.Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";

                    if (CustModel.CustNo != "")
                        sql += " and ci.CustNo like '%" + CustModel.CustNo + "%'";
                    if (CustModel.CustName != "")
                        sql += " and ci.CustName like '%" + CustModel.CustName + "%'";
                    if (CustModel.CustShort != "")
                        sql += " and ci.CustShort like '%" + CustModel.CustShort + "%'";
                 
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据传的一批ID批量删除ID对应信息
        /// <summary>
        /// 根据传的一批ID批量删除ID对应信息
        /// </summary>
        /// <param name="CustID">客户信息ID</param>
        /// <returns>返回影响行数</returns>
        public static int DelCustInfo(string[] CustID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string allCustID = "";
            string[] Delsql = new string[1];

            if (CustID.Length == 0)
            {
                return 0;
            }

            try
            {
                for (int i = 0; i < CustID.Length; i++)
                {
                    CustID[i] = "'" + CustID[i] + "'";
                    sb.Append(CustID[i]);
                }

                allCustID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from officedba.CustInfo where id in (" + allCustID + ")";

                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? SqlHelper.Result.OprateCount : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion


        #region 根据客户ID获取是否含有客户的方法
        /// <summary>
        /// 根据客户ID获取是否含客户的方法
        /// </summary>
        /// <param name="LinkManID"></param>
        /// <returns></returns>
        public static bool GetCustInfoByID(string CompanyCD,string[] IDList,string[] NoList)
        {
            if (IDList.Length == 0 || NoList.Length == 0)
            {
                return false;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            string allCustID = "";
            string allCustNo = "";

            try
            {
                //string[] LinkManIDs = null;
                for (int i = 0; i < IDList.Length; i++)
                {
                    //LinkManIDs[i] = "'" + IDList[i] + "'";
                    sb.Append("'" + IDList[i] + "'");
                }

                allCustID = sb.ToString().Replace("''", "','");

                for (int i = 0; i < NoList.Length; i++)
                {
                    sb1.Append("'" + NoList[i] + "'");
                }
                allCustNo = sb1.ToString().Replace("''","','");

                string sql = "select id from officedba.CustLinkMan where CustNo in (" + allCustNo + ") and CompanyCD='" + CompanyCD + " '";
                if (IsHave(sql))
                    return true;
                else
                {
                    string sql1 = "select id from officedba.CustTalk where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                    if (IsHave(sql1))
                        return true;
                    else
                    {
                        string sql2 = "select id from officedba.CustLove where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                        if (IsHave(sql2))
                            return true;
                        else
                        {
                            string sql3 = "select id from officedba.CustService where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                            if (IsHave(sql3))
                                return true;
                            else
                            {
                                string sql4 = "select id from officedba.CustComplain where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                                if (IsHave(sql4))
                                    return true;
                                else
                                {
                                    string sql5 = "select id from officedba.CustAdvice where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                                    if (IsHave(sql5))
                                        return true;
                                    else
                                    {
                                        string sql6 = "select id from officedba.CustCall where CustID in (" + allCustID + ") and CompanyCD='" + CompanyCD + "'";
                                        if (IsHave(sql6))
                                            return true;
                                        else
                                          return false;
                                    }
                                }
                            }
                        }
                    }
                }  

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

      
        #region 获取客户信息 added by jiangym
        public static DataTable GetCustInfo(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CustName from officedba.CustInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD and UsedStatus=1");
            sql.AppendLine("AND  (  ");
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sql.AppendLine(" (Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sql.AppendLine(" (Creator IN  ");
                    sql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sql.AppendLine(" (Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }


            sql.AppendLine("(CHARINDEX('," + userInfo.EmployeeID + ",',','+CanViewUser+',')>0 )");
            sql.AppendLine("or (Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");
              
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

        #region 根据客户编号获取联系人信息条数
        /// <summary>
        /// 根据客户编号获取联系人信息条数
        /// </summary>
        /// <param name="CustNo"></param>
        /// <returns></returns>
        public static bool GetLinkManByCustNo(string[] CustNo, string[] CustID)
        {
            if (CustNo.Length == 0 || CustID.Length == 0)
            {
                return false;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sbID = new System.Text.StringBuilder();
            string allCustNo = "";
            string allCustID = "";

            try
            {
                //string[] Nos = null;
                //string[] IDs = null;
                for (int i = 0; i < CustNo.Length; i++)
                {
                    //Nos[i] = "'" + CustNo[i].ToString() + "'";
                    //IDs[i] = "'" + CustID[i].ToString() + "'";
                    sb.Append("'" + CustNo[i].ToString() + "'");
                    sbID.Append("'" + CustID[i].ToString() + "'");
                }

                allCustNo = sb.ToString().Replace("''", "','");
                allCustID = sbID.ToString().Replace("''", "','");

                string sql = "select id from officedba.CustLinkMan where CustNo in (" + allCustNo + ")";
                if (IsHave(sql))
                    return true;
                else
                {
                    string sql2 = "select id from officedba.SellChance where CustID in (" + allCustID + ")";
                    if (IsHave(sql2))
                        return true;
                    else
                    {
                        string sql3 = "select id from officedba.SellOrder where CustID in (" + allCustID + ")";
                        if (IsHave(sql3))
                            return true;
                        else
                            return false;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
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

        public static DataTable CheckArea(string AreaName, string CompanyCD)
        {
            try
            {
                string sql = "select ID from officedba.CodePublicType where TypeName=@AreaName and CompanyCD=@CompanyCD and TypeFlag='4' and TypeCode='12'";
                
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@AreaName", AreaName);
                param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        #region 获取附件列表
        public static DataTable GetCustAttachInfoBycondition(string CompanyCD, string CustName, string Attachment, string remark, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select a.id, a.companycd,a.parentid,isnull(a.annexflag,'') as annexflag,a.annaddr,a.annFileName,CONVERT(varchar(100), a.upDatetime, 23) upDatetime,a.annRemark," +
                                   " ci.id custid,ci.CustBig, ci.CustNo,ci.CustName " +
                               " from " +
                               " officedba.Annex a " +
                               " left join officedba.CustInfo ci on ci.custno = a.ParentId and ci.CompanyCD=a.CompanyCD " +
                               " where a.CompanyCD = '" + CompanyCD + "'";
                sql += " AND  (  ";
                XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
                DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["RoleRange"].ToString() == "1")
                    {
                        sql += " (ci.Creator IN  ";
                        sql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "2")
                    {
                        sql += " (ci.Creator IN  ";
                        sql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) )) or ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "3")
                    {
                        sql += " (ci.Creator IN  ";
                        sql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID))) or ";
                    }
                }


                sql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+ci.CanViewUser+',')>0 )";
                sql += " or (ci.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (ci.Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";

                if (!string.IsNullOrEmpty(CustName))
                {
                    sql += " and ci.CustName like '%"+CustName+"%'";
                }
                if (!string.IsNullOrEmpty(Attachment))
                {
                    sql += " and substring(a.annFileName,0,charindex('.',a.annFileName)) like '%" + Attachment + "%'";
                }
                if (!string.IsNullOrEmpty(remark))
                {
                    sql += " and a.annRemark like '%"+remark+"%'";
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 删除附件
        public static int DelAttachment(string CompanyCD, string filename, string realname)
        {
           SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
           conn.Open();
           SqlTransaction mytran = conn.BeginTransaction();
            try {
                string sql = "delete from officedba.Annex where CompanyCD=@CompanyCD and replace(AnnAddr,'\\\\','\\')=@AnnAddr and annFileName=@annFileName ";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                parm[1] = SqlHelper.GetParameter("@AnnAddr",filename);
                parm[2] = SqlHelper.GetParameter("@annFileName", realname);
                int result=SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,sql,parm);
                mytran.Commit();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion


        #region 判断客户是否有附件
        public static bool GetAttachment(string CompanyCD, string filename, string realname)
        {
            string sql = "select id from officedba.Annex where CompanyCD='" + CompanyCD + "' and replace(AnnAddr,'\\\\','\\')='" + filename + " ' and annFileName='"+realname+"'";
            if (IsHave(sql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region 运营模式

        #region 客户一览表查询
        /// <summary>
        /// 客户一览表查询
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustListByCondition(CustInfoModel CustModel, string CanUserID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select " +
                                   " ci.ID,ci.CustNo,ci.CustName,ci.CustBig," +
                               " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' else '' end) CustTypeManage," +
                               " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' else '' end) CustTypeSell," +
                               " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' else '' end) CustTypeTime," +
                                   " ci.CustType,isnull(cp.TypeName,'') CustTypeName," +
                                   " ci.CustClass,isnull(cc.CodeName,'') CustClassName," +
                                   " ci.CreditGrade,isnull(cp2.TypeName,'') CreditGradeName," +
                               " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end) RelaGrade," +
                                   " ci.AreaID,isnull(cp3.TypeName,'') Area,ci.Province,ci.City," +
                                   " ci.Manager,isnull(ei.EmployeeName,'') ManagerName,ci.ContactName,ci.Tel," +
                                   " ci.Creator,isnull(ei2.EmployeeName,'') CreatorName,CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate" +
                               " from " +
                                   " officedba.CustInfo ci" +
                               " left join officedba.CodePublicType cp on ci.CustType = cp.id" +
                               " left join officedba.CodeCompanyType cc on ci.CustClass = cc.id" +
                               " left join officedba.CodePublicType cp2 on ci.CreditGrade = cp2.id" +
                               " left join officedba.CodePublicType cp3 on ci.AreaID = cp3.id" +
                               " left join officedba.EmployeeInfo ei on ci.Manager = ei.id" +
                               " left join officedba.EmployeeInfo ei2 on ci.Creator = ei2.id" +
                               " where ci.CompanyCD = '" + CustModel.CompanyCD + "'";
                //" and (ci.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = ci.Creator or '" + CanUserID + "' = ci.Manager or ci.CanViewUser = ',,')";

                if (CustModel.CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustModel.CustTypeManage + "'";
                if (CustModel.CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustModel.CustTypeSell + "'";
                if (CustModel.CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustModel.CustTypeTime + "'";
                if (CustModel.CustType != 0)
                    sql += " and ci.CustType='" + CustModel.CustType + "'";
                if (CustModel.CustClass != 0)
                    sql += " and ci.CustClass='" + CustModel.CustClass + "'";
                if (CustModel.CreditGrade != 0)
                    sql += " and ci.CreditGrade='" + CustModel.CreditGrade + "'";
                if (CustModel.RelaGrade != "0")
                    sql += " and ci.RelaGrade='" + CustModel.RelaGrade + "'";
                if (CustModel.AreaID != 0)
                    sql += " and ci.AreaID='" + CustModel.AreaID + "'";
                if (CustModel.Seller != 0)
                    sql += " and ci.Manager='" + CustModel.Seller + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                //return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户一览表查询打印
        /// <summary>
        /// 客户一览表查询打印
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByConditionPrint(CustInfoModel CustModel, string ord)
        {
            try
            {
                string sql = "  select " +
                                   " ci.ID,ci.CustNo,ci.CustName," +
                               " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' else '' end) CustTypeManage," +
                               " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' else '' end) CustTypeSell," +
                               " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' else '' end) CustTypeTime," +
                                   " ci.CustType,isnull(cp.TypeName,'') CustTypeName," +
                                   " ci.CustClass,isnull(cc.CodeName,'') CustClassName," +
                                   " ci.CreditGrade,isnull(cp2.TypeName,'') CreditGradeName," +
                               " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end) RelaGrade," +
                                   " ci.AreaID,isnull(cp3.TypeName,'') Area,ci.Province,ci.City," +
                                   " ci.Manager,isnull(ei.EmployeeName,'') ManagerName,ci.ContactName,ci.Tel," +
                                   " ci.Creator,isnull(ei2.EmployeeName,'') CreatorName,CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate" +
                               " from " +
                                   " officedba.CustInfo ci" +
                               " left join officedba.CodePublicType cp on ci.CustType = cp.id" +
                               " left join officedba.CodeCompanyType cc on ci.CustClass = cc.id" +
                               " left join officedba.CodePublicType cp2 on ci.CreditGrade = cp2.id" +
                               " left join officedba.CodePublicType cp3 on ci.AreaID = cp3.id" +
                               " left join officedba.EmployeeInfo ei on ci.Manager = ei.id" +
                               " left join officedba.EmployeeInfo ei2 on ci.Creator = ei2.id" +
                               " where ci.CompanyCD = '" + CustModel.CompanyCD + "'";

                if (CustModel.CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustModel.CustTypeManage + "'";
                if (CustModel.CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustModel.CustTypeSell + "'";
                if (CustModel.CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustModel.CustTypeTime + "'";
                if (CustModel.CustType != 0)
                    sql += " and ci.CustType='" + CustModel.CustType + "'";
                if (CustModel.CustClass != 0)
                    sql += " and ci.CustClass='" + CustModel.CustClass + "'";
                if (CustModel.CreditGrade != 0)
                    sql += " and ci.CreditGrade='" + CustModel.CreditGrade + "'";
                if (CustModel.RelaGrade != "0")
                    sql += " and ci.RelaGrade='" + CustModel.RelaGrade + "'";
                if (CustModel.AreaID != 0)
                    sql += " and ci.AreaID='" + CustModel.AreaID + "'";
                if (CustModel.Seller != 0)
                    sql += " and ci.Manager='" + CustModel.Seller + "'";

                sql = sql + ord;

                //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户管理分类统计
        /// <summary>
        /// 按客户分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeManage(string CompanyCD, string ord)
        {
            try
            {
                string sql = "  select " +
                                   " (case CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeName ," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeManage <> 0 and CompanyCD = '" + CompanyCD + "'" +
                                   " group by CustTypeManage order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按客户分类对比
        /// <summary>
        /// 根据条件 取客户管理分类类型与客户数
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustType"></param>
        /// <param name="CustClass"></param>
        /// <param name="Area"></param>
        /// <param name="RelaGrade"></param>
        /// <param name="CreditGrade"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeManageNew(string CompanyCD, string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "  select (case CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeName ," + 
                                   " CustTypeManage CustTypeID," +//CustTypeManageID," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeManage <> 0 and CompanyCD = '" + CompanyCD + "'";
                if (CustType != "0")
                    sql += " and CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and CreatedDate <= '" + DateEnd + "'";

                sql += " group by CustTypeManage ";
                                  
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByTypeManageNewList(string CompanyCD,string CustSeleTypeID, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade,string CreditGrade,string BeginDate,string EndDate,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                                   " ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade,"+
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci." + CustSeleType + " = '" + CustSeleTypeID + "' ";
                              // " and (ci.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = ci.Creator or '" + CanUserID + "' = ci.Manager or ci.CanViewUser = ',,')";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        #region 判断是否能删除
        public static bool isdel(string[] CustID)
        {
            int num = 0;
            if (CustID.Length > 0)
            {
                for (int i = 0; i < CustID.Length; i++)
                {
//                    string sql = @"select id from officedba.SellChance where custid="+CustID[i]+@" union
//                                select id from officedba. SellOffer where custid="+CustID[i]+@" union
//                                select id from officedba. SellContract where custid="+CustID[i]+@" union
//                                select id from officedba. SellOrder where custid="+CustID[i]+@" union
//                                select id from officedba. SellSend where custid="+CustID[i]+@" union
//                                select id from officedba. SellGathering where custid="+CustID[i]+@" union 
//                                select id from officedba.SellBack where custid="+CustID[i]+@"
//                                select id from officedba.StorageInOther where  OtherCorpID="+CustID[i]+@" and CorpBigType=1 union
//                                select id from officedba.StorageOutOther where  OtherCorpID="+CustID[i]+@" and CorpBigType=1 ";
                    string sql = @"select id from dbo.ContractHead_Sale where cCusCode=" + CustID[i];
                    DataTable dt = new DataTable();
                    dt = SqlHelper.ExecuteSql(sql);
                    if (dt.Rows.Count > 0)
                    {
                        num++;
                        break;
                    }
                }
            }
            if (num == 0)
                return true;
            else
                return false;
        }
        #endregion
        //根据客户类型id、客户分类类型等条件检索客户列表信息--导出用
        public static DataTable GetCustListByTypeManageNewList(string CompanyCD, string CustSeleTypeID, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                                   " ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci." + CustSeleType + " = '" + CustSeleTypeID + "' ";
                // " and (ci.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = ci.Creator or '" + CanUserID + "' = ci.Manager or ci.CanViewUser = ',,')";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByTypeManageNewDetail(string CompanyCD, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,(ci." + CustSeleType + ") CustTypeCode, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                                   " ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";
                // " and (ci.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = ci.Creator or '" + CanUserID + "' = ci.Manager or ci.CanViewUser = ',,')";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }


        //根据条件 取客户销售分类类型与客户数
        public static DataTable GetCustListByTypeSellNew(string CompanyCD, string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "  select (case CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeName , " +
                                    " CustTypeSell CustTypeID, " + //CustTypeSellID," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeSell <> 0 and CompanyCD = '" + CompanyCD + "'";
                if (CustType != "0")
                    sql += " and CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and CreatedDate <= '" + DateEnd + "'";

                sql += " group by CustTypeSell ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        
        //根据条件 取客户时间分类类型与客户数
        public static DataTable GetCustListByTypeTimeNew(string CompanyCD, string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "  select (case CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeName , " +
                                     " CustTypeTime CustTypeID," + //CustTypeTimeID," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeTime <> 0 and CompanyCD = '" + CompanyCD + "'";
                if (CustType != "0")
                    sql += " and CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and CreatedDate <= '" + DateEnd + "'";

                sql += " group by CustTypeTime ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按客户营销分类统计
        /// <summary>
        /// 按客户营销分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeSell(string CompanyCD, string ord)
        {
            try
            {
                string sql = "  select " +
                                   " (case CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeName ," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeSell <> 0 and CompanyCD = '" + CompanyCD + "'" +
                                   " group by CustTypeSell order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按客户时间分类统计
        /// <summary>
        /// 按客户时间分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeTime(string CompanyCD, string ord)
        {
            try
            {
                string sql = "  select " +
                                   " (case CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeName ," +
                                    " count(*) num" +
                                   " from " +
                                       " officedba.CustInfo " +
                                  " where CustTypeTime <> 0 and CompanyCD = '" + CompanyCD + "'" +
                                   " group by CustTypeTime order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按客户类别分类统计
        /// <summary>
        /// 按客户类别分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByType(string CompanyCD, string ord)
        {
            try
            {
                string sql = "select a.CustType,cp.TypeName,a.num" +
                           " from " +
                               " (select	ci.CustType,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' " +
                               " group by ci.CustType) a " +
                           " left join officedba.CodePublicType cp on cp.id = a.CustType where cp.TypeName is not null order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户类别对比
        public static DataTable GetCustListByTypeNew(string CompanyCD, string CustTypeManage,string CustTypeSell,string CustTypeTime,string CustClass,string Area,string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "select cp.TypeName,a.CustType,a.num" +
                           " from " +
                               " (select ci.CustType,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' and ci.CustType is not null and ci.CustType <> 0 ";
                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and ci.CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and ci.CreatedDate <= '" + DateEnd + "'";

                sql += " group by ci.CustType) a " +
                           " left join officedba.CodePublicType cp on cp.id = a.CustType ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        
        //根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByTypeNewList(string CompanyCD, string CustTypeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                                   //" ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +                              
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CustType = '" + CustTypeID + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息 导出用
        public static DataTable GetCustListByTypeNewList(string CompanyCD, string CustTypeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                    //" ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                    //" left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CustType = '" + CustTypeID + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByTypeNewDetail(string CompanyCD,  string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustType, " +
                                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                    //" ci.CustType,isnull(cp.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                    //" left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        
        #endregion

        #region 按客户细分统计
        /// <summary>
        /// 按客户细分统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByClass(string CompanyCD, string ord)
        {
            try
            {
                string sql = "select" +
                               " cc.CodeName," +
                               " count(ci.CustClass) num" +
                           " from " +
                           " officedba.CustInfo ci" +
                           " inner join officedba.CodeCompanyType cc on cc.id = ci.CustClass" +
                           " where ci.CustClass <> 0 and ci.CompanyCD = '" + CompanyCD + "'" +
                           " group by cc.CodeName order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户细分对比
        public static DataTable GetCustListByClassNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "select cp.CodeName,a.CustClass,a.num" +
                           " from " +
                               " (select ci.CustClass,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' and ci.CustClass is not null and ci.CustClass <> 0 ";
                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and ci.CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and ci.CreatedDate <= '" + DateEnd + "'";

                sql += " group by ci.CustClass) a " +
                           " left join officedba.CodeCompanyType cp on cp.id = a.CustClass ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByClassNewList(string CompanyCD, string CustClassID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +                   
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                    //" left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CustClass = '" + CustClassID + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息 导出用
        public static DataTable GetCustListByClassNewList(string CompanyCD, string CustClassID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                    //" left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CustClass = '" + CustClassID + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByClassNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustClass, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                    //" left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按客户所在区域统计
        /// <summary>
        /// 按客户所在区域统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByArea(string CompanyCD, string ord)
        {
            try
            {
                string sql = "select " +
                               " cp.TypeName," +
                               " count(ci.AreaID) num" +
                            " from " +
                            " officedba.CustInfo ci" +
                            " left join officedba.CodePublicType cp on cp.id = ci.AreaID" +
                            " where ci.AreaID <> 0 and cp.TypeName is not null  and ci.CompanyCD = '" + CompanyCD + "'" +
                            " group by cp.TypeName  order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户所在区域统计
        public static DataTable GetCustListByAreaNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "select cp.TypeName,a.AreaID,a.num" +
                           " from " +
                               " (select ci.AreaID,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' and ci.AreaID is not null and ci.AreaID <> 0 ";
                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and ci.CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and ci.CreatedDate <= '" + DateEnd + "'";

                sql += " group by ci.AreaID) a " +
                           " left join officedba.CodePublicType cp on cp.id = a.AreaID ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户所在区域统计后列表
        public static DataTable GetCustListByAreaNewList(string CompanyCD, string AreaID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   //" ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               //" left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.AreaID = '" + AreaID + "' and ci.AreaID is not null and ci.AreaID <> 0 ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户所在区域统计后列表 导出用
        public static DataTable GetCustListByAreaNewList(string CompanyCD, string AreaID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                    //" ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                    //" left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.AreaID = '" + AreaID + "' and ci.AreaID is not null and ci.AreaID <> 0 ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByAreaNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.AreaID, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                    //" ci.AreaID,isnull(cpa.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                    //" left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.AreaID is not null and ci.AreaID <> 0 ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        
        #endregion

        #region 按客户关系等级统计
        /// <summary>
        /// 按客户关系等级统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByRelaGrade(string CompanyCD, string ord)
        {
            try
            {
                string sql = "select (case RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end) RelaGrade," +
                                   " count(*)num from officedba.CustInfo " +
                               " where CompanyCD = '" + CompanyCD + "' and RelaGrade <> 0 " +
                               " group by RelaGrade order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户关系等级统计
        public static DataTable GetCustListByRelaGradeNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string Area, string CreditGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "select (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end ) RelaGradeName,ci.RelaGrade," +
                               " count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' and ci.RelaGrade is not null and ci.RelaGrade <> 0 ";
                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (DateBegin != "")
                    sql += " and ci.CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and ci.CreatedDate <= '" + DateEnd + "'";

                sql += " group by ci.RelaGrade ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户关系等级统计后列表
        public static DataTable GetCustListByRelaGradeNewList(string CompanyCD, string RelaGrade, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string Area, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   //" (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.RelaGrade = '" + RelaGrade + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户关系等级统计后列表 导出用
        public static DataTable GetCustListByRelaGradeNewList(string CompanyCD, string RelaGrade, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string Area, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                    //" (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.RelaGrade = '" + RelaGrade + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByRelaGradeNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string Area, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.RelaGrade, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                    //" (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        
        #endregion

        #region 按客户优质级别统计
        /// <summary>
        /// 按客户优质级别统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByCreditGrade(string CompanyCD, string ord)
        {
            try
            {
                string sql = "select  " +
                               " cp.TypeName, " +
                               " count(ci.CreditGrade) num " +
                            " from  " +
                            " officedba.CustInfo ci " +
                            " left join officedba.CodePublicType cp on cp.id = ci.CreditGrade " +
                            " where ci.CreditGrade <> 0 and ci.CompanyCD = '" + CompanyCD + "'" +
                            " group by cp.TypeName order by " + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户优质级别统计
        public static DataTable GetCustListByCreditGradeNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string Area, string RelaGrade, string DateBegin, string DateEnd)
        {
            try
            {
                string sql = "select cp.TypeName,a.CreditGrade,a.num" +
                           " from " +
                               " (select ci.CreditGrade,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "' and ci.CreditGrade is not null and ci.CreditGrade <> 0 ";
                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (DateBegin != "")
                    sql += " and ci.CreatedDate >= '" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and ci.CreatedDate <= '" + DateEnd + "'";

                sql += " group by ci.CreditGrade) a " +
                           " left join officedba.CodePublicType cp on cp.id = a.CreditGrade ";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户优质级别统计后列表
        public static DataTable GetCustListByCreditGradeNewList(string CompanyCD, string CreditGradeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string Area, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   //" isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               //" left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CreditGrade = '" + CreditGradeID + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按客户优质级别列表 导出用
        public static DataTable GetCustListByCreditGradeNewList(string CompanyCD, string CreditGradeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
                    string RelaGrade, string Area, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                    //" isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                    //" left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and ci.CreditGrade = '" + CreditGradeID + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByCreditGradeNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
                    string RelaGrade, string Area, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CreditGrade, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                   " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                    //" isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                               " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                    //" left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        #endregion

        #region 按时间统计
        public static DataTable GetCustListByTime(string CompanyCD,string DateSele, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string Area, string RelaGrade,string CreditGrade, string DateBegin, string DateEnd)
        {
            string ColumnName = "";
            if (DateSele == "1")
            {
                //ColumnName = "datename(yyyy,ci.CreatedDate)+'年'";
                ColumnName = "datename(yyyy,ci.CreatedDate)";
            }
            if (DateSele == "2")
            {
                //ColumnName = "datename(yyyy,ci.CreatedDate)+'年'+datename(mm,ci.CreatedDate)+'月'";
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(mm,ci.CreatedDate)+'月'";
            }
            if (DateSele == "3")
            {
                //ColumnName = "datename(yyyy,ci.CreatedDate)+'年第'+datename(week,ci.CreatedDate)+'周'";
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(week,ci.CreatedDate)+'周'";
            }
            string sql = "select (" + ColumnName + ") t,count(*) num " +
                               " from " +
                                   " officedba.CustInfo ci where ci.CompanyCD = '" + CompanyCD + "'";
            if (CustTypeManage != "0")
                sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
            if (CustTypeSell != "0")
                sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
            if (CustTypeTime != "0")
                sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";

            if (CustType != "0")
                sql += " and ci.CustType = '" + CustType + "'";
            if (CustClass != "")
                sql += " and ci.CustClass = '" + CustClass + "'";
            if (RelaGrade != "0")
                sql += " and ci.RelaGrade = '" + RelaGrade + "'";
            if (Area != "0")
                sql += " and ci.AreaID = '" + Area + "'";
            if (CreditGrade != "0")
                sql += " and ci.CreditGrade = '" + CreditGrade + "'";

            if (DateBegin != "")
                sql += " and ci.CreatedDate >= '" + DateBegin + "'";
            if (DateEnd != "")
                sql += " and ci.CreatedDate <= '" + DateEnd + "'";

            sql += " group by " + ColumnName;

            return SqlHelper.ExecuteSql(sql);            
        }

        //按时间统计后列表
        public static DataTable GetCustListByTimeNewList(string CompanyCD,string DateSele,string DateTimeID, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string ColumnName = "";
            if (DateSele == "1")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)";
            }
            if (DateSele == "2")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(mm,ci.CreatedDate)+'月'";
            }
            if (DateSele == "3")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(week,ci.CreatedDate)+'周'";
            }

            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,ci.CustBig, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                    " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                    " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and " + ColumnName + "= '" + DateTimeID + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //按时间统计后列表 导出用
        public static DataTable GetCustListByTimeNewList(string CompanyCD, string DateSele, string DateTimeID, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            string ColumnName = "";
            if (DateSele == "1")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)";
            }
            if (DateSele == "2")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(mm,ci.CreatedDate)+'月'";
            }
            if (DateSele == "3")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(week,ci.CreatedDate)+'周'";
            }

            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                    " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                    " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' and " + ColumnName + "= '" + DateTimeID + "'";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustListByTimeNewDetail(string CompanyCD, string DateSele, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            string ColumnName = "";
            if (DateSele == "1")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)";
            }
            if (DateSele == "2")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(mm,ci.CreatedDate)+'月'";
            }
            if (DateSele == "3")
            {
                ColumnName = "datename(yyyy,ci.CreatedDate)+'-'+datename(week,ci.CreatedDate)+'周'";
            }

            try
            {
                string sql = "  select distinct " +
                                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort,(" + ColumnName + ") t," + 
                                   " ci.CustType,isnull(cc.TypeName,'') TypeName," +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell," +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                    " ci.AreaID,isnull(are.TypeName,'') Area," +
                                   " ci.CustClass,isnull(cpa.CodeName,'') CustClassName," +
                                   "  isnull(ei.EmployeeName,'') Manager," +
                                   " isnull(cpc.TypeName,'') CreditGrade," +
                                   " (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' else '' end )RelaGrade," +
                                   " isnull(eic.EmployeeName,'') Creator," +
                                   " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus,ci.ModifiedDate " +
                               " from " +
                               " officedba.CustInfo ci " +
                               " left join officedba.CodePublicType cc on cc.id = ci.CustType " +
                               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                    " left join officedba.CodePublicType are on are.id = ci.AreaID" +
                               " left join officedba.CodeCompanyType cpa on cc.id = ci.CustClass " +
                               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                               " where ci.CompanyCD = '" + CompanyCD + "' ";

                if (CustTypeManage != "0")
                    sql += " and ci.CustTypeManage = '" + CustTypeManage + "'";
                if (CustTypeSell != "0")
                    sql += " and ci.CustTypeSell = '" + CustTypeSell + "'";
                if (CustTypeTime != "0")
                    sql += " and ci.CustTypeTime = '" + CustTypeTime + "'";
                if (CustType != "0")
                    sql += " and ci.CustType = '" + CustType + "'";
                if (CustClass != "")
                    sql += " and ci.CustClass = '" + CustClass + "'";
                if (Area != "0")
                    sql += " and ci.AreaID = '" + Area + "'";
                if (RelaGrade != "0")
                    sql += " and ci.RelaGrade = '" + RelaGrade + "'";
                if (CreditGrade != "0")
                    sql += " and ci.CreditGrade = '" + CreditGrade + "'";
                if (BeginDate != "")
                    sql += " and ci.CreatedDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ci.CreatedDate <= '" + EndDate + "'";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        #endregion

        #region 按订单量统计
        /// <summary>
        /// 按订单量统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="NumBegin"></param>
        /// <param name="NumEnd"></param>
        /// <param name="PriceBegin"></param>
        /// <param name="PriceEnd"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustOrders(string CompanyCD,string ProductID,string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select c.ProductCount,c.totalFee,c.CustName,c.CustNo,c.id,c.CustBig  " +
                               " from " +
                                   "  (select 	sum(b.ProductCount) ProductCount,sum(b.totalFee) totalFee " +
                                       "  ,ci.CustName,ci.CustNo,ci.id,ci.CustBig " +
                                   "  from  " +
                                        " ( " +
                                        " select a.OrderNo,a.ProductID,a.ProductCount,a.TotalFee " +
                                               "  ,so.CustID,so.orderDate  " +
                                        " from  " +
                                            " (select sod.OrderNo,sod.ProductID,sum(sod.ProductCount) ProductCount,sum(sod.TotalFee) TotalFee " +
                                            " from officedba. SellOrderDetail sod " +
                                            " where sod.CompanyCD = '" + CompanyCD + "'";
                if (ProductID != "")
                    sql += " and sod.ProductID = '" + ProductID + "'";

                sql += " group by sod.OrderNo,sod.ProductID) a		 " +
                              " left join officedba.SellOrder so on a.OrderNo = so.OrderNo and so.CompanyCD = '" + CompanyCD + "' " +
                              " ) b  " +
                          " left join officedba.CustInfo ci on ci.ID = b.CustID  " +
                          " where 1=1     ";
                if (CustID != "")
                    sql += " and b.CustID = '" + CustID + "'";
                if (DateBegin != "")
                    sql += " and b.orderdate >='" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and b.orderdate<='" + DateEnd + "'";

                sql += " group by ci.CustName,ci.CustNo,ci.id,ci.CustBig ) c " +
                                " where 1=1  ";
                if (NumBegin != "")
                    sql += " and c.ProductCount >= " + NumBegin;
                if (NumEnd != "")
                    sql += " and c.ProductCount <= " + NumEnd;
                if (PriceBegin != "")
                    sql += " and c.totalFee >= " + PriceBegin;
                if (PriceEnd != "")
                    sql += " and c.totalFee <= " + PriceEnd;

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        //导出
        public static DataTable GetCustOrders(string CompanyCD, string ProductID, string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd,  string ord)
        {
            try
            {
                string sql = "select c.ProductCount,c.totalFee,c.CustName,c.CustNo  " +
                               " from " +
                                   "  (select 	sum(b.ProductCount) ProductCount,sum(b.totalFee) totalFee " +
                                       "  ,ci.CustName,ci.CustNo " +
                                   "  from  " +
                                        " ( " +
                                        " select a.OrderNo,a.ProductID,a.ProductCount,a.TotalFee " +
                                               "  ,so.CustID,so.orderDate  " +
                                        " from  " +
                                            " (select sod.OrderNo,sod.ProductID,sum(sod.ProductCount) ProductCount,sum(sod.TotalFee) TotalFee " +
                                            " from officedba. SellOrderDetail sod " +
                                            " where sod.CompanyCD = '" + CompanyCD + "'";
                if (ProductID != "")
                    sql += " and sod.ProductID = '" + ProductID + "'";

                sql += " group by sod.OrderNo,sod.ProductID) a		 " +
                              " left join officedba.SellOrder so on a.OrderNo = so.OrderNo and so.CompanyCD = '" + CompanyCD + "' " +
                              " ) b  " +
                          " left join officedba.CustInfo ci on ci.ID = b.CustID  " +
                          " where 1=1     ";
                if (CustID != "")
                    sql += " and b.CustID = '" + CustID + "'";
                if (DateBegin != "")
                    sql += " and b.orderdate >='" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and b.orderdate<='" + DateEnd + "'";

                sql += " group by ci.CustName,ci.CustNo ) c " +
                                " where 1=1  ";
                if (NumBegin != "")
                    sql += " and c.ProductCount >= " + NumBegin;
                if (NumEnd != "")
                    sql += " and c.ProductCount <= " + NumEnd;
                if (PriceBegin != "")
                    sql += " and c.totalFee >= " + PriceBegin;
                if (PriceEnd != "")
                    sql += " and c.totalFee <= " + PriceEnd;

                sql += ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按订单量统计打印
        /// <summary>
        /// 按订单量统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="NumBegin"></param>
        /// <param name="NumEnd"></param>
        /// <param name="PriceBegin"></param>
        /// <param name="PriceEnd"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <returns></returns>
        public static DataTable GetCustOrdersPrint(string CompanyCD, string ProductID, string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, string ord)
        {
            try
            {
                string sql = "select c.ProductCount num,c.totalFee TotalPrice,c.CustName,c.CustNo  " +
                               " from " +
                                   "  (select 	sum(b.ProductCount) ProductCount,sum(b.totalFee) totalFee " +
                                       "  ,ci.CustName,ci.CustNo " +
                                   "  from  " +
                                        " ( " +
                                        " select a.OrderNo,a.ProductID,a.ProductCount,a.TotalFee " +
                                               "  ,so.CustID,so.orderDate  " +
                                        " from  " +
                                            " (select sod.OrderNo,sod.ProductID,sum(sod.ProductCount) ProductCount,sum(sod.TotalFee) TotalFee " +
                                            " from officedba. SellOrderDetail sod " +
                                            " where sod.CompanyCD = '" + CompanyCD + "'";
                if (ProductID != "")
                    sql += " and sod.ProductID = '" + ProductID + "'";

                sql += " group by sod.OrderNo,sod.ProductID) a		 " +
                              " left join officedba.SellOrder so on a.OrderNo = so.OrderNo and so.CompanyCD = '" + CompanyCD + "' " +
                              " ) b  " +
                          " left join officedba.CustInfo ci on ci.ID = b.CustID  " +
                          " where 1=1     ";
                if (CustID != "")
                    sql += " and b.CustID = '" + CustID + "'";
                if (DateBegin != "")
                    sql += " and b.orderdate >='" + DateBegin + "'";
                if (DateEnd != "")
                    sql += " and b.orderdate<='" + DateEnd + "'";

                sql += " group by ci.CustName,ci.CustNo ) c " +
                                " where 1=1  ";
                if (NumBegin != "")
                    sql += " and c.ProductCount >= " + NumBegin;
                if (NumEnd != "")
                    sql += " and c.ProductCount <= " + NumEnd;
                if (PriceBegin != "")
                    sql += " and c.totalFee >= " + PriceBegin;
                if (PriceEnd != "")
                    sql += " and c.totalFee <= " + PriceEnd;

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion 

        #region  按购买物品统计_报表
        /// <summary>
        /// 按购买物品统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="MinCount">数量最小值</param>
        /// <param name="MaxCount">数量最大值</param>
        /// <param name="MinPrice">金额最小值</param>
        /// <param name="MaxPrice">金额最大值</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByProduct(string ProductName,string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {              
                string sql = "select b.ProductCount,b.TotalFee,p.ProdNo,p.ProductName from " +
                            "(select a.ProductID," +
                                " sum(a.ProductCount) ProductCount,sum(a.TotalFee) TotalFee " +
                             " from " +
                               "   (select so.OrderNo,so.OrderDate, " +
                                 "     sod.ProductID,sod.ProductCount,sod.TotalFee " +
                                 " from officedba. SellOrder so " +
                                 " left join officedba. SellOrderDetail sod on sod.OrderNo = so.OrderNo  and sod.CompanyCD = '" + CompanyCD + "' " +
                                 " where so.CompanyCD = '" + CompanyCD + "' and so.BillStatus='2' ";
                if (ProductName != "")
                    sql += " and sod.ProductID = '" + ProductName + "' ";
                if (LinkDateBegin != "")
                    sql += " and so.OrderDate >='" + LinkDateBegin + "' ";
                if (LinkDateEnd != "")
                    sql += " and so.OrderDate <='" + LinkDateEnd + "' ";
                if (CustID != "")
                    sql += " and so.CustID = '" + CustID + "'";

                sql += " ) a  where 1=1  ";

                if (MinCount != "")
                    sql += " and a.ProductCount >= " + MinCount;
                if (MaxCount != "")
                    sql += " and a.ProductCount <= " + MaxCount;
                if (MinPrice != "")
                    sql += " and a.TotalFee >= " + MinPrice;
                if (MaxPrice != "")
                    sql += " and a.TotalFee <= " + MaxPrice;

                sql += " group by a.ProductID) b left join officedba.ProductInfo p on p.id = b.productId";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        //导出
        public static DataTable GetStatCustBuyByProduct(string ProductName, string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                string sql = "select b.ProductCount,b.TotalFee,p.ProdNo,p.ProductName from " +
                            "(select a.ProductID," +
                                " sum(a.ProductCount) ProductCount,sum(a.TotalFee) TotalFee " +
                             " from " +
                               "   (select so.OrderNo,so.OrderDate, " +
                                 "     sod.ProductID,sod.ProductCount,sod.TotalFee " +
                                 " from officedba. SellOrder so " +
                                 " left join officedba. SellOrderDetail sod on sod.OrderNo = so.OrderNo  and sod.CompanyCD = '" + CompanyCD + "' " +
                                 " where so.CompanyCD = '" + CompanyCD + "' and so.BillStatus='2' ";
                if (ProductName != "")
                    sql += " and sod.ProductID = '" + ProductName + "' ";
                if (LinkDateBegin != "")
                    sql += " and so.OrderDate >='" + LinkDateBegin + "' ";
                if (LinkDateEnd != "")
                    sql += " and so.OrderDate <='" + LinkDateEnd + "' ";
                if (CustID != "")
                    sql += " and so.CustID = '" + CustID + "'";

                sql += " ) a  where 1=1  ";

                if (MinCount != "")
                    sql += " and a.ProductCount >= " + MinCount;
                if (MaxCount != "")
                    sql += " and a.ProductCount <= " + MaxCount;
                if (MinPrice != "")
                    sql += " and a.TotalFee >= " + MinPrice;
                if (MaxPrice != "")
                    sql += " and a.TotalFee <= " + MaxPrice;

                sql += " group by a.ProductID) b left join officedba.ProductInfo p on p.id = b.productId";
                sql += ord;

                return SqlHelper.ExecuteSql(sql);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 按购买物品统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="MinCount">数量最小值</param>
        /// <param name="MaxCount">数量最大值</param>
        /// <param name="MinPrice">金额最小值</param>
        /// <param name="MaxPrice">金额最大值</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByProductPrint(string ProductName,string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
               
                string sql = "select b.ProductCount,b.TotalFee TotalPrice,p.ProdNo,p.ProductName from " +
                            "(select a.ProductID," +
                                " sum(a.ProductCount) ProductCount,sum(a.TotalFee) TotalFee " +
                             " from " +
                               "   (select so.OrderNo,so.OrderDate, " +
                                 "     sod.ProductID,sod.ProductCount,sod.TotalFee " +
                                 " from officedba. SellOrder so " +
                                 " left join officedba. SellOrderDetail sod on sod.OrderNo = so.OrderNo  and sod.CompanyCD = '" + CompanyCD + "' " +
                                 " where so.CompanyCD = '" + CompanyCD + "' and so.BillStatus='2' ";
                if (ProductName != "")
                    sql += " and sod.ProductID = '" + ProductName + "' ";
                if (LinkDateBegin != "")
                    sql += " and so.OrderDate >='" + LinkDateBegin + "' ";
                if (LinkDateEnd != "")
                    sql += " and so.OrderDate <='" + LinkDateEnd + "' ";
                if (CustID != "")
                    sql += " and so.CustID = '" + CustID + "'";

                sql += " ) a  where 1=1  ";

                if (MinCount != "")
                    sql += " and a.ProductCount >= " + MinCount;
                if (MaxCount != "")
                    sql += " and a.ProductCount <= " + MaxCount;
                if (MinPrice != "")
                    sql += " and a.TotalFee >= " + MinPrice;
                if (MaxPrice != "")
                    sql += " and a.TotalFee <= " + MaxPrice;

                sql += " group by a.ProductID) b left join officedba.ProductInfo p on p.id = b.productId" + " Order by " + ord;
              
                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region  按购买日期统计_报表
        /// <summary>
        /// 按购买日期统计
        /// </summary>
        /// <param name="ProductName">物品名称</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDate(string ProductName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.id,c.CustBig,c.CustName,b.ProdNO,b.ProductName,a.OrderDate,a.ProductCount,a.TotalPrice from( ");
                sql.Append(" select a.ProductId,sum(a.ProductCount) ProductCount,sum(a.TotalFee) TotalPrice,b.CustId, ");
                sql.Append(" isnull(CONVERT(varchar(100),b.OrderDate, 23),'') OrderDate from  ");
                sql.Append(" officedba. SellOrderDetail a inner join officedba. SellOrder b on a.OrderNO=b.OrderNO and a.CompanyCD = b.CompanyCD  where 1=1  and b.BillStatus!=1 ");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (LinkDateBegin != "")
                {
                    sql.Append(" and b.OrderDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd != "")
                {
                    sql.Append(" and b.OrderDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by a.ProductId,b.CustId,b.OrderDate)a inner join officedba.productInfo b on a.ProductId=b.Id  ");
                sql.Append(" inner join officedba.CustInfo c on a.custId=c.Id where 1=1 ");

                if (ProductName != "")
                {
                    sql.Append(" and b.Id=");
                    sql.Append(ProductName);
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 按购买日期统计
        /// </summary>
        /// <param name="ProductName">物品名称</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDatePrint(string ProductName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.ProdNO,b.ProductName,a.OrderDate,a.ProductCount,a.TotalPrice from( ");
                sql.Append(" select a.ProductId,sum(a.ProductCount) ProductCount,sum(a.TotalFee) TotalPrice,b.CustId, ");
                sql.Append(" isnull(CONVERT(varchar(100),b.OrderDate, 23),'') OrderDate from  ");
                sql.Append(" officedba. SellOrderDetail a inner join officedba. SellOrder b on a.OrderNO=b.OrderNO and a.CompanyCD = b.CompanyCD  where 1=1  and b.BillStatus!=1 ");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (LinkDateBegin != "")
                {
                    sql.Append(" and b.OrderDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd != "")
                {
                    sql.Append(" and b.OrderDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by a.ProductId,b.CustId,b.OrderDate)a inner join officedba.productInfo b on a.ProductId=b.Id  ");
                sql.Append(" inner join officedba.CustInfo c on a.custId=c.Id where 1=1 ");

                if (ProductName != "")
                {
                    sql.Append(" and b.Id=");
                    sql.Append(ProductName);
                }

                sql.Append("Order by ");
                sql.Append(ord);
                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }

        }
        #endregion

        #region  零购买客户统计_报表
        /// <summary>
        /// 零购买客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDays(string CompanyCD, int Days, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.id,a.CustBig,a.CustName,f.TypeName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" left join officedba.CodePublicType f on a.LinkCycle=f.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.SellOrder where 1=1  and BillStatus!=1  ");
                if (Days.ToString() != "")
                {
                    sql.Append(" and OrderDate>dateadd(dd,-");
                    sql.Append(Days);
                    sql.Append(",getdate())");
                }
                sql.Append("  ) ");

                if (CompanyCD != "")
                {
                    sql.Append("and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("'");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 零购买客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDaysPrint(string CompanyCD, int Days, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,f.TypeName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" left join officedba.CodePublicType f on a.LinkCycle=f.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.SellOrder where 1=1  and BillStatus!=1  ");
                if (Days.ToString() != "" && Days.ToString()!="0")
                {
                    sql.Append(" and OrderDate>dateadd(dd,-");
                    sql.Append(Days);
                    sql.Append(",getdate())");
                }
                sql.Append("  ) ");

                if (CompanyCD != "")
                {
                    sql.Append("and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("'");
                }

                sql.Append("Order by ");
                sql.Append(ord);
                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户信息打印
        /// <summary>
        /// 客户信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <returns></returns>
        public static DataTable GetCustInfoByNo(string CompanyCD,string CustBig, string CustNo)
        {
            try
            {
                string sql = "";

                if (CustBig == "2")
                {
                    #region 带联系人的客户信息打印
                    sql = "SELECT ci.ID," +
                                    "ci.CompanyCD,(case ci.CustBig when '2' then '会员' else '企业' end)BigType,ci.CustNo,ci.CustName,ci.CustNum," +
                                    "ci.CustClass,cct.CodeName CustClassName,ci.CustType,ctt.TypeName CustTypaNm," +
                                    "(case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                    "(case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户'  when '4' then '道德型客户' end)CustTypeSell ," +
                                    "(case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                    "ci.CreditGrade,cp.TypeName CreditGradeNm,ci.CustShort,ci.CustNote,ci.Province,ci.AreaID,ci.ReceiveAddress," +
                                     "(case ci.BusiType when '1' then '普通销售' when '2' then '委托代销' when '3' then '直运' when '4' then '零售' when '5' then '销售调拨' end) BusiType," +
                                     "ci.Manager,em.EmployeeName ManagerName,ci.LinkCycle,cpl.TypeName LinkCycleNm,ci.Creator,ei.EmployeeName CreatorName," +
                                     "(case ci.CreditManage when '1' then '否' when '2' then '是' end) CreditManage,ci.ModifiedUserID," +
                                      "(case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end)RelaGrade," +
                                      "ci.MaxCreditDate,ci.PayType,cpp.TypeName PayTypeNm,ci.CanViewUser,ci.CanViewUserName," +
                                     "(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus," +
                                     "CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate," +
                                    "CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate," +
                                    "(case cl.Sex when '1' then '男' else '女' end)Sex,cl.LinkType,a.TypeName LinkTypeName,cl.PaperNum,Convert(varchar(20),cl.Birthday,23) Birthday," +
                                    "cl.WorkTel,cl.Handset,cl.Fax,cl.Position,cl.Age,cl.MailAddress,cl.Post,cl.HomeTown," +
                                    " cl.NationalID,b.TypeName NationalName,cl.CultureLevel,c.TypeName CultureLevelName,cl.Professional,d.TypeName ProfessionalName,"+
                                    " cl.IncomeYear,cl.FuoodDrink,cl.Appearance,cl.AdoutBody,cl.AboutFamily,cl.Car,cl.CanViewUser,cl.CanViewUserName," +
                                    " cl.LoveMusic,cl.LoveColor,cl.LoveSmoke,cl.LoveDrink,cl.LoveTea,cl.LoveBook,cl.LoveSport,cl.LoveClothes,cl.Cosmetic,cl.Nature," +

                                    "CONVERT(varchar(100), ci.FirstBuyDate, 23) FirstBuyDate ,ci.CountryID,cpc.TypeName CountryName," +
                                    "ci.BigType,ci.CustNam,cpa.TypeName AreaName,ci.City,ci.Tel,ci.Fax,ci.OnLine,ci.WebSite,ci.Post," +  
                                    "(case ci.HotIs when '1' then '是' when '2' then '否' end) HotIs," +
                                    "(case ci.HotHow when '1' then '低热' when '2' then '中热' when '3' then '高热' end)HotHow," +
                                    "(case ci.MeritGrade when '1' then '高' when '2' then '中' when '3' then '低' end)MeritGrade,ci.Relation," +
                                    "(case ci.CompanyType when '1' then '事业' when '2' then '企业' when '3' then '社团' when '4' then '自然人' when '5' then '其他' end) CompanyType," +
                                    "ci.StaffCount,ci.Source,ci.Phase,ci.CustSupe,ci.Trade,CONVERT(varchar(100), ci.SetupDate, 23) SetupDate," +
                                    "ci.ArtiPerson,ci.SetupMoney,ci.SetupAddress,ci.CapitalScale,ci.SaleroomY,ci.ProfitY,ci.TaxCD,ci.BusiNumber," +
                                    "(case ci.IsTax when '0' then '否' when '1' then '是' end) IsTax,ci.SellArea,ci.SellMode,ci.ContactName,ci.Mobile,ci.email," +
                                    "ci.TakeType,cpt.TypeName TakeTypeNm," +
                                    "ci.CarryType,cpca.TypeName CarryTypeNm," +                                   
                                    "(case ci.BillType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' when '4' then '收据' end) BillTypeNm," +                                    
                                    "ci.MoneyType,cpm.TypeName MoneyTypeNm," +
                                    "ci.CurrencyType,cts.CurrencyName CurrencyaNm," +
                                    "ci.MaxCredit," +                                    
                                    "ci.OpenBank," +
                                    "ci.AccountMan," +
                                    "ci.AccountNum," +
                                     " ci.ExtField1,ci.ExtField2,ci.ExtField3,ci.ExtField4,ci.ExtField5,ci.ExtField6,ci.ExtField7,ci.ExtField8,ci.ExtField9,ci.ExtField10," +
                                       " ci.ExtField11,ci.ExtField12,ci.ExtField13,ci.ExtField14,ci.ExtField15,ci.ExtField16,ci.ExtField17,ci.ExtField18,ci.ExtField19,ci.ExtField20," +
                                       " ci.ExtField21,ci.ExtField22,ci.ExtField23,ci.ExtField24,ci.ExtField25,ci.ExtField26,ci.ExtField27,ci.ExtField28,ci.ExtField29,ci.ExtField30," +
                                    "ci.Remark" +                                       
                                " from " +
                                   " officedba.CustInfo ci" +
                                   " left join officedba.CustLinkMan cl on cl.CustNo = ci.CustNo and cl.CompanyCD = ci.CompanyCD " +
                                   "                   and cl.id=(select min(id) from officedba.CustLinkMan where CustNo = @CustNo) " +
                                   " left join officedba.CodePublicType ctt on ctt.id = ci.CustType " +
                                   " left join officedba.EmployeeInfo ei on ei.id = ci.Creator" +
                                   " left join officedba.EmployeeInfo em on em.id = ci.Manager" +
                                   " left join officedba.CodeCompanyType cct on cct.id = ci.CustClass " +
                                   " left join officedba.CodePublicType cp on cp.id = ci.CreditGrade " +
                                   " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID " +
                                   " left join officedba.CodePublicType cpl on cpl.id = ci.LinkCycle " +
                                   " left join officedba.CodePublicType cpc on cpc.id = ci.CountryID " +
                                   " left join officedba.CodePublicType a on a.id = cl.LinkType " +
                                   " left join officedba.CodePublicType b on b.id = cl.NationalID " +
                                   " left join officedba.CodePublicType c on c.id = cl.CultureLevel " +
                                   " left join officedba.CodePublicType d on d.id = cl.Professional " +
                                   " left join officedba.CodePublicType cpt on cpt.id = ci.TakeType " +
                                   " left join officedba.CodePublicType cpca on cpca.id = ci.CarryType " +
                                   " left join officedba.CodePublicType cpp on cpp.id = ci.PayType " +
                                   " left join officedba.CodePublicType cpm on cpm.id = ci.MoneyType " +
                                   " left join officedba.CurrencyTypeSetting cts on cts.id = ci.CurrencyType " +
                                   " where " +
                                        "ci.CustNo=@CustNo and ci.CompanyCD=@CompanyCD";
                    #endregion
                }
                else
                {
                    #region 拼写SQL语句
                    sql = "SELECT ci.ID," +
                                    "ci.CompanyCD,(case ci.CustBig when '2' then '会员' else '企业' end)CustBig," +
                                    "(case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage," +
                                    "(case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户'  when '4' then '道德型客户' end)CustTypeSell ," +
                                    "(case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime," +
                                    "CONVERT(varchar(100), ci.FirstBuyDate, 23) FirstBuyDate ," +
                                    "ci.BigType," +
                                    "ci.CustType,ctt.TypeName CustTypaNm," +
                                    "ci.CustClass,cct.CodeName CustClassName," +
                                    "ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort," +
                                    "ci.CreditGrade,cp.TypeName CreditGradeNm," +
                                    "ci.CustNote," +
                                    "ci.Manager," +
                                    "em.EmployeeName ManagerName," +
                                    "ci.AreaID,cpa.TypeName AreaName," +
                                    "ci.CountryID,cpc.TypeName CountryName," +
                                    "ci.Province," +
                                    "ci.City," +
                                    "ci.Tel," +
                                    "ci.Fax," +
                                    "ci.OnLine," +
                                    "ci.WebSite," +
                                    "ci.Post," +
                                    "ci.LinkCycle,cpl.TypeName LinkCycleNm," +
                                    "(case ci.HotIs when '1' then '是' when '2' then '否' end) HotIs," +
                                    "(case ci.HotHow when '1' then '低热' when '2' then '中热' when '3' then '高热' end)HotHow," +
                                    "(case ci.MeritGrade when '1' then '高' when '2' then '中' when '3' then '低' end)MeritGrade," +
                                    "(case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end)RelaGrade," +
                                    "ci.Relation," +
                                    "(case ci.CompanyType when '1' then '事业' when '2' then '企业' when '3' then '社团' when '4' then '自然人' when '5' then '其他' end) CompanyType," +
                                    "ci.StaffCount," +
                                    "ci.Source," +
                                    "ci.Phase," +
                                    "ci.CustSupe," +
                                    "ci.Trade," +
                                    "CONVERT(varchar(100), ci.SetupDate, 23) SetupDate," +
                                    "ci.ArtiPerson," +
                                    "ci.SetupMoney," +
                                    "ci.SetupAddress," +
                                    "ci.CapitalScale," +
                                    "ci.SaleroomY," +
                                    "ci.ProfitY," +
                                    "ci.TaxCD," +
                                    "ci.BusiNumber," +
                                    "(case ci.IsTax when '0' then '否' when '1' then '是' end) IsTax," +
                                    "ci.SellArea," +
                                    "ci.SellMode," +
                                    "ci.ReceiveAddress," +
                                    "ci.ContactName," +
                                    "ci.Mobile," +
                                    "ci.email," +
                                    "ci.TakeType,cpt.TypeName TakeTypeNm," +
                                    "ci.CarryType,cpca.TypeName CarryTypeNm," +
                                    "(case ci.BusiType when '1' then '普通销售' when '2' then '委托代销' when '3' then '直运' when '4' then '零售' when '5' then '销售调拨' end) BusiType," +
                                    "(case ci.BillType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' when '4' then '收据' end) BillTypeNm," +
                                    "ci.PayType,cpp.TypeName PayTypeNm," +
                                    "ci.MoneyType,cpm.TypeName MoneyTypeNm," +
                                    "ci.CurrencyType,cts.CurrencyName CurrencyaNm," +
                                    "ci.MaxCredit," +
                                    "ci.MaxCreditDate," +
                                    "(case ci.CreditManage when '1' then '否' when '2' then '是' end) CreditManage," +
                                    "ci.OpenBank," +
                                    "ci.AccountMan," +
                                    "ci.AccountNum," +
                                    "ci.Remark," +
                                    "(case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus," +
                                    "ci.CanViewUser," +
                                    "ci.CanViewUserName," +
                                    "ci.Creator," +
                                    "ei.EmployeeName CreatorName," +
                                    "CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate," +
                                    "CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate," +
                                       " ci.[CompanyValues]  ," +
                                       " ci.[CatchWord]      ," +
                                       " ci.[ManageValues]   ," +
                                       " ci.[Potential]      ," +
                                       " ci.[Problem]        ," +
                                       " ci.[Advantages]     ," +
                                       " ci.[TradePosition]  ," +
                                       " ci.[Competition]    ," +
                                       " ci.[Collaborator]   ," +
                                       " ci.[ManagePlan]     ," +
                                       " ci.[Collaborate]    ," +
                                       " ci.ExtField1,ci.ExtField2,ci.ExtField3,ci.ExtField4,ci.ExtField5,ci.ExtField6,ci.ExtField7,ci.ExtField8,ci.ExtField9,ci.ExtField10," +
                                       " ci.ExtField11,ci.ExtField12,ci.ExtField13,ci.ExtField14,ci.ExtField15,ci.ExtField16,ci.ExtField17,ci.ExtField18,ci.ExtField19,ci.ExtField20," +
                                       " ci.ExtField21,ci.ExtField22,ci.ExtField23,ci.ExtField24,ci.ExtField25,ci.ExtField26,ci.ExtField27,ci.ExtField28,ci.ExtField29,ci.ExtField30," +
                                    "ci.ModifiedUserID " +
                                " from " +
                                   " officedba.CustInfo ci" +
                                   " left join officedba.CodePublicType ctt on ctt.id = ci.CustType " +
                                   " left join officedba.EmployeeInfo ei on ei.id = ci.Creator" +
                                   " left join officedba.EmployeeInfo em on em.id = ci.Manager" +
                                   " left join officedba.CodeCompanyType cct on cct.id = ci.CustClass " +
                                   " left join officedba.CodePublicType cp on cp.id = ci.CreditGrade " +
                                   " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID " +
                                   " left join officedba.CodePublicType cpl on cpl.id = ci.LinkCycle " +
                                   " left join officedba.CodePublicType cpc on cpc.id = ci.CountryID " +
                                   " left join officedba.CodePublicType cpt on cpt.id = ci.TakeType " +
                                   " left join officedba.CodePublicType cpca on cpca.id = ci.CarryType " +
                                   " left join officedba.CodePublicType cpp on cpp.id = ci.PayType " +
                                   " left join officedba.CodePublicType cpm on cpm.id = ci.MoneyType " +
                                   " left join officedba.CurrencyTypeSetting cts on cts.id = ci.CurrencyType " +
                                  " where " +
                                        "ci.CustNo=@CustNo and ci.CompanyCD=@CompanyCD";
                    #endregion
                }

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustNo", CustNo);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 导出客户列表信息
        /// <summary>
        /// 导出客户列表信息
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="Manager"></param>
        /// <param name="CanUserID"></param>
        /// <param name="CreatedBegin"></param>
        /// <param name="CreatedEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        // public static DataTable ExportCustInfo(CustInfoModel CustModel, string Manager, string CanUserID, string CreatedBegin, string CreatedEnd, string Usebegin, string Useend,string Certibegin,string Certiend,string Warbegin,string Warend,string Powerbegin,string Powerend,string Gmspbegin,string Gmspend, string ord)
        public static DataTable ExportCustInfo(CustInfoModel CustModel, string Manager, string CanUserID, string CreatedBegin, string CreatedEnd, string ord)
        {
            try
            {
                #region sql
                //string sql = "  select distinct " +
                //                   " ci.id, ci.CustNo,ci.CustName,ci.CustNam,ci.CustShort, " +
                //                   " ci.CustClass,isnull(cc.CodeName,'') CodeName," +
                //                   " (case ci.CustBig when '2' then '会员' else '企业' end)CustBigName, " +
                //                   " ci.CustType,isnull(cp.TypeName,'') TypeName," +
                //                   " ci.AreaID,isnull(cpa.TypeName,'') Area," +
                //                   "  isnull(ei.EmployeeName,'') Manager," +
                //                   " isnull(cpc.TypeName,'') CreditGrade," +
                //                   " ci.RelaGrade,(case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end) RelaGradeName," +
                //                   "isnull(eic.EmployeeName,'') Creator," +
                //                   "  CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate,ci.UsedStatus, " +
                //                   " (case ci.UsedStatus when '1' then '启用' when '0' then '停用' end) UsedStatusName" +
                //               " from " +
                //               " officedba.CustInfo ci " +
                //               " left join officedba.CodeCompanyType cc on cc.id = ci.CustClass " +
                //               " left join officedba.EmployeeInfo ei on ei.id = ci.Manager  " +
                //               " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID" +
                //               " left join officedba.CodePublicType cpc on cpc.id = ci.CreditGrade" +
                //               " left join officedba.CodePublicType cp on cp.id = ci.CustType" +
                //               " left join officedba.EmployeeInfo eic on eic.id = ci.Creator" +
                //               " left join officedba.CustLinkMan cl on ci.custno = cl.custno " +
                //               " where ci.CompanyCD = '" + CustModel.CompanyCD + "'" +
                //               " and (ci.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = ci.Creator or '" + CanUserID + "' = ci.Manager or ci.CanViewUser = ',,')";
                #endregion

                #region 
                string sql = "SELECT ci.ID, " +
                                   " ci.CompanyCD,(case ci.CustBig when '2' then '会员' else '企业' end)BigType,ci.CustNo,ci.CustName,ci.CustNum, " +
                                   " ci.CustClass,cct.CodeName CustClassName,ci.CustType,ctt.TypeName CustTypaNm, " +
                                   " (case ci.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage, " +
                                   " (case ci.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' when '3' then '方便型客户'  when '4' then '道德型客户' end)CustTypeSell , " +
                                   " (case ci.CustTypeTime when '1' then '老客户' when '2' then '新客户' when '3' then '潜在客户' end) CustTypeTime, " +
                                   " ci.CreditGrade,cp.TypeName CreditGradeNm,ci.CustShort,ci.CustNote,ci.Province,ci.AreaID,ci.ReceiveAddress, " +
                                   "  (case ci.BusiType when '1' then '普通销售' when '2' then '委托代销' when '3' then '直运' when '4' then '零售' when '5' then '销售调拨' end) BusiType, " +
                                   "  ci.Manager,em.EmployeeName ManagerName,ci.LinkCycle,cpl.TypeName LinkCycleNm,ci.Creator,ei.EmployeeName CreatorName, " +
                                    " (case ci.CreditManage when '1' then '否' when '2' then '是' end) CreditManage,ci.ModifiedUserID, " +
                                    "  (case ci.RelaGrade when '1' then '密切' when '2' then '较好' when '3' then '一般' when '4' then '较差' end)RelaGrade, " +
                                    "  ci.MaxCreditDate,ci.PayType,cpp.TypeName PayTypeNm,ci.CanViewUser,ci.CanViewUserName, " +
                                    " (case ci.UsedStatus when '0' then '停用' when '1' then '启用' end) UsedStatus, " +
                                    " CONVERT(varchar(100), ci.CreatedDate, 23) CreatedDate, " +
                                    "CONVERT(varchar(100), ci.ModifiedDate, 23) ModifiedDate, " +
                                     " (case ci.Corptype when '1' then '生产企业' when '2' then '私营' end) Corptype, " +//新添加20120726
                                       " (case ci.Category when '1' then '个人' when '2' then '集体' end) Category, " +
                                         " (case ci.Pagestatus when '1' then '确认' when '2' then '未确认' end) Pagestatus, " +
                                          "CONVERT(varchar(100), ci.Usedata, 23) Usedata, " +
                                           "CONVERT(varchar(100), ci.Certidata, 23) Certidata, " +
                                            "CONVERT(varchar(100), ci.Wardata, 23) Wardata, " +
                                             "CONVERT(varchar(100), ci.Powerdata, 23) Powerdata, " +
                                              "CONVERT(varchar(100), ci.Gmspdata, 23) Gmspdata, " +
                                   " a.TypeName LinkTypeName, " +                                  
                                   " b.TypeName NationalName, c.TypeName CultureLevelName, d.TypeName ProfessionalName," +
                                   
                                  
                                   " CONVERT(varchar(100), ci.FirstBuyDate, 23) FirstBuyDate ,ci.CountryID,cpc.TypeName CountryName, " +
                                   " ci.BigType,ci.CustNam,cpa.TypeName AreaName,ci.City,ci.Tel,ci.Fax,ci.OnLine,ci.WebSite,ci.Post,   " +
                                   " (case ci.HotIs when '1' then '是' when '2' then '否' end) HotIs, " +
                                   " (case ci.HotHow when '1' then '低热' when '2' then '中热' when '3' then '高热' end)HotHow, " +
                                   " (case ci.MeritGrade when '1' then '高' when '2' then '中' when '3' then '低' end)MeritGrade,ci.Relation, " +
                                   " (case ci.CompanyType when '1' then '事业' when '2' then '企业' when '3' then '社团' when '4' then '自然人' when '5' then '其他' end) CompanyType, " +
                                   " ci.StaffCount,ci.Source,ci.Phase,ci.CustSupe,ci.Trade,CONVERT(varchar(100), ci.SetupDate, 23) SetupDate, " +
                                   " ci.ArtiPerson,ci.SetupMoney,ci.SetupAddress,ci.CapitalScale,ci.SaleroomY,ci.ProfitY,ci.TaxCD,ci.BusiNumber," +
                                   " (case ci.IsTax when '0' then '否' when '1' then '是' end) IsTax,ci.SellArea,ci.SellMode,ci.ContactName,ci.Mobile,ci.email, " +
                                   " ci.TakeType,cpt.TypeName TakeTypeNm, ci.CarryType,cpca.TypeName CarryTypeNm,                                    " +
                                   " (case ci.BillType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' when '4' then '收据' end) BillTypeNm,                                     " +
                                   " ci.MoneyType,cpm.TypeName MoneyTypeNm, ci.CurrencyType,cts.CurrencyName CurrencyaNm, " +
                                   " ci.MaxCredit,ci.OpenBank, ci.AccountMan,ci.AccountNum, " +
                                   "   ci.ExtField1,ci.ExtField2,ci.ExtField3,ci.ExtField4,ci.ExtField5,ci.ExtField6,ci.ExtField7,ci.ExtField8,ci.ExtField9,ci.ExtField10, " +
                                   "     ci.ExtField11,ci.ExtField12,ci.ExtField13,ci.ExtField14,ci.ExtField15,ci.ExtField16,ci.ExtField17,ci.ExtField18,ci.ExtField19,ci.ExtField20, " +
                                   "     ci.ExtField21,ci.ExtField22,ci.ExtField23,ci.ExtField24,ci.ExtField25,ci.ExtField26,ci.ExtField27,ci.ExtField28,ci.ExtField29,ci.ExtField30, " +
                                   " ci.Remark,ci.CompanyValues,ci.CatchWord,ci.ManageValues,ci.Potential,ci.Problem,ci.Advantages,ci.TradePosition," +
                                   " ci.Competition,ci.Collaborator,ci.ManagePlan,ci.Collaborate" +
                                " from  " +
                                   " officedba.CustInfo ci " +
                                   " left join officedba.CustLinkMan cl on cl.CustNo = ci.CustNo and cl.CompanyCD = ci.CompanyCD  " +
                                   "                   and cl.id=(select min(id) from officedba.CustLinkMan )  " +
                                   " left join officedba.CodePublicType ctt on ctt.id = ci.CustType  " +
                                   " left join officedba.EmployeeInfo ei on ei.id = ci.Creator " +
                                   " left join officedba.EmployeeInfo em on em.id = ci.Manager " +
                                   " left join officedba.CodeCompanyType cct on cct.id = ci.CustClass  " +
                                   " left join officedba.CodePublicType cp on cp.id = ci.CreditGrade  " +
                                   " left join officedba.CodePublicType cpa on cpa.id = ci.AreaID  " +
                                   " left join officedba.CodePublicType cpl on cpl.id = ci.LinkCycle " +
                                   " left join officedba.CodePublicType cpc on cpc.id = ci.CountryID  " +
                                   " left join officedba.CodePublicType a on a.id = cl.LinkType  " +
                                   " left join officedba.CodePublicType b on b.id = cl.NationalID  " +
                                   " left join officedba.CodePublicType c on c.id = cl.CultureLevel  " +
                                   " left join officedba.CodePublicType d on d.id = cl.Professional  " +
                                   " left join officedba.CodePublicType cpt on cpt.id = ci.TakeType  " +
                                   " left join officedba.CodePublicType cpca on cpca.id = ci.CarryType " +
                                   " left join officedba.CodePublicType cpp on cpp.id = ci.PayType  " +
                                   " left join officedba.CodePublicType cpm on cpm.id = ci.MoneyType  " +
                                   " left join officedba.CurrencyTypeSetting cts on cts.id = ci.CurrencyType  " +
                                   " where ci.CompanyCD='" + CustModel.CompanyCD + "'";
                #endregion

                if (CustModel.CustNo != "")
                    sql += " and ci.CustNo like '%" + CustModel.CustNo + "%'";
                if (CustModel.CustNam != "")
                    sql += " and ci.CustNam like '%" + CustModel.CustNam + "%'";
                if (CustModel.CustClass != 0)
                    sql += " and ci.CustClass='" + CustModel.CustClass + "'";
                if (CustModel.CustName != "")
                    sql += " and ci.CustName like '%" + CustModel.CustName + "%'";
                if (CustModel.Tel != "")
                    sql += " and (ci.Tel like '%" + CustModel.Tel + "%' or cl.worktel like '%" + CustModel.Tel + "%')";
                if (CustModel.CustShort != "")
                    sql += " and ci.CustShort like '%" + CustModel.CustShort + "%'";
                if (CustModel.AreaID != 0)
                    sql += " and ci.AreaID='" + CustModel.AreaID + "'";
                if (CustModel.CreditGrade != 0)
                    sql += " and ci.CreditGrade='" + CustModel.CreditGrade + "'";
                if (CustModel.RelaGrade != "0")
                    sql += " and ci.RelaGrade='" + CustModel.RelaGrade + "'";
                if (Manager != "")
                    sql += " and ei.EmployeeName like '%" + Manager + "%'";
                if (CreatedBegin.ToString() != "")
                    sql += " and ci.CreatedDate >= '" + CreatedBegin.ToString() + "'";
                if (CreatedEnd.ToString() != "")
                    sql += " and ci.CreatedDate <= '" + CreatedEnd.ToString() + "'";
                if (CustModel.UsedStatus != "-1")
                    sql += " and ci.UsedStatus = '" + CustModel.UsedStatus + "'";
                if (CustModel.Corptype != "0")
                    sql += " and ci.Corptype = '" + CustModel.Corptype + "'";
                if (CustModel.Category != "0")
                    sql += " and ci.Category = '" + CustModel.Category + "'";
                if (CustModel.Pagestatus != "0")
                    sql += " and ci.Pagestatus = '" + CustModel.Pagestatus + "'";
               
                //if (Usebegin.ToString() != "")
                //    sql += " and ci.Usedata >= '" + Usebegin.ToString() + "'";
                //if (Useend.ToString() != "")
                //    sql += " and ci.Usedata <= '" + Useend.ToString() + "'";
                //if (Certibegin.ToString() != "")
                //    sql += " and ci.Certidata >= '" + Certibegin.ToString() + "'";
                //if (Certiend.ToString() != "")
                //    sql += " and ci.Certidata <= '" + Certiend.ToString() + "'";
                //if (Warbegin.ToString() != "")
                //    sql += " and ci.Wardata >= '" + Usebegin.ToString() + "'";
                //if (Warend.ToString() != "")
                //    sql += " and ci.Wardata <= '" + Warend.ToString() + "'";
                //if (Powerbegin.ToString() != "")
                //    sql += " and ci.Powerdata >= '" + Powerbegin.ToString() + "'";
                //if (Powerend.ToString() != "")
                //    sql += " and ci.Powerdata <= '" + Powerend.ToString() + "'";
                //if (Gmspbegin.ToString() != "")
                //    sql += " and ci.Gmspdata >= '" + Gmspbegin.ToString() + "'";
                //if (Gmspend.ToString() != "")
                //    sql += " and ci.Gmspdata <= '" + Gmspend.ToString() + "'";

                sql += ord;

                //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 批量导入客户档案信息
        /// <summary>
        /// 批量导入客户档案信息
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertCustInfoRecord(DataTable dt, string CompanyCD, string EmployeeID, string UserID)
        {
            try
            {
                string BigType = "1";//往来单位大类，直接写入值1表示客户
                string NonceDate = DateTime.Now.ToString("yyyy-MM-dd");//建档日期&最后更新日期
                string UsedStatus = "1";//启用状态（0停用，1启用）

                SqlCommand[] comms = new SqlCommand[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++) //循环dt
                {
                    string CustNo = dt.Rows[i]["客户编号"].ToString();//客户编号
                    string CustName = dt.Rows[i]["客户名称"].ToString();//客户名称
                    string CustNote = dt.Rows[i]["客户简介"].ToString();//客户简介
                    string Manager = dt.Rows[i]["Manager"].ToString();//分管业务员
                    string Area = dt.Rows[i]["AreaID"].ToString();//区域

                    string Province = dt.Rows[i]["省"].ToString();//省
                    string City = dt.Rows[i]["市"].ToString();//市
                    string ContactName = dt.Rows[i]["联系人"].ToString();//联系人
                    string Tel = dt.Rows[i]["电话"].ToString();//电话
                    string Mobile = dt.Rows[i]["手机"].ToString();//手机
                    string Fax = dt.Rows[i]["传真"].ToString();//传真
                    string OnLine = dt.Rows[i]["在线咨询"].ToString();//在线咨询
                    string WebSite = dt.Rows[i]["公司网址"].ToString();//公司网址
                    string Post = dt.Rows[i]["邮编"].ToString();//邮编
                    string email = dt.Rows[i]["电子邮件"].ToString();
                    string ReceiveAddress = dt.Rows[i]["收货地址"].ToString();
                    string SellArea = dt.Rows[i]["经营范围"].ToString();
               
                    string CompanyType = dt.Rows[i]["CompanyType"].ToString();//单位性质（1事业，2企业，3社团，4自然人，5其他）
                    SqlDateTime ? SetupDate = dt.Rows[i]["成立时间"]==DBNull.Value ? SqlDateTime.Null : SqlDateTime.Parse(dt.Rows[i]["成立时间"].ToString());//成立时间
                    string SetupMoney = dt.Rows[i]["注册资本(万元)"].ToString();//注册资本(万元)
                    string CapitalScale = dt.Rows[i]["资产规模(万元)"].ToString();//资产规模(万元)
                    string SaleroomY = dt.Rows[i]["年销售额(万元)"].ToString();//年销售额(万元)
                    string StaffCount = dt.Rows[i]["员工总数(个)"].ToString();//员工总数(个)

                    string ArtiPerson = dt.Rows[i]["法人代表"].ToString();
                    string Trade = dt.Rows[i]["行业"].ToString();
                    string SetupAddress = dt.Rows[i]["注册地址"].ToString();
                    string Relation = dt.Rows[i]["关系描述"].ToString();
                    string ManagePlan = dt.Rows[i]["发展计划"].ToString();
                    string Collaborate = dt.Rows[i]["合作方法"].ToString();
                                        
                    #region 拼写添加入库明细信息sql语句
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO officedba.CustInfo");
                    sql.AppendLine("(CompanyCD");
                    sql.AppendLine(",CustNo");
                    sql.AppendLine(",CustName");
                    sql.AppendLine(",CustNote");
                    sql.AppendLine(",Manager   ");
                    sql.AppendLine(",AreaID   ");

                    sql.AppendLine(",Province   ");
                    sql.AppendLine(",City   ");
                    sql.AppendLine(",ContactName   ");
                    sql.AppendLine(",Tel   ");
                    sql.AppendLine(",Mobile   ");
                    sql.AppendLine(",Fax   ");
                    sql.AppendLine(",OnLine   ");
                    sql.AppendLine(",WebSite   ");
                    sql.AppendLine(",Post   ");
                    sql.AppendLine(",email   ");
                    sql.AppendLine(",ReceiveAddress   ");
                    sql.AppendLine(",SellArea   ");

                    sql.AppendLine(",CompanyType  ");
                    sql.AppendLine(",SetupDate    ");
                    sql.AppendLine(",SetupMoney    ");
                    sql.AppendLine(",CapitalScale");
                    sql.AppendLine(",SaleroomY");
                    sql.AppendLine(",StaffCount");

                    sql.AppendLine(",ArtiPerson");
                    sql.AppendLine(",Trade");
                    sql.AppendLine(",SetupAddress");
                    sql.AppendLine(",Relation");
                    sql.AppendLine(",ManagePlan");
                    sql.AppendLine(",Collaborate");

                    sql.AppendLine(",BigType");
                    sql.AppendLine(",Creator");
                    sql.AppendLine(",CreatedDate");
                    sql.AppendLine(",ModifiedUserID");
                    sql.AppendLine(",ModifiedDate");

                    sql.AppendLine(",CanViewUser");
                    sql.AppendLine(",CanViewUserName");

                    sql.AppendLine(",UsedStatus)");
                    sql.AppendLine(" values ");
                    sql.AppendLine("(@CompanyCD");
                    sql.AppendLine(",@CustNo");
                    sql.AppendLine(",@CustName");
                    sql.AppendLine(",@CustNote");
                    sql.AppendLine(",@Manager   ");
                    sql.AppendLine(",@Area   ");

                    sql.AppendLine(",@Province   ");
                    sql.AppendLine(",@City   ");
                    sql.AppendLine(",@ContactName   ");
                    sql.AppendLine(",@Tel   ");
                    sql.AppendLine(",@Mobile   ");
                    sql.AppendLine(",@Fax   ");
                    sql.AppendLine(",@OnLine   ");
                    sql.AppendLine(",@WebSite   ");
                    sql.AppendLine(",@Post   ");
                    sql.AppendLine(",@email   ");
                    sql.AppendLine(",@ReceiveAddress   ");
                    sql.AppendLine(",@SellArea   ");

                    sql.AppendLine(",@CompanyType  ");
                    sql.AppendLine(",@SetupDate    ");
                    sql.AppendLine(",@SetupMoney    ");
                    sql.AppendLine(",@CapitalScale");
                    sql.AppendLine(",@SaleroomY");
                    sql.AppendLine(",@StaffCount");

                    sql.AppendLine(",@ArtiPerson");
                    sql.AppendLine(",@Trade");
                    sql.AppendLine(",@SetupAddress");
                    sql.AppendLine(",@Relation");
                    sql.AppendLine(",@ManagePlan");
                    sql.AppendLine(",@Collaborate");

                    sql.AppendLine(",@BigType");
                    sql.AppendLine(",@Creator");
                    sql.AppendLine(",@CreatedDate");
                    sql.AppendLine(",@ModifiedUserID");
                    sql.AppendLine(",@ModifiedDate");

                    sql.AppendLine(",@CanViewUser");
                    sql.AppendLine(",@CanViewUserName");

                    sql.AppendLine(",@UsedStatus)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] Params = new SqlParameter[38];
                    Params[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                    Params[1] = SqlHelper.GetParameter("@CustNo", CustNo);
                    Params[2] = SqlHelper.GetParameter("@CustName", CustName);
                    Params[3] = SqlHelper.GetParameter("@CustNote", CustNote);
                    Params[4] = SqlHelper.GetParameter("@Manager", Manager);

                    Params[5] = SqlHelper.GetParameter("@Province", Province);
                    Params[6] = SqlHelper.GetParameter("@City", City);
                    Params[7] = SqlHelper.GetParameter("@ContactName", ContactName);
                    Params[8] = SqlHelper.GetParameter("@Tel", Tel);
                    Params[9] = SqlHelper.GetParameter("@Mobile", Mobile);
                    Params[10] = SqlHelper.GetParameter("@Fax", Fax);
                    Params[11] = SqlHelper.GetParameter("@OnLine", OnLine);
                    Params[12] = SqlHelper.GetParameter("@WebSite", WebSite);
                    Params[13] = SqlHelper.GetParameter("@Post", Post);
                    Params[14] = SqlHelper.GetParameter("@email", email);
                    Params[15] = SqlHelper.GetParameter("@ReceiveAddress", ReceiveAddress);
                    Params[16] = SqlHelper.GetParameter("@SellArea", SellArea);

                    Params[17] = SqlHelper.GetParameter("@CompanyType", CompanyType);                    
                    Params[18] = SqlHelper.GetParameter("@SetupDate", SetupDate); 
                    Params[19] = SqlHelper.GetParameter("@SetupMoney", SetupMoney == "" ? "0.00" : SetupMoney);
                    Params[20] = SqlHelper.GetParameter("@CapitalScale", CapitalScale == "" ? "0.00" : CapitalScale);
                    Params[21] = SqlHelper.GetParameter("@SaleroomY", SaleroomY == "" ? "0.00" : SaleroomY);
                    Params[22] = SqlHelper.GetParameter("@StaffCount", StaffCount == "" ? "0" : StaffCount);

                    Params[23] = SqlHelper.GetParameter("@ArtiPerson", ArtiPerson);
                    Params[24] = SqlHelper.GetParameter("@Trade", Trade);
                    Params[25] = SqlHelper.GetParameter("@SetupAddress", SetupAddress);
                    Params[26] = SqlHelper.GetParameter("@Relation", Relation);
                    Params[27] = SqlHelper.GetParameter("@ManagePlan", ManagePlan);
                    Params[28] = SqlHelper.GetParameter("@Collaborate", Collaborate);

                    Params[29] = SqlHelper.GetParameter("@BigType", BigType);
                    Params[30] = SqlHelper.GetParameter("@Creator", EmployeeID);
                    Params[31] = SqlHelper.GetParameter("@CreatedDate", NonceDate);
                    Params[32] = SqlHelper.GetParameter("@ModifiedUserID", UserID);
                    Params[33] = SqlHelper.GetParameter("@ModifiedDate", NonceDate);
                    Params[34] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
                    Params[35] = SqlHelper.GetParameter("@CanViewUser", ",,");
                    Params[36] = SqlHelper.GetParameter("@CanViewUserName", "");
                    Params[37] = SqlHelper.GetParameter("@Area", Area);

                    SqlCommand Command = new SqlCommand(sql.ToString());
                    Command.Parameters.AddRange(Params);
                    comms[i] = Command;
                    #endregion
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool InsertCustInfoRecord(DataRow dr, string CompanyCD, string EmployeeID, string UserID)
        {
            try
            {
                string BigType = "1";//往来单位大类，直接写入值1表示客户
                string NonceDate = DateTime.Now.ToString("yyyy-MM-dd");//建档日期&最后更新日期
                string UsedStatus = "1";//启用状态（0停用，1启用）

                SqlCommand[] comms = new SqlCommand[1];

                
                    string CustNo = dr["客户编号"].ToString();//客户编号
                    string CustName = dr["客户名称"].ToString();//客户名称
                    string CustNam = dr["客户简称"].ToString();//客户简介
                    string Manager = dr["Manager"].ToString();//分管业务员
                    string Area = dr["AreaID"].ToString();//区域

                    string Province = dr["省"].ToString();//省
                    string City = dr["市"].ToString();//市
                    string ContactName = dr["联系人"].ToString();//联系人
                    string Tel = dr["电话"].ToString();//电话
                    string Mobile = dr["手机"].ToString();//手机
                    string Fax = dr["传真"].ToString();//传真
                    string OnLine = dr["在线咨询"].ToString();//在线咨询
                    string WebSite = dr["公司网址"].ToString();//公司网址
                    string Post = dr["邮编"].ToString();//邮编
                    string email = dr["电子邮件"].ToString();
                    string ReceiveAddress = dr["收货地址"].ToString();
                    string SellArea = dr["经营范围"].ToString();

                    string CompanyType = dr["CompanyType"].ToString();//单位性质（1事业，2企业，3社团，4自然人，5其他）
                    SqlDateTime? SetupDate = dr["成立时间"] == DBNull.Value ? SqlDateTime.Null : SqlDateTime.Parse(dr["成立时间"].ToString());//成立时间
                    string SetupMoney = dr["注册资本(万元)"].ToString();//注册资本(万元)
                    string CapitalScale = dr["资产规模(万元)"].ToString();//资产规模(万元)
                    string SaleroomY = dr["年销售额(万元)"].ToString();//年销售额(万元)
                    string StaffCount = dr["员工总数(个)"].ToString();//员工总数(个)

                    string ArtiPerson = dr["法人代表"].ToString();
                    string Trade = dr["行业"].ToString();
                    string SetupAddress = dr["注册地址"].ToString();
                    string Relation = dr["关系描述"].ToString();
                    string ManagePlan = dr["发展计划"].ToString();
                    string Collaborate = dr["合作方法"].ToString();

                    #region 拼写添加入库明细信息sql语句
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO officedba.CustInfo");
                    sql.AppendLine("(CompanyCD");
                    sql.AppendLine(",CustNo");
                    sql.AppendLine(",CustName");
                    sql.AppendLine(",CustNam");
                    sql.AppendLine(",Manager   ");
                    sql.AppendLine(",AreaID   ");

                    sql.AppendLine(",Province   ");
                    sql.AppendLine(",City   ");
                    sql.AppendLine(",ContactName   ");
                    sql.AppendLine(",Tel   ");
                    sql.AppendLine(",Mobile   ");
                    sql.AppendLine(",Fax   ");
                    sql.AppendLine(",OnLine   ");
                    sql.AppendLine(",WebSite   ");
                    sql.AppendLine(",Post   ");
                    sql.AppendLine(",email   ");
                    sql.AppendLine(",ReceiveAddress   ");
                    sql.AppendLine(",SellArea   ");

                    sql.AppendLine(",CompanyType  ");
                    sql.AppendLine(",SetupDate    ");
                    sql.AppendLine(",SetupMoney    ");
                    sql.AppendLine(",CapitalScale");
                    sql.AppendLine(",SaleroomY");
                    sql.AppendLine(",StaffCount");

                    sql.AppendLine(",ArtiPerson");
                    sql.AppendLine(",Trade");
                    sql.AppendLine(",SetupAddress");
                    sql.AppendLine(",Relation");
                    sql.AppendLine(",ManagePlan");
                    sql.AppendLine(",Collaborate");

                    sql.AppendLine(",BigType");
                    sql.AppendLine(",Creator");
                    sql.AppendLine(",CreatedDate");
                    sql.AppendLine(",ModifiedUserID");
                    sql.AppendLine(",ModifiedDate");

                    sql.AppendLine(",CanViewUser");
                    sql.AppendLine(",CanViewUserName");

                    sql.AppendLine(",UsedStatus)");
                    sql.AppendLine(" values ");
                    sql.AppendLine("(@CompanyCD");
                    sql.AppendLine(",@CustNo");
                    sql.AppendLine(",@CustName");
                    sql.AppendLine(",@CustNam");
                    sql.AppendLine(",@Manager   ");
                    sql.AppendLine(",@Area   ");

                    sql.AppendLine(",@Province   ");
                    sql.AppendLine(",@City   ");
                    sql.AppendLine(",@ContactName   ");
                    sql.AppendLine(",@Tel   ");
                    sql.AppendLine(",@Mobile   ");
                    sql.AppendLine(",@Fax   ");
                    sql.AppendLine(",@OnLine   ");
                    sql.AppendLine(",@WebSite   ");
                    sql.AppendLine(",@Post   ");
                    sql.AppendLine(",@email   ");
                    sql.AppendLine(",@ReceiveAddress   ");
                    sql.AppendLine(",@SellArea   ");

                    sql.AppendLine(",@CompanyType  ");
                    sql.AppendLine(",@SetupDate    ");
                    sql.AppendLine(",@SetupMoney    ");
                    sql.AppendLine(",@CapitalScale");
                    sql.AppendLine(",@SaleroomY");
                    sql.AppendLine(",@StaffCount");

                    sql.AppendLine(",@ArtiPerson");
                    sql.AppendLine(",@Trade");
                    sql.AppendLine(",@SetupAddress");
                    sql.AppendLine(",@Relation");
                    sql.AppendLine(",@ManagePlan");
                    sql.AppendLine(",@Collaborate");

                    sql.AppendLine(",@BigType");
                    sql.AppendLine(",@Creator");
                    sql.AppendLine(",@CreatedDate");
                    sql.AppendLine(",@ModifiedUserID");
                    sql.AppendLine(",@ModifiedDate");

                    sql.AppendLine(",@CanViewUser");
                    sql.AppendLine(",@CanViewUserName");

                    sql.AppendLine(",@UsedStatus)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] Params = new SqlParameter[38];
                    Params[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                    Params[1] = SqlHelper.GetParameter("@CustNo", CustNo);
                    Params[2] = SqlHelper.GetParameter("@CustName", CustName);
                    Params[3] = SqlHelper.GetParameter("@CustNam", CustNam);
                    Params[4] = SqlHelper.GetParameter("@Manager", Manager);

                    Params[5] = SqlHelper.GetParameter("@Province", Province);
                    Params[6] = SqlHelper.GetParameter("@City", City);
                    Params[7] = SqlHelper.GetParameter("@ContactName", ContactName);
                    Params[8] = SqlHelper.GetParameter("@Tel", Tel);
                    Params[9] = SqlHelper.GetParameter("@Mobile", Mobile);
                    Params[10] = SqlHelper.GetParameter("@Fax", Fax);
                    Params[11] = SqlHelper.GetParameter("@OnLine", OnLine);
                    Params[12] = SqlHelper.GetParameter("@WebSite", WebSite);
                    Params[13] = SqlHelper.GetParameter("@Post", Post);
                    Params[14] = SqlHelper.GetParameter("@email", email);
                    Params[15] = SqlHelper.GetParameter("@ReceiveAddress", ReceiveAddress);
                    Params[16] = SqlHelper.GetParameter("@SellArea", SellArea);

                    Params[17] = SqlHelper.GetParameter("@CompanyType", CompanyType);
                    Params[18] = SqlHelper.GetParameter("@SetupDate", SetupDate);
                    Params[19] = SqlHelper.GetParameter("@SetupMoney", SetupMoney == "" ? "0.00" : SetupMoney);
                    Params[20] = SqlHelper.GetParameter("@CapitalScale", CapitalScale == "" ? "0.00" : CapitalScale);
                    Params[21] = SqlHelper.GetParameter("@SaleroomY", SaleroomY == "" ? "0.00" : SaleroomY);
                    Params[22] = SqlHelper.GetParameter("@StaffCount", StaffCount == "" ? "0" : StaffCount);

                    Params[23] = SqlHelper.GetParameter("@ArtiPerson", ArtiPerson);
                    Params[24] = SqlHelper.GetParameter("@Trade", Trade);
                    Params[25] = SqlHelper.GetParameter("@SetupAddress", SetupAddress);
                    Params[26] = SqlHelper.GetParameter("@Relation", Relation);
                    Params[27] = SqlHelper.GetParameter("@ManagePlan", ManagePlan);
                    Params[28] = SqlHelper.GetParameter("@Collaborate", Collaborate);

                    Params[29] = SqlHelper.GetParameter("@BigType", BigType);
                    Params[30] = SqlHelper.GetParameter("@Creator", EmployeeID);
                    Params[31] = SqlHelper.GetParameter("@CreatedDate", NonceDate);
                    Params[32] = SqlHelper.GetParameter("@ModifiedUserID", UserID);
                    Params[33] = SqlHelper.GetParameter("@ModifiedDate", NonceDate);
                    Params[34] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
                    Params[35] = SqlHelper.GetParameter("@CanViewUser", ",,");
                    Params[36] = SqlHelper.GetParameter("@CanViewUserName", "");
                    Params[37] = SqlHelper.GetParameter("@Area", Area);

                    SqlCommand Command = new SqlCommand(sql.ToString());
                    Command.Parameters.AddRange(Params);
                    comms[0] = Command;
                    #endregion
                
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 客户洽谈方式分析
        /// zxb 2009-12-2
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="custName"></param>
        /// <returns></returns>
        public static DataTable GetCustTalkByType(string CompanyCD, string begindate,string enddate,string custName)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@custName","%" +custName + "%")
            };

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select TalkType, case when A.TalkType=1 then '电话'");
                sql.AppendLine("when A.TalkType=2 then '传真'");
                sql.AppendLine("when A.TalkType=3 then '邮件'");
                sql.AppendLine("when A.TalkType=4 then '远程在线'");
                sql.AppendLine("when A.TalkType=5 then '会晤拜访'");
                sql.AppendLine("when A.TalkType=6 then '综合' else '其他' end TalkTypeName,count(A.ID) countNum from officedba.CustTalk  A ");
                sql.AppendLine("left join officedba.CustInfo B on A.CustID=B.ID ");
                sql.AppendLine("where A.CompanyCD=@companyCD and A.Status!=1 and convert(char(10),A.CreatedDate,120)>=@begindate ");
                sql.AppendLine("and convert(char(10),A.CreatedDate,120)<=@enddate");
                if (custName.Trim().Length > 1)
                {
                    sql.AppendLine("and B.CustName like @custName");
                }
                sql.AppendLine("group by A.TalkType");
                return SqlHelper.ExecuteSql(sql.ToString(),param);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 洽谈方式明细
        /// zxb 2009-12-03
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="custName"></param>
        /// <param name="talkType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustTalkByTypeDetails(string CompanyCD, string begindate, string enddate, string custName,string talkType,int pageIndex,int pageCount,string ord,ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@custName","%" +custName + "%"),
                new SqlParameter("@talkType",talkType)
            };

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select B.CustName,A.* from officedba.CustTalk A left join officedba.CustInfo B");
            sql.AppendLine("on A.CustID=B.ID where A.CompanyCD=@companyCD and A.Status!=1 ");
            if (talkType.Trim().Length > 0)
            {
                sql.AppendLine("and TalkType=@talkType");
            }
            if (custName.Trim().Length > 0)
            {
                sql.AppendLine("and B.CustName like @custName");
            }
            sql.AppendLine("and convert(char(10),A.CreatedDate,120)>=@begindate and convert(char(10),A.CreatedDate,120)<=@enddate ");
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(),pageIndex, pageCount, "id ", param, ref TotalCount);
        }

        /// <summary>
        /// 优先级别分析
        /// zxb 2009-12-03
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="custName"></param>
        /// <returns></returns>
        public static DataTable GetCustTalkByPriority(string CompanyCD, string begindate, string enddate, string custName)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@custName","%" +custName + "%")
            };

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select Priority, case when A.Priority=1 then '暂缓'");
                sql.AppendLine("when A.Priority=2 then '普通'");
                sql.AppendLine("when A.Priority=3 then '尽快'");
                sql.AppendLine("when A.Priority=4 then '立即' else '其他' end PriorityName,count(A.ID) countNum from officedba.CustTalk  A ");
                sql.AppendLine("left join officedba.CustInfo B on A.CustID=B.ID ");
                sql.AppendLine("where A.CompanyCD=@companyCD and A.Status!=1 and convert(char(10),A.CreatedDate,120)>=@begindate ");
                sql.AppendLine("and convert(char(10),A.CreatedDate,120)<=@enddate");
                if (custName.Trim().Length > 1)
                {
                    sql.AppendLine("and B.CustName like @custName");
                }
                sql.AppendLine("group by A.Priority");
                return SqlHelper.ExecuteSql(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 优先级别分析明细
        /// zxb 2009-12-03
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="custName"></param>
        /// <param name="Priority"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustTalkByPriorityDetails(string CompanyCD, string begindate, string enddate, string custName, string Priority, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@custName","%" +custName + "%"),
                new SqlParameter("@Priority",Priority)
            };

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select B.CustName,A.* from officedba.CustTalk A left join officedba.CustInfo B");
            sql.AppendLine("on A.CustID=B.ID where A.CompanyCD=@companyCD and A.Status!=1 ");
            if (Priority.Trim().Length > 0)
            {
                sql.AppendLine("and Priority=@Priority");
            }
            if (custName.Trim().Length > 0)
            {
                sql.AppendLine("and B.CustName like @custName");
            }
            sql.AppendLine("and convert(char(10),A.CreatedDate,120)>=@begindate and convert(char(10),A.CreatedDate,120)<=@enddate ");
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, "id ", param, ref TotalCount);
        }

        public static DataTable GetCustTalkByCountDetails(string CompanyCD, string begindate, string enddate, string custName, string CustID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@custName","%" +custName + "%"),
                new SqlParameter("@CustID",CustID)
            };

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select B.CustName,A.* from officedba.CustTalk A left join officedba.CustInfo B");
            sql.AppendLine("on A.CustID=B.ID where A.CompanyCD=@companyCD and A.Status!=1 ");
            if (CustID.Trim().Length > 0)
            {
                sql.AppendLine("and A.CustID=@CustID");
            }
            if (custName.Trim().Length > 0)
            {
                sql.AppendLine("and B.CustName like @custName");
            }
            sql.AppendLine("and convert(char(10),A.CreatedDate,120)>=@begindate and convert(char(10),A.CreatedDate,120)<=@enddate ");
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, "id ", param, ref TotalCount);
        }

        #endregion

        #region 获取客户数量
        public static string getcustcountbytype(string custtype,string manager,string time1,string time2)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = "select count(1) from officedba.custinfo where companycd='" + companyCD + "'";
            if (custtype != "")
            {
                sql += " and custtype=" + custtype;
            }
            if (manager != "")
            {
                sql += " and manager=" + manager;
            }
            if (time1 != "" && time2 != "")
            {
                sql += " and createddate between '" + time1 + " 00:00:00' and '" + time2 + "  23:59:59'";
            }
            else if (time1 != "" && time2 == "")
            {
                sql += " and createddate>='" + time1 + " 00:00:00'";
            }
            else if (time1 == "" && time2 != "")
            {
                sql += " and createddate<='" + time2 + " 23:59:59'";
            }
            DataTable dt = SqlHelper.ExecuteSql(sql);
            return dt.Rows[0][0].ToString();
        }
        #endregion
        #region 获取洽谈次数
        public static string getQTcount(string custtype, string manager, string time1, string time2)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = "select count(1) from officedba.CustTalk where companycd='" + companyCD + "'";
            if (custtype != "")
            {
                sql += " and talktype=" + custtype;
            }
            if (manager != "")
            {
                sql += " and linker=" + manager;
            }
            if (time1 != "" && time2 != "")
            {
                sql += " and createddate between '" + time1 + " 00:00:00' and '" + time2 + "  23:59:59'";
            }
            else if (time1 != "" && time2 == "")
            {
                sql += " and createddate>='" + time1 + " 00:00:00'";
            }
            else if (time1 == "" && time2 != "")
            {
                sql += " and createddate<='" + time2 + " 23:59:59'";
            }
            DataTable dt = SqlHelper.ExecuteSql(sql);
            return dt.Rows[0][0].ToString();
        }
        #endregion
        #region 获取洽谈次数
        public static string getfwcount(string custtype, string manager, string time1, string time2)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = "select count(1) from officedba.CustService where companycd='" + companyCD + "'";
            if (custtype != "")
            {
                sql += " and fashion=" + custtype;
            }
            if (manager != "")
            {
                sql += " and executant=" + manager;
            }
            if (time1 != "" && time2 != "")
            {
                sql += " and begindate between '" + time1 + " 00:00:00' and '" + time2 + "  23:59:59'";
            }
            else if (time1 != "" && time2 == "")
            {
                sql += " and begindate>='" + time1 + " 00:00:00'";
            }
            else if (time1 == "" && time2 != "")
            {
                sql += " and begindate<='" + time2 + " 23:59:59'";
            }
            DataTable dt = SqlHelper.ExecuteSql(sql);
            return dt.Rows[0][0].ToString();
        }
        #endregion


        #region 洽谈分析
        public static DataTable GetQTCountBytime( int custid, int manger, string begindate, string enddate, int timeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD),
                new SqlParameter("@custid",custid),
                new SqlParameter("@manager",manger),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType),
                
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetCustQTcount]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataTable GetQTCountBytimeDetails(string type,string timetype,string begindate, string enddate, string manger, string CustID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@manger", manger ),
                new SqlParameter("@CustID",CustID)
            };

            StringBuilder sql = new StringBuilder();
            if (type == "3")
            {
                sql.AppendLine(" select case when datename(week,a.createddate)<10 then datename(yy,a.createddate)+'年0'+datename(week,a.createddate)+'周' else  datename(yy,a.createddate)+'年'+datename(week,a.createddate)+'周' end timeType");
            }
            else if (type == "2")
            {
                sql.AppendLine(" select datename(yy,a.createddate)+'年'+datename(mm,a.createddate)+'月' timeType");
            }
            else if (type == "1")
            {
                sql.AppendLine(" select datename(yy,a.createddate)+'年' timeType");
            }
            sql.AppendLine(" ,B.CustName,A.* from officedba.CustTalk A left join officedba.CustInfo B");
            sql.AppendLine("on A.CustID=B.ID where A.CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and A.Status!=1 ");
            if (CustID.Trim().Length > 0)
            {
                sql.AppendLine("and A.CustID="+CustID+"");
            }
            if (manger.Trim().Length > 0)
            {
                sql.AppendLine("and a.linker="+manger+"");
            }

            sql.AppendLine("and convert(char(10),A.CreatedDate,120)>='" + begindate + "' and convert(char(10),A.CreatedDate,120)<='"+enddate+"' ");
            if (type == "3" && timetype != "")
            {
                sql.AppendLine(" and (datename(yy,a.createddate)+'年'+datename(week,a.createddate)+'周'='" + timetype + "' or datename(yy,a.createddate)+'年0'+datename(week,a.createddate)+'周'='" + timetype + "' )");
            }
            else if (type == "2" && timetype != "")
            {
                sql.AppendLine(" and datename(yy,a.createddate)+'年'+datename(mm,a.createddate)+'月'='" + timetype + "'");
            }
            else if (type == "1" && timetype != "")
            {
                sql.AppendLine(" and datename(yy,a.createddate)+'年'='" + timetype + "'");
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, "id ", param, ref TotalCount);
        }
        #endregion

        #region 洽谈分析
        public static DataTable GetCustsevicecountbytime(int custid, int manger, string begindate, string enddate, int timeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD),
                new SqlParameter("@custid",custid),
                new SqlParameter("@manager",manger),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType),
                
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetCustsevicecountbytime]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataTable GetCustsevicecountbytimedetails(string type, string timetype, string begindate, string enddate, string manger, string CustID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@manger", manger ),
                new SqlParameter("@CustID",CustID)
            };

            StringBuilder sql = new StringBuilder();
            if (type == "3")
            {
                sql.AppendLine(" select case when datename(week,b.begindate)<10 then  datename(yy,b.begindate)+'年0'+datename(week,b.begindate)+'周' else datename(yy,b.begindate)+'年'+datename(week,b.begindate)+'周' end timeType");
            }
            else if (type == "2")
            {
                sql.AppendLine(" select datename(yy,b.begindate)+'年'+datename(mm,b.begindate)+'月' timeType");
            }
            else if (type == "1")
            {
                sql.AppendLine(" select datename(yy,b.begindate)+'年' timeType");
            }
            sql.AppendLine(@",b.id,serveno,case when Fashion=1 then '远程支持' when Fashion=2 then '现场服务' else '综合服务' end fashion,custid,isnull(dbo.getCustname(custid),'') custname,executant,isnull(dbo.getEmployeeName(executant),'')employeename,custlinkman,c.linkmanname,begindate,title,b.servetype,a.typename from officedba.CustService b left join 
(select ID,TypeName,companycd from officedba.codepublictype b where  TypeFlag=4 and TypeCode=7  ) a on  a.Id=b.ServeType and a.companycd=b.companycd left join officedba.CustLinkMan c
on c.id=b.custlinkman");
            sql.AppendLine(" where b.CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ");
            if (CustID.Trim().Length > 0)
            {
                sql.AppendLine("and b.CustID=" + CustID + "");
            }
            if (manger.Trim().Length > 0)
            {
                sql.AppendLine("and b.executant=" + manger + "");
            }

            sql.AppendLine("and convert(char(10),b.begindate,120)>='" + begindate + "' and convert(char(10),b.begindate,120)<='" + enddate + "' ");
            if (type == "3" && timetype != "")
            {
                sql.AppendLine(" and (datename(yy,b.begindate)+'年'+datename(week,b.begindate)+'周'='" + timetype + "' or datename(yy,b.begindate)+'年0'+datename(week,b.begindate)+'周'='" + timetype + "')");
            }
            else if (type == "2" && timetype != "")
            {
                sql.AppendLine(" and datename(yy,b.begindate)+'年'+datename(mm,b.begindate)+'月'='" + timetype + "'");
            }
            else if (type == "1" && timetype != "")
            {
                sql.AppendLine(" and datename(yy,b.begindate)+'年'='" + timetype + "'");
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, "id ", param, ref TotalCount);
        }
        #endregion

        #region insertcustinfo
        public static void CustInfoAddReg(CustInfoModel custinfoM,TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.CustInfo (");
            strSql.Append("CompanyCD,CustNo,CustName,ReceiveAddress,UsedStatus,CustBig,CorrYYCode )");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustNo,@CustName,@ReceiveAddress,@UsedStatus,@CustBig,@CorrYYCode )");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@CustName", SqlDbType.VarChar,100),
					new SqlParameter("@ReceiveAddress", SqlDbType.VarChar,100),
                    new SqlParameter("@UsedStatus",SqlDbType.Char,1),
                    new SqlParameter("@CustBig",SqlDbType.Char,1),
                    new SqlParameter("@CorrYYCode", SqlDbType.VarChar,50),
					};
            parameters[0].Value = custinfoM.CompanyCD;
            parameters[1].Value = custinfoM.CustNo;
            parameters[2].Value = custinfoM.CustName;
            parameters[3].Value = custinfoM.ReceiveAddress;
            parameters[4].Value = custinfoM.UsedStatus;
            parameters[5].Value = custinfoM.CustBig;
            parameters[6].Value = custinfoM.CorrYYCode;
           
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion
        #region getcustno
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>客户列表结果集</returns>
        public static string GetCustNo(string CompanyCD, string CustName)
        {
            string str="";
            string sql = "select CustNo from officedba.CustInfo where CompanyCD='"+CompanyCD+"' and CustName='"+CustName+"' ";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                str = dt.Rows[0]["CustNo"].ToString();
            }
            return str;
        }
        #endregion

        //UpdateCustInfo creator and manager
        public static void UpdateCustInfo(LinkManModel LinkManM,int empID, TransactionManager tran)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("update officedba.CustInfo set ");
            sqlstr.AppendLine(" Creator=@empID,");
            sqlstr.AppendLine(" Manager=@empID where CustNo=@CustNo and CompanyCD=@CompanyCD");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
                     new SqlParameter("@empID",SqlDbType.Int,4),
                      new SqlParameter("@CustNo", SqlDbType.VarChar,50)
					};
            parameters[0].Value = LinkManM.CompanyCD;
            parameters[1].Value = empID;
            parameters[2].Value = LinkManM.CustNo;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), parameters);
        }
    }
}

