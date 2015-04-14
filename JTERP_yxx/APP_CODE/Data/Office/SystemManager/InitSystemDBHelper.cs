/**********************************************
 * 类作用：   系统初始化
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/22
 ***********************************************/
using System;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;

namespace XBase.Data.Office.SystemManager
{
    public class InitSystemDBHelper
    {

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>

        public static bool DeleteInfo(string flag, string CompanyCD)
        {
            string where = "  where 1=1";
            if (CompanyCD != "")
            {
                where += "and CompanyCD='" + CompanyCD+"'";
            }

            IList<string> dd = new List<string>();
            /*删除进销存*/
            /*销售管理*/
            /*删除销售计划表*/
            dd.Add("delete from officedba.SellPlan " + where);
            dd.Add("delete from officedba.SellPlanDetail  " + where);

            /*删除销售计划表*/
            dd.Add("delete from officedba.AdversaryInfo " + where);
            dd.Add("delete from officedba.AdversaryDynamic" + where);
            dd.Add("delete from officedba.AdversarySell" + where);
            /*删除销售机会表*/
            dd.Add("delete from officedba.SellChance " + where);
            dd.Add("delete from officedba.SellChancePush" + where);
            /*删除销售报价表*/
            dd.Add("delete from officedba.SellOffer " + where);
            dd.Add("delete from officedba.SellOfferDetail" + where);
            dd.Add("delete from officedba.SellOfferHistory" + where);
            /*删除销售合同表*/
            dd.Add("delete from officedba.SellContract " + where);
            dd.Add("delete from officedba.SellContractDetail" + where);
            /*删除销售订单表*/
            dd.Add("delete from officedba.SellOrder " + where);
            dd.Add("delete from officedba.SellOrderDetail" + where);
            dd.Add("delete from officedba.SellOrderFeeDetail" + where);
            /*删除销售发货表*/
            dd.Add("delete from officedba.SellSend" + where);
            dd.Add("delete from officedba.SellSendDetail" + where);
            /*删除销售回款表*/
            dd.Add("delete from officedba.SellGathering" + where);
            /*删除销售退货表*/
            dd.Add("delete from officedba.SellBack" + where);
            dd.Add("delete from officedba.SellBackDetail" + where);
            /*删除销售代销表*/
            dd.Add("delete from officedba.SellChannelSttl" + where);
            dd.Add("delete from officedba.SellChannelSttlDetail" + where);
            /*删除销售产品表*/
            dd.Add("delete from officedba.UserProductInfo" + where);


            /*采购管理*/
          
            /*删除采购申请表*/
            dd.Add("delete from officedba.PurchaseApply" + where);
            dd.Add("delete from officedba.PurchaseApplyDetail" + where);
            dd.Add("delete from officedba.PurchaseApplyDetailSource" + where);
            /*删除采购需求表*/
            dd.Add("delete from officedba.PurchaseRequire" + where);
            /*删除采购计划表*/
            dd.Add("delete from officedba.PurchasePlan" + where);
            dd.Add("delete from officedba.PurchasePlanDetail" + where);
            dd.Add("delete from officedba.PurchasePlanSource" + where);
            /*删除采购询价表*/
            dd.Add("delete from officedba.PurchaseAskPrice" + where);
            dd.Add("delete from officedba.PurchaseAskPriceDetail" + where);
            dd.Add("delete from officedba.PurchaseAskPriceHistory" + where);
            /*删除采购合同表*/
            dd.Add("delete from officedba.PurchaseContract" + where);
            dd.Add("delete from officedba.PurchaseContractDetail" + where);
            /*删除采购订单表*/
            dd.Add("delete from officedba.PurchaseOrder" + where);
            dd.Add("delete from officedba.PurchaseOrderDetail" + where);
            /*删除采购到货表*/
            dd.Add("delete from officedba.PurchaseArrive" + where);
            dd.Add("delete from officedba.PurchaseArriveDetail" + where);
            /*删除采购退货表*/
            dd.Add("delete from officedba.PurchaseReject" + where);
            dd.Add("delete from officedba.PurchaseRejectDetail" + where);
         
            /*删除入库表*/
            dd.Add("delete from officedba.StorageInPurchase" + where);
            dd.Add("delete from officedba.StorageInPurchaseDetail" + where);
            dd.Add("delete from officedba.StorageInProcess" + where);
            dd.Add("delete from officedba.StorageInProcessDetail" + where);
            dd.Add("delete from officedba.StorageInOther" + where);
            dd.Add("delete from officedba.StorageInOtherDetail" + where);
            dd.Add("delete from officedba.StorageInRed" + where);
            dd.Add("delete from officedba.StorageInRedDetail" + where);
            /*删除出库表*/
            dd.Add("delete from officedba.StorageOutSell" + where);
            dd.Add("delete from officedba.StorageOutSellDetail" + where);
            dd.Add("delete from officedba.StorageOutOther" + where);
            dd.Add("delete from officedba.StorageOutOtherDetail" + where);
            dd.Add("delete from officedba.StorageOutRed" + where);
            dd.Add("delete from officedba.StorageOutRedDetail" + where);
            /*删除出库表*/
            dd.Add("delete from officedba.StorageBorrow" + where);
            dd.Add("delete from officedba.StorageBorrowDetail" + where);
            dd.Add("delete from officedba.StorageReturn" + where);
            dd.Add("delete from officedba.StorageReturnDetail" + where);
            /*删除调拨表*/
            dd.Add("delete from officedba.StorageTransfer" + where);
            dd.Add("delete from officedba.StorageTransferDetail" + where);
            /*删除调整表*/
            dd.Add("delete from officedba.StorageAdjust" + where);
            dd.Add("delete from officedba.StorageAdjustDetail" + where);
            /*删除期末盘点表*/
            dd.Add("delete from officedba.StorageCheck" + where);
            dd.Add("delete from officedba.StorageCheckDetail" + where);
            /*删除报损表*/
            dd.Add("delete from officedba.StorageLoss" + where);
            dd.Add("delete from officedba.StorageLossDetail" + where);
            /*删除日结结表*/
            dd.Add("delete from officedba.StorageDaily" + where);
            /*删除月结表*/
            dd.Add("delete from officedba.StorageMonthly" + where);


            /*生产管理*/
            /*删除成本核算表*/
            dd.Add("delete from officedba.CostProduction" + where);
            dd.Add("delete from officedba.WorkCenter" + where);
            dd.Add("delete from officedba.TechnicsArchives" + where);
            dd.Add("delete from officedba.StandardSequ" + where);
            dd.Add("delete from officedba.StandardSequDetail" + where);
            dd.Add("delete from officedba.TechnicsRouting" + where);
            dd.Add("delete from officedba.TechnicsRoutingDetail" + where);
            /*删除物料清单表*/
            dd.Add("delete from officedba.Bom" + where);
            dd.Add("delete from officedba.BomDetail" + where);
            dd.Add("delete from officedba.MasterProductSchedule" + where);
            dd.Add("delete from officedba.MasterProductScheduleDetail" + where);
            /*删除物料需求表*/
            dd.Add("delete from officedba.ManufactureTask" + where);
            dd.Add("delete from officedba.ManufactureTaskDetail" + where);
            dd.Add("delete from officedba.ManufactureReport" + where);
            dd.Add("delete from officedba.ManufactureReportMachine" + where);
            dd.Add("delete from officedba.ManufactureReportMeterial" + where);
            dd.Add("delete from officedba.ManufactureReportProduct" + where);
            dd.Add("delete from officedba.ManufactureReportStaff" + where);
            dd.Add("delete from officedba.MRP" + where);
            dd.Add("delete from officedba.MRPDetail" + where);
            /*删除生产物料表*/
            dd.Add("delete from officedba.TakeMaterial" + where);
            dd.Add("delete from officedba.TakeMaterialDetail" + where);
            dd.Add("delete from officedba.BackMaterial" + where);
            dd.Add("delete from officedba.BackMaterialDetail" + where);


            /*质检管理*/
            /*删除质检申请表*/
            dd.Add("delete from officedba.QualityCheckApplay" + where);
            dd.Add("delete from officedba.QualityCheckApplyDetail" + where);
            /*删除质检报告表*/
            dd.Add("delete from officedba.QualityCheckReport" + where);
            dd.Add("delete from officedba.CheckReportDetail" + where);
            /*删除不合格品处置表*/
            dd.Add("delete from officedba.CheckNotPass" + where);
            dd.Add("delete from officedba.CheckNotPassDetail" + where);



            /*财务管理*/
           
            /*删除固定资产表*/
            dd.Add("delete from officedba.AssetTypeSetting" + where);
            dd.Add("delete from officedba.FixAssetInfo" + where);
            /*删除出纳表*/
            dd.Add("delete from officedba.Billing" + where);
            dd.Add("delete from officedba.IncomeBill" + where);
            dd.Add("delete from officedba.PayBill" + where);
            dd.Add("delete from officedba.StoreFetchBill" + where);
            dd.Add("delete from officedba.InSideChangeAcco" + where);
            dd.Add("delete from officedba.Fees" + where);
            dd.Add("delete from officedba.FeesDetail" + where);
            /*删除财务处理表*/

            dd.Add("delete from officedba.AttestBillDetails WHERE AttestBillID in (select ID from officedba.AttestBill" + where + ")");
            dd.Add("delete from officedba.AttestBill" + where);
            dd.Add("delete from officedba.EndItemProcessedRecord" + where);
            dd.Add("delete from officedba.EndItemProcSetting" + where);
            
            /*删除财务预算表*/
            dd.Add("delete from officedba.Financialbudgetbill" + where);
            dd.Add("delete from officedba.Financialbudgetdetails" + where);


            dd.Add("delete from officedba.SubStorageProduct" + where);
            dd.Add("delete from officedba.SubProductSendPrice" + where);
            dd.Add("delete from officedba.SubProductSellPrice" + where);
            /*删除配送表*/
            dd.Add("delete from officedba.SubDeliverySend" + where);
            dd.Add("delete from officedba.SubDeliverySendDetail" + where);
            /*删除退货表*/
            dd.Add("delete from officedba.SubDeliveryBack" + where);
            dd.Add("delete from officedba.SubDeliveryBackDetail" + where);
            /*删除调拨表*/
            dd.Add("delete from officedba.SubDeliveryTrans" + where);
            dd.Add("delete from officedba.SubDeliveryTransDetail" + where);



            /*门店管理*/
            /*删除库存表*/
            dd.Add("delete from officedba.SubStorageIn" + where);
            dd.Add("delete from officedba.SubStorageInDetail" + where);
            /*删除销售表*/
            dd.Add("delete from officedba.SubSellOrder" + where);
            dd.Add("delete from officedba.SubSellOrderDetail" + where);
            dd.Add("delete from officedba.SubSellBack" + where);
            dd.Add("delete from officedba.SubSellBackDetail" + where);
            dd.Add("delete from officedba.SubSellCustInfo" + where);


            /*订货中心*/
            /*删除商品表*/
            dd.Add("delete from websitedba.WebSiteProductInfo WHERE ProductID IN (select ID from officedba.ProductInfo " + where + ")");
            dd.Add("delete from websitedba.WebStieSellOrder" + where);
            dd.Add("delete from websitedba.WebSiteSellOrderDetail" + where);
            dd.Add("delete from websitedba.WebSiteCustomInfo WHERE CustomID IN (select ID from officedba.CustInfo " + where + ")");
            //库存量
            dd.Add("update officedba.StorageProduct set   ProductCount = 0,OrderCount = 0,RoadCount= 0,OutCount=0,InCount =0 " + where );
            //出入库流水账
            dd.Add("delete from officedba.StorageAccount" + where);
            dd.Add("delete from officedba.SubStorageAccount" + where);
            //月末处理
            dd.Add("delete from officedba.StorageCost" + where);

            if ( Int32.Parse(flag) > 1)
            {
                //人力资源
                //员工信息表
                dd.Add("delete from  officedba.EmployeeInfo" + where);
                //员工履历表
                dd.Add("delete from  officedba.EmployeeHistory" + where);
                //技能信息表
                dd.Add("delete from officedba.EmployeeSkill" + where);
                //人才代理表
                dd.Add("delete from officedba.HRProxy" + where);
                //招聘申请表
                dd.Add("delete from officedba.RectApply" + where);
                //招聘申请明细表
                dd.Add("delete from officedba.RectApplyDetail" + where);
                //招聘计划表
                dd.Add("delete from officedba.RectPlan" + where);
                //招聘计划目标表
                dd.Add("delete from officedba.RectGoal" + where);
                //招聘信息发布表
                dd.Add("delete from officedba.RectPublish" + where);
                //招聘面试记录表
                dd.Add("delete from officedba.RectInterview" + where);
                //招聘面试评测表
                dd.Add("delete from officedba.RectInterviewDetail" + where);
                //面试评测模板表
                dd.Add("delete from officedba.RectCheckTemplate" + where);
                //面试模板要素表
                dd.Add("delete from officedba.RectCheckTemplateElem" + where);
                //面试评测要素表
                dd.Add("delete from officedba.RectCheckElem" + where);
                //员工合同管理表
                dd.Add("delete from officedba.EmployeeContract" + where);
                //员工调职申请表
                dd.Add("delete from officedba.EmplApply" + where);
                //员工离职申请表
                dd.Add("delete from officedba.MoveApply" + where);
                //员工调职单表
                dd.Add("delete from officedba.EmplApplyNotify" + where);
                //员工离职单表
                dd.Add("delete from  officedba.MoveNotify" + where);
                //员工培训表
                dd.Add("delete from  officedba.EmployeeTraining" + where);
                //培训人员表
                dd.Add("delete from  officedba.TrainingUser" + where);
                //培训进度表
                dd.Add("delete from officedba.TrainingSchedule" + where);
                //培训考核表
                dd.Add("delete from officedba.TrainingAsse" + where);
                //考核详细表
                dd.Add("delete from officedba.TrainingDetail" + where);
                //考试管理相关表
                //员工考试表
                dd.Add("delete from officedba.EmployeeTest" + where);
                //员工考试成绩表
                dd.Add("delete from officedba.EmployeeTestScore" + where);
                //薪酬管理相关表
                //工资项设置表(考虑放到基础模块删除)
                dd.Add("delete from officedba.SalaryItem" + where);
                //工资标准设置表
                dd.Add("delete from officedba.SalaryStandard" + where);
                //计件项目表(考虑放到基础模块删除)
                dd.Add("delete from officedba.PieceworkItem" + where);
                //计件工资表
                dd.Add("delete from officedba.PieceworkSalary" + where);
                //计时项目表(考虑放到基础模块删除)
                dd.Add("delete from officedba.TimeItem" + where);
                //计时工资表
                dd.Add("delete from officedba.TimeSalary" + where);
                //提成项目表
                dd.Add("delete from  officedba.CommissionItem" + where);
                //提成工资表
                dd.Add("delete from  officedba.CommissionSalary" + where);
                //保险福利
                //员工社会保险表
                dd.Add("delete from officedba.InsuEmployee" + where);
                //员工个人所得税表
                dd.Add("delete from officedba.IncomeTax" + where);
                //员工工资设置表
                dd.Add("delete from officedba.SalaryEmployee" + where);
                //员工月工资合计表
                dd.Add("delete from officedba.SalaryReportSummary" + where);
                //员工月工资明细表
                dd.Add("delete from officedba.SalaryReportDetail" + where);
                //绩效考核相关表
                //人员考核流程表" 
                dd.Add("delete from officedba.PerformanceTemplateEmp" + where);
                //考核任务表
                dd.Add("delete from officedba.PerformanceTask" + where);
                //考核评分记录表
                dd.Add("delete from officedba.PerformanceScore" + where);
                //考核总评表
                dd.Add("delete from officedba.PerformanceSummary" + where);
                //员工自我鉴定表
                dd.Add("delete from officedba.PerformancePersonal" + where);
                //绩效改进计划表
                dd.Add("delete from officedba.PerformanceBetter" + where);
                //绩效改进计划详细表
                dd.Add("delete from officedba.PerformanceBetterDetail" + where);
                //客户模块
                //客户信息表
                dd.Add("delete from officedba.CustInfo " + where);
                //联系人表
                dd.Add("delete from officedba.CustLinkMan" + where);
                //客户联络表
                dd.Add("delete from officedba.CustContact" + where);
                //客户跟踪洽谈管理
                dd.Add("delete from officedba.CustTalk" + where);
                //客户关怀表
                dd.Add("delete from officedba.CustLove" + where);

                //客户服务表
                dd.Add("delete from officedba.CustService" + where);
                //客户投诉表
                dd.Add("delete from officedba.CustComplain" + where);
                //客户建议表
                dd.Add("delete from officedba.CustAdvice" + where);
                //客户来电列表
                dd.Add("delete from  officedba.CustCall" + where);

                /*删除供应商表*/
                dd.Add("delete from officedba.ProviderInfo" + where);
                /*删除供应商联系人表*/
                dd.Add("delete from officedba.ProviderLinkMan" + where);
                /*删除供应商联络表*/
                dd.Add("delete from officedba.ProviderContactHistory" + where);
                /*删除供应商物品表*/
                dd.Add("delete from officedba.ProviderProduct" + where);

               //	目标表
                dd.Add("delete  FROM officedba.PlanAim " + where);
                //目标检查表
                dd.Add("delete  FROM officedba.PlanCheck " + where);
                ////任务管理
                //任务表
                dd.Add("delete  FROM officedba.Task " + where);
                ////任务检查表
                //dd.Add("delete  FROM officedba.TaskCheck
                ////工作日志
                //工作日志表
                dd.Add("delete  FROM officedba.PersonalNote " + where);
                ////备忘录
                //备忘录表
                dd.Add("delete  FROM  officedba.PersonalMemo " + where);
                ////日程安排
                //日程安排表
                dd.Add("delete  FROM officedba. PersonalDateArrange " + where);
                //日程提醒记录表
                dd.Add("delete  FROM officedba.NoticeHistory " + where);
                ////企业文化
                //企业文化分类表
                dd.Add("delete  FROM officedba.CultureType " + where);
                //企业文化信息表
                dd.Add("delete  FROM officedba.CultureDocs " + where);
                ////公共交流
                //站内短信联系人表
                dd.Add("delete  FROM officedba.MyContact " + where);
                //站内短信联系人分组表
                dd.Add("delete  FROM officedba. MyContactGroup " + where);
                //站内短信收件夹表
                dd.Add("delete  FROM officedba.MessageInputBox " + where);
                //站内短信发件夹表
                dd.Add("delete  FROM officedba.MessageSendBox " + where);
                //个人建议发件夹表
                dd.Add("delete  FROM officedba.PersonalAdviceSend " + where);
                //个人建议收件夹表
                dd.Add("delete  FROM officedba.PersonalAdviceInput " + where);
                //公告板信息表
                dd.Add("delete  FROM officedba.PublicNotice " + where);
                //手机短信发送表
                dd.Add("delete  FROM officedba.MobileMsgMonitor " + where);
                ////个人通讯录
                //个人通讯录表
                dd.Add("delete  FROM officedba.PersonalLinkman " + where);
                //通讯录分组表
                dd.Add("delete  FROM officedba.LinkmanType " + where);
                ////记事便签
                //记事便签表  
                dd.Add("delete  FROM officedba.PersonalScratchpad " + where);
                //客户物品对照表
                dd.Add("delete  FROM officedba.CustProdDetails " + where);

            }
            if (Int32.Parse(flag) > 2)
            {
                //客户基础设置
                //分类信息列表 
                dd.Add("delete from  officedba.CodePublicType" + where);
                //客户细分设置 
                dd.Add("delete from  officedba.CodeCompanyType" + where);
                //编号规则设置  
                dd.Add("delete from  officedba.ItemCodingRule" + where);
                //客户特性设置
                dd.Add("delete from  officedba.TableExtFields" + where);

                //人力资源

                //社会保险比例表
                dd.Add("delete from officedba.InsuSocial" + where);

                //个人所得税率表
                dd.Add("delete from officedba.IncomeTaxPercent" + where);
                //月工资报表编制表
                dd.Add("delete from officedba.SalaryReport" + where);
                ////绩效考核相关表
                //考核类型表
                dd.Add("delete from officedba.PerformanceType" + where);
                //考核指标表
                dd.Add("delete from officedba.PerformanceElem" + where);
                //考核模板表
                dd.Add("delete from officedba.PerformanceTemplate" + where);
                //考核模板指标表
                dd.Add("delete from officedba.PerformanceTemplateElem" + where);
                ////供应链设置
                //基础设置

                /*财务管理*/
                /*删除基本设置表*/
                dd.Add("delete from officedba.SummaryType" + where);
                dd.Add("delete from officedba.SummarySetting" + where);
                dd.Add("delete from officedba.AccountSubjects" + where);
                dd.Add("delete from officedba.SubjectsBeginDetails" + where);
                
                dd.Add("delete from officedba.AssistantType" + where);
                dd.Add("delete from officedba.CurrencyTypeSetting" + where);
                dd.Add("delete from officedba.VoucherTemplate" + where);
                dd.Add("delete from officedba.VoucherTemplateDetail" + where);

                ////-往来单位设置
                //往来单位分类代码表
                dd.Add("delete from officedba.CodeCompanyType" + where);
                //银行信息表
                dd.Add("delete from officedba.BankInfo" + where);
                //其他往来单位信息表
                dd.Add("delete from officedba.OtherCorpInfo" + where);
                ////物品设置
                //物品分类代码表
                dd.Add("delete from officedba.CodeProductType" + where);
                //计量单位代码表
                dd.Add("delete from officedba.CodeUnitType" + where);
                //计量单位换算规则表
                dd.Add("delete from officedba.UnitGroup" + where);
                //计量单位换算规则明细表
                dd.Add("delete from officedba.UnitGroupDetail" + where);
                ////物品档案
                //物品档案表
                dd.Add("delete from officedba.ProductInfo" + where);
                //物品特性表
                //dd.Add("delete from officedba.ProductIdentity" + where);
                //物品特性关联表
              //  dd.Add("delete from officedba.ProductIdentityValue" + where);
                //物品售价调整表
                dd.Add("delete from officedba.ProductPriceChange" + where);
                ////////////////慎重
                ////组织机构表officedba.DeptInfo
                dd.Add("delete from  officedba.DeptInfo" + where + "  and DeptNO!='000001'");
                ////岗位表
                dd.Add("delete from officedba.DeptQuarter" + where);
                ////员工信息表
                dd.Add("delete from officedba.EmployeeInfo" + where);
                ////权限
              
                //用户角色关联表
                dd.Add("delete from officedba.UserRole where UserID in (select userID from officedba.UserInfo " + where + " and IsRoot!='1')");
                //用户·
                dd.Add("delete from officedba.UserInfo " + where + " and IsRoot!='1'");

                /*库存管理*/
                /*删除仓库档案表*/
                dd.Add("delete from officedba.StorageInfo" + where);
                dd.Add("delete from officedba.StorageInitail" + where);
                dd.Add("delete from officedba.StorageInitailDetail" + where);
                dd.Add("delete from officedba.StorageProduct" + where);
                //原因设置
                dd.Add("delete from officedba.CodeReasonType" + where);
                //费用设置
                dd.Add("delete from officedba.CodeFeeType" + where);
                //批次设置
                dd.Add("delete from officedba.BatchRule" + where);
                //审批流程
                dd.Add("delete from officedba.Flow" + where);
                dd.Add("delete from officedba.FlowInstance" + where);
                dd.Add("delete from officedba.FlowStepActor" + where);
                dd.Add("delete from officedba.FlowTaskHistory" + where);
                dd.Add("delete from officedba.FlowTaskList" + where);
                //预警设置
                dd.Add("delete from officedba.RemindSet" + where);
              
            }

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                foreach (string str in dd)
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
            return true;

        }
        #endregion
    }
}