/**********************************************
 * 类作用：   生产任务单数据层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/23
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.SellManager;
using System.Collections.Generic;
using XBase.Data.Common;


namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureTaskDBHelper
    {
        #region 生产任务单插入
        /// <summary>
        /// 生产任务单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureTask(ManufactureTaskModel model, Hashtable htExtAttr, string loginUserID, out string ID, string[] strCustomdetail)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  生产任务单添加SQL语句
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine("INSERT INTO officedba.ManufactureTask   ");
                sqlTask.AppendLine("           (CompanyCD                   ");
                sqlTask.AppendLine("           ,TaskNo                      ");
                sqlTask.AppendLine("           ,Subject                     ");
                sqlTask.AppendLine("           ,Principal                   ");
                sqlTask.AppendLine("           ,DeptID                      ");
                sqlTask.AppendLine("           ,FromType                    ");
                sqlTask.AppendLine("           ,ManufactureType             ");
                sqlTask.AppendLine("           ,CountTotal                  ");
                sqlTask.AppendLine("           ,BillStatus                  ");
                sqlTask.AppendLine("           ,Creator                     ");
                sqlTask.AppendLine("           ,CreateDate                  ");
                sqlTask.AppendLine("           ,Remark                      ");
                sqlTask.AppendLine("           ,DocumentURL                 ");
                sqlTask.AppendLine("           ,ProjectID                   ");
                sqlTask.AppendLine("           ,ModifiedDate                ");
                //--------------------20121212 洛阳电镀问题 添加----------------------------//
              //  sqlTask.AppendLine("           ,Color,MateNo,GoodProbability,ProRemark");
                //-------------------------------------------------------------------------//
                sqlTask.AppendLine("          ,ModifiedUserID)              ");
                sqlTask.AppendLine("     VALUES                             ");
                sqlTask.AppendLine("          (@CompanyCD                   ");
                sqlTask.AppendLine("           ,@TaskNo                     ");
                sqlTask.AppendLine("           ,@Subject                    ");
                sqlTask.AppendLine("           ,@Principal                  ");
                sqlTask.AppendLine("           ,@DeptID                     ");
                sqlTask.AppendLine("           ,@FromType                   ");
                sqlTask.AppendLine("           ,@ManufactureType            ");
                sqlTask.AppendLine("           ,@CountTotal                 ");
                sqlTask.AppendLine("           ,@BillStatus                 ");
                sqlTask.AppendLine("           ,@Creator                    ");
                sqlTask.AppendLine("           ,@CreateDate                 ");
                sqlTask.AppendLine("           ,@Remark                     ");
                sqlTask.AppendLine("           ,@DocumentURL                ");
                sqlTask.AppendLine("           ,@ProjectID                  ");
                sqlTask.AppendLine("           ,getdate()                   ");
                //------------------- 20121212 洛阳电镀问题 添加------------------------//
               // sqlTask.AppendLine("           ,@Color,@MateNo,@GoodProbability,@ProRemark");
                //---------------------------------end-------------------------------------//
                sqlTask.AppendLine("           ,'"+loginUserID+"')			");
                sqlTask.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlTask.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@DocumentURL", model.DocumentURL));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                //------------------- 20121212 洛阳电镀问题 添加------------------------//
               // comm.Parameters.Add(SqlHelper.GetParameter("@Color",model.Color));
              //  comm.Parameters.Add(SqlHelper.GetParameter("@MateNo",model.MateNo));
               // comm.Parameters.Add(SqlHelper.GetParameter("@GoodProbability",model.GoodProbability));
               // comm.Parameters.Add(SqlHelper.GetParameter("@ProRemark",model.ProRemark));
                //-----------------------------------end-----------------------------------//
                listADD.Add(comm);
                #endregion

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                #region 生产任务单明细信息添加SQL语句
                if (!String.IsNullOrEmpty(model.DetSortNo) && !String.IsNullOrEmpty(model.DetProductID) && !String.IsNullOrEmpty(model.DetProductCount) && !String.IsNullOrEmpty(model.DetStartDate) && !String.IsNullOrEmpty(model.DetEndDate))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    string[] dtSortNo = model.DetSortNo.Split(',');
                    string[] dtProductID = model.DetProductID.Split(',');
                    string[] dtProductCount = model.DetProductCount.Split(',');
                    string[] dtBomID = model.DetBomID.Split(',');
                    string[] dtRouteID = model.DetRouteID.Split(',');
                    string[] dtStartDate = model.DetStartDate.Split(',');
                    string[] dtEndDate = model.DetEndDate.Split(',');
                    string[] dtFromType = model.DetFromType.Split(',');
                    string[] dtFromBillID = model.DetFromBillID.Split(',');
                    string[] dtFromBillNo = model.DetFromBillNo.Split(',');
                    string[] dtFromLineNo = model.DetFromLineNo.Split(',');
                    string[] dtUsedUnitID = model.DetUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.DetUsedUnitCount.Split(',');
                    string[] dtExRate = model.DetExRate.Split(',');

                    string[] TotalSquare = model.TotalSquare.Split(',');
                    string[] Pnumber = model.Pnumber.Split(',');
                    string[] Pnumberid = model.Pnumberid.Split(',');
                    string[] AbrasionResistid = model.AbrasionResistid.Split(',');
                    string[] AbrasionResist = model.AbrasionResist.Split(',');
                    string[] BalancePaperid = model.BalancePaperid.Split(',');
                    string[] BalancePaper = model.BalancePaper.Split(',');
                    string[] BaseMaterialid = model.BaseMaterialid.Split(',');
                    string[] BaseMaterial = model.BaseMaterial.Split(',');
                    string[] SurfaceTreatment = model.SurfaceTreatment.Split(',');
                    string[] BackBottomPlate = model.BackBottomPlate.Split(',');
                    string[] BuckleType = model.BuckleType.Split(',');
                    string[] PieceCount = model.PieceCount.Split(',');
                    string[] TotalNumber = model.TotalNumber.Split(',');
                    string[] pakeid = model.Pakeageid.Split(',');
                    //------------------- 20121212 洛阳电镀问题 添加------------------------//@Color,@MateNo,@GoodProbability,@ProRemark"
                    string[] Color = model.Color.Split(',');
                    string[] MateNo=model.MateNo.Split(',');
                    string[] GoodProbability = model.GoodProbability.Split(',');
                    string[] ProRemark = model.ProRemark.Split(',');
                    //----------------------------------------------------------------------//

                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (dtProductID.Length >= 1)
                    {
                        for (int i = 0; i < dtProductID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.ManufactureTaskDetail");  
                            cmdsql.AppendLine("           (CompanyCD");                  
                            cmdsql.AppendLine("           ,TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,SortNo");                
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,ProductID");    
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,ProductCount");    
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("           ,UsedUnitID");
                                cmdsql.AppendLine("           ,UsedUnitCount");
                                cmdsql.AppendLine("           ,ExRate");   
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,BomID");       
                                }
                            }
                            //---------20121212 洛阳电镀问题 添加 ---------------------//
                            if (Color[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,Color");
                                }
                            }
                            if (GoodProbability[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,GoodProbability");
                                }
                            }
                            if (MateNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,MateNo");
                                }
                            }
                            if (ProRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,ProRemark");
                                }
                            }
                            //---------------------------------------------------------//
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,RouteID");     
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,StartDate");   
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,EndDate");  
                                }
                            }

                            cmdsql.AppendLine("           ,FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromBillID");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromLineNo");
                                }
                            }
                            if (userInfo.Version == "floor")
                            {
                                if (TotalSquare[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalSquare[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,TotalSquare ");
                                    }
                                }
                                if (Pnumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumber[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,Pnumber  ");
                                    }
                                }
                                if (Pnumberid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumberid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,Pnumberid  ");
                                    }
                                }
                                if (AbrasionResist[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResist[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,AbrasionResist  ");
                                    }
                                }
                                if (AbrasionResistid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResistid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,AbrasionResistid  ");
                                    }
                                }
                                if (BalancePaper[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaper[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BalancePaper ");
                                    }
                                }

                                if (BalancePaperid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaperid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BalancePaperid");
                                    }
                                }
                                if (BaseMaterial[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterial[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BaseMaterial");
                                    }
                                }
                                if (BaseMaterialid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterialid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BaseMaterialid ");
                                    }
                                }
                                if (SurfaceTreatment[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(SurfaceTreatment[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,SurfaceTreatment");
                                    }
                                }
                                if (BackBottomPlate[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BackBottomPlate[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BackBottomPlate");
                                    }
                                }
                                if (BuckleType[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BuckleType[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,BuckleType");
                                    }
                                }

                                if (PieceCount[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(PieceCount[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,PieceCount");
                                    }
                                }

                                if (TotalNumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalNumber[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,TotalNumber");
                                    }
                                }
                                if (pakeid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(pakeid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,pakeages");
                                    }
                                }
                            }

                            cmdsql.AppendLine("           ,ModifiedDate");               
                            cmdsql.AppendLine("           ,ModifiedUserID");
                            //添加扩展项
                            cmdsql.Append(GetBillTableCellsDBHelper.insertCustomFilder(strCustomdetail, (i + 1)));
                            cmdsql.AppendLine("     )VALUES");                            
                            cmdsql.AppendLine("           (@CompanyCD");                 
                            cmdsql.AppendLine("           ,@TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@SortNo");      
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ProductID");    
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ProductCount");      
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("           ,@UsedUnitID");
                                cmdsql.AppendLine("           ,@UsedUnitCount");
                                cmdsql.AppendLine("           ,@ExRate");   
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@BomID"); 
                                }
                            }
                            //-----------20121212 洛阳电镀问题 添加-------------------//
                            if (Color[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,@Color");
                                }
                            }

                            if (GoodProbability[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,@GoodProbability");
                                }
                            }
                            if (MateNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,@MateNo");
                                }
                            }
                            if (ProRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("   ,@ProRemark");
                                }
                            }
                            //-------------------------------------------------------//
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@RouteID"); 
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@StartDate");    
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@EndDate"); 
                                }
                            }

                            cmdsql.AppendLine("           ,@FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromBillID ");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromLineNo");
                                }
                            }
                            #region
                            if (userInfo.Version == "floor")
                            {
                                if (TotalSquare[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalSquare[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + TotalSquare[i].ToString() + "'");
                                    }
                                }
                                if (Pnumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumber[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + Pnumber[i].ToString() + "'");
                                    }
                                }
                                if (Pnumberid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumberid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + Pnumberid[i].ToString() + "'");
                                    }
                                }
                                if (AbrasionResist[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResist[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + AbrasionResist[i].ToString() + "'");
                                    }
                                }
                                if (AbrasionResistid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResistid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + AbrasionResistid[i].ToString() + "'");
                                    }
                                }
                                if (BalancePaper[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaper[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + BalancePaper[i].ToString() + "'");
                                    }
                                }

                                if (BalancePaperid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaperid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + BalancePaperid[i].ToString() + "'");
                                    }
                                }
                                if (BaseMaterial[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterial[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + BaseMaterial[i].ToString() + "'");
                                    }
                                }
                                if (BaseMaterialid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterialid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      ,'" + BaseMaterialid[i].ToString() + "'");
                                    }
                                }
                                if (SurfaceTreatment[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(SurfaceTreatment[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + SurfaceTreatment[i].ToString() + "'");
                                    }
                                }
                                if (BackBottomPlate[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BackBottomPlate[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + BackBottomPlate[i].ToString() + "'");
                                    }
                                }
                                if (BuckleType[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BuckleType[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + BuckleType[i].ToString() + "'");
                                    }
                                }

                                if (PieceCount[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(PieceCount[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + PieceCount[i].ToString() + "'");
                                    }
                                }

                                if (TotalNumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalNumber[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + TotalNumber[i].ToString() + "'");
                                    }
                                }
                                if (pakeid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(pakeid[i].ToString().Trim()))
                                    {
                                        cmdsql.AppendLine("      , '" + pakeid[i].ToString() + "'");
                                    }
                                }
                            }
                            #endregion
                            cmdsql.AppendLine("           ,getdate()  ");            
                            cmdsql.AppendLine("           ,'"+loginUserID+"'");
                            //扩展项值
                            cmdsql.Append(GetBillTableCellsDBHelper.insertCustomvalue(strCustomdetail, (i + 1)));
                            cmdsql.AppendLine(")");
                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                                }
                            }
                            //-----------20121212 洛阳电镀问题 添加-------------------//
                            if (Color[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@Color", Color[i].ToString()));
                                }
                            }

                            if (GoodProbability[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@GoodProbability", GoodProbability[i].ToString()));
                                }
                            }
                            if (MateNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@MateNo", MateNo[i].ToString()));
                                }
                            }
                            if (ProRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProRemark", ProRemark[i].ToString()));
                                }
                            }
                            //-------------------------------------------------------//

                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                                }
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromType", dtFromType[i].ToString()));
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                                }
                            }

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

        #region 修改生产任务单和生产任务单明细信息
        /// <summary>
        /// 修改生产任务单和生产任务单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureTaskInfo(ManufactureTaskModel model, Hashtable htExtAttr, string loginUserID, string UpdateID, string[] strCustomdetail)
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
            sqlDet.AppendLine("UPDATE officedba.ManufactureTask");      
            sqlDet.AppendLine("   SET CompanyCD = @CompanyCD");          
            sqlDet.AppendLine("      ,TaskNo = @TaskNo");                
            sqlDet.AppendLine("      ,FromType = @FromType");            
            sqlDet.AppendLine("      ,Subject = @Subject");              
            sqlDet.AppendLine("      ,Principal = @Principal");          
            sqlDet.AppendLine("      ,DeptID = @DeptID");                
            sqlDet.AppendLine("      ,ManufactureType = @ManufactureType");
            sqlDet.AppendLine("      ,CountTotal = @CountTotal");               
            sqlDet.AppendLine("      ,Remark = @Remark");
            sqlDet.AppendLine("      ,DocumentURL = @DocumentURL");
            sqlDet.AppendLine("      ,ProjectID = @ProjectID");
            sqlDet.AppendLine("      ,ModifiedDate = getdate()");   
            //-------------------------20121212 洛阳电镀问题 修改 ---------------------------------//
            //sqlDet.AppendLine("      ,Color=@Color,MateNo=@MateNo,GoodProbability=@GoodProbability,ProRemark=@ProRemark");
            //------------------------------------end------------------------------------------------//
            sqlDet.AppendLine("      ,ModifiedUserID = '"+loginUserID+"'");
            sqlDet.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlDet.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@DocumentURL", model.DocumentURL));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));
            //-----------------20121212 洛阳电镀问题修改--------------------------------------------//
           // comm.Parameters.Add(SqlHelper.GetParameter("@Color", model.Color));
          //  comm.Parameters.Add(SqlHelper.GetParameter("@MateNo", model.MateNo));
          //  comm.Parameters.Add(SqlHelper.GetParameter("@ProRemark", model.Remark));
          //  comm.Parameters.Add(SqlHelper.GetParameter("@GoodProbability", model.GoodProbability));
            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 生产任务单元明细信息更新语句
            //1.先删除不在生产任务单明细中的
            //2.更新明细中的ID
            //3.添加其它明细
            //shjp 2011-9-16 修改为直接删除明细后再保存
            
            #region 先删除不在生产任务单明细中的
            //if (!string.IsNullOrEmpty(UpdateID))
            //{
            //    StringBuilder sqlDel = new StringBuilder();
            //    sqlDel.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=@TaskNo and  ID not in(" + UpdateID + ")");

            //    SqlCommand commDel = new SqlCommand();
            //    commDel.CommandText = sqlDel.ToString();
            //    commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            //    commDel.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));

            //    listADD.Add(commDel);
            //}
            //else
            //{
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=@TaskNo");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));

                listADD.Add(commDel);
            //}
            #endregion


            #region 添加或更新操作
            string[] updateID = UpdateID.Split(',');
           
            if (!string.IsNullOrEmpty(UpdateID) && updateID.Length > 0)
            {
                if (!String.IsNullOrEmpty(model.DetSortNo) && !String.IsNullOrEmpty(model.DetProductID) && !String.IsNullOrEmpty(model.DetProductCount) && !String.IsNullOrEmpty(model.DetStartDate) && !String.IsNullOrEmpty(model.DetEndDate))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    string[] dtSortNo = model.DetSortNo.Split(',');
                    string[] dtProductID = model.DetProductID.Split(',');
                    string[] dtProductCount = model.DetProductCount.Split(',');
                    string[] dtBomID = model.DetBomID.Split(',');
                    string[] dtRouteID = model.DetRouteID.Split(',');
                    string[] dtStartDate = model.DetStartDate.Split(',');
                    string[] dtEndDate = model.DetEndDate.Split(',');
                    string[] dtFromType = model.DetFromType.Split(',');
                    string[] dtFromBillID = model.DetFromBillID.Split(',');
                    string[] dtFromBillNo = model.DetFromBillNo.Split(',');
                    string[] dtFromLineNo = model.DetFromLineNo.Split(',');
                    string[] dtUsedUnitID = model.DetUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.DetUsedUnitCount.Split(',');
                    string[] dtExRate = model.DetExRate.Split(',');
                    string[] TotalSquare = model.TotalSquare.Split(',');
                    string[] Pnumber = model.Pnumber.Split(',');
                    string[] Pnumberid = model.Pnumberid.Split(',');
                    string[] AbrasionResistid = model.AbrasionResistid.Split(',');
                    string[] AbrasionResist = model.AbrasionResist.Split(',');
                    string[] BalancePaperid = model.BalancePaperid.Split(',');
                    string[] BalancePaper = model.BalancePaper.Split(',');
                    string[] BaseMaterialid = model.BaseMaterialid.Split(',');
                    string[] BaseMaterial = model.BaseMaterial.Split(',');
                    string[] SurfaceTreatment = model.SurfaceTreatment.Split(',');
                    string[] BackBottomPlate = model.BackBottomPlate.Split(',');
                    string[] BuckleType = model.BuckleType.Split(',');
                    string[] PieceCount = model.PieceCount.Split(',');
                    string[] TotalNumber = model.TotalNumber.Split(',');
                    string[] pakeid = model.Pakeageid.Split(',');
                    //------------------- 20121212 洛阳电镀问题 添加------------------------//Color,MateNo,GoodProbability,ProRemark"
                    string[] Color = model.Color.Split(',');
                    string[] MateNo = model.MateNo.Split(',');
                    string[] GoodProbability = model.GoodProbability.Split(',');
                    string[] ProRemark = model.ProRemark.Split(',');
                    //----------------------------------------------------------------------//
                    for (int i = 0; i < dtProductID.Length; i++)
                    {
                        #region
                        //int intUpdateID = int.Parse(updateID[i].ToString());
                        //if (intUpdateID > 0)
                        //{

                        //    #region 更新MRP明细中的ID
                        //    StringBuilder sqlEdit = new StringBuilder();
                        //    sqlEdit.AppendLine("UPDATE officedba.ManufactureTaskDetail		");
                        //    sqlEdit.AppendLine("SET                                         ");
                        //    if (dtSortNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("       SortNo = @SortNo						");
                        //        }
                        //    }
                        //    if (dtProductID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,ProductID = @ProductID				");
                        //        }
                        //    }
                        //    if (dtProductCount[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,ProductCount = @ProductCount			");
                        //        }
                        //    }
                        //    if (userInfo.IsMoreUnit)
                        //    {
                        //        sqlEdit.AppendLine("      ,UsedUnitID = @UsedUnitID			");
                        //        sqlEdit.AppendLine("      ,UsedUnitCount = @UsedUnitCount			");
                        //        sqlEdit.AppendLine("      ,ExRate = @ExRate			");
                        //    }
                        //    if (dtBomID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,BomID = @BomID						");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        sqlEdit.AppendLine("      ,BomID = null						");
                        //    }
                        //    if (dtRouteID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,RouteID = @RouteID					");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        sqlEdit.AppendLine("      ,RouteID = null						");
                        //    }
                        //    if (dtStartDate[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,StartDate = @StartDate				");
                        //        }
                        //    }
                        //    if (dtEndDate[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,EndDate = @EndDate					");
                        //        }
                        //    }

                        //    sqlEdit.AppendLine("      ,FromType = @FromType					");
                        //    if (dtFromBillID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,FromBillID = @FromBillID				");
                        //        }
                        //    }
                        //    if (dtFromBillNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,FromBillNo = @FromBillNo				");
                        //        }
                        //    }
                        //    if (dtFromLineNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                        //        {
                        //            sqlEdit.AppendLine("      ,FromLineNo = @FromLineNo				");
                        //        }
                        //    }
                        //    if (userInfo.Version == "floor")
                        //    {
                        //        if (TotalSquare[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(TotalSquare[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,TotalSquare = '"+TotalSquare[i].ToString()+"'");
                        //            }
                        //        }
                        //        if (Pnumber[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(Pnumber[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,Pnumber = '" + Pnumber[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (Pnumberid[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(Pnumberid[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,Pnumberid = '" + Pnumberid[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (AbrasionResist[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(AbrasionResist[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,AbrasionResist = '" + AbrasionResist[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (AbrasionResistid[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(AbrasionResistid[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,AbrasionResistid = '" + AbrasionResistid[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (BalancePaper[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BalancePaper[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BalancePaper = '" + BalancePaper[i].ToString() + "'");
                        //            }
                        //        }
           
                        //        if (BalancePaperid[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BalancePaperid[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BalancePaperid = '" + BalancePaperid[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (BaseMaterial[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BaseMaterial[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BaseMaterial = '" + BaseMaterial[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (BaseMaterialid[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BaseMaterialid[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BaseMaterialid = '" + BaseMaterialid[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (SurfaceTreatment[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(SurfaceTreatment[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,SurfaceTreatment = '" + SurfaceTreatment[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (BackBottomPlate[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BackBottomPlate[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BackBottomPlate = '" + BackBottomPlate[i].ToString() + "'");
                        //            }
                        //        }
                        //        if (BuckleType[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(BuckleType[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,BuckleType = '" + BuckleType[i].ToString() + "'");
                        //            }
                        //        }

                        //        if (PieceCount[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(PieceCount[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,PieceCount = '" + PieceCount[i].ToString() + "'");
                        //            }
                        //        }

                        //        if (TotalNumber[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(TotalNumber[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,TotalNumber = '" + TotalNumber[i].ToString() + "'");
                        //            }
                        //        }

                        //        if (pakeid[i].ToString().Length > 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(pakeid[i].ToString().Trim()))
                        //            {
                        //                sqlEdit.AppendLine("      ,pakeages = '" + pakeid[i].ToString() + "'");
                        //            }
                        //        }
                        //    }
                        //    sqlEdit.AppendLine("      ,ModifiedDate = getdate()			    ");
                        //    sqlEdit.AppendLine("      ,ModifiedUserID = '"+loginUserID+"'	");
                        //    sqlEdit.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID		");														

                        //    SqlCommand commEdit = new SqlCommand();
                        //    commEdit.CommandText = sqlEdit.ToString();
                        //    if (dtSortNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                        //        }
                        //    }
                        //    if (dtProductID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                        //        }
                        //    }
                        //    if (dtProductCount[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                        //        }
                        //    }
                        //    if (userInfo.IsMoreUnit)
                        //    {
                        //        commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                        //        commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                        //        commEdit.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                        //    }
                        //    if (dtBomID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                        //        }
                        //    }
                        //    if (dtRouteID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                        //        }
                        //    }
                        //    if (dtStartDate[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                        //        }
                        //    }
                        //    if (dtEndDate[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                        //        }
                        //    }
                        //    commEdit.Parameters.Add(SqlHelper.GetParameter("@FromType", dtFromType[i].ToString()));
                        //    if (dtFromBillID[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                        //        }
                        //    }
                        //    if (dtFromBillNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                        //        }
                        //    }
                        //    if (dtFromLineNo[i].ToString().Length > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                        //        {
                        //            commEdit.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                        //        }
                        //    }
                        //    commEdit.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        //    commEdit.Parameters.Add(SqlHelper.GetParameter("@ID", intUpdateID));

                        //    listADD.Add(commEdit);
                        //    #endregion
                        //}
                        //else
                        //{
                        #endregion
                        #region 添加MRP明细中的ID
                        //页面上这些字段都是必填，数组的长度必须是相同的
                            System.Text.StringBuilder sqlIn = new System.Text.StringBuilder();
                            sqlIn.AppendLine("INSERT INTO officedba.ManufactureTaskDetail");
                            sqlIn.AppendLine("           (CompanyCD");
                            sqlIn.AppendLine("           ,TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,SortNo");
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,ProductID");
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,ProductCount");
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("           ,UsedUnitID");
                                sqlIn.AppendLine("           ,UsedUnitCount");
                                sqlIn.AppendLine("           ,ExRate");
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,BomID");
                                }
                            }
                            //---------20121212 洛阳电镀问题 添加 ---------------------//
                            /*
                             * 20121219 修改 添加版本控制 
                             */
                            if (userInfo.Version == "general")
                            {
                                if (Color[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,Color");
                                    }
                                }
                                if (GoodProbability[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,GoodProbability");
                                    }
                                }
                                if (MateNo[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,MateNo");
                                    }
                                }
                                if (ProRemark[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,ProRemark");
                                    }
                                }
                            }
                            //---------------------------------------------------------//
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,RouteID");
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,StartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,EndDate");
                                }
                            }

                            sqlIn.AppendLine("           ,FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromBillID");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromLineNo");
                                }
                            }
                            if (userInfo.Version == "floor")
                            {
                                if (TotalSquare[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalSquare[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,TotalSquare ");
                                    }
                                }
                                if (Pnumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumber[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,Pnumber  ");
                                    }
                                }
                                if (Pnumberid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumberid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,Pnumberid  ");
                                    }
                                }
                                if (AbrasionResist[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResist[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,AbrasionResist  ");
                                    }
                                }
                                if (AbrasionResistid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResistid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,AbrasionResistid  ");
                                    }
                                }
                                if (BalancePaper[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaper[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BalancePaper ");
                                    }
                                }

                                if (BalancePaperid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaperid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BalancePaperid");
                                    }
                                }
                                if (BaseMaterial[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterial[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BaseMaterial");
                                    }
                                }
                                if (BaseMaterialid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterialid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BaseMaterialid ");
                                    }
                                }
                                if (SurfaceTreatment[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(SurfaceTreatment[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,SurfaceTreatment");
                                    }
                                }
                                if (BackBottomPlate[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BackBottomPlate[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BackBottomPlate");
                                    }
                                }
                                if (BuckleType[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BuckleType[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,BuckleType");
                                    }
                                }
                                if (PieceCount[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(PieceCount[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,PieceCount");
                                    }
                                }
                                if (TotalNumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalNumber[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,TotalNumber");
                                    }
                                }
                                if (pakeid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(pakeid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,pakeages");
                                    }
                                }
                            }
                            sqlIn.AppendLine("           ,ModifiedDate");
                            sqlIn.AppendLine("           ,ModifiedUserID");
                            //添加扩展项
                            sqlIn.Append(GetBillTableCellsDBHelper.insertCustomFilder(strCustomdetail, (i + 1)));
                            sqlIn.AppendLine("     )VALUES");
                            sqlIn.AppendLine("           (@CompanyCD");
                            sqlIn.AppendLine("           ,@TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@SortNo");
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@ProductID");
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@ProductCount");
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("           ,@UsedUnitID");
                                sqlIn.AppendLine("           ,@UsedUnitCount");
                                sqlIn.AppendLine("           ,@ExRate");
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@BomID");
                                }
                            }
                            //-----------20121212 洛阳电镀问题 添加-------------------//
                           /*
                            * 20121219 修改 添加版本控制 DYG
                            */
                            if (userInfo.Version == "general")
                            {
                                if (Color[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,@Color");
                                    }
                                }

                                if (GoodProbability[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,@GoodProbability");
                                    }
                                }
                                if (MateNo[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,@MateNo");
                                    }
                                }
                                if (ProRemark[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("   ,@ProRemark");
                                    }
                                }
                            }
                            //-------------------------------------------------------//
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@RouteID");
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@StartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@EndDate");
                                }
                            }

                            sqlIn.AppendLine("           ,@FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromBillID ");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromLineNo");
                                }
                            }
                            if (userInfo.Version == "floor")
                            {
                                if (TotalSquare[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalSquare[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + TotalSquare[i].ToString() + "'");
                                    }
                                }
                                if (Pnumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumber[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + Pnumber[i].ToString() + "'");
                                    }
                                }
                                if (Pnumberid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Pnumberid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + Pnumberid[i].ToString() + "'");
                                    }
                                }
                                if (AbrasionResist[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResist[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + AbrasionResist[i].ToString() + "'");
                                    }
                                }
                                if (AbrasionResistid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(AbrasionResistid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + AbrasionResistid[i].ToString() + "'");
                                    }
                                }
                                if (BalancePaper[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaper[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + BalancePaper[i].ToString() + "'");
                                    }
                                }

                                if (BalancePaperid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BalancePaperid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + BalancePaperid[i].ToString() + "'");
                                    }
                                }
                                if (BaseMaterial[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterial[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + BaseMaterial[i].ToString() + "'");
                                    }
                                }
                                if (BaseMaterialid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BaseMaterialid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      ,'" + BaseMaterialid[i].ToString() + "'");
                                    }
                                }
                                if (SurfaceTreatment[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(SurfaceTreatment[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + SurfaceTreatment[i].ToString() + "'");
                                    }
                                }
                                if (BackBottomPlate[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BackBottomPlate[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + BackBottomPlate[i].ToString() + "'");
                                    }
                                }
                                if (BuckleType[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(BuckleType[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + BuckleType[i].ToString() + "'");
                                    }
                                }

                                if (PieceCount[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(PieceCount[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + PieceCount[i].ToString() + "'");
                                    }
                                }

                                if (TotalNumber[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(TotalNumber[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + TotalNumber[i].ToString() + "'");
                                    }
                                }
                                if (pakeid[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(pakeid[i].ToString().Trim()))
                                    {
                                        sqlIn.AppendLine("      , '" + pakeid[i].ToString() + "'");
                                    }
                                }
                            }
                            sqlIn.AppendLine("           ,getdate()  ");
                            sqlIn.AppendLine("           ,'" + loginUserID + "'");
                            //扩展项值
                            sqlIn.Append(GetBillTableCellsDBHelper.insertCustomvalue(strCustomdetail, (i + 1)));
                            sqlIn.AppendLine(")");
                            SqlCommand commIn = new SqlCommand();
                            commIn.CommandText = sqlIn.ToString();
                            commIn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                                }
                            }
                            //-----------20121212 洛阳电镀问题 添加-------------------//
                        /*
                         * 20121219 添加 版本控制 DYG
                         */
                            if (userInfo.Version == "general")
                            {
                                if (Color[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(Color[i].ToString().Trim()))
                                    {
                                        commIn.Parameters.Add(SqlHelper.GetParameter("@Color", Color[i].ToString()));
                                    }
                                }

                                if (GoodProbability[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(GoodProbability[i].ToString().Trim()))
                                    {
                                        commIn.Parameters.Add(SqlHelper.GetParameter("@GoodProbability", GoodProbability[i].ToString()));
                                    }
                                }
                                if (MateNo[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(MateNo[i].ToString().Trim()))
                                    {
                                        commIn.Parameters.Add(SqlHelper.GetParameter("@MateNo", MateNo[i].ToString()));
                                    }
                                }
                                if (ProRemark[i].ToString().Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(ProRemark[i].ToString().Trim()))
                                    {
                                        commIn.Parameters.Add(SqlHelper.GetParameter("@ProRemark", ProRemark[i].ToString()));
                                    }
                                }
                            }
                            //-------------------------------------------------------//
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                                }
                            }

                            commIn.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                                }
                            }

                            listADD.Add(commIn);
                            #endregion
                        //}
                    }
                }
            }
            #endregion


            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 生产任务单详细信息
        /// <summary>
        /// 生产任务单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTaskInfo(ManufactureTaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from ( select");
            if (userInfo.Version == "floor")
            {
                infoSql.AppendLine(" isnull(dbo.getCustname(sell.custid),'')CustName,");
            }
            infoSql.AppendLine("					a.CompanyCD,k.FromBillNo,a.ID,a.TaskNo,a.FromType,a.Subject,a.Principal,g.EmployeeName as PricipalReal,a.DocumentURL,");
            infoSql.AppendLine("						a.DeptID,f.DeptName,a.TaskType,a.ManufactureType,Convert(numeric(16,"+userInfo.SelPoint+"),a.CountTotal) as CountTotal ,");
            infoSql.AppendLine("                        isnull( dbo.getCodeublic(a.ManufactureType),'') as strManufactureType,");
            infoSql.AppendLine("                        case when a.FromType=0 then '无来源' when a.FromType=1 then '主生产计划' end as strFromType,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.BillStatus,a.Creator,b.EmployeeName as CreatorReal,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("						a.Confirmor,c.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("						a.Closer,d.EmployeeName as CloserReal,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,a.Remark,");
            infoSql.AppendLine("                        a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,h.EmployeeName as ModifiedUserReal,a.ProjectID,p.ProjectName ");
            infoSql.AppendLine("				from officedba.ManufactureTask a left join officedba.ManufactureTaskdetail k on a.companycd=k.companycd and a.taskno=k.taskno");
            if (userInfo.Version == "floor")
            {
                infoSql.AppendLine(" left join  officedba.sellorderdetail selll on selll.id=k.frombillid and selll.OrderNo=k.frombillNo left join  officedba.sellorder sell on sell.orderno=selll.orderno and sell.companycd=selll.companycd  ");
            }
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as b on a.Creator=b.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as c on a.Confirmor=c.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as d on a.Closer=d.ID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo as g on a.Principal=g.ID");
            infoSql.AppendLine("            					left join officedba.DeptInfo as f on a.DeptID=f.ID");
            infoSql.AppendLine("								left join officedba.UserInfo as i on a.ModifiedUserID=i.UserID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo h on i.EmployeeID=h.ID");
            infoSql.AppendLine("                                left join officedba.ProjectInfo p on a.ProjectID=p.ID");
            infoSql.AppendLine(") as info");
            infoSql.AppendLine("Where ID=@ID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 生产任务单明细详细信息
        /// <summary>
        /// 生产任务单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTaskDetailInfo(ManufactureTaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine(@"				select	isnull(A.Custom1,'')Custom1,isnull(A.Custom2,'')Custom2,isnull(A.Custom3,'')Custom3,isnull(A.Custom4,'')Custom4,isnull(A.Custom5,'')Custom5,isnull(A.Custom6,'')Custom6,isnull(A.Custom7,'')Custom7,isnull(A.Custom8,'')Custom8,isnull(A.Custom9,'')Custom9,isnull(A.Custom10,'')Custom10,
                isnull(a.Pakeages,0)pakeageid,isnull(dbo.getCodeublic(a.Pakeages),'')pakeage,b.Size,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(a.PieceCount,0))PieceCount,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(a.TotalNumber,0))TotalNumber,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(a.TotalSquare,0))TotalSquare,isnull(a.Pnumber,'')Pnumber,isnull(a.Pnumberid,0)Pnumberid,isnull(a.AbrasionResist,'')AbrasionResist
                ,isnull(a.AbrasionResistid,0)AbrasionResistid,isnull(a.BalancePaper,'')BalancePaper,isnull(a.BalancePaperid,0)BalancePaperid,isnull(a.BaseMaterial,'')BaseMaterial,isnull(a.BaseMaterialid,0)BaseMaterialid
                ,isnull(a.SurfaceTreatment,'')SurfaceTreatment,isnull(a.BackBottomPlate,'')BackBottomPlate,isnull(a.BuckleType,'')BuckleType,a.CompanyCD,a.ID as DetailID,a.TaskNo,a.SortNo,a.ProductID,b.ProdNo,b.ProductName,");
            //---------20121213洛阳电镀问题添加----------------DYG------------------------//
            //---------20121219修改 添加版权控制-------------DYG--------------------------//
            if (userInfo.Version.ToString() == "general")
            detSql.AppendLine("   a.Color,a.MateNo,a.GoodProbability,a.ProRemark,");
            //------------------------------------------------------------------------//
            detSql.AppendLine("						b.Specification,b.UnitID,c.CodeName as UnitName,a.UsedUnitID,f.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount,");
            detSql.AppendLine("						Convert(numeric(14," + userInfo.SelPoint+ "),a.ProductCount) as ProductCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.ProductedCount) as ProductedCount,");
            detSql.AppendLine("						a.BomID,d.BomNo,a.RouteID,e.RouteNo,");
            detSql.AppendLine("						isnull( CONVERT(CHAR(10), a.StartDate, 23),'') as StartDate,");
            detSql.AppendLine("						isnull( CONVERT(CHAR(10), a.EndDate, 23),'') as EndDate,");
            detSql.AppendLine("						a.FromType,a.FromBillID,a.FromBillNo,a.FromLineNo,Convert(numeric(14," + userInfo.SelPoint+ "),a.InCount) as InCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.ApplyCheckCount) as ApplyCheckCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.CheckedCount) as CheckedCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.PassCount) as PassCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.NotPassCount) as NotPassCount");
            detSql.AppendLine("				from officedba.ManufactureTaskDetail a");
            detSql.AppendLine("				left join officedba.ProductInfo b on b.ID=a.ProductID");
            detSql.AppendLine("				left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("				left join officedba.CodeUnitType f on a.UsedUnitID=f.ID");
            detSql.AppendLine("				left outer join officedba.Bom d on a.BomID=d.ID");
            detSql.AppendLine("				left outer join officedba.TechnicsRouting e on a.RouteID=e.ID");
            detSql.AppendLine("				where a.CompanyCD=@CompanyCD and TaskNo = (select top 1 TaskNo from officedba.ManufactureTask where ID=@ID)");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where CompanyCD=@CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 源单总览：主生产计划明细
        /// <summary>
        /// 主生产计划明细
        /// </summary>
        /// <param name="strMasterID"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleFromPlan(string CompanyCD,string strPlanID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("	select  a.CompanyCD,a.ProductID,b.ProductName,b.ProdNo,b.UnitID,c.CodeName as UnitName,b.Specification,a.UsedUnitID,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount,d.ID as BomID,d.BomNo,d.[Type] as BomType,");
            detSql.AppendLine("			Convert(numeric(14," + userInfo.SelPoint+ "),isnull(a.ProduceCount,0)) as ProduceCount,a.PlanNo,a.ID as DetailID,a.SortNo from officedba.MasterProductScheduleDetail a");
            detSql.AppendLine("	left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("	left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("	left join officedba.Bom d on a.ProductID=d.ProductID");
            detSql.AppendLine("	where a.CompanyCD=@CompanyCD and PlanNo in(select PlanNo from officedba.MasterProductSchedule where ID in(" + strPlanID + "))");
            detSql.AppendLine(")as info");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 源单总览：销售订单明细
        /// <summary>
        /// 销售订单明细
        /// </summary>
        /// <param name="strMasterID"></param>
        /// <returns></returns>
        public static DataTable GetSellOreder(string CompanyCD, string strPlanID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            SqlCommand comm = new SqlCommand();
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select '' as ismore,* from (");
            detSql.AppendLine(@"	select  isnull(a.package,0)pakeageid,isnull(dbo.getCodeublic(a.package),'')pakeage,b.Size,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(a.PieceCount,0))PieceCount,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(a.TotalNumber,0))TotalNumber,Convert(numeric(14," + userInfo.SelPoint + @"),isnull(a.TotalSquare,0))TotalSquare,isnull(a.Pnumber,'')Pnumber,isnull(a.Pnumberid,0)Pnumberid,isnull(a.AbrasionResist,'')AbrasionResist
                ,isnull(a.AbrasionResistid,0)AbrasionResistid,isnull(a.BalancePaper,'')BalancePaper,isnull(a.BalancePaperid,0)BalancePaperid,isnull(a.BaseMaterial,'')BaseMaterial,isnull(a.BaseMaterialid,0)BaseMaterialid
                ,isnull(a.SurfaceTreatment,'')SurfaceTreatment,isnull(a.BackBottomPlate,'')BackBottomPlate,isnull(a.BuckleType,'')BuckleType,a.CompanyCD,a.ProductID,b.ProductName,b.ProdNo,b.UnitID,c.CodeName as UnitName,b.Specification,a.UsedUnitID,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount,d.ID as BomID,d.BomNo,d.[Type] as BomType,");
            detSql.AppendLine("			Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.Productcount,0)) as ProduceCount,a.orderno as PlanNo,a.ID as DetailID,a.SortNo from officedba.sellorderdetail a");
            detSql.AppendLine("	left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("	left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("	left join officedba.Bom d on a.ProductID=d.ProductID");
            detSql.AppendLine("	where a.CompanyCD='"+CompanyCD+"' and a.id in(" + strPlanID + ")");
            detSql.AppendLine(")as info");

            #endregion

            //定义查询的命令
           
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            DataTable dt=SqlHelper.ExecuteSearch(comm);

            DataTable dt2 = new DataTable();
            //dt2 = dt.Clone();
            #region 创建列
            dt2.Columns.Add("ismore");
            dt2.Columns.Add("CompanyCD");
            dt2.Columns.Add("ProductID");
            dt2.Columns.Add("ProductName");
            dt2.Columns.Add("ProdNo");
            dt2.Columns.Add("UnitID");
            dt2.Columns.Add("UnitName");
            dt2.Columns.Add("Specification");
            dt2.Columns.Add("UsedUnitID");
            dt2.Columns.Add("UsedUnitCount");
            dt2.Columns.Add("BomID");
            dt2.Columns.Add("BomNo");
            dt2.Columns.Add("BomType");
            dt2.Columns.Add("ProduceCount");
            dt2.Columns.Add("PlanNo");
            dt2.Columns.Add("DetailID");
            dt2.Columns.Add("SortNo");
            dt2.Columns.Add("TotalSquare");
            dt2.Columns.Add("Pnumber");
            dt2.Columns.Add("Pnumberid");
            dt2.Columns.Add("AbrasionResist");
            dt2.Columns.Add("AbrasionResistid");
            dt2.Columns.Add("BalancePaper");
            dt2.Columns.Add("BalancePaperid");
            dt2.Columns.Add("BaseMaterial");
            dt2.Columns.Add("BaseMaterialid");
            dt2.Columns.Add("SurfaceTreatment");
            dt2.Columns.Add("BackBottomPlate");
            dt2.Columns.Add("BuckleType");
            dt2.Columns.Add("Size");
            dt2.Columns.Add("PieceCount");
            dt2.Columns.Add("TotalNumber");
            dt2.Columns.Add("pakeageid");
            dt2.Columns.Add("pakeage");
            #endregion
            List<SellOrderDetailModel> list = new List<SellOrderDetailModel>();
            #region 获取自制物料
            foreach (DataRow item1 in dt.Rows)
            {
                
                if (item1["BomID"].ToString() != "" && item1["BomID"].ToString() != "NULL")
                {
                    SellOrderDetailModel model = new SellOrderDetailModel();
                    List<SellOrderDetailModel> li = new List<SellOrderDetailModel>();
                    #region 获取销售订单数据
                    model.ProductID = int.Parse(item1["ProductID"].ToString());
                    model.ProductCount = decimal.Parse(item1["ProduceCount"].ToString());
                    model.UsedUnitCount = decimal.Parse(item1["UsedUnitCount"].ToString());
                    model.UsedUnitID = int.Parse(item1["UsedUnitID"].ToString());
                    model.OrderNo = item1["PlanNo"].ToString();
                    model.ID = int.Parse(item1["DetailID"].ToString());
                    model.SortNo = int.Parse(item1["SortNo"].ToString());
                    model.Remark = item1["BomID"].ToString();
                    #endregion
                    list.Add(model);
                    li.Add(model);
                    int num = 0;
                    while (1 == 1)
                    {
                        list.Clear();
                        foreach (SellOrderDetailModel item2 in li)
                        {
                            list.Add(item2);
                        }
                        int listcount = list.Count;
                      
                        li.Clear();
                        for (int i = 0; i < list.Count; i++)
                        {
                            #region 查询父件下类型为自制的子键
                            string sql = @"select 'Y' as ismore,Convert(numeric(18," + userInfo.SelPoint + @"),isnull(b.PieceCount,0))PieceCount,Convert(numeric(18," + userInfo.SelPoint + @"),0)TotalNumber, b.Size,Convert(numeric(14," + userInfo.SelPoint + @"),0)TotalSquare,isnull(b.Pnumber,'')Pnumber,isnull(b.Pnumberid,0)Pnumberid,isnull(b.AbrasionResist,'')AbrasionResist
                ,isnull(b.AbrasionResistid,0)AbrasionResistid,isnull(b.BalancePaper,'')BalancePaper,isnull(b.BalancePaperid,0)BalancePaperid,isnull(b.BaseMaterial,'')BaseMaterial,isnull(b.BaseMaterialid,0)BaseMaterialid
                ,isnull(b.SurfaceTreatment,'')SurfaceTreatment,isnull(b.BackBottomPlate,'')BackBottomPlate,isnull(b.BuckleType,'')BuckleType,a.CompanyCD,a.ProductID,b.ProductName,b.ProdNo,b.UnitID,c.CodeName as UnitName,b.Specification,a.unitid UsedUnitID,Convert(numeric(14,2)," + list[i].UsedUnitCount + @"*isnull(a.Quota,0)) as UsedUnitCount,d.ID as BomID,d.BomNo,d.[Type] as BomType,
			                                Convert(numeric(14," + userInfo.SelPoint + @"),isnull(" + list[i].ProductCount + @"*a.Quota+" + list[i].ProductCount + @"*a.Quota*isnull(a.rateloss,0)/100,0)) as ProduceCount,'" + list[i].OrderNo + @"' as PlanNo,a.ID as DetailID,'" + list[i].SortNo + @"' SortNo from 
            (select  * from  officedba.BomDetail where bomno =( select bomno from officedba.bom where id =" + list[i].Remark + @") and SourceType=0 and UsedStatus=1 ) a 
                                left join officedba.ProductInfo b on a.ProductID=b.ID	left join officedba.CodeUnitType c on b.UnitID=c.ID
	                                left join officedba.Bom d  on a.bomno=d.bomno and d.companycd=a.companycd
                                where a.CompanyCD='" + CompanyCD + "'";
                            #endregion
                            comm.CommandText = sql;
                            DataTable dt1 = SqlHelper.ExecuteSearch(comm);
                            if (dt1.Rows.Count > 0)
                            {

                                foreach (DataRow item in dt1.Rows)
                                {
                                    
                                    #region 新建行并填充数据
                                    dt2.Rows.Add(dt2.NewRow());
                                    dt2.Rows[dt2.Rows.Count - 1]["ismore"] = item["ismore"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["CompanyCD"] = item["CompanyCD"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["ProductID"] = item["ProductID"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["ProductName"] = item["ProductName"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["ProdNo"] = item["ProdNo"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["UnitID"] = item["UnitID"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["UnitName"] = item["UnitName"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["Specification"] = item["Specification"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["UsedUnitID"] = item["UsedUnitID"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["UsedUnitCount"] = item["UsedUnitCount"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BomID"] = item["BomID"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BomNo"] = item["BomNo"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BomType"] = item["BomType"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["ProduceCount"] = item["ProduceCount"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["PlanNo"] = item["PlanNo"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["DetailID"] = item["DetailID"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["SortNo"] = item["SortNo"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["TotalSquare"] = item["TotalSquare"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["Pnumberid"] = item["Pnumberid"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["Pnumber"] = item["Pnumber"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["AbrasionResistid"] = item["AbrasionResistid"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["AbrasionResist"] = item["AbrasionResist"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BalancePaperid"] = item["BalancePaperid"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BalancePaper"] = item["BalancePaper"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BaseMaterial"] = item["BaseMaterial"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BaseMaterialid"] = item["BaseMaterialid"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["SurfaceTreatment"] = item["SurfaceTreatment"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BackBottomPlate"] = item["BackBottomPlate"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["BuckleType"] = item["BuckleType"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["Size"] = item["Size"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["PieceCount"] = item["PieceCount"].ToString();
                                    dt2.Rows[dt2.Rows.Count - 1]["TotalNumber"] = item["TotalNumber"].ToString();
                                    #endregion
                                }

                            }
                                //判断是否有下级BOM
                                string sq = "select  * from  officedba.Bom where ParentNo = " + list[i].Remark + "";
                                comm.CommandText = sq;
                                DataTable tab = SqlHelper.ExecuteSearch(comm);
                                if (tab.Rows.Count <= 0)
                                {
                                    num++;
                                    continue;

                                }
                                else
                                {
                                    for (int j = 0; j < tab.Rows.Count; j++)
                                    {
                                        model.Remark = tab.Rows[j]["ID"].ToString();
                                        li.Add(model);
                                    }
                                }
                           
                        }
                        if (num == listcount)
                        {
                            break;
                        }

                    }
                   
                }
            }
            #endregion
            #region 新建行并填充数据
            foreach (DataRow item in dt.Rows)
            {
                
                dt2.Rows.Add(dt2.NewRow());
                dt2.Rows[dt2.Rows.Count - 1]["ismore"] = item["ismore"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["CompanyCD"] = item["CompanyCD"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["ProductID"] = item["ProductID"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["ProductName"] = item["ProductName"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["ProdNo"] = item["ProdNo"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["UnitID"] = item["UnitID"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["UnitName"] = item["UnitName"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["Specification"] = item["Specification"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["UsedUnitID"] = item["UsedUnitID"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["UsedUnitCount"] = item["UsedUnitCount"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BomID"] = item["BomID"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BomNo"] = item["BomNo"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BomType"] = item["BomType"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["ProduceCount"] = item["ProduceCount"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["PlanNo"] = item["PlanNo"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["DetailID"] = item["DetailID"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["SortNo"] = item["SortNo"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["TotalSquare"] = item["TotalSquare"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["Pnumberid"] = item["Pnumberid"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["Pnumber"] = item["Pnumber"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["AbrasionResistid"] = item["AbrasionResistid"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["AbrasionResist"] = item["AbrasionResist"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BalancePaperid"] = item["BalancePaperid"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BalancePaper"] = item["BalancePaper"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BaseMaterial"] = item["BaseMaterial"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BaseMaterialid"] = item["BaseMaterialid"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["SurfaceTreatment"] = item["SurfaceTreatment"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BackBottomPlate"] = item["BackBottomPlate"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["BuckleType"] = item["BuckleType"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["Size"] = item["Size"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["PieceCount"] = item["PieceCount"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["TotalNumber"] = item["TotalNumber"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["pakeageid"] = item["pakeageid"].ToString();
                dt2.Rows[dt2.Rows.Count - 1]["pakeage"] = item["pakeage"].ToString();

            }
            #endregion
            return dt2;
        }
        #endregion

        #region 通过检索条件查询生产任务单信息
        /// <summary>
        /// 通过检索条件查询生产任务单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition(ManufactureTaskModel model, int FlowStatus, int BillTypeFlag, int BillTypeCode, string CreateDate, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount,string time1,string time2,string time11,string time12)
        {

            #region 查询语句
            //查询SQL拼写
             XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select  convert(numeric(16," + userInfo.SelPoint + "),de.productcount)ProductCount,convert(numeric(16," + userInfo.SelPoint + "),isnull(de.incount,0))InCount ,de.ProductID,dbo.getPnameByID(de.ProductID)productname,a.CompanyCD,");
            searchSql.AppendLine("		a.ID,de.id deid,");
            searchSql.AppendLine("		a.TaskNo,");
            searchSql.AppendLine("		a.Subject,");
            searchSql.AppendLine("		a.Principal,isnull(b.EmployeeName,'') as PrincipalReal,");
            searchSql.AppendLine("		a.DeptID,d.DeptName,");
            searchSql.AppendLine("		a.ManufactureType,p.ProjectName,isnull(a.ProjectID,0) as ProjectID,");
            searchSql.AppendLine("      isnull( dbo.getCodeublic(a.ManufactureType),'') as strManufactureType,");
            searchSql.AppendLine("      case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '变更' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatus,");
            searchSql.AppendLine("      case when e.FlowStatus=1 then '待审批' when e.FlowStatus=2 then '审批中' when e.FlowStatus=3 then '审批通过' when e.FlowStatus=4 then '审批不通过' when e.FlowStatus=5 then '撤消审批' end as strFlowStatus,");
            searchSql.AppendLine("	    isnull(e.FlowStatus,'0')as FlowStatus,");
            searchSql.AppendLine("      a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("		a.Creator,isnull(c.EmployeeName,'') as CreatorReal,");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            searchSql.AppendLine("		a.BillStatus,a.ModifiedDate ");
            searchSql.AppendLine("from officedba.ManufactureTaskdetail de left join officedba.ManufactureTask a on a.taskno=de.taskno and a.companycd=de.companycd ");
            searchSql.AppendLine("LEFT JOIN officedba.EmployeeInfo b on a.Principal=b.ID "); 
            searchSql.AppendLine("LEFT JOIN officedba.EmployeeInfo c on a.Creator=c.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.DeptInfo d on a.DeptID=d.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.ProjectInfo p on a.ProjectID=p.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.FlowInstance e ON a.ID=e.BillID ");
            searchSql.AppendLine(" and e.BillTypeFlag=@BillTypeFlag");
            searchSql.AppendLine(" and e.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine(" and e.ID=( ");
            searchSql.AppendLine("                      select  max(ID)");
            searchSql.AppendLine("                      from  officedba.FlowInstance H");
            searchSql.AppendLine("                      where   H.CompanyCD = A.CompanyCD");
            searchSql.AppendLine("                      and H.BillID = A.ID");
            searchSql.AppendLine("                      and H.BillTypeFlag =@BillTypeFlag");
            searchSql.AppendLine("                      and H.BillTypeCode =@BillTypeCode)");

            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine(" and (  ");
           
            DataTable dtt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                
                if (dtt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    searchSql.AppendLine(" (a.Creator IN  ");
                    searchSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    searchSql.AppendLine("  WHERE DeptID IN (SELECT x.ID  ");
                    searchSql.AppendLine(" FROM officedba.DeptInfo x,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") y  ");
                    searchSql.AppendLine("  WHERE x.ID=y.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) OR ");
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    searchSql.AppendLine(" (a.Creator IN  ");
                    searchSql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    searchSql.AppendLine("  WHERE DeptID IN (SELECT x.ID  ");
                    searchSql.AppendLine(" FROM officedba.DeptInfo x,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") y  ");
                    searchSql.AppendLine("  WHERE x.ID=y.ID) ))  OR ");
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    searchSql.AppendLine(" (a.Creator IN  ");
                    searchSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    searchSql.AppendLine("  WHERE DeptID IN (SELECT x.ID  ");
                    searchSql.AppendLine(" FROM officedba.DeptInfo x,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") y  ");
                    searchSql.AppendLine("  WHERE x.ID=y.ID)))  OR ");
                }
            }
            searchSql.AppendLine(" (select COUNT(*) from officedba.FlowTaskHistory where FlowNo=(SELECT TOP 1 FlowNo FROM officedba.FlowInstance WHERE BillID = a.ID AND BillTypeFlag = @BillTypeFlag AND BillTypeCode = @BillTypeCode) AND BillID = a.ID   AND operateUserId = '" + userInfo.UserID + "')>0 ");

            searchSql.AppendLine("or (a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (a.Principal IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

           
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));

            //单据编号
            if (!string.IsNullOrEmpty(model.TaskNo))
            {
                searchSql.AppendLine("and a.TaskNo like @TaskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + model.TaskNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and a.Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //加工类型
            if (model.ManufactureType > -1)
            {
                searchSql.AppendLine(" and a.ManufactureType=@ManufactureType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ManufactureType", model.ManufactureType.ToString()));
            }
            //负责人
            if (model.Principal > 0)
            {
                searchSql.AppendLine(" and a.Principal=@Principal ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Principal", model.Principal.ToString()));
            }
            //部门
            if (model.DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID.ToString()));
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and a.BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            //审批状态
            if (FlowStatus > -1)
            {
                searchSql.AppendLine(" and FlowStatus=@FlowStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus.ToString()));
            }

            //创建日期
            //if (!string.IsNullOrEmpty(CreateDate))
            //{
            //    searchSql.AppendLine(" and CreateDate=@CreateDate ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
            //}
            if (model.ProjectID > 0)
            {
                searchSql.AppendLine(" and a.ProjectID=@ProjectID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID.ToString()));
            }
            if (model.Productname != null && model.Productname != "")
            {
                searchSql.AppendLine(" and dbo.getPnameByID(de.ProductID) like '%" + model.Productname + "%' ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", model.ProjectID.ToString()));
            }
            if (time1 != "" && time2 == "")
            {
                searchSql.AppendLine(" and de.StartDate<='" + time1 + "' and de.EndDate >='" + time1 + "'  ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", model.ProjectID.ToString()));
            }
            if (time2 != "" && time1 == "")
            {
                searchSql.AppendLine(" and de.StartDate<='" + time2 + "' and de.EndDate >='" + time2 + "'  ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", model.ProjectID.ToString()));
            }
            if (time2 != "" && time1 != "")
            {
                searchSql.AppendLine(" and ((de.StartDate<='" + time1 + "' and de.EndDate >='" + time1 + "') or (de.StartDate<='" + time2 + "' and de.EndDate >='" + time2 + "') or (de.StartDate>'" + time1 + "') or (de.EndDate<'"+time2+"'))  ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", model.ProjectID.ToString()));
            }
            if (time11 != "" && time12 != "")
            {
                searchSql.AppendLine(" and a.CreateDate between '" + time11 + "' and '" + time12 + "' ");
            }
            else if (time11 == "" && time12 != "")
            {
                searchSql.AppendLine(" and a.CreateDate<='" + time12 + "' ");
            }
            else if (time11 != "" && time12 == "")
            {
                searchSql.AppendLine(" and a.CreateDate>='" + time11 + "' ");
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除主生产任务
        /// <summary>
        /// 删除主生产任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteManufactureTask(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=(select top 1 TaskNo from officedba.ManufactureTask where CompanyCD=@CompanYCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.ManufactureTask where CompanyCD=@CompanyCD and ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 修改：确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteManufactureTask(ManufactureTaskModel model, string loginUserID, int OperateType)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            //1：确认  2:结单  3:取消结单
            if (OperateType == 1)
            {
                DataTable dbTask = GetTaskInfo(model);
                if (dbTask.Rows.Count > 0)
                {
                    int BillStatus = int.Parse(dbTask.Rows[0]["BillStatus"].ToString());
                    if (BillStatus == 1)
                    {
                        #region 更新相关地方的数据
                        //注：单据确认后自动更新对应主生产计划明细中的已下达数量
                        //    更新对应的物品在分仓存量表（分仓存量表officedba.StorageProduct）中在途量（增加），
                        //    更新存量时，先根据物品从物品档案表中获取该物品对应的主放仓库ID，再更新分仓存量表中对应的物品+仓库的在途量

                        DataTable dtTaskDetail = new DataTable();
                        dtTaskDetail = GetTaskDetailInfo(model);
                        if (dtTaskDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTaskDetail.Rows.Count; i++)
                            {
                                //Decimal ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["ProductCount"].ToString()));
                                Decimal ProductCount =Decimal.Parse(dtTaskDetail.Rows[i]["ProductCount"].ToString());
                                if (userInfo.IsMoreUnit)
                                {
                                    //ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()));
                                    if (dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()!="")
                                    ProductCount = Decimal.Parse(dtTaskDetail.Rows[i]["UsedUnitCount"].ToString());
                                }
                                int ProductID = int.Parse(dtTaskDetail.Rows[i]["ProductID"].ToString());

                                if (!string.IsNullOrEmpty(dtTaskDetail.Rows[i]["FromBillID"].ToString()))
                                {
                                    int FromBillID = int.Parse(dtTaskDetail.Rows[i]["FromBillID"].ToString());
                                    string FromBillNo = dtTaskDetail.Rows[i]["FromBillNo"].ToString();


                                    if (FromBillID > 0)
                                    {
                                        #region  更新主生产计划中已下达数量语句
                                        //update officedba.MasterProductSchedule set PlanCount=PlanCount+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID
                                        StringBuilder sqlPlan = new StringBuilder();
                                        sqlPlan.AppendLine("Update officedba.MasterProductScheduleDetail set PlanCount=isnull(PlanCount,0)+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID");

                                        SqlCommand commPlan = new SqlCommand();
                                        commPlan.CommandText = sqlPlan.ToString();
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));

                                        listADD.Add(commPlan);
                                        #endregion


                                    }
                                }
                                #region 更新分仓存量表里的在途量
                                //Update officedba.StorageProduct Set RoadCount=RoadCount+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)
                                StringBuilder sqlStoPro = new StringBuilder();
                                sqlStoPro.AppendLine("Update officedba.StorageProduct Set RoadCount=isnull(RoadCount,0)+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID  and batchno is null and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)");

                                SqlCommand commStoPro = new SqlCommand();
                                commStoPro.CommandText = sqlStoPro.ToString();
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));

                                listADD.Add(commStoPro);
                                #endregion
                            }
                        }
                        #endregion

                        #region 更新主表
                        StringBuilder sqlTask = new StringBuilder();
                        sqlTask.AppendLine(" UPDATE officedba.ManufactureTask SET");
                        sqlTask.AppendLine(" Confirmor         = @Confirmor,");
                        sqlTask.AppendLine(" ConfirmDate        = @ConfirmDate,");
                        sqlTask.AppendLine(" ModifiedDate   = getdate(),");
                        sqlTask.AppendLine(" BillStatus   = 2,");
                        sqlTask.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                        sqlTask.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                        SqlCommand commTask = new SqlCommand();
                        commTask.CommandText = sqlTask.ToString();
                        commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                        listADD.Add(commTask);
                        #endregion
                    }
                }

            }
            else if (OperateType == 2)
            {
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine(" UPDATE officedba.ManufactureTask SET");
                sqlTask.AppendLine(" Closer         = @Closer,");
                sqlTask.AppendLine(" CloseDate   = @CloseDate,");
                sqlTask.AppendLine(" BillStatus   = 4,");
                sqlTask.AppendLine(" ModifiedDate   = getdate(),");
                sqlTask.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sqlTask.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTask = new SqlCommand();
                commTask.CommandText = sqlTask.ToString();
                commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTask.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTask);
            }
            else
            {
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine(" update officedba.ManufactureTask set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sqlTask.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTask = new SqlCommand();
                commTask.CommandText = sqlTask.ToString();
                commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTask);
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 单据是否被引用
        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(select top 1 TaskNo from officedba.ManufactureTask where ID in("+ID+"))";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 单据是否被领料单引用
        /// <summary>
        /// 单据是否被领料单引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrenceTakeMaterial(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in("+ID+")";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 根据MRP编号和明细ID查找明细的源单编号和源单ID
        /// <summary>
        /// 根据MRP编号和明细ID查找明细的源单编号和源单ID 
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="MRPNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetMRPDetail_ByMRPNoID(string CompanyCD, string MRPNo,int ID)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select Top 1 FromBillNo,FromBillID from officedba.MRPDetail where CompanyCD=@CompanyCD and MRPNo=@MRPNo and ID=@ID");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MRPNo", MRPNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 取消确认
        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool CancelConfirmOperate(ManufactureTaskModel model, int BillTypeFlag, int BillTypeCode, string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                DataTable dbTask = GetTaskInfo(model);
                if (dbTask.Rows.Count > 0)
                {
                    int BillStatus = int.Parse(dbTask.Rows[0]["BillStatus"].ToString());
                    if (BillStatus == 2)
                    {
                        #region 撤消审批流程
                        #region 撤消审批处理逻辑描述
                        //可参见撤消审批的存储过程[FlowApproval_Update],个别的判断去掉

                        //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
                        //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
                        //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
                        //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())

                        //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
                        //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
                        //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID

                        //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
                        //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
                        //Where CompanyCD=@CompanyCD 
                        //and FlowNo=@tempFlowNo 
                        //and BillTypeFlag=@BillTypeFlag 
                        //and BillTypeCode=@BillTypeCode 
                        //and BillID=@BillID
                        #endregion


                        DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
                        if (dtFlowInstance.Rows.Count > 0)
                        {
                            //提交审批了的单据
                            string FlowInstanceID = dtFlowInstance.Rows[0]["FlowInstanceID"].ToString();
                            string FlowStatus = dtFlowInstance.Rows[0]["FlowStatus"].ToString();
                            string FlowNo = dtFlowInstance.Rows[0]["FlowNo"].ToString();

                            #region 往流程任务历史记录表
                            StringBuilder sqlHis = new StringBuilder();
                            sqlHis.AppendLine("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)");
                            sqlHis.AppendLine("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");


                            SqlCommand commHis = new SqlCommand();
                            commHis.CommandText = sqlHis.ToString();
                            commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commHis);
                            #endregion

                            #region 更新流程任务处理表
                            StringBuilder sqlTask = new StringBuilder();
                            sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                            sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                            SqlCommand commTask = new SqlCommand();
                            commTask.CommandText = sqlTask.ToString();
                            commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commTask);
                            #endregion

                            #region 更新流程实例表
                            StringBuilder sqlIns = new StringBuilder();
                            sqlIns.AppendLine("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                            sqlIns.AppendLine("Where CompanyCD=@CompanyCD ");
                            sqlIns.AppendLine("and FlowNo=@tempFlowNo ");
                            sqlIns.AppendLine("and BillTypeFlag=@BillTypeFlag ");
                            sqlIns.AppendLine("and BillTypeCode=@BillTypeCode ");
                            sqlIns.AppendLine("and BillID=@BillID");


                            SqlCommand commIns = new SqlCommand();
                            commIns.CommandText = sqlIns.ToString();
                            commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commIns);
                            #endregion

                        }
                        #endregion

                        #region  处理自己的业务逻辑
                        #region 更新相关地方的数据
                        //注：单据确认后自动更新对应主生产计划明细中的已下达数量
                        //    更新对应的物品在分仓存量表（分仓存量表officedba.StorageProduct）中在途量（增加），
                        //    更新存量时，先根据物品从物品档案表中获取该物品对应的主放仓库ID，再更新分仓存量表中对应的物品+仓库的在途量


                        DataTable dtTaskDetail = new DataTable();
                        dtTaskDetail = GetTaskDetailInfo(model);
                        if (dtTaskDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTaskDetail.Rows.Count; i++)
                            {
                                Decimal ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["ProductCount"].ToString()));
                                if (userInfo.IsMoreUnit)
                                {
                                    if (dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()!="")
                                    ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()));
                                }
                                int ProductID = int.Parse(dtTaskDetail.Rows[i]["ProductID"].ToString());

                                if (!string.IsNullOrEmpty(dtTaskDetail.Rows[i]["FromBillID"].ToString()))
                                {
                                    int FromBillID = int.Parse(dtTaskDetail.Rows[i]["FromBillID"].ToString());
                                    string FromBillNo = dtTaskDetail.Rows[i]["FromBillNo"].ToString();


                                    if (FromBillID > 0)
                                    {
                                        #region  更新主生产计划中已下达数量语句
                                        //update officedba.MasterProductSchedule set PlanCount=PlanCount+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID
                                        StringBuilder sqlPlan = new StringBuilder();
                                        sqlPlan.AppendLine("Update officedba.MasterProductScheduleDetail set PlanCount=isnull(PlanCount,0)-@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID");

                                        SqlCommand commPlan = new SqlCommand();
                                        commPlan.CommandText = sqlPlan.ToString();
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));

                                        listADD.Add(commPlan);
                                        #endregion
                                    }
                                }
                                #region 更新分仓存量表里的在途量
                                //Update officedba.StorageProduct Set RoadCount=RoadCount+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)
                                StringBuilder sqlStoPro = new StringBuilder();
                                sqlStoPro.AppendLine("Update officedba.StorageProduct Set RoadCount=isnull(RoadCount,0)-@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)");

                                SqlCommand commStoPro = new SqlCommand();
                                commStoPro.CommandText = sqlStoPro.ToString();
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));

                                listADD.Add(commStoPro);
                                #endregion
                            }
                        }
                        #endregion

                        StringBuilder sqlConfirm = new StringBuilder();
                        sqlConfirm.AppendLine(" UPDATE officedba.ManufactureTask SET");
                        sqlConfirm.AppendLine(" Confirmor         = null,");
                        sqlConfirm.AppendLine(" ConfirmDate        = null,");
                        sqlConfirm.AppendLine(" ModifiedDate   = getdate(),");
                        sqlConfirm.AppendLine(" BillStatus   = 1,");
                        sqlConfirm.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                        sqlConfirm.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                        SqlCommand commConfirm = new SqlCommand();
                        commConfirm.CommandText = sqlConfirm.ToString();
                        commConfirm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commConfirm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                        listADD.Add(commConfirm);
                        #endregion
                    }
                }
                return SqlHelper.ExecuteTransWithArrayList(listADD);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(ManufactureTaskModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.ManufactureTask set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.Add("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND TaskNo = @TaskNo";
                cmd.Parameters.Add("@CompanyCD", model.CompanyCD);
                cmd.Parameters.Add("@TaskNo", model.TaskNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 运营模式：(生产任务单执行汇总表)
        /// <summary>
        /// 通过检索条件查询生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating(string CompanyCD, int DeptID, string TaskNo, string MaterialsName, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select");
            searchSql.AppendLine("		Convert(varchar(10),a.EndDate,120) as EndDate,g.ProductName,a.ID,a.CompanyCD,b.DeptID,c.DeptName,");
            searchSql.AppendLine("		b.Principal,d.EmployeeName,b.TaskNo,");
            searchSql.AppendLine("		b.ConfirmDate,Convert(numeric(16," + point + "),a.ProductCount) as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),a.ProductCount))+'&nbsp;' as ProductCount1,Convert(numeric(10," + point + "),a.ProductedCount) as ProductedCount,Convert(char(20),Convert(numeric(16," + point + "),a.ProductedCount))+'&nbsp;' as ProductedCount1,");
            searchSql.AppendLine("		Convert(numeric(16," + point + "),a.InCount) as InCount,Convert(char(20),Convert(numeric(16," + point + "),a.InCount))+'&nbsp;' as InCount1,Convert(numeric(16," + point + "),a.NotPassCount) as NotPassCount,Convert(char(20),Convert(numeric(16," + point + "),a.NotPassCount))+'&nbsp;' as NotPassCount1,Convert(numeric(14," + point + "),TaskInfo.WorkTimeTotals)WorkTimeTotals,Convert(char(20),Convert(numeric(14," + point + "),TaskInfo.WorkTimeTotals))+'&nbsp;' as WorkTimeTotals1,b.BillStatus");
            searchSql.AppendLine("	from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on b.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on b.Principal=d.ID");
            searchSql.AppendLine("  left join officedba.ProductInfo g on g.id = a.ProductID");
            searchSql.AppendLine("	left join (");
            searchSql.AppendLine("				select sum(WorkTimeTotal) as WorkTimeTotals,g.id from");
            searchSql.AppendLine("				officedba.ManufactureReport  f,officedba.ManufactureTaskDetail g");
            searchSql.AppendLine("				where g.TaskNo=f.TaskNo group by g.id");
            searchSql.AppendLine("	) as TaskInfo on a.ID=TaskInfo.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info where CompanyCD=@CompanyCD and BillStatus=2 ");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--生产部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }

            //--任务单编号
            if (!string.IsNullOrEmpty(TaskNo))
            {
                searchSql.AppendLine(" and TaskNo=@TaskNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));
            }
            //--物料名称
            if (!string.IsNullOrEmpty(MaterialsName))
            {
                searchSql.AppendLine(" and ProductName like @MaterialsName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaterialsName", "%" + MaterialsName + "%"));
            }
            //--确认起始日期
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateStart", ConfirmDateStart));
            }
            //--确认截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateEnd", ConfirmDateEnd));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产任务单执行汇总表)
        /// <summary>
        /// 通过检索条件查询打印生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating_Print(string CompanyCD, int DeptID, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select");
            searchSql.AppendLine("		a.ID,a.CompanyCD,b.DeptID,c.DeptName,");
            searchSql.AppendLine("		b.Principal,d.EmployeeName,b.TaskNo,");
            searchSql.AppendLine("		b.ConfirmDate,a.ProductCount,a.ProductedCount,");
            searchSql.AppendLine("		a.InCount,a.NotPassCount,TaskInfo.WorkTimeTotals,b.BillStatus");
            searchSql.AppendLine("	from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on b.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on b.Principal=d.ID");
            searchSql.AppendLine("	left join (");
            searchSql.AppendLine("				select sum(WorkTimeTotal) as WorkTimeTotals,g.id from");
            searchSql.AppendLine("				officedba.ManufactureReport  f,officedba.ManufactureTaskDetail g");
            searchSql.AppendLine("				where g.TaskNo=f.TaskNo group by g.id");
            searchSql.AppendLine("	) as TaskInfo on a.ID=TaskInfo.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info where CompanyCD=@CompanyCD and BillStatus=2 ");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--生产部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID.ToString()));

            }
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                arr.Add(new SqlParameter("@ConfirmDateStart", ConfirmDateStart));
            }
            //发料截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                arr.Add(new SqlParameter("@ConfirmDateEnd", ConfirmDateEnd));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(在制品存量统计表)
        /// <summary>
        /// 在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct a.CompanyCD,stoInfo.* from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,");
            searchSql.AppendLine("					info.ProductCounts,info.CanUseCounts,info.OrderCounts,info.RoadCounts,info.OutCounts,info.ProductCounts1,info.CanUseCounts1,info.OrderCounts1,info.RoadCounts1,info.OutCounts1 from (");
            searchSql.AppendLine("							select	ProductID,");
            searchSql.AppendLine("									Convert(numeric(12,"+point+"),isnull(sum(ProductCount),0)) as ProductCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))) as CanUseCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)) as OrderCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)) as RoadCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(OutCount),0)) as OutCounts,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+'&nbsp;' as ProductCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))))+'&nbsp;' as CanUseCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))+'&nbsp;' as OrderCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))+'&nbsp;' as RoadCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))+'&nbsp;' as OutCounts1");
            searchSql.AppendLine("							from officedba.StorageProduct");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");





            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--物品
            if (ProductID > 0)
            {
                searchSql.AppendLine(" and stoInfo.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印在制品存量统计表)
        /// <summary>
        /// 打印在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating_Print(string CompanyCD, int ProductID,string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct a.CompanyCD,stoInfo.* from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,");
            searchSql.AppendLine("					info.ProductCounts,info.CanUseCounts,info.OrderCounts,info.RoadCounts,info.OutCounts from (");
            searchSql.AppendLine("							select	ProductID,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(ProductCount),0)) as ProductCounts,");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(OrderCount),0))) as CanUseCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(OrderCount),0)) as OrderCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(RoadCount),0)) as RoadCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(OutCount),0)) as OutCounts");
            searchSql.AppendLine("							from officedba.StorageProduct");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--物品
            if (ProductID > 0)
            {
                searchSql.AppendLine(" and stoInfo.ProductID=@ProductID ");
                arr.Add(new SqlParameter("@ProductID", ProductID));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ProductID ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(在制品价值汇总表)
        /// <summary>
        /// 在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select distinct a.CompanyCD,stoInfo.*");
            searchSql.AppendLine(" from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,info.RoadCounts,info.RoadCounts1,");
            searchSql.AppendLine("				    Convert(numeric(12," + point + "),isnull(a.StandardSell,0)) as StandardSell,Convert(char(20),Convert(numeric(12," + point + "),isnull(a.StandardSell,0)))+'&nbsp;' as StandardSell1,");
            searchSql.AppendLine("					Convert(numeric(12," + point + "),Convert(numeric(12," + point + "),isnull(a.StandardSell,0))*info.RoadCounts) as SellPrince,Convert(char(20),Convert(numeric(12," + point + "),Convert(numeric(12," + point + "),isnull(a.StandardSell,0))*info.RoadCounts))+'&nbsp;' as SellPrince1");
            searchSql.AppendLine("			 from (");
            searchSql.AppendLine("							select	ProductID,Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)) as RoadCounts,Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))+'&nbsp;' as RoadCounts1");
            searchSql.AppendLine("							from officedba.StorageProduct where CompanyCD=@CompanyCD");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo ");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");
            searchSql.AppendLine("and ConfirmDate is not null");






            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            if (ProductID > 0)
            {
                searchSql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印在制品价值汇总表)
        /// <summary>
        /// 在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating_Print(string CompanyCD, int ProductID,string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select distinct a.CompanyCD,stoInfo.*");
            searchSql.AppendLine(" from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,info.RoadCounts,");
            searchSql.AppendLine("				    Convert(numeric(12,2),isnull(a.StandardSell,0)) as StandardSell,");
            searchSql.AppendLine("					Convert(numeric(12,2),Convert(numeric(12,2),isnull(a.StandardSell,0))*info.RoadCounts) as SellPrince");
            searchSql.AppendLine("			 from (");
            searchSql.AppendLine("							select	ProductID,Convert(numeric(12,2),isnull(sum(RoadCount),0)) as RoadCounts");
            searchSql.AppendLine("							from officedba.StorageProduct where CompanyCD=@CompanyCD");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo ");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            if (ProductID > 0)
            {
                searchSql.AppendLine(" and a.ProductID=@ProductID ");
                arr.Add(new SqlParameter("@ProductID", ProductID.ToString()));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ProductID ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(生产日报表)
        /// <summary>
        /// 生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating(string CompanyCD, int DeptID,string theDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.CompanyCD,e.DeptName,a.DeptID,c.ProdNo,c.ProductName,d.CodeName as UnitName,c.Specification,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),isnull(b.FinishNum,0)) as FinishNum,Convert(char(20),Convert(numeric(12," + point + "),isnull(b.FinishNum,0)))+'&nbsp;' FinishNum1,Convert(numeric(20," + point + "),b.WorkTime)WorkTime,Convert(char(50),Convert(numeric(20," + point + "),b.WorkTime))+'&nbsp;' WorkTime1,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime) as ProductionTotal,Convert(char(20),Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime))+'&nbsp;' ProductionTotal1,");
            searchSql.AppendLine("		CONVERT(CHAR(10), a.ConfirmDate, 23)as ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReport a ");
            searchSql.AppendLine("left join officedba. ManufactureReportProduct b on a.ReportNo=b.ReportNo");
            searchSql.AppendLine("left join officedba.ProductInfo c on b.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID");
            searchSql.AppendLine("left join officedba.DeptInfo e on a.DeptID=e.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(theDate))
            {

                searchSql.AppendLine("and CONVERT(CHAR(10), a.ConfirmDate, 23)=@TheDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TheDate", theDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产日报表)
        /// <summary>
        /// 打印生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating_Print(string CompanyCD, int DeptID,string theDate, string orderColumn, string orderType)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.CompanyCD,e.DeptName,a.DeptID,c.ProdNo,c.ProductName,c.Specification,d.CodeName as UnitName,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),isnull(b.FinishNum,0)) as FinishNum,b.WorkTime,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime) as ProductionTotal,");
            searchSql.AppendLine("		CONVERT(CHAR(10), a.ConfirmDate, 23)as ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReport a ");
            searchSql.AppendLine("left join officedba. ManufactureReportProduct b on a.ReportNo=b.ReportNo");
            searchSql.AppendLine("left join officedba.ProductInfo c on b.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID");
            searchSql.AppendLine("left join officedba.DeptInfo e on a.DeptID=e.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(theDate))
            {
                searchSql.AppendLine(" and CONVERT(CHAR(10), a.ConfirmDate, 23)=@TheDate");
                arr.Add(new SqlParameter("@TheDate", theDate));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine(" order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(生产月报表)
        /// <summary>
        /// 生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating(string CompanyCD, int DeptID, string QueryDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.ID,g.DeptID,h.DeptName,a.ProductID,c.ProdNo,c.ProductName,");
            searchSql.AppendLine("		c.Specification,d.CodeName as UnitName,Convert(numeric(12," + point + "),f.ProductCount)  as OrderCount,Convert(char(20),Convert(numeric(14," + point + "),f.ProductCount))+'&nbsp;'  as OrderCount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),b.ProductCount)  as ProductCount,Convert(char(20),Convert(numeric(12," + point + "),b.ProductCount))+'&nbsp;'  as ProductCount1,Convert(numeric(14," + point + "),b.ProductedCount)  as ProductedCount,Convert(char(20),Convert(numeric(14," + point + "),b.ProductedCount))+'&nbsp;'  as ProductedCount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),(b.ProductCount-isnull(b.ProductedCount,0))) as UnFinishcount,Convert(char(20),Convert(numeric(14," + point + "),(b.ProductCount-isnull(b.ProductedCount,0))))+'&nbsp;' as UnFinishcount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),((b.ProductedCount/b.ProductCount)*100)) as FinishPercent,Convert(char(20),Convert(numeric(14," + point + "),((b.ProductedCount/b.ProductCount)*100)))+'&nbsp;' as FinishPercent1,");
            searchSql.AppendLine("		Convert(numeric(20," + point + "),a.WorkTime)WorkTime,Convert(char(50),Convert(numeric(20," + point + "),a.WorkTime))+'&nbsp;' WorkTime1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),(a.FinishNum/a.WorkTime )) as ChanLiang,Convert(char(20),Convert(numeric(14," + point + "),(a.FinishNum/a.WorkTime )))+'&nbsp;' as ChanLiang1,");
            searchSql.AppendLine("		e.FromBillNo,e.FromBillId,");
            searchSql.AppendLine("		g.ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReportProduct a");
            searchSql.AppendLine("left join officedba.ManufactureTaskDetail b on a.FromBillNo=b.TaskNo and a.FromBillID=b.ID");
            searchSql.AppendLine("left join officedba.MasterProductScheduleDetail  e on b.FromBillNo=e.PlanNo and b.FromBillID=e.ID");
            searchSql.AppendLine("left join officedba.SellOrderDetail f on e.FromBillNo=f.OrderNo and e.FromBillID=f.ID");
            searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID ");
            searchSql.AppendLine("left join officedba.ManufactureReport g on a.ReportNo=g.ReportNo");
            searchSql.AppendLine("left join officedba.DeptInfo h on g.DeptID=h.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and g.ConfirmDate is not null");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and g.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(QueryDate))
            {

                searchSql.AppendLine("and ConfirmDate>=@QueryDate and ConfirmDate <dateadd(month,1,@QueryDate)");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QueryDate", QueryDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产月报表)
        /// <summary>
        /// 打印生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="QueryDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating_Print(string CompanyCD, int DeptID, string QueryDate, string orderColumn, string orderType)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.ID,g.DeptID,h.DeptName,a.ProductID,c.ProdNo,c.ProductName,");
            searchSql.AppendLine("		c.Specification,d.CodeName as UnitName,f.ProductCount  as OrderCount,");
            searchSql.AppendLine("		b.ProductCount,b.ProductedCount,");
            searchSql.AppendLine("		(b.ProductCount-Convert(numeric(12," + point + "),isnull(b.ProductedCount,0))) as UnFinishcount,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),b.ProductedCount)/b.ProductCount)*100) as FinishPercent,");
            searchSql.AppendLine("		a.WorkTime,");
            searchSql.AppendLine("		(a.FinishNum/a.WorkTime ) as ChanLiang,");
            searchSql.AppendLine("		e.FromBillNo,e.FromBillId,");
            searchSql.AppendLine("		g.ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReportProduct a");
            searchSql.AppendLine("left join officedba.ManufactureTaskDetail b on a.FromBillNo=b.TaskNo and a.FromBillID=b.ID");
            searchSql.AppendLine("left join officedba.MasterProductScheduleDetail  e on b.FromBillNo=e.PlanNo and b.FromBillID=e.ID");
            searchSql.AppendLine("left join officedba.SellOrderDetail f on e.FromBillNo=f.OrderNo and e.FromBillID=f.ID");
            searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID ");
            searchSql.AppendLine("left join officedba.ManufactureReport g on a.ReportNo=g.ReportNo");
            searchSql.AppendLine("left join officedba.DeptInfo h on g.DeptID=h.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and g.ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(QueryDate))
            {

                searchSql.AppendLine("and ConfirmDate>=@QueryDate and ConfirmDate <dateadd(month,1,@QueryDate)");
                arr.Add(new SqlParameter("@QueryDate", QueryDate));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion


        #region 生产任务报表
        /// <summary>
        /// 生产任务单报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetProductProgressreport(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string hbno, string startdate1, string enddate1,string task)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine(@"select task.productname,isnull(task.Specification,'')Specification,isnull(task.SequName,'')RouteName,isnull(task.sequDes,'')Remark,ISNULL(Convert(numeric(10,2),rpt.ProductedCount),0)ProductedCount,ISNULL(Convert(numeric(10,2),rpt.PassNum),0)PassCount,
ISNULL(Convert(numeric(10,2),rpt.ProductedCount-rpt.PassNum),0)NotPassCount,ISNULL(Convert(numeric(10,2),rpt.worktime),0)worktime ,isnull(task.EmployeesName,'')EmployeesName,isnull(task.taskno,'')taskno,isnull(task.FromBillNo,'')FromBillNo  from (
select max(a.ReportDate)as ReportDate,sum(b.worktime)as worktime,sum(b.finishnum)as ProductedCount,sum(b.PassNum)PassNum,b.Morderno from officedba.ManufactureProgressRpt a left join 
officedba.ManufactureProgressRptDetail b on  a.reportno=b.reportno group by b.Morderno) rpt left join 
(select c.companycd,h.Specification,i.FromBillNo,h.productname,d.id,c.taskno,d.sequDes,f.SequName,e.EmployeesName
 from officedba.Manufacturedispatching c left join officedba.ManufacturedispatchingDetail d
on c.id=d.taskid left join (select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,
isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a left join officedba.deptinfo  as b on a.DeptID=b.ID where a.CompanyCD='DBHY6' and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3') e
on e.id=d.chargeman left join officedba.StandardSequ f on d.SequNo=f.id left join officedba.ManufactureTaskDetail g on g.taskno=c.taskno and g.id=d.ManuTaskDetilID left join officedba.ProductInfo h on h.id=g.ProductID 
 left join(select aa.FromBillNo,bb.taskno from  officedba.MasterProductScheduleDetail aa inner join  officedba.ManufactureTaskDetail bb on aa.planno=bb.FromBillNo where aa.FromBillNo is not null and aa.FromBillNo <>'')i on i.taskno=g.taskno)as task
 on task.id=rpt.Morderno
   where task.companycd='" + userInfo.CompanyCD + @"' ");
           
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (TaskNo != "")
            {
                detSql.AppendLine(" and task.SequName='" + TaskNo + "'");
            }
            if (task != "")
            {
                detSql.AppendLine(" and task.taskno='" + task + "'");
            }
            if (Productname != "")
            {
                detSql.AppendLine(" and task.productname like '%" + Productname + "%'");
            }
            if (hbno != "")
            {
                detSql.AppendLine(" and task.FromBillNo='" + hbno + "'");
            }
            if (startdate1 != "")
            {
                detSql.AppendLine(" and rpt.ReportDate between '" + startdate1 + " 00:00:00' and '" + enddate1 + " 23:59:59' ");
            }
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion


    }
}
