using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
   public class FirstCompanyModel
   {
       #region model
       private string _billStatus;

       public string BillStatus
       {
           get { return _billStatus; }
           set { _billStatus = value; }
       }

       private string _creator;

       public string Creator
       {
           get { return _creator; }
           set { _creator = value; }
       }
       private string _createDate;

       public string CreateDate
       {
           get { return _createDate; }
           set { _createDate = value; }
       }
       private int _id;

       public int Id
       {
           get { return _id; }
           set { _id = value; }
       }
       private string _companyid;
       /// <summary>
       /// 供应商编码同T6
       /// </summary>
       public string Companyid
       {
           get { return _companyid; }
           set { _companyid = value; }
       }
       private string _companycd;

       public string Companycd
       {
           get { return _companycd; }
           set { _companycd = value; }
       }
       private string _neibie;
       /// <summary>
       /// 类别
       /// </summary>
       public string Neibie
       {
           get { return _neibie; }
           set { _neibie = value; }
       }
       private string _jingjixingzhi;

       /// <summary>
       /// 经济性质
       /// </summary>
       public string Jingjixingzhi
       {
           get { return _jingjixingzhi; }
           set { _jingjixingzhi = value; }
       }
       private string _jingyingfangshi;
       /// <summary>
       /// 经营方式
       /// </summary>
       public string Jingyingfangshi
       {
           get { return _jingyingfangshi; }
           set { _jingyingfangshi = value; }
       }
       private string _faren;
       /// <summary>
       /// 法人
       /// </summary>
       public string Faren
       {
           get { return _faren; }
           set { _faren = value; }
       }
       private string _fuzeren;
       /// <summary>
       /// 负责人
       /// </summary>
       public string Fuzeren
       {
           get { return _fuzeren; }
           set { _fuzeren = value; }
       }
       private string _xukezheng;

       /// <summary>
       /// 许可证号码
       /// </summary>
       public string Xukezheng
       {
           get { return _xukezheng; }
           set { _xukezheng = value; }
       }
       private string _xkzenddate;
       /// <summary>
       /// 许可证有效期至
       /// </summary>
       public string Xkzenddate
       {
           get { return _xkzenddate; }
           set { _xkzenddate = value; }
       }
       private string _xkzfazhengjg;
       /// <summary>
       /// 许可证发证机关
       /// </summary>
       public string Xkzfazhengjg
       {
           get { return _xkzfazhengjg; }
           set { _xkzfazhengjg = value; }
       }
       private string _yyzzno;
       /// <summary>
       /// 营业执照号码
       /// </summary>
       public string Yyzzno
       {
           get { return _yyzzno; }
           set { _yyzzno = value; }
       }
       private string _yyzzenddate;
       /// <summary>
       /// 营业执照有效期至
       /// </summary>
       public string Yyzzenddate
       {
           get { return _yyzzenddate; }
           set { _yyzzenddate = value; }
       }
       private string _yyzzfazhengjg;
       /// <summary>
       /// 营业执照发证机关
       /// </summary>
       public string Yyzzfazhengjg
       {
           get { return _yyzzfazhengjg; }
           set { _yyzzfazhengjg = value; }
       }
       private string _jingyingfanwei;
       /// <summary>
       /// 经营方式
       /// </summary>
       public string Jingyingfanwei
       {
           get { return _jingyingfanwei; }
           set { _jingyingfanwei = value; }
       }
       private string _renzhengshuno;
       /// <summary>
       /// 认证书编号
       /// </summary>
       public string Renzhengshuno
       {
           get { return _renzhengshuno; }
           set { _renzhengshuno = value; }
       }
       private string _rzsenddate;
       /// <summary>
       /// 认证书有效期至
       /// </summary>
       public string Rzsenddate
       {
           get { return _rzsenddate; }
           set { _rzsenddate = value; }
       }
       #endregion
       private string _xukefrom;

       public string Xukefrom
       {
           get { return _xukefrom; }
           set { _xukefrom = value; }
       }
       private string _xuketo;

       public string Xuketo
       {
           get { return _xuketo; }
           set { _xuketo = value; }
       }
       private string _yyzzfrom;

       public string Yyzzfrom
       {
           get { return _yyzzfrom; }
           set { _yyzzfrom = value; }
       }
       private string _yyzzto;

       public string Yyzzto
       {
           get { return _yyzzto; }
           set { _yyzzto = value; }
       }
       private string _rzzsfrom;

       public string Rzzsfrom
       {
           get { return _rzzsfrom; }
           set { _rzzsfrom = value; }
       }
       private string _rzzsto;

       public string Rzzsto
       {
           get { return _rzzsto; }
           set { _rzzsto = value; }
       }
        
   }
}
