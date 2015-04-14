/**********************************************
 * 类作用：   InitStorage表数据模板(期初金额录入)
 * 建立人：   肖合明
 * 建立时间： 2009/12/19
 ***********************************************/



namespace XBase.Model.Office.BasicData
{
    public class InitStorageModel
    {
        public InitStorageModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private int? _branchid;
        private int? _storageid;
        private int? _productid;
        private string _batchno;
        private decimal? _initnum;
        private decimal? _initprice;
        private decimal? _totalprice;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InitNum
        {
            set { _initnum = value; }
            get { return _initnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InitPrice
        {
            set { _initprice = value; }
            get { return _initprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        #endregion Model

    }
}

