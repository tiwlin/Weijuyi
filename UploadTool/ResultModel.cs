using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadTool
{
    public class ResultLoginModel
    {
        public ResultStatus status { get; set; }

        public ResultLoginData data { get; set; }
    }

    public class ResultAccountModel
    {
        public ResultStatus status { get; set; }

        public AccountData data { get; set; }
    }

    public class ResultCategoryModel
    {
        public ResultStatus status { get; set; }

        public IList<CategoryResult> data { get; set; } 
    }

    public class ResultStatus
    {
        public string code { get; set; }

        public string message { get; set; }
    }

    public class ResultLoginData
    {
        public string token { get; set; }
    }

    public class AccountData
    {
        public IList<AccountItem> items { get; set; }

        public string count { get; set; }
    }

    public class AccountItem
    {
        public string id { get; set; }

        public string name { get; set; }
    }

    public class CategoryModel
    {
        public string id { get; set; }

        public string name { get; set; }

        public IList<CategoryModel> data { get; set; }
    }

    public class CategoryResult
    {
        public string sid { get; set; }

        public string p_name { get; set; }

        public int is_brand { get; set; }

        public int type { get; set; }

        public IList<CategoryData> data { get; set; }
    }

    public class CategoryData
    {
        public string id { get; set; }

        public string name { get; set; }

        public IList<CategoryItem> data { get; set; }
    }

    public class CategoryItem
    {
        public string sid { get; set; }

        public string name { get; set; }

        public int leaf { get; set; }

        public string spell { get; set; }
    }
}
