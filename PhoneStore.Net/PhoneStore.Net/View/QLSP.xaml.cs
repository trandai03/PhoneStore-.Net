﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using static PhoneStore.Net.DBClass.DBConnect;
using System.Collections.ObjectModel;
using PhoneStore.Net.ViewModel;
using System.Reflection;
using Xamarin.Forms;

namespace PhoneStore.Net.View
{
    public partial class QLSP 
    {
        SANPHAM sp = new SANPHAM();
        string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public QLSP()
        {
            InitializeComponent();
            LoadData();
            Load_CombboBox();
        }
        
        private void LoadData()
        {
            try
            {
                string query = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE, MOTA, HINHSP FROM SANPHAMs where SL >=0";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtSanPham.ItemsSource = dataTable.DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        public void Load_CombboBox()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT DISTINCT  LOAISP  FROM SANPHAMs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string loaisp = reader.GetString(0);
                cbxChon1.Items.Add(loaisp);
            }
            
            _con.Close();
        }
        private void LoadData1()

        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE, MOTA FROM SANPHAMs";
            SQLiteCommand cmd = new SQLiteCommand(query,_con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<SANPHAM> SANPHAMS  = new List<SANPHAM>();
            while (reader.Read())
            {
                SANPHAMS.Add(new SANPHAM()
                {
                    MASP = reader.GetString(0),
                    TENSP = reader.GetString(1),
                    GIA = reader.GetInt32(2),
                    SL = reader.GetInt32(3),
                    LOAISP = reader.GetString(4),
                    SIZE = reader.GetString(5),
                });
            }

            dtSanPham.ItemsSource = SANPHAMS;
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.MaSp.Text = rdma(newProduct);
            newProduct.ShowDialog();
            LoadData();
        }
        bool check(string m, NewProduct p)
        {
            SQLiteConnection con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            con.Open();
            string query = "SELECT COUNT(*) FROM SANPHAMs WHERE MASP = @masp";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@masp", p.MaSp.Text);

            int dem = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();
            if (dem > 0) return true;
            else return false;
        }
        string rdma(NewProduct p)
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "SP" + rand.Next(0, 10000).ToString();
            } while (check(ma, p));
            return ma;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try{
                if (txbSearch.Text != "")
                {
                    DataTable dataTable = DBConnect.DataProvider.Instance.SearchSP(txbSearch.Text);
                    dtSanPham.ItemsSource = dataTable.DefaultView;
                }
                else
                    LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        
        private void dtSanPham_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
                DataRowView selectedSanpham = (DataRowView)dtSanPham.SelectedItem;
                if(selectedSanpham != null)
                {
                
                Detail_product detail_sp = new Detail_product();
                detail_sp.MaSPValue = selectedSanpham["MASP"].ToString();
                detail_sp.TenSPValue = selectedSanpham["TENSP"].ToString();
                detail_sp.GiaSPValue = selectedSanpham["GIA"].ToString();
                detail_sp.MotaValue = selectedSanpham["MOTA"].ToString();
                detail_sp.SLSPValue = selectedSanpham["SL"].ToString();
                detail_sp.LoaiSPValue = selectedSanpham["LOAISP"].ToString();
                detail_sp.SizeValue = selectedSanpham["SIZE"].ToString();
                detail_sp.HinhSPVALUE = selectedSanpham["HINHSP"].ToString();
                detail_sp.UpdateData();
                detail_sp.ShowDialog();
                LoadData();
                }
            else
            {
                
            }
                
        }

        private void cbxChon1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (cbxChon1.SelectedIndex==0)
            {
                LoadData();
                Console.WriteLine(cbxChon1.SelectedItem.ToString());
            }
            else
            {
                DataTable dataTable = DBConnect.DataProvider.Instance.FilterSP(cbxChon1.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last());
                dtSanPham.ItemsSource = dataTable.DefaultView;
            }
        }
    } 
}

