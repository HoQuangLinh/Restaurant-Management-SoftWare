using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyNhaHang
{
    public partial class fAddFood : Form
    {
        public fAddFood()
        {
            InitializeComponent();
            txtFoodID.Text = (FoodDAO.Instance.GetMaxIdFood() + 1).ToString();
            LoadFoodCategory();
        }

        private void buttonClosefAf_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fAddFood_Load(object sender, EventArgs e)
        {

        }
        void LoadFoodCategory()
        {
            List<FoodCategory> categories = FoodCategoryDAO.Instance.GetListFoodCategories();
            cbbFoodCategory.DataSource = categories;
            cbbFoodCategory.DisplayMember = "Tenloai";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (txtFoodName.Text == "" ||
                txtPrice.Text == "" || txtTypeID.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            try
            {

                string tenthucan = txtFoodName.Text;
                int gia = Convert.ToInt32(txtPrice.Text);
                int maloai = Convert.ToInt32(txtTypeID.Text);
                if (FoodDAO.Instance.InsertFood(tenthucan, gia, maloai) == true)
                {
                    MessageBox.Show("Thêm món ăn thành công");
                    txtFoodID.Text = (Convert.ToInt32(txtFoodID.Text) + 1).ToString();
                    txtFoodName.Text = "";
                    txtPrice.Text = "";
                    txtTypeID.Text = "";
                    cbbFoodCategory.Text = "";

                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627: //Mã lỗi này show ra vi phạm khoá chính
                        MessageBox.Show("Mã món ăn đã tồn tại hoặc tên món ăn bị trùng");
                        break;
                    case 547:
                        MessageBox.Show("Mã loại không tồn tại ");
                        break;
                    default:
                        throw;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi định dạng");
            }
            //Khi thêm xong các textbox trở thành rỗng và tăng mã thức ăn lên 1




        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;

            FoodCategory category = cb.SelectedItem as FoodCategory;
            string tenloai = category.Tenloai;
            int maloai = FoodCategoryDAO.Instance.GetIdByNameFoodCategory(tenloai);
            txtTypeID.Text = maloai.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
