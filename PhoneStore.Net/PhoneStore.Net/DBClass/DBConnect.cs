﻿using System.Data.SQLite;
using System.Data;
using PhoneStore.Net.Model;
using PhoneStore.Net.Controller;
using System;
using System.Windows;

namespace PhoneStore.Net.DBClass
{
    internal class DBConnect
    {
        public class DataProvider 
        {
            string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
            SQLiteConnection _con = new SQLiteConnection();
            private static DataProvider instance;
            public static DataProvider Instance
            {
                get
                {
                    if (instance == null)
                        instance = new DataProvider();
                    return DataProvider.instance;
                }
                private set { DataProvider.instance = value; }
            }
            
            public DataTable Sql_select(string sql_querry)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql_querry, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }

            public NGUOIDUNG checkUser(string username, string password)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"SELECT * FROM NGUOIDUNGs WHERE USERNAME = $username AND PASS = $password";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                var reader = command.ExecuteReader();

                if (reader != null)
                {
                    NGUOIDUNG u = new NGUOIDUNG();
                    while (reader.Read())
                    {
                        try
                        {
                            u.MAND = reader.GetString(0);
                        }catch (Exception ex) { }
                        try
                        {
                            u.TENND = reader.GetString(1);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.NGSINH = DateTime.Parse(reader.GetString(2));
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.GIOITINH = reader.GetString(3);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.SDT = reader.GetString(4);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.DIACHI = reader.GetString(5);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.USERNAME = reader.GetString(6);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.PASS = reader.GetString(7);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.AVA = Utility.ConvertToBitmapImage(reader.GetString(10));
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.MAIL = reader.GetString(11);
                        }
                        catch (Exception ex) { }
                    }
                    _con.Close();
                    return u;
                }
                else
                {
                    return null;
                }
            }

            public bool updateUser(NGUOIDUNG newU)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"UPDATE NGUOIDUNGs SET TENND = $ten , GIOITINH = $gioitinh , NGSINH = $ngsinh , SDT = $sdt , DIACHI = $diachi , AVA = $ava , MAIL = $mail WHERE MAND = $mand";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$ten", newU.TENND);
                command.Parameters.AddWithValue("$gioitinh", newU.GIOITINH);
                command.Parameters.AddWithValue("$ngsinh", newU.NGSINH.ToString());
                command.Parameters.AddWithValue("$sdt", newU.SDT);
                command.Parameters.AddWithValue("$diachi", newU.DIACHI);
                command.Parameters.AddWithValue("$ava", Utility.ConvertToString(newU.AVA, "png"));
                command.Parameters.AddWithValue("$mail", newU.MAIL);
                command.Parameters.AddWithValue("$mand", newU.MAND);

                var _ = command.ExecuteNonQuery();
                _con.Close();

                return true;
            }

            public bool changeUserLogin(NGUOIDUNG newU)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"UPDATE NGUOIDUNGs SET USERNAME = $username , PASS = $pass WHERE MAND = $mand";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$username", newU.USERNAME);
                command.Parameters.AddWithValue("$pass", newU.PASS);
                command.Parameters.AddWithValue("$mand", newU.MAND);

                var _ = command.ExecuteNonQuery();

                return true;
            }
        }

        public static string StartupPath { get; }
        
        
        
        
        public DBConnect() { }
    }
}
