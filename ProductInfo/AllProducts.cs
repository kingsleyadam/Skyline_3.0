using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInfo
{
    public class AllProducts
    {
        private int _categoryID;
        private string _searchString;
        private string _searchField;
        private string _sortExpression;
        private int _pageNum;
        private int _itemsPerPage;
        private int _totalProducts;
        private int _totalPages;
        private int _topNum;
        private string _connectionString;

        public AllProducts(string dbConnection)
        {
            CategoryID = 1;
            SearchString = "";
            SearchField = "";
            SortExpression = "";
            TopNum = 0;
            PageNumber = 0;
            ItemsPerPage = 0;
            ConnectionString = dbConnection;
        }

        public AllProducts(int mCategoryID, int mPageNum, int mItemsPerPage, string dbConnection)
        {
            CategoryID = mCategoryID;
            SearchString = "";
            SearchField = "";
            SortExpression = "";
            TopNum = 0;
            PageNumber = mPageNum;
            ItemsPerPage = mItemsPerPage;
            ConnectionString = dbConnection;
        }

        public AllProducts(int mCategoryID, string mSortExpression, int mPageNum, int mItemsPerPage, string dbConnection)
        {
            CategoryID = mCategoryID;
            SearchString = "";
            SearchField = "";
            SortExpression = mSortExpression;
            TopNum = 0;
            PageNumber = mPageNum;
            ItemsPerPage = mItemsPerPage;
            ConnectionString = dbConnection;
        }

        public AllProducts(int mCategoryID, string mSearchString, string mSearchField, string mSortExpression, int mPageNum, int mItemsPerPage, string dbConnection)
        {
            CategoryID = mCategoryID;
            SearchString = mSearchString;
            SearchField = mSearchField;
            SortExpression = mSortExpression;
            TopNum = 0;
            PageNumber = mPageNum;
            ItemsPerPage = mItemsPerPage;
            ConnectionString = dbConnection;
        }

        public AllProducts(int mCategoryID, string mSearchString, string mSearchField, string mSortExpression, int mTopNum, int mPageNum, int mItemsPerPage, string dbConnection)
        {
            CategoryID = mCategoryID;
            SearchString = mSearchString;
            SearchField = mSearchField;
            SortExpression = mSortExpression;
            TopNum = mTopNum;
            PageNumber = mPageNum;
            ItemsPerPage = mItemsPerPage;
            ConnectionString = dbConnection;
        }

        public int CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; }
        }

        public string SearchField
        {
            get { return _searchField; }
            set { _searchField = value; }
        }

        public string SortExpression
        {
            get { return _sortExpression; }
            set { _sortExpression = value; }
        }

        public int PageNumber
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }

        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set { _itemsPerPage = value; }
        }

        public int TotalProducts
        {
            get { return _totalProducts; }
            set { _totalProducts = value; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        public int TopNum
        {
            get { return _topNum; }
            set { _topNum = value; }
        }


        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("idxIndexSearchPaged", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                paramCategoryID.Direction = ParameterDirection.Input;
                SqlParameter paramSearchString = new SqlParameter("@SearchString", SqlDbType.NVarChar, 2000);
                paramSearchString.Direction = ParameterDirection.Input;
                SqlParameter paramSearchField = new SqlParameter("@SearchField", SqlDbType.NVarChar, 50);
                paramSearchField.Direction = ParameterDirection.Input;
                SqlParameter paramSortExpression = new SqlParameter("@SortExpression", SqlDbType.NVarChar, 50);
                paramSortExpression.Direction = ParameterDirection.Input;
                SqlParameter paramTopNum = new SqlParameter("@TopNum", SqlDbType.Int);
                paramTopNum.Direction = ParameterDirection.Input;
                SqlParameter paramPageNum = new SqlParameter("@PageNum", SqlDbType.Int);
                paramPageNum.Direction = ParameterDirection.Input;
                SqlParameter paramPerPage = new SqlParameter("@PerPage", SqlDbType.Int);
                paramPerPage.Direction = ParameterDirection.Input;
                SqlParameter paramTotalProducts = new SqlParameter("@TotalProducts", SqlDbType.Int);
                paramTotalProducts.Direction = ParameterDirection.Output;
                SqlParameter paramTotalPages = new SqlParameter("@TotalPages", SqlDbType.Int);
                paramTotalPages.Direction = ParameterDirection.ReturnValue;

                paramCategoryID.Value = CategoryID;
                paramSearchString.Value = SearchString;
                paramSearchField.Value = SearchField;
                paramSortExpression.Value = SortExpression;
                paramTopNum.Value = TopNum;
                paramPageNum.Value = PageNumber;
                paramPerPage.Value = ItemsPerPage;

                cmd.Parameters.Add(paramCategoryID);
                cmd.Parameters.Add(paramSearchString);
                cmd.Parameters.Add(paramSearchField);
                cmd.Parameters.Add(paramSortExpression);
                cmd.Parameters.Add(paramTopNum);
                cmd.Parameters.Add(paramPageNum);
                cmd.Parameters.Add(paramPerPage);
                cmd.Parameters.Add(paramTotalProducts);
                cmd.Parameters.Add(paramTotalPages);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Products");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                        TotalProducts = (int)paramTotalProducts.Value;
                        TotalPages = (int)paramTotalPages.Value;
                    }
                    ds.Tables.Add(dt);

                    
                }
            }

            return ds;
        }

        public DataSet GetPagerDataSet(bool grpDataSet, int pageGrp = 0, int grpSize = 5)
        {
            int totalPages = TotalPages;
            DataSet ds = new DataSet("Pager");
            DataTable pagerTable = new DataTable("PagerTable");
            pagerTable.Columns.Add("Page");

            if (grpDataSet)
            {
                int maxPageGrp = (int)Math.Ceiling((decimal)totalPages / (decimal)grpSize);
                if (totalPages < grpSize)
                    grpSize = totalPages;

                int value1, value2;
                if (pageGrp == 1)
                {
                    value1 = 1;
                    value2 = grpSize;
                }
                else
                {
                    value1 = ((pageGrp - 1) * grpSize) + 1;
                    value2 = (value1 + grpSize) - 1;
                }

                if (pageGrp == maxPageGrp)
                    value2 = totalPages;

                for (int i = value1; i <= value2; i++)
                {
                    pagerTable.Rows.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    pagerTable.Rows.Add(i);
                }
            }

            ds.Tables.Add(pagerTable);
            return ds;
        }

        public DataSet GetCategoryDataSet(bool includeAll)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("prdGetCategoriesWNewProducts", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIncludeAll = new SqlParameter("@IncludeAll", SqlDbType.Bit);

                paramIncludeAll.Value = includeAll;

                cmd.Parameters.Add(paramIncludeAll);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Categories");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
    }

}
