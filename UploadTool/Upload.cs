using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CommonHelper;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace UploadTool
{
    public partial class Upload : Form
    {
        private delegate void AsyncDelegate(int toProgress);//代理
        private int _progress = 0;
        private UserModel _userModel;
        private IList<AccountItem> _lstAccounts;
        private ResultAccountModel _resultAccountModel;
        private ResultCategoryModel _resultCategoryModel;

        public Upload()
        {
            InitializeComponent();
        }

        public Upload(UserModel userModel)
            : this()
        {
            _userModel = userModel;

            if (GetAccounts())
            {
                //GetCategorys(_resultAccountModel.data.items[0].id);
            }
        }

        private bool GetAccounts()
        {
            bool isSucceed = false;

            string strAccountLink = "http://apitest.weijuyi.com/rest?method=public.accounts.get&token=" + _userModel.session_id;

            string strAccount = CommonHelper.HttpHelper.GetResponse(strAccountLink);

            try
            {
                //strAccount = "{ 'status': { 'code': 0, 'message': '获取成功'},'data': {'items': [{ 'id': '1', 'name': '测试'},{ 'id': '2','name': '郑某人' }],'count': 2}}";
                _resultAccountModel = JsonConvert.DeserializeObject<ResultAccountModel>(strAccount);

                isSucceed = _resultAccountModel.status.code == "0";
            }
            catch (Exception ex)
            {
                
            }
            if (isSucceed)
            {
                _lstAccounts = _resultAccountModel.data.items;

                this.rlbAccount.Items.Clear();
                this.rlbAccount.Items.AddRange(_lstAccounts.Select(b => b.name).ToArray());

                this.rlbAccount.SelectedIndex = 0;
            }

            return isSucceed;
        }

        private void GetCategorys(string aaid)
        {
            string strCategoryLink = string.Format("http://apitest.weijuyi.com/rest?method=public.shop.categories.query&token={0}&aaid={1}", _userModel.session_id, aaid);
            string strCategory = CommonHelper.HttpHelper.GetResponse(strCategoryLink);

            try
            {
                _resultCategoryModel = JsonConvert.DeserializeObject<ResultCategoryModel>(strCategory);

                string str = _resultCategoryModel.status.code;
            }
            catch (Exception ex)
            {
                return;
            }

            if (_resultCategoryModel.status.code == "0")
            {
                tvCategory.Nodes.Clear();

                foreach (var item in _resultCategoryModel.data)
                {
                    TreeNode node = new TreeNode()
                    {
                        Name = item.sid,
                        Text = "目录"
                    };

                    foreach (var category in item.data)
                    {
                        TreeNode categoryNode = new TreeNode() { Name = category.id, Text = category.name };

                        foreach (var detail in category.data)
                        {
                            categoryNode.Nodes.Add(new TreeNode() { Name = detail.sid + "|" +detail.leaf, Text = detail.name });
                        }

                        node.Nodes.Add(categoryNode);
                    }

                    tvCategory.Nodes.Add(node);
                }
            }

            //CategoryModel model = new CategoryModel();
            //model.data = new List<CategoryModel>();
            //model.id = "1";
            //model.name = "Frankie";

            //model.data.Add(new CategoryModel() { id = "1.1", name = "frankie1" });

            //TreeNode node = new TreeNode();
            //node.Text = model.name;
            //foreach (CategoryModel item in model.data)
            //{
            //    node.Nodes.Add(new TreeNode() { Text = item.name });
            //}

            //CategoryModel model1 = new CategoryModel();
            //model1.data = new List<CategoryModel>();
            //model1.id = "1";
            //model1.name = "Frankie";

            //model1.data.Add(new CategoryModel() { id = "1.1", name = "frankie1" });

            //TreeNode node1 = new TreeNode();
            //node1.Text = model.name;
            //foreach (CategoryModel item in model1.data)
            //{
            //    node1.Nodes.Add(new TreeNode() { Text = item.name });
            //}

            //tvCategory.Nodes.Add(node);
            //tvCategory.Nodes.Add(node1);

            ////tvCategory.TransparentBrush = new SolidBrush(this.BackColor);
            //tvCategory.BorderStyle = BorderStyle.None;
            tvCategory.ExpandAll();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dlgSelectFile = new OpenFileDialog();
            dlgSelectFile.Filter = "CSV文件|*.csv";
            if (dlgSelectFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFilePath.Text = dlgSelectFile.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            InvokeProgressBar(80);

            this.lblProgress.Text = "匹配中";
            DataTable dt = new DataTable();

            string errorMsg = string.Empty;
            IList<string> lstCsv = FileHelper.OpenCSVFile(this.txtFilePath.Text.Trim(), ref errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                IList<string> lstModifyCsv = new List<string>();
                IList<string[]> lstContent = new List<string[]>();

                if (lstCsv != null && lstCsv.Count > 3)
                {
                    for (int i = 0; i < lstCsv.Count; i++)
                    {
                        if (i < 3)
                        {
                            lstModifyCsv.Add(lstCsv[i]);
                        }
                        else
                        {
                            string[] arr = Regex.Split(lstCsv[i], "\\t");

                            string[] arrPic = arr[28].Split(';');
                            string strPics = string.Empty;
                            foreach (string pic in arrPic)
                            {
                                string[] arrPicDetail = pic.Split('|');

                                if (arrPicDetail.Length == 2)
                                {
                                    string strPicPath = arrPicDetail[1];
                                    // 上传图片

                                    strPics += arrPicDetail[0] + "|" + "修改后的图片" + ";";
                                }
                                else
                                {
                                    strPics += pic;
                                }
                            }

                            arr[28] = strPics;

                            string strContent = string.Empty;
                            //重新整合详细内容
                            foreach (string item in arr)
                            {
                                strContent += item + "\\t";
                            }

                            strContent.Substring(0, strContent.Length - 2);
                            lstModifyCsv.Add(strContent);

                        }
                    }
                }

                UploadFile(this.txtFilePath.Text.Trim());

                TreeNode node = this.tvCategory.SelectedNode;
                AccountItem account = _resultAccountModel.data.items[this.rlbAccount.SelectedIndex];
                if (node == null)
                {
                    MessageBox.Show("请选择分类");
                }
                Console.WriteLine(node.Name);
                Console.WriteLine(account.id);
            }
            else
            {
                MessageBox.Show(errorMsg);
            }
        }

        private void UploadFile(string filePath)
        {
            //FileHelper.Upload_Request("http://localhost:52063/About.aspx", filePath, string.Empty);
            //FileHelper.UploadFileEx(filePath, "http://localhost:52063/About.aspx", string.Empty, string.Empty, null, null);

        }

        private void InvokeProgressBar(int toProgress)
        {
            Thread thread = new Thread(() => ShowProgressBar(toProgress));
            thread.IsBackground = true;
            thread.Start();
        }

        private void ShowProgressBar(int toProgress)
        {
            AsyncDelegate ad = new AsyncDelegate((b) =>
            {
                for (int i = _progress; i < b; i++)
                {
                    Thread.Sleep(50);
                    pbUpload.Value++;

                    _progress = pbUpload.Value;
                }

            });
            this.Invoke(ad, toProgress);
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
            {
                this.btnUpload.Enabled = false;
            }
            else
            {
                this.btnUpload.Enabled = true;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 窗体移动
        private Point mPoint = new Point();

        private void Upload_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Upload_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }
        #endregion

        private void rlbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCategorys(_resultAccountModel.data.items[rlbAccount.SelectedIndex].id);
        }
    }
}
