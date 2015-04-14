/**********************************************
 * 类作用：  生产进度汇报数据层处理
 * 建立人：   宋杰
 * 建立时间： 2010/03/21
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;


namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureDispatchReportDBHelper
    {

        #region 生产任务汇报单插入
        /// <summary>
        /// 生产任务汇报单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureProgressRpt(ManufactureDispatchReportModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            try
            {
                #region  生产任务汇报单插入添加SQL语句
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine("INSERT INTO officedba.ManufactureProgressRpt   ");
                sqlTask.AppendLine("           (CompanyCD                   ");
                sqlTask.AppendLine("           ,ReportNo                      ");
                sqlTask.AppendLine("           ,DeptID                     ");
                sqlTask.AppendLine("           ,Reporter                   ");
                sqlTask.AppendLine("           ,ReportDate                      ");
                sqlTask.AppendLine("           ,Remark                    ");
                sqlTask.AppendLine("           ,Confirmor             ");
                sqlTask.AppendLine("           ,Creator                  ");
                sqlTask.AppendLine("           ,CreateDate                     ");
                sqlTask.AppendLine("           ,BillStatus                  ");
                sqlTask.AppendLine("           ,ModifiedDate                      ");
                sqlTask.AppendLine("          ,ModifiedUserID)              ");
                sqlTask.AppendLine("     VALUES                             ");
                sqlTask.AppendLine("          (@CompanyCD                   ");
                sqlTask.AppendLine("           ,@ReportNo                     ");
                sqlTask.AppendLine("           ,@DeptID                    ");
                sqlTask.AppendLine("           ,@Reporter                  ");
                sqlTask.AppendLine("           ,@ReportDate                     ");
                sqlTask.AppendLine("           ,@Remark                   ");
                sqlTask.AppendLine("           ,@Confirmor            ");
                sqlTask.AppendLine("           ,@Creator                 ");
                sqlTask.AppendLine("           ,@CreateDate                    ");
                sqlTask.AppendLine("           ,@BillStatus                 ");
                sqlTask.AppendLine("           ,getdate()                   ");
                sqlTask.AppendLine("           ,'" + loginUserID + "')			");
                sqlTask.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlTask.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNO));
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                comm.Parameters.Add(SqlHelper.GetParameter("@Reporter", model.Reporter));
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportDate", model.ReportDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                listADD.Add(comm);
                #endregion

                #region 生产任务汇报单插入单明细信息添加SQL语句
                if (!String.IsNullOrEmpty(Convert.ToString(model.DetRealStartDate)) && !String.IsNullOrEmpty(Convert.ToString(model.DetRealStartDate)))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    //ReportNo,MorderNo,WorkTime,FinishNum,PassNum,PassPercent,RealStartDate,RealEndDate,Operator,Remark
                   // string[] dtReportNo = model.ReportNO.Split(',');
                    string[] dtMorderNo = model.DetMorderNo.Split(',');
                    string[] dtFinishNum = model.DetFinishNum.Split(',');
                    string[] dtPassNum = model.DetPassNum.Split(',');
                    string[] dtOperator = model.DetOperator.Split(',');
                    string[] dtRealStartDate = Convert.ToString(model.DetRealStartDate).Split(',');
                    string[] dtEndDate = Convert.ToString(model.DetEndDate).Split(',');
                    string[] dtWorkTime = Convert.ToString(model.DetWorkTime).Split(',');
                //    string[] dtRemark = model.DetRemark.Split(',');



                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (dtMorderNo.Length >= 1)
                    {
                        for (int i = 0; i < dtMorderNo.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.ManufactureProgressRptDetail");
                            cmdsql.AppendLine("           (CompanyCD");
                            cmdsql.AppendLine("          ,ReportNo");
                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                            //        cmdsql.AppendLine("           ,ReportNo");
                            //    }
                            //}
                            if (dtMorderNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtMorderNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,MorderNO");
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FinishNum");
                                }
                            }
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,PassNum");
                                }
                            }
                             cmdsql.AppendLine("           ,PassPercent");
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,RealStartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,RealEndDate");
                                }
                            }
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,WorkTime");
                                }
                            }
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,Operator)");
                                }
                            }
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        cmdsql.AppendLine("           ,FromLineNo");
                            //    }
                            //}
                            //cmdsql.AppendLine("           ,ModifiedDate");
                            //cmdsql.AppendLine("           ,ModifiedUserID)");
                            cmdsql.AppendLine("     VALUES");
                            cmdsql.AppendLine("           (@CompanyCD");
                            cmdsql.AppendLine("          ,@ReportNo");
                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                            //        cmdsql.AppendLine("           ,@ReportNo");
                            //    }
                            //}
                            if (dtMorderNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtMorderNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@MorderNo");
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FinishNum");
                                }
                            }
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@PassNum");
                                }
                            }

                            cmdsql.AppendLine("           ,@PassPercent");
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@RealStartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@RealEndDate");
                                }
                            }

                            //cmdsql.AppendLine("           ,@FromType");
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,"+dtWorkTime[i].ToString()+" ");
                                }
                            }
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@Operator)");
                                }
                            }
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        cmdsql.AppendLine("           ,@FromLineNo");
                            //    }
                            //}

                            //cmdsql.AppendLine("           ,getdate()  ");
                            //cmdsql.AppendLine("           ,'" + loginUserID + "')");
                            

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNO));
                            if (dtMorderNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtMorderNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@MorderNo", dtMorderNo[i].ToString()));
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FinishNum", dtFinishNum[i].ToString()));
                                }
                            }
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@PassNum", dtPassNum[i].ToString()));
                                }
                            }

                            comms.Parameters.Add(SqlHelper.GetParameter("@PassPercent", (Double.Parse(dtPassNum[i].ToString()) / Double.Parse(dtFinishNum[i].ToString())).ToString()));
                                
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@RealStartDate", dtRealStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@RealEndDate", dtEndDate[i].ToString()));
                                }
                            }
                            //comms.Parameters.Add(SqlHelper.GetParameter("@FromType", dtOperator[i].ToString()));
                            //if (dtWorkTime[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                            //    {
                            //        comms.Parameters.Add(SqlHelper.GetParameter("@WorkTime", dtWorkTime[i].ToString()));
                            //    }
                            //}
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@Operator", dtOperator[i].ToString()));
                                }
                            }
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                            //    }
                            //}

                            listADD.Add(comms);
                        }
                    }
                }
                #endregion

                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                else
                {
                    ID = "0";
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改生产进度汇报单和生产进度汇报单明细信息
        /// <summary>
        /// 修改生产进度汇报单和生产进度汇报单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureProgressRpt(ManufactureDispatchReportModel model, Hashtable htExtAttr, string loginUserID, string UpdateID)
        {
            //获取登陆用户ID
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }
            #region  生产任务单修改SQL语句
            StringBuilder sqlDet = new StringBuilder();
            sqlDet.AppendLine("UPDATE officedba.ManufactureProgressRpt");
            sqlDet.AppendLine("   SET CompanyCD = @CompanyCD");
            sqlDet.AppendLine("      ,ReportNo = @ReportNo");
            sqlDet.AppendLine("      ,DeptID = @DeptID");
            sqlDet.AppendLine("      ,Reporter = @Reporter");
            sqlDet.AppendLine("      ,ReportDate = @ReportDate");
            sqlDet.AppendLine("      ,Remark = @Remark");
            sqlDet.AppendLine("      ,Confirmor = @Confirmor");
            sqlDet.AppendLine("      ,CreateDate = @CreateDate");
            sqlDet.AppendLine("      ,Creator = @Creator");
            sqlDet.AppendLine("      ,BillStatus = @BillStatus");
            sqlDet.AppendLine("      ,ModifiedDate = getdate()");
            sqlDet.AppendLine("      ,ModifiedUserID = '" + loginUserID + "'");
            sqlDet.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlDet.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNO));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Reporter", model.Reporter));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportDate", model.ReportDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            listADD.Add(comm);
            #endregion


            #region 生产任务单元明细信息更新语句
            //1.先删除不在生产任务单明细中的
            //2.更新明细中的ID
            //3.添加其它明细

            #region 先删除不在生产任务单明细中的
            if (!string.IsNullOrEmpty(UpdateID))
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.ManufactureProgressRptDetail where CompanyCD=@CompanyCD and ReportNo=@ReportNo and  ID not in(" + UpdateID + ")");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@ReportNO", model.ReportNO));

                listADD.Add(commDel);
            }
            else
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.ManufactureProgressRptDetail where CompanyCD=@CompanyCD and ReportNo=@ReportNo");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@ReportNO", model.ReportNO));

                listADD.Add(commDel);
            }
            #endregion


            #region 添加或更新操作
            string[] updateID = UpdateID.Split(',');
            if (!string.IsNullOrEmpty(UpdateID) && updateID.Length > 0)
            {
                if (!String.IsNullOrEmpty(model.ReportNO) && !String.IsNullOrEmpty(model.DetWorkTime) && !String.IsNullOrEmpty(model.DetPassNum) && !String.IsNullOrEmpty(model.DetFinishNum) && !String.IsNullOrEmpty(model.DetEndDate))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    //ReportNo,WorkTime ,FinishNum ,PassNum,RealStartDate ,RealEndDate ,Operator 
                    //string[] dtReportNo = model.ReportNO.Split(',');
                    string[] dtWorkTime = model.DetWorkTime.Split(',');
                    string[] dtFinishNum = model.DetFinishNum.Split(',');
                    string[] dtPassNum = model.DetPassNum.Split(',');
                 //   string[] dtPassPercent = model.DetPassPercent.Split(',');
                    string[] dtRealStartDate = model.DetRealStartDate.Split(',');
                    string[] dtRealEndDate = model.DetEndDate.Split(',');
                    string[] dtOperator = model.DetOperator.Split(',');
                    for (int i = 0; i < updateID.Length; i++)
                    {
                        int intUpdateID = int.Parse(updateID[i].ToString());
                        if (intUpdateID > 0)
                        {

                            #region 更新MRP明细中的ID
                            StringBuilder sqlEdit = new StringBuilder();
                            sqlEdit.AppendLine("UPDATE officedba.ManufactureProgressRptDetail		");
                            sqlEdit.AppendLine("SET                                         ");
                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                            //        sqlEdit.AppendLine("       ReportNo = @ReportNo						");
                            //    }
                            //}
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      WorkTime = @WorkTime				");
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,FinishNum = @FinishNum			");
                                }
                            }
                            //if (userInfo.IsMoreUnit)
                            //{
                            //    sqlEdit.AppendLine("      ,UsedUnitID = @UsedUnitID			");
                            //    sqlEdit.AppendLine("      ,UsedUnitCount = @UsedUnitCount			");
                            //    sqlEdit.AppendLine("      ,ExRate = @ExRate			");
                            //}
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,PassNum = @PassNum						");
                                }
                            }
                            else
                            {
                                sqlEdit.AppendLine("      ,PassNum = null						");
                            }
                          
                            sqlEdit.AppendLine("      ,PassPercent = @PassPercent					");
                               
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,RealStartDate = @RealStartDate				");
                                }
                            }
                            if (dtRealEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealEndDate[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,RealEndDate = @RealEndDate					");
                                }
                            }
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,Operator = @Operator				");
                                }
                            }
                            else
                            {
                                sqlEdit.AppendLine("      ,Operator = null				");
                            }
                            //if (dtFromBillNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                            //    {
                            //        sqlEdit.AppendLine("      ,FromBillNo = @FromBillNo				");
                            //    }
                            //}
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        sqlEdit.AppendLine("      ,FromLineNo = @FromLineNo				");
                            //    }
                            //}

                            //sqlEdit.AppendLine("      ,ModifiedDate = getdate()			    ");
                            //sqlEdit.AppendLine("      ,ModifiedUserID = '" + loginUserID + "'	");
                            sqlEdit.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID		");

                            SqlCommand commEdit = new SqlCommand();
                            commEdit.CommandText = sqlEdit.ToString();

                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                            //        commEdit.Parameters.Add(SqlHelper.GetParameter("@ReportNo", dtReportNo[i].ToString()));
                            //    }
                            //}
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@WorkTime", dtWorkTime[i].ToString()));
                                }
                            }

                           
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@FinishNum", dtFinishNum[i].ToString()));
                                }
                            }
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@PassNum", dtPassNum[i].ToString()));
                                }
                            }
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@PassPercent", (Double.Parse(dtPassNum[i].ToString()) / Double.Parse(dtFinishNum[i].ToString())).ToString()));
                               
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@RealStartDate", dtRealStartDate[i].ToString()));
                                }
                            }
                            if (dtRealEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealEndDate[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@RealEndDate", dtRealEndDate[i].ToString()));
                                }
                            }
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@Operator", dtOperator[i].ToString()));
                                }
                            }
                            //if (dtFromBillNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                            //    {
                            //        commEdit.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                            //    }
                            //}
                            //if (dtOperator[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                            //    {
                            //        commEdit.Parameters.Add(SqlHelper.GetParameter("@Operator", dtOperator[i].ToString()));
                            //    }
                            //}
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@ID", intUpdateID));

                            listADD.Add(commEdit);
                            #endregion
                        }
                        else
                        {
                            #region 添加MRP明细中的ID
                            //页面上这些字段都是必填，数组的长度必须是相同的
                            System.Text.StringBuilder sqlIn = new System.Text.StringBuilder();
                            sqlIn.AppendLine("INSERT INTO officedba.ManufactureProgressRptDetail");
                            sqlIn.AppendLine("           (CompanyCD");
                            sqlIn.AppendLine("           ,ReportNo");
                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                                  //  sqlIn.AppendLine("           ,ReportNo");
                            //    }
                            //}

                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,WorkTime");
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FinishNum");
                                }
                            }
                            //if (userInfo.IsMoreUnit)
                            //{
                            //    sqlIn.AppendLine("           ,UsedUnitID");
                            //    sqlIn.AppendLine("           ,UsedUnitCount");
                            //    sqlIn.AppendLine("           ,ExRate");
                            //}
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,PassNum");
                                }
                            }
                             sqlIn.AppendLine("           ,PassPercent");
             
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,RealStartDate");
                                }
                            }
                            if (dtRealEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,RealEndDate");
                                }
                            }

                            //sqlIn.AppendLine("           ,FromType");
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,Operator)");
                                }
                            }
                            //if (dtFromBillNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                            //    {
                            //        sqlIn.AppendLine("           ,FromBillNo");
                            //    }
                            //}
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        sqlIn.AppendLine("           ,FromLineNo");
                            //    }
                            //}
                            //sqlIn.AppendLine("           ,ModifiedDate");
                            //sqlIn.AppendLine("           ,ModifiedUserID)");
                            sqlIn.AppendLine("     VALUES");
                            sqlIn.AppendLine("           (@CompanyCD");
                            sqlIn.AppendLine("           ,@ReportNo");
                            //if (dtReportNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtReportNo[i].ToString().Trim()))
                            //    {
                                   // sqlIn.AppendLine("           ,@ReportNo");
                            //    }
                            //}
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@WorkTime");
                                }
                            }
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FinishNum");
                                }
                            }
                            //if (userInfo.IsMoreUnit)
                            //{
                            //    sqlIn.AppendLine("           ,@UsedUnitID");
                            //    sqlIn.AppendLine("           ,@UsedUnitCount");
                            //    sqlIn.AppendLine("           ,@ExRate");
                            //}
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@PassNum");
                                }
                            }
                            sqlIn.AppendLine("           ,@PassPercent");
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@RealStartDate");
                                }
                            }
                            if (dtRealEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@RealEndDate");
                                }
                            }

                            //sqlIn.AppendLine("           ,@FromType");
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@Operator)");
                                }
                            }
                            //if (dtFromBillNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                            //    {
                            //        sqlIn.AppendLine("           ,@FromBillNo");
                            //    }
                            //}
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        sqlIn.AppendLine("           ,@FromLineNo");
                            //    }
                            //}

                            //sqlIn.AppendLine("           ,getdate()  ");
                            //sqlIn.AppendLine("           ,'" + loginUserID + "')");

                            SqlCommand commIn = new SqlCommand();
                            commIn.CommandText = sqlIn.ToString();
                            commIn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@ReportNO", model.ReportNO));
                           // commIn.Parameters.Add(SqlHelper.GetParameter("@ReportNo", dtReportNo[i].ToString()));
                            if (dtWorkTime[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtWorkTime[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@WorkTime", dtWorkTime[i].ToString()));
                                }
                            }
                            // ReportNo,WorkTime ,FinishNum ,PassNum,RealStartDate ,RealEndDate ,Operator 
                            if (dtFinishNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFinishNum[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FinishNum", dtFinishNum[i].ToString()));
                                }
                            }
                            //if (userInfo.IsMoreUnit)
                            //{
                            //    commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                            //    commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                            //    commIn.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            //}
                            if (dtPassNum[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtPassNum[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@PassNum", dtPassNum[i].ToString()));
                                }
                            }
                            commIn.Parameters.Add(SqlHelper.GetParameter("@PassPercent", (Double.Parse(dtPassNum[i].ToString()) /Double.Parse(dtFinishNum[i].ToString())).ToString()));
                            if (dtRealStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealStartDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@RealStartDate", dtRealStartDate[i].ToString()));
                                }
                            }
                            if (dtRealEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRealEndDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@RealEndDate", dtRealEndDate[i].ToString()));
                                }
                            }

                            //commIn.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                            if (dtOperator[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtOperator[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@Operator", dtOperator[i].ToString()));
                                }
                            }
                            //if (dtFromBillNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                            //    {
                            //        commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                            //    }
                            //}
                            //if (dtFromLineNo[i].ToString().Length > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                            //    {
                            //        commIn.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                            //    }
                            //}

                            listADD.Add(commIn);
                            #endregion
                        }
                    }
                }
            }
            #endregion


            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 生产进度汇报单详细信息
        /// <summary>
        /// 生产进度汇报单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetProgressRptInfo(string ReportNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("				select	a.CompanyCD,a.ID,c.EmployeeName as PricipalReal,a.ReportNO,a.Reporter,Convert(varchar(10),a.reportdate,120) as reportdate,");
            infoSql.AppendLine("						a.DeptID,f.DeptName,dbo.getEmployeeName(a.Reporter) as ReportMan,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.BillStatus,a.Creator,b.EmployeeName as CreatorReal,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("						a.Confirmor,c.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("						a.Remark,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,h.EmployeeName as ModifiedUserReal");
            infoSql.AppendLine("				from officedba.ManufactureProgressRpt a");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as b on a.Creator=b.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as c on a.Confirmor=c.ID");
            infoSql.AppendLine("            					left join officedba.DeptInfo as f on a.DeptID=f.ID");
            infoSql.AppendLine("            					left join officedba.UserInfo as i on a.ModifiedUserID=i.UserID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo h on i.EmployeeID=h.ID");
            infoSql.AppendLine(") as info");
            infoSql.AppendLine("Where ReportNO=@ReportNo");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportNo", ReportNo));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 生产进度汇报单明细详细信息
        /// <summary>
        /// 生产进度汇报单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetProgressRptDetailInfo(string ReportNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("				select  b.chargeman chargemanid,a.Operator Operatorid ,a.CompanyCD,a.ID, a.ReportNo,a.morderNo,Convert(varchar(10),a.RealStartDate,120)as RealStartDate ,");
            detSql.AppendLine("						Convert(varchar(10),a.RealEndDate,120)as RealEndDate,isnull(convert(numeric(12,2),a.finishNum),0) as finishNum,convert(numeric(12,2),a.passNum) as passNum,a.worktime,dbo.getEmployeeName(a.Operator)as Operator,");
            detSql.AppendLine("						dbo.getEmployeeName(b.chargeman) as chargeman,convert(numeric(12,2),c.ProductCount) as ProductCount,b.timeunit,");
            detSql.AppendLine("						d.productName,c.FromBillNo,e.sequname,f.WCName,(isnull(convert(numeric(12,2),a.finishNum),0)-isnull(convert(numeric(12,2),a.passNum),0))as NotPassNum,");
            detSql.AppendLine("				a.PassPercent  from officedba.ManufactureProgressRptDetail a");
            detSql.AppendLine("				left outer join officedba.ManufacturedispatchingDetail b on a.MorderNo = b.ID and a.companyCD=b.companyCD");
            detSql.AppendLine("				LEFT OUTER JOIN officedba.ManufactureTaskDetail c on c.id = b.ManuTaskDetilID and c.companyCD = b.companyCD");
            detSql.AppendLine("				LEFT OUTER  JOIN officedba.ProductInfo d on d.id = c.ProductID and d.companyCD=b.companyCD");
            detSql.AppendLine("				left outer join officedba.StandardSequ e on e.id = b.SequNo and e.companyCD = b.companyCD");
            detSql.AppendLine("				left outer join officedba.WorkCenter f on f.id= e.WCID and f.companyCD=E.companyCD");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where ReportNo=@ReportNo ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportNo", ReportNo));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 弹出对话框 查询：生产派工单
        /// <summary>
        /// 生产派工单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureDispatchReport(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string Purchase, string TaskNo, string GoodsName, string WorkCenter, string StartLinkDate, string EndLinkDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.modifieddate mdate,D.ID as ID,F.WCName as WCName,isnull(D.TaskID,'') as DispatchNo,isnull(A.TaskNO,'') as TaskNo,isnull(C.ProductName,'') as ProductName ,convert(decimal(12,0),isnull(B.ProductedCount,0)) as ProductedCount       ");
            sql.AppendLine("      ,convert(decimal(12,0),isnull(B.ProductCount,0)) as ProductCount,isnull(convert(varchar(10),B.StartDate,120),'') as StartDate,isnull(convert(varchar(10),B.EndDate,120),'') as EndDate        ");
            sql.AppendLine("      ,isnull(convert(varchar(10),D.StartDate,120),'') as StartDate1,isnull(convert(varchar(10),D.EndDate,120),'') as EndDate1,isnull(E.SequName,'') as SequName       ");
            sql.AppendLine("      ,isnull(convert(varchar(10),D.worktime,120),0) as WorkTime, isnull(D.totaltime,0) as TotalTime    ");
            sql.AppendLine(" FROM officedba.Manufacturedispatching A                                 ");
            sql.AppendLine("LEFT OUTER JOIN officedba.ManufactureTaskDetail B on A.TaskNO = B.TaskNO and A.CompanyCD = B.CompanyCD ");
            sql.AppendLine("LEFT OUTER JOIN officedba.ProductInfo C on C.ID = B.ProductID and C.CompanyCD = B.CompanyCD ");
            sql.AppendLine("INNER JOIN officedba.ManufacturedispatchingDetail D on  D.TaskID = A.id and D.ManuTaskDetilID=B.ID and D.CompanyCD = A.CompanyCD");
            sql.AppendLine("LEFT OUTER JOIN officedba.StandardSequ E on E.ID = D.SequNo and E.CompanyCD = D.CompanyCD");
            sql.AppendLine("LEFT OUTER JOIN officedba.WorkCenter F on F.ID = E.WCID and F.CompanyCD = E.CompanyCD");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            if (Purchase != "" && Purchase != null)
            {
                sql.AppendLine(" AND D.ID like @DispatchNo ");
            }
            if (TaskNo != null && TaskNo != "")
            {
                sql.AppendLine(" AND A.TaskNo like @TaskNo");
            }
            if (GoodsName != "" && GoodsName != null)
            {
                sql.AppendLine(" AND C.ProductName like  @productname ");
            }
            if (WorkCenter != null && WorkCenter != "")
            {
                sql.AppendLine(" AND F.WCName like  @WCName");
            }
            if (StartLinkDate != "" && StartLinkDate != null)
            {
                sql.AppendLine(" AND B.StartDate >= @StartLinkDate ");
            }
            if (EndLinkDate != null && EndLinkDate != "")
            {
                sql.AppendLine(" AND B.EndDate <= @EndLinkDate");
            }
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispatchNo", "%" + Purchase + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + TaskNo + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@productname", "%" + GoodsName + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WCName", "%" + WorkCenter + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartLinkDate", StartLinkDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndLinkDate", EndLinkDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        

        /// <summary>
        /// 查询派工单
        /// </summary>
        /// <param name="strID">ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetManufactureDispatchReportByCheck(string strID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string sql = "";
            sql += "            select a.ID as ID, isnull(dbo.getEmployeeName(a.chargeman),'')as Chargeman, isnull(c.SequName,'') as SequName,isnull(d.wcname,'')as Wcname,                   ";
            sql += "            isnull(convert(numeric(12," + userInfo.SelPoint + "),e.ProductCount),0)as ProductCount,isnull(f.productname,'') as Productname, isnull(convert(numeric(12,2),g.worktime),0)as WorkTime,";
            sql += "            isnull(e.FromBillNo,'') as FromBillID ,isnull(convert(varchar(10),g.RealStartDate,120),'') as RealStartDate, ";
            sql += "            isnull(a.timeunit,'') as TimeUnit,isnull(convert(numeric(12," + userInfo.SelPoint + "),e.productedCount),0) as ProductedCount,isnull(convert(numeric(12," + userInfo.SelPoint + "),e.passcount),0) as PassCount,                                                 ";
            sql += "            isnull(convert(varchar(10),g.RealEndDate,120),'') as RealEndDate  from officedba.ManufacturedispatchingDetail a            ";
            sql += "            left outer join officedba.Manufacturedispatching b on a.taskid = b.id and a.companycd = b.companycd                             ";
            sql += "            left outer join officedba.StandardSequ c on c.id = a.sequno and c.companycd = a.companycd                                      ";
            sql += "            left outer join officedba.WorkCenter d on d.id =  c.wcid and d.companycd = c.companycd                                         ";
            sql += "            left outer join  officedba.ManufactureTaskDetail e on e.id = a.ManuTaskDetilID and e.companycd = a.companycd                    ";
            sql += "            LEFT OUTER JOIN officedba.ProductInfo f on f.id = e.productid and f.companycd = e.companycd                                    ";
            sql += "            left outer join officedba.ManufactureProgressRptDetail g on g.morderno = a.id and g.companycd = a.companycd                     ";
            sql += "            where a.CompanyCD=@CompanyCD                                                               ";
            string allid = "";
            StringBuilder sb = new System.Text.StringBuilder();
            string[] IdS = null;
            //userid = userid.Substring(0, userid.Length);
            IdS = strID.Split(',');
            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allid = sb.ToString().Replace("''", "','");
            if (!string.IsNullOrEmpty(strID))
            {
                sql += " AND a.ID in  (" + allid + ")";
            }

            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //编号

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion



        #region 生产进度汇报单详细信息
        /// <summary>
        /// 生产进度汇报单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneProgressRptInfo(string ReportNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("				select	a.CompanyCD,a.ID,c.EmployeeName as PricipalReal,a.ReportNO,a.Reporter,Convert(varchar(10),a.reportdate,120) as reportdate,");
            infoSql.AppendLine("						a.DeptID,f.DeptName,dbo.getEmployeeName(a.Reporter) as ReportMan,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.BillStatus,a.Creator,b.EmployeeName as CreatorReal,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("						a.Confirmor,c.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("						a.Remark,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,h.EmployeeName as ModifiedUserReal");
            infoSql.AppendLine("				from officedba.ManufactureProgressRpt a");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as b on a.Creator=b.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as c on a.Confirmor=c.ID");
            infoSql.AppendLine("            					left join officedba.DeptInfo as f on a.DeptID=f.ID");
            infoSql.AppendLine("            					left join officedba.UserInfo as i on a.ModifiedUserID=i.UserID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo h on i.EmployeeID=h.ID");
            infoSql.AppendLine(") as info");
            infoSql.AppendLine("Where ReportNo=@ReportNo");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportNo",ReportNo));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 生产进度汇报单明细详细信息
        /// <summary>
        /// 生产进度汇报单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneProgressRptDetailInfo(string ReportNO)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("				select a.CompanyCD,a.ID, a.ReportNo,a.morderNo,Convert(varchar(10),a.RealStartDate,120)as RealStartDate ,");
            detSql.AppendLine("						Convert(varchar(10),a.RealEndDate,120)as RealEndDate,isnull(convert(numeric(12,2),a.finishNum),0) as finishNum,convert(numeric(12,2),a.passNum) as passNum,a.worktime,dbo.getEmployeeName(a.Operator)as Operator,");
            detSql.AppendLine("						dbo.getEmployeeName(b.chargeman) as chargeman,convert(numeric(12,2),c.ProductCount) as ProductCount,b.timeunit,");
            detSql.AppendLine("						d.productName,c.FromBillNo,e.sequname,f.WCName,(isnull(convert(numeric(12,2),a.finishNum),0)-isnull(convert(numeric(12,2),a.passNum),0))as NotPassNum,");
            detSql.AppendLine("				a.PassPercent  from officedba.ManufactureProgressRptDetail a");
            detSql.AppendLine("				left outer join officedba.ManufacturedispatchingDetail b on a.MorderNo = b.ID");
            detSql.AppendLine("				LEFT OUTER JOIN officedba.ManufactureTaskDetail c on c.id = b.ManuTaskDetilID");
            detSql.AppendLine("				LEFT OUTER  JOIN officedba.ProductInfo d on d.id = c.ProductID");
            detSql.AppendLine("				left outer join officedba.StandardSequ e on e.id = b.SequNo");
            detSql.AppendLine("				left outer join officedba.WorkCenter f on f.id= e.WCID");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where ReportNO=@ReportNO ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportNO", ReportNO));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

    }
}
