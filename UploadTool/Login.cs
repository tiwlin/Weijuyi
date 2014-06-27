using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UploadTool
{
    public partial class Login : Form
    {
        private string _key = "w9s7d_w918@#$80q2!@#s970";
        private byte[] _iv = { 0X00, 0X00, 0X00, 0X00, 0X00, 0X00, 0X00, 0X00 };
        private UserModel _userModel;

        public Login()
        {
            InitializeComponent();
            this.txtAccount.Text = "testtest";
            this.txtPwd.Text = "z1x2c3ok";
        }

        public Login(UserModel userModel) : this()
        {
            _userModel = userModel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                string strHash = GetHashParam();

                string result = UserLogin(strHash);

                CheckLogin(result);
            }
        }

        private bool CheckEmpty()
        {
            bool isPass = true;

            if (string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
            {
                this.lblTips.Text = "请填写用户账号！";
                isPass = false; 
            }

            else if (string.IsNullOrEmpty(this.txtPwd.Text.Trim()))
            {
                this.lblTips.Text = "请填写登陆密码！";
                isPass = false; 
            }

            return isPass;
        }

        private string GetHashParam()
        {
            string strHash = string.Empty;

            try
            {
                //string strAccount = string.Format("username={0}&password={1}", this.txtAccount.Text.Trim(), System.Web.HttpUtility.UrlEncode(this.txtPwd.Text.Trim()));

                //strHash = CommonHelper.EncryptHelper.Encrypt3DES(strAccount, _key, _iv);

                //strHash = System.Web.HttpUtility.UrlEncode(strHash);

                strHash = string.Format("username={0}&password={1}", this.txtAccount.Text.Trim(), CommonHelper.EncryptHelper.Md5(System.Web.HttpUtility.UrlEncode(this.txtPwd.Text.Trim())).ToLower());
            }
            catch (System.Exception ex)
            {

            }

            return strHash;
        }

        private string UserLogin(string strHash)
        {
            string urlLogin = "http://apitest.weijuyi.com/rest?method=public.auth.login&" + strHash;

            string result = CommonHelper.HttpHelper.GetResponse(urlLogin);

            return result;
        }

        private void CheckLogin(string strResult)
        {
            try
            {
                ResultLoginModel result = JsonConvert.DeserializeObject<ResultLoginModel>(strResult);

                if (result.status.code == "0")
                {
                    _userModel.UserName = this.txtAccount.Text.Trim();
                    _userModel.Pwd = this.txtPwd.Text.Trim();
                    _userModel.session_id = result.data.token;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(result.status.message);

                    this.lblTips.Text = "登陆失败";
                }
            }
            catch (Exception ex)
            {
                this.lblTips.Text = "登陆失败";

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        #region 窗体移动
        private Point mPoint = new Point();

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            //this.Close();
        }
        #endregion

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtAccount.Text = "";
            this.txtPwd.Text = "";
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

            switch (this.DialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    e.Cancel = false;
                    break;
                case System.Windows.Forms.DialogResult.Abort:
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }
    }
}