using System;
using XBase.Model.Office.SellManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Office.SellManager
{
    public class CostForeignDBHelper
    {
        #region 成本核算插入
        /// <summary>
        /// 成本核算插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertCostForeign(CostForeignModel model, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;
            string[] dContainer20Info = model.Container20Info.Split(',');
            string[] Container40Info = model.Container40Info.Split(',');
            string[] Container41Info = model.Container41Info.Split(',');
            #region 采购进货单添加SQL语句
            StringBuilder sqlSoIn = new StringBuilder();
            sqlSoIn.AppendLine("INSERT INTO officedba.CostForeign");
            sqlSoIn.AppendLine("           (CompanyCD           ");
            sqlSoIn.AppendLine("            ,CostNo              ");
            sqlSoIn.AppendLine("            ,ProductID           ");
            sqlSoIn.AppendLine("            ,ContainerSp         ");
            sqlSoIn.AppendLine("            ,ShippedPort         ");
            sqlSoIn.AppendLine("            ,ObjectivePort       ");
            sqlSoIn.AppendLine("            ,TotalPrice          ");
            sqlSoIn.AppendLine("            ,FOBPrice            ");
            sqlSoIn.AppendLine("            ,CIFPrice            ");
            sqlSoIn.AppendLine("            ,OTotalPrice         ");
            sqlSoIn.AppendLine("            ,OFOBPrice           ");
            sqlSoIn.AppendLine("            ,OCIFPrice           ");
            sqlSoIn.AppendLine("            ,TotalPriceW          ");
            sqlSoIn.AppendLine("            ,FOBPriceW            ");
            sqlSoIn.AppendLine("            ,CIFPriceW            ");
            sqlSoIn.AppendLine("            ,OTotalPriceW         ");
            sqlSoIn.AppendLine("            ,OFOBPriceW           ");
            sqlSoIn.AppendLine("            ,OCIFPriceW           ");//
            sqlSoIn.AppendLine("            ,CostDate             ");//CostDate
            sqlSoIn.AppendLine("            ,Remarks              ");//Remarks

            sqlSoIn.AppendLine("            ,TotalWeight20        ");
            sqlSoIn.AppendLine("            ,Volume20             ");
            sqlSoIn.AppendLine("            ,Single20             ");
            sqlSoIn.AppendLine("            ,SingleVolume20       ");
            sqlSoIn.AppendLine("            ,theory20             ");
            sqlSoIn.AppendLine("            ,Actual20             ");
            sqlSoIn.AppendLine("            ,Remarks20            ");

            sqlSoIn.AppendLine("            ,TotalWeight40        ");
            sqlSoIn.AppendLine("            ,Volume40             ");
            sqlSoIn.AppendLine("            ,Single40             ");
            sqlSoIn.AppendLine("            ,SingleVolume40       ");
            sqlSoIn.AppendLine("            ,theory40             ");
            sqlSoIn.AppendLine("            ,Actual40             ");
            sqlSoIn.AppendLine("            ,Remarks40            ");

            sqlSoIn.AppendLine("            ,TotalWeight41        ");
            sqlSoIn.AppendLine("            ,Volume41             ");
            sqlSoIn.AppendLine("            ,Single41             ");
            sqlSoIn.AppendLine("            ,SingleVolume41       ");
            sqlSoIn.AppendLine("            ,theory41             ");
            sqlSoIn.AppendLine("            ,Actual41             ");
            sqlSoIn.AppendLine("            ,Remarks41            ");

            sqlSoIn.AppendLine("            ,Creator             ");
            sqlSoIn.AppendLine("            ,CreateDate)         ");
            sqlSoIn.AppendLine("     VALUES");
            sqlSoIn.AppendLine("           (@CompanyCD");
            sqlSoIn.AppendLine("            ,@CostNo              ");
            sqlSoIn.AppendLine("            ,@ProductID           ");
            sqlSoIn.AppendLine("            ,@ContainerSp         ");
            sqlSoIn.AppendLine("            ,@ShippedPort         ");
            sqlSoIn.AppendLine("            ,@ObjectivePort       ");
            sqlSoIn.AppendLine("            ,@TotalPrice          ");
            sqlSoIn.AppendLine("            ,@FOBPrice            ");
            sqlSoIn.AppendLine("            ,@CIFPrice            ");
            sqlSoIn.AppendLine("            ,@OTotalPrice         ");
            sqlSoIn.AppendLine("            ,@OFOBPrice           ");
            sqlSoIn.AppendLine("            ,@OCIFPrice           ");
            sqlSoIn.AppendLine("            ,@TotalPriceW          ");
            sqlSoIn.AppendLine("            ,@FOBPriceW            ");
            sqlSoIn.AppendLine("            ,@CIFPriceW            ");
            sqlSoIn.AppendLine("            ,@OTotalPriceW         ");
            sqlSoIn.AppendLine("            ,@OFOBPriceW           ");
            sqlSoIn.AppendLine("            ,@OCIFPriceW           ");
            sqlSoIn.AppendLine("            ,@CostDate             ");//CostDate
            sqlSoIn.AppendLine("            ,@Remarks              ");//Remarks


            sqlSoIn.AppendLine("            ,@TotalWeight20        ");
            sqlSoIn.AppendLine("            ,@Volume20             ");
            sqlSoIn.AppendLine("            ,@Single20             ");
            sqlSoIn.AppendLine("            ,@SingleVolume20       ");
            sqlSoIn.AppendLine("            ,@theory20             ");
            sqlSoIn.AppendLine("            ,@Actual20             ");
            sqlSoIn.AppendLine("            ,@Remarks20            ");

            sqlSoIn.AppendLine("            ,@TotalWeight40        ");
            sqlSoIn.AppendLine("            ,@Volume40             ");
            sqlSoIn.AppendLine("            ,@Single40             ");
            sqlSoIn.AppendLine("            ,@SingleVolume40       ");
            sqlSoIn.AppendLine("            ,@theory40             ");
            sqlSoIn.AppendLine("            ,@Actual40             ");
            sqlSoIn.AppendLine("            ,@Remarks40            ");

            sqlSoIn.AppendLine("            ,@TotalWeight41        ");
            sqlSoIn.AppendLine("            ,@Volume41             ");
            sqlSoIn.AppendLine("            ,@Single41             ");
            sqlSoIn.AppendLine("            ,@SingleVolume41       ");
            sqlSoIn.AppendLine("            ,@theory41             ");
            sqlSoIn.AppendLine("            ,@Actual41             ");
            sqlSoIn.AppendLine("            ,@Remarks41            ");

            sqlSoIn.AppendLine("           ,@Creator");
            sqlSoIn.AppendLine("           ,getdate())");
            sqlSoIn.AppendLine("set @ID=@@IDENTITY                ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlSoIn.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CostNo", model.CostNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContainerSp", model.ContainerSp));
            comm.Parameters.Add(SqlHelper.GetParameter("@ShippedPort", model.ShippedPort));
            comm.Parameters.Add(SqlHelper.GetParameter("@ObjectivePort", model.ObjectivePort));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@FOBPrice", model.FOBPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@CIFPrice", model.CIFPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OTotalPrice", model.OTotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OFOBPrice", model.OFOBPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OCIFPrice", model.OCIFPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPriceW", model.TotalPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@FOBPriceW", model.FOBPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@CIFPriceW", model.CIFPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OTotalPriceW", model.OTotalPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OFOBPriceW", model.OFOBPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OCIFPriceW", model.OCIFPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@CostDate", model.CostDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks", model.Remarks));

            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight20", dContainer20Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume20", dContainer20Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single20", dContainer20Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume20", dContainer20Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory20", dContainer20Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual20", dContainer20Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks20", dContainer20Info[6]));


            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight40", Container40Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume40", Container40Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single40", Container40Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume40", Container40Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory40", Container40Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual40", Container40Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks40", Container40Info[6]));

            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight41", Container41Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume41", Container41Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single41", Container41Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume41", Container41Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory41", Container41Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual41", Container41Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks41", Container41Info[6]));

            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));


            listADD.Add(comm);
            #endregion

            try
            {

                #region 成本核算明细处理
                if (!String.IsNullOrEmpty(model.DProductID))
                {
                    //var DetailTotalPriceW = new Array();      //外协采购价   
                    //var DetailRemarks = new Array();          //备注   
                    //var DetailSpecifications = new Array();   //明细
                    string[] dProductID = model.DProductID.Split(',');
                    string[] dDetailCount = model.DDetailCount.Split(',');
                    string[] dWholePrice = model.DWholePrice.Split(',');
                    string[] dSingleWeight = model.DSingleWeight.Split(',');
                    string[] dTotalWeight = model.DTotalWeight.Split(',');
                    string[] dTotalPrice = model.DTotalPrice.Split(',');
                    string[] dProdType = model.DProdType.Split(',');

                    string[] dTotalPriceW = model.DetailTotalPriceW.Split(',');
                    string[] dRemarks = model.DRemarks.Split(',');
                    string[] dSpecifications = model.DetailSpecifications.Split(',');
                    if (dProductID.Length >= 1)
                    {
                        for (int i = 0; i < dProductID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.CostDetailForeign   ");
                            cmdsql.AppendLine("           (CompanyCD                    ");
                            if (dProductID[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,ProductID                    ");
                            }
                            cmdsql.AppendLine("           ,CostNo                       ");
                            if (dDetailCount[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,DetailCount                  ");
                            }
                            if (dSingleWeight[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,SingleWeight                 ");
                            }
                            if (dTotalWeight[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,TotalWeight                  ");
                            }
                            if (dWholePrice[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,WholePrice                   ");
                            }
                            if (dTotalPrice[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,TotalPrice                   ");
                            }
                            if (dTotalPriceW[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,PurchasePrice                   ");
                            }
                            if (dRemarks[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,Remarks                   ");
                            }
                            if (dSpecifications[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,Specifications                   ");
                            }
                            cmdsql.AppendLine("           ,ProdType )                   ");
                            cmdsql.AppendLine("     VALUES                              ");
                            cmdsql.AppendLine("           (@CompanyCD                   ");
                            if (dProductID[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@ProductID                    ");
                            }
                            cmdsql.AppendLine("           ,@CostNo                       ");
                            if (dDetailCount[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@DetailCount                  ");
                            }
                            if (dSingleWeight[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@SingleWeight                 ");
                            }
                            if (dTotalWeight[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@TotalWeight                  ");
                            }
                            if (dWholePrice[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@WholePrice                   ");
                            }
                            if (dTotalPrice[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@TotalPrice                   ");
                            }
                            if (dTotalPriceW[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@PurchasePrice                   ");
                            }
                            if (dRemarks[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@Remarks                   ");
                            }
                            if (dSpecifications[i].ToString() != "")
                            {
                                cmdsql.AppendLine("           ,@Specifications                   ");
                            }
                            cmdsql.AppendLine("           ,@ProdType )                   ");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            if (dProductID[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", int.Parse(dProductID[i].ToString())));
                            }

                            comms.Parameters.Add(SqlHelper.GetParameter("@CostNo", model.CostNo));
                            if (dDetailCount[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@DetailCount", decimal.Parse(dDetailCount[i].ToString())));
                            }
                            if (dSingleWeight[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@SingleWeight", decimal.Parse(dSingleWeight[i].ToString())));
                            }
                            if (dTotalWeight[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@TotalWeight", decimal.Parse(dTotalWeight[i].ToString())));
                            }
                            if (dWholePrice[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@WholePrice", decimal.Parse(dWholePrice[i].ToString())));
                            }
                            if (dTotalPrice[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", decimal.Parse(dTotalPrice[i].ToString())));
                            }
                            if (dTotalPriceW[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@PurchasePrice", dTotalPriceW[i].ToString()));
                            }
                            if (dRemarks[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Remarks", dRemarks[i].ToString()));
                            }
                            if (dSpecifications[i].ToString() != "")
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Specifications", dSpecifications[i].ToString()));
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProdType", dProdType[i].ToString()));
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

        #region 成本核算修改
        /// <summary>
        /// 成本核算修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateCostForeign(CostForeignModel model)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }
            string[] dContainer20Info = model.Container20Info.Split(',');
            string[] Container40Info = model.Container40Info.Split(',');
            string[] Container41Info = model.Container41Info.Split(',');
            #region  成本核算修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.CostForeign              ");
            sqlEdit.AppendLine("   SET           ");
            //sqlEdit.AppendLine("      ,ContainerSp = @ContainerSp                 ");
            //sqlEdit.AppendLine("      ,ShippedPort = @ShippedPort                 ");
            //sqlEdit.AppendLine("      ,ObjectivePort = @ObjectivePort     ");
            //sqlEdit.AppendLine("      ,TotalPrice = @TotalPrice           ");
            //sqlEdit.AppendLine("           ,FOBPrice = @FOBPrice");
            //sqlEdit.AppendLine("           ,CIFPrice = @CIFPrice");
            //sqlEdit.AppendLine("           ,OTotalPrice = @OTotalPrice");
            //sqlEdit.AppendLine("           ,OFOBPrice = @OFOBPrice");
            //sqlEdit.AppendLine("           ,OCIFPrice = @OCIFPrice");
            sqlEdit.AppendLine("           Laster = @Laster");
            sqlEdit.AppendLine("           ,LastDate =getdate()");

            sqlEdit.AppendLine("            ,ProductID=@ProductID              ");
            sqlEdit.AppendLine("            ,ContainerSp=@ContainerSp          ");
            sqlEdit.AppendLine("            ,ShippedPort=@ShippedPort          ");
            sqlEdit.AppendLine("            ,ObjectivePort=@ObjectivePort      ");
            sqlEdit.AppendLine("            ,TotalPrice=@TotalPrice            ");
            sqlEdit.AppendLine("            ,FOBPrice=@FOBPrice                ");
            sqlEdit.AppendLine("            ,CIFPrice=@CIFPrice                ");
            sqlEdit.AppendLine("            ,OTotalPrice=@OTotalPrice          ");
            sqlEdit.AppendLine("            ,OFOBPrice=@OFOBPrice              ");
            sqlEdit.AppendLine("            ,OCIFPrice=@OCIFPrice              ");
            sqlEdit.AppendLine("            ,TotalPriceW=@TotalPriceW          ");
            sqlEdit.AppendLine("            ,FOBPriceW=@FOBPriceW              ");
            sqlEdit.AppendLine("            ,CIFPriceW=@CIFPriceW              ");
            sqlEdit.AppendLine("            ,OTotalPriceW=@OTotalPriceW        ");
            sqlEdit.AppendLine("            ,OFOBPriceW=@OFOBPriceW            ");
            sqlEdit.AppendLine("            ,OCIFPriceW=@OCIFPriceW            ");//
            sqlEdit.AppendLine("            ,CostDate=@CostDate                ");
            sqlEdit.AppendLine("            ,Remarks=@Remarks                  ");//Remarks

            sqlEdit.AppendLine("            ,TotalWeight20=@TotalWeight20      ");
            sqlEdit.AppendLine("            ,Volume20=@Volume20                ");
            sqlEdit.AppendLine("            ,Single20=@Single20                ");
            sqlEdit.AppendLine("            ,SingleVolume20=@SingleVolume20    ");
            sqlEdit.AppendLine("            ,theory20=@theory20                ");
            sqlEdit.AppendLine("            ,Actual20=@Actual20                ");
            sqlEdit.AppendLine("            ,Remarks20=@Remarks20              ");

            sqlEdit.AppendLine("            ,TotalWeight40=@TotalWeight40      ");
            sqlEdit.AppendLine("            ,Volume40=@Volume40                ");
            sqlEdit.AppendLine("            ,Single40=@Single40                ");
            sqlEdit.AppendLine("            ,SingleVolume40=@SingleVolume40    ");
            sqlEdit.AppendLine("            ,theory40=@theory40                ");
            sqlEdit.AppendLine("            ,Actual40=@Actual40                ");
            sqlEdit.AppendLine("            ,Remarks40=@Remarks40              ");

            sqlEdit.AppendLine("            ,TotalWeight41=@TotalWeight41      ");
            sqlEdit.AppendLine("            ,Volume41=@Volume41                ");
            sqlEdit.AppendLine("            ,Single41=@Single41                ");
            sqlEdit.AppendLine("            ,SingleVolume41=@SingleVolume41    ");
            sqlEdit.AppendLine("            ,theory41=@theory41                ");
            sqlEdit.AppendLine("            ,Actual41=@Actual41                ");
            sqlEdit.AppendLine("            ,Remarks41=@Remarks41              ");



            sqlEdit.AppendLine("   WHERE ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            //comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            //comm.Parameters.Add(SqlHelper.GetParameter("@ContainerSp", model.ContainerSp));
            //comm.Parameters.Add(SqlHelper.GetParameter("@ShippedPort", model.ShippedPort));
            //comm.Parameters.Add(SqlHelper.GetParameter("@ObjectivePort", model.ObjectivePort));
            //comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            //comm.Parameters.Add(SqlHelper.GetParameter("@FOBPrice", model.FOBPrice));
            //comm.Parameters.Add(SqlHelper.GetParameter("@CIFPrice", model.CIFPrice));
            //comm.Parameters.Add(SqlHelper.GetParameter("@OTotalPrice", model.OTotalPrice));
            //comm.Parameters.Add(SqlHelper.GetParameter("@OFOBPrice", model.OFOBPrice));
            //comm.Parameters.Add(SqlHelper.GetParameter("@OCIFPrice", model.OCIFPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@Laster", model.Creator));


            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContainerSp", model.ContainerSp));
            comm.Parameters.Add(SqlHelper.GetParameter("@ShippedPort", model.ShippedPort));
            comm.Parameters.Add(SqlHelper.GetParameter("@ObjectivePort", model.ObjectivePort));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@FOBPrice", model.FOBPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@CIFPrice", model.CIFPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OTotalPrice", model.OTotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OFOBPrice", model.OFOBPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@OCIFPrice", model.OCIFPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPriceW", model.TotalPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@FOBPriceW", model.FOBPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@CIFPriceW", model.CIFPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OTotalPriceW", model.OTotalPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OFOBPriceW", model.OFOBPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@OCIFPriceW", model.OCIFPriceW));
            comm.Parameters.Add(SqlHelper.GetParameter("@CostDate", model.CostDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks", model.Remarks));

            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight20", dContainer20Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume20", dContainer20Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single20", dContainer20Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume20", dContainer20Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory20", dContainer20Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual20", dContainer20Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks20", dContainer20Info[6]));


            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight40", Container40Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume40", Container40Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single40", Container40Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume40", Container40Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory40", Container40Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual40", Container40Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks40", Container40Info[6]));

            comm.Parameters.Add(SqlHelper.GetParameter("@TotalWeight41", Container41Info[0]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Volume41", Container41Info[1]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Single41", Container41Info[2]));
            comm.Parameters.Add(SqlHelper.GetParameter("@SingleVolume41", Container41Info[3]));
            comm.Parameters.Add(SqlHelper.GetParameter("@theory41", Container41Info[4]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Actual41", Container41Info[5]));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remarks41", Container41Info[6]));


            listADD.Add(comm);
            #endregion


            #region 先删除不在成本核算明细中的ID
            StringBuilder sqlDel = new StringBuilder();
            sqlDel.AppendLine("Delete From officedba.CostDetailForeign where CompanyCD=@CompanyCD and CostNo=@CostNo");

            SqlCommand commDel = new SqlCommand();
            commDel.CommandText = sqlDel.ToString();
            commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            commDel.Parameters.Add(SqlHelper.GetParameter("@CostNo", model.CostNo));
            listADD.Add(commDel);
            #endregion

            #region 成本核算明细处理
            if (!String.IsNullOrEmpty(model.DProductID))
            {
                #region 删除代码
                //string[] dProductID = model.DProductID.Split(',');
                //string[] dDetailCount = model.DDetailCount.Split(',');
                //string[] dWholePrice = model.DWholePrice.Split(',');
                //string[] dSingleWeight = model.DSingleWeight.Split(',');
                //string[] dTotalWeight = model.DTotalWeight.Split(',');
                //string[] dTotalPrice = model.DTotalPrice.Split(',');
                //string[] dProdType = model.DProdType.Split(',');

                //if (dProductID.Length >= 1)
                //{
                //    for (int i = 0; i < dProductID.Length; i++)
                //    {
                //        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                //        cmdsql.AppendLine("INSERT INTO officedba.CostDetailForeign   ");
                //        cmdsql.AppendLine("           (CompanyCD                    ");
                //        if (dProductID[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,ProductID                    ");
                //        }
                //        cmdsql.AppendLine("           ,CostNo                       ");
                //        if (dDetailCount[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,DetailCount                  ");
                //        }
                //        if (dSingleWeight[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,SingleWeight                 ");
                //        }
                //        if (dTotalWeight[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,TotalWeight                  ");
                //        }
                //        if (dWholePrice[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,WholePrice                   ");
                //        }                       
                //        cmdsql.AppendLine("           ,TotalPrice                   ");
                //        cmdsql.AppendLine("           ,ProdType )                   ");
                //        cmdsql.AppendLine("     VALUES                              ");
                //        cmdsql.AppendLine("           (@CompanyCD                   ");
                //        if (dProductID[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,@ProductID                    ");
                //        }
                //        cmdsql.AppendLine("           ,@CostNo                       ");
                //        if (dDetailCount[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,@DetailCount                  ");
                //        }
                //        if (dSingleWeight[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,@SingleWeight                 ");
                //        }
                //        if (dTotalWeight[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,@TotalWeight                  ");
                //        }
                //        if (dWholePrice[i].ToString() != "")
                //        {
                //            cmdsql.AppendLine("           ,@WholePrice                   ");
                //        }
                //        cmdsql.AppendLine("           ,@TotalPrice                   ");
                //        cmdsql.AppendLine("           ,@ProdType )                   ");

                //        SqlCommand comms = new SqlCommand();
                //        comms.CommandText = cmdsql.ToString();
                //        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                //        if (dProductID[i].ToString() != "")
                //        {
                //            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", int.Parse(dProductID[i].ToString())));
                //        }

                //        comms.Parameters.Add(SqlHelper.GetParameter("@CostNo", model.CostNo));
                //        if (dDetailCount[i].ToString() != "")
                //        {
                //            comms.Parameters.Add(SqlHelper.GetParameter("@DetailCount", decimal.Parse(dDetailCount[i].ToString())));
                //        }
                //        if (dSingleWeight[i].ToString() != "")
                //        {
                //            comms.Parameters.Add(SqlHelper.GetParameter("@SingleWeight", decimal.Parse(dSingleWeight[i].ToString())));
                //        }
                //        if (dTotalWeight[i].ToString() != "")
                //        {
                //            comms.Parameters.Add(SqlHelper.GetParameter("@TotalWeight", decimal.Parse(dTotalWeight[i].ToString())));
                //        }
                //        if (dWholePrice[i].ToString() != "")
                //        {
                //            comms.Parameters.Add(SqlHelper.GetParameter("@WholePrice", decimal.Parse(dWholePrice[i].ToString())));
                //        }
                //        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", decimal.Parse(dTotalPrice[i].ToString())));
                //        comms.Parameters.Add(SqlHelper.GetParameter("@ProdType", dProdType[i].ToString()));
                //        listADD.Add(comms);
                #endregion

                string[] dProductID = model.DProductID.Split(',');
                string[] dDetailCount = model.DDetailCount.Split(',');
                string[] dWholePrice = model.DWholePrice.Split(',');
                string[] dSingleWeight = model.DSingleWeight.Split(',');
                string[] dTotalWeight = model.DTotalWeight.Split(',');
                string[] dTotalPrice = model.DTotalPrice.Split(',');
                string[] dProdType = model.DProdType.Split(',');

                string[] dTotalPriceW = model.DetailTotalPriceW.Split(',');
                string[] dRemarks = model.DRemarks.Split(',');
                string[] dSpecifications = model.DetailSpecifications.Split(',');
                if (dProductID.Length >= 1)
                {
                    for (int i = 0; i < dProductID.Length; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        cmdsql.AppendLine("INSERT INTO officedba.CostDetailForeign   ");
                        cmdsql.AppendLine("           (CompanyCD                    ");
                        if (dProductID[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,ProductID                    ");
                        }
                        cmdsql.AppendLine("           ,CostNo                       ");
                        if (dDetailCount[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,DetailCount                  ");
                        }
                        if (dSingleWeight[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,SingleWeight                 ");
                        }
                        if (dTotalWeight[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,TotalWeight                  ");
                        }
                        if (dWholePrice[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,WholePrice                   ");
                        }
                        if (dTotalPrice[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,TotalPrice                   ");
                        }
                        if (dTotalPriceW[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,PurchasePrice                   ");
                        }
                        if (dRemarks[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,Remarks                   ");
                        }
                        if (dSpecifications[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,Specifications                   ");
                        }
                        cmdsql.AppendLine("           ,ProdType )                   ");
                        cmdsql.AppendLine("     VALUES                              ");
                        cmdsql.AppendLine("           (@CompanyCD                   ");
                        if (dProductID[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@ProductID                    ");
                        }
                        cmdsql.AppendLine("           ,@CostNo                       ");
                        if (dDetailCount[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@DetailCount                  ");
                        }
                        if (dSingleWeight[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@SingleWeight                 ");
                        }
                        if (dTotalWeight[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@TotalWeight                  ");
                        }
                        if (dWholePrice[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@WholePrice                   ");
                        }
                        if (dTotalPrice[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@TotalPrice                   ");
                        }
                        if (dTotalPriceW[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@PurchasePrice                   ");
                        }
                        if (dRemarks[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@Remarks                   ");
                        }
                        if (dSpecifications[i].ToString() != "")
                        {
                            cmdsql.AppendLine("           ,@Specifications                   ");
                        }
                        cmdsql.AppendLine("           ,@ProdType )                   ");

                        SqlCommand comms = new SqlCommand();
                        comms.CommandText = cmdsql.ToString();
                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        if (dProductID[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", int.Parse(dProductID[i].ToString())));
                        }

                        comms.Parameters.Add(SqlHelper.GetParameter("@CostNo", model.CostNo));
                        if (dDetailCount[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@DetailCount", decimal.Parse(dDetailCount[i].ToString())));
                        }
                        if (dSingleWeight[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@SingleWeight", decimal.Parse(dSingleWeight[i].ToString())));
                        }
                        if (dTotalWeight[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@TotalWeight", decimal.Parse(dTotalWeight[i].ToString())));
                        }
                        if (dWholePrice[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@WholePrice", decimal.Parse(dWholePrice[i].ToString())));
                        }
                        if (dTotalPrice[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", decimal.Parse(dTotalPrice[i].ToString())));
                        }
                        if (dTotalPriceW[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@PurchasePrice", dTotalPriceW[i].ToString()));
                        }
                        if (dRemarks[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@Remarks", dRemarks[i].ToString()));
                        }
                        if (dSpecifications[i].ToString() != "")
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@Specifications", dSpecifications[i].ToString()));
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProdType", dProdType[i].ToString()));
                        listADD.Add(comms);
                    }
                }
            }
            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion
        #region 成本核算详细信息
        /// <summary>
        /// 成本核算详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCostForeignInfo(CostForeignModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select  a.ID, a.CompanyCD, a.CostNo, a.ProductID,e.ProductName, a.ContainerSp, a.ShippedPort, a.ObjectivePort,");
            infoSql.AppendLine(" a.TotalPrice, a.FOBPrice, a.CIFPrice,a.TotalPriceW, a.FOBPriceW, a.CIFPriceW, a.OTotalPrice, a.OFOBPrice,");
            infoSql.AppendLine("	 a.OCIFPrice,a.OTotalPriceW, a.OFOBPriceW,a.OCIFPriceW, a.Creator, a.Laster,d.CurrencyName,	");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.LastDate, 23),'') as LastDate,");
            infoSql.AppendLine("		b.EmployeeName as CreatorReal,c.EmployeeName as Lastername");
            infoSql.AppendLine("		,isnull( CONVERT(CHAR(10),a.CostDate, 23),'') as CostDate,a.TotalWeight20 ,a.Volume20,a.Single20 ,a.SingleVolume20 ,a.theory20 ,a.Actual20   ,a.Remarks20 ");
            infoSql.AppendLine("  ,a.TotalWeight40  ,a.Volume40 ,a.Single40 ,a.SingleVolume40 ,a.theory40,a.Actual40  ,a.Remarks40 ");
            infoSql.AppendLine(" ,a.TotalWeight41 ,a.Volume41  ,a.Single41  ,a.SingleVolume41 ,a.theory41 ,a.Actual41  ,a.Remarks41");

            infoSql.AppendLine("from officedba.CostForeign a");
            infoSql.AppendLine("left join officedba.EmployeeInfo b on a.Creator=b.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo c on a.Laster=c.ID");
            infoSql.AppendLine("left join officedba.CurrencyTypeSetting d on a.ContainerSp=d.ID");//[officedba].[ProductInfo]
            infoSql.AppendLine("left join officedba.ProductInfo e on e.id=a.ProductID");//[officedba].[ProductInfo]
            infoSql.AppendLine("where a.ID=@ID  ");
           
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
        #region 成本核算详细信息
        /// <summary>
        /// 成本核算详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCostForeignInfo(string ProductID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select top(1) a.ID, a.CompanyCD, a.CostNo, a.ProductID,e.ProductName, a.ContainerSp, a.ShippedPort, a.ObjectivePort,");
            infoSql.AppendLine(" a.TotalPrice, a.FOBPrice, a.CIFPrice,a.TotalPriceW, a.FOBPriceW, a.CIFPriceW, a.OTotalPrice, a.OFOBPrice,");
            infoSql.AppendLine("	 a.OCIFPrice,a.OTotalPriceW, a.OFOBPriceW,a.OCIFPriceW, a.Creator, a.Laster,d.CurrencyName,	");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.LastDate, 23),'') as LastDate,");
            infoSql.AppendLine("		b.EmployeeName as CreatorReal,c.EmployeeName as Lastername");
            infoSql.AppendLine("		,isnull( CONVERT(CHAR(10),a.CostDate, 23),'') as CostDate,a.TotalWeight20 ,a.Volume20,a.Single20 ,a.SingleVolume20 ,a.theory20 ,a.Actual20   ,a.Remarks20 ");
            infoSql.AppendLine("  ,a.TotalWeight40  ,a.Volume40 ,a.Single40 ,a.SingleVolume40 ,a.theory40,a.Actual40  ,a.Remarks40 ");
            infoSql.AppendLine(" ,a.TotalWeight41 ,a.Volume41  ,a.Single41  ,a.SingleVolume41 ,a.theory41 ,a.Actual41  ,a.Remarks41");

            infoSql.AppendLine("from officedba.CostForeign a");
            infoSql.AppendLine("left join officedba.EmployeeInfo b on a.Creator=b.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo c on a.Laster=c.ID");
            infoSql.AppendLine("left join officedba.CurrencyTypeSetting d on a.ContainerSp=d.ID");//[officedba].[ProductInfo]
            infoSql.AppendLine("left join officedba.ProductInfo e on e.id=a.ProductID");//[officedba].[ProductInfo]
            infoSql.AppendLine("where a.ProductID=@ProductID order by ID desc ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 成本核算明细详细信息
        /// <summary>
        /// 成本核算明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCostForeignDetailInfoList(CostForeignModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select a.ID as DetailID, a.CompanyCD, a.CostNo, a.ProductID, a.DetailCount, a.TotalCount, a.SingleWeight, ");
            detSql.AppendLine("a.TotalWeight, a.WholePrice, a.TotalPrice, a.ProdType, ");
            detSql.AppendLine("         b.ProductName, b.ProductNo, b.Specification,a.PurchasePrice,a.Remarks,c.QualityName");
            detSql.AppendLine("from officedba.CostDetailForeign a");
            detSql.AppendLine("left join officedba.ProductInfo b  on a.ProductID=b.ID");//QualityName
            detSql.AppendLine("left join officedba.Quality c  on c.ID=b.Quality");//QualityName
            detSql.AppendLine("where a.CompanyCD=@CompanyCD and ProdType ='1' and a.CostNo=(select top 1 CostNo from officedba.CostForeign where ID=@ID)");
            detSql.AppendLine("union");
            detSql.AppendLine("select a.ID as DetailID, a.CompanyCD, a.CostNo, a.ProductID, a.DetailCount, a.TotalCount, a.SingleWeight, ");
            detSql.AppendLine("a.TotalWeight, a.WholePrice, a.TotalPrice, a.ProdType, ");
            detSql.AppendLine("         b.TeachName as ProductName, b.TeachNo as ProductNo, b.Specification as Specification,a.PurchasePrice,a.Remarks,c.QualityName");
            detSql.AppendLine("from officedba.CostDetailForeign a");
            detSql.AppendLine("left join officedba.TeachInfo b  on a.ProductID=b.ID");//QualityName
            detSql.AppendLine("left join officedba.Quality c  on c.ID=b.Quality");//QualityName
            detSql.AppendLine("where a.CompanyCD=@CompanyCD and ProdType <>'1' and a.CostNo=(select top 1 CostNo from officedba.CostForeign where ID=@ID)");
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

        #region 通过检索条件查询销售订单信息
        /// <summary>
        /// 通过检索条件查询销售订单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetCostForeignListBycondition(CostForeignModel model, string UserID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            searchSql.AppendLine("select  a.ID, a.CompanyCD, a.CostNo,e.ProductName, a.Title,isnull( CONVERT(CHAR(10),a.CostDate, 23),'') as CostDate, a.ContainerSp, a.ShippedPort, a.ObjectivePort,     ");
            searchSql.AppendLine(" a.TotalPrice, a.FOBPrice, a.CIFPrice, a.OTotalPrice, a.OFOBPrice,");
            searchSql.AppendLine("	 a.OCIFPrice, a.Creator,  a.Laster,d.CurrencyName,	");
            searchSql.AppendLine(" a.TotalPriceW, a.FOBPriceW, a.CIFPriceW, a.OTotalPriceW, a.OFOBPriceW, a.OCIFPriceW, ");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.LastDate, 23),'') as LastDate,    ");
            searchSql.AppendLine("		b.EmployeeName as CreatorReal,c.EmployeeName as Lastername     ");
            searchSql.AppendLine("from officedba.CostForeign a     ");
            searchSql.AppendLine("left join officedba.EmployeeInfo b on a.Creator=b.ID   ");
            searchSql.AppendLine("left join officedba.EmployeeInfo c on a.Laster=c.ID   ");
            searchSql.AppendLine("left join officedba.CurrencyTypeSetting d on a.ContainerSp=d.ID    ");
            searchSql.AppendLine("left join officedba.ProductInfo e on e.id=a.ProductID");//[officedba].[ProductInfo]
            searchSql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and ((a.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            searchSql.AppendLine("  INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            searchSql.AppendLine(" CROSS JOIN            officedba.UserInfo AS v  ");
            searchSql.AppendLine(" INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            searchSql.AppendLine("  INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            searchSql.AppendLine("                WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0  ");
            searchSql.AppendLine(" or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "'))))");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //单据编号
            if (!string.IsNullOrEmpty(model.CostNo))
            {
                searchSql.AppendLine(" and a.CostNo like @CostNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CostNo", "%" + model.CostNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" and a.Title like @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(model.ProductID))
            {
                searchSql.AppendLine(" and a.ProductID = @ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", model.ProductID));
            }
            if (!string.IsNullOrEmpty(model.CostDateEnd) && !string.IsNullOrEmpty(model.CostDateEnd))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CostDateEnd", model.CostDateEnd));
                searchSql.Append(" and CONVERT(CHAR(10), a.CostDate, 23)<=@CostDateEnd ");
            }
            if (!string.IsNullOrEmpty(model.CostDateStart) && !string.IsNullOrEmpty(model.CostDateStart))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CostDateStart", model.CostDateStart));
                searchSql.Append(" and CONVERT(CHAR(10), a.CostDate, 23)>=@CostDateStart  ");
            }
            //供应商
            if (int.Parse(model.ContainerSp) > 0)
            {
                searchSql.AppendLine(" and a.ContainerSp=@ContainerSp ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContainerSp", model.ContainerSp));
            }
            //指定命令的SQL文

            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
        #region 销售订单删除
        /// <summary>
        /// 销售订单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteCostForeign(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlMasterDet = new StringBuilder();
                    StringBuilder sqlMaster = new StringBuilder();
                    sqlMasterDet.AppendLine("delete from officedba.CostDetailForeign where CompanyCD=@CompanyCD and CostNo=(select CostNo from officedba.CostForeign where ID=@ID)");
                    sqlMaster.AppendLine("delete from officedba.CostForeign where ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlMasterDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlMaster.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion
    }
}
