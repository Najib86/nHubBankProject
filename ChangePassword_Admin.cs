﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApp
{
    public partial class ChangePassword_Admin : UserControl
    {
        public ChangePassword_Admin()
        {
            InitializeComponent();
            dateLabel.Text = DateTime.Now.ToLongDateString();
        }
        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            oldPasswordTxt.UseSystemPasswordChar = false;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            oldPasswordTxt.UseSystemPasswordChar = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            newPasswordTxt.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            newPasswordTxt.UseSystemPasswordChar = true;
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            confirmPasswordTxt.UseSystemPasswordChar = false;
        }

        private void pictureBox7_MouseUp(object sender, MouseEventArgs e)
        {
            confirmPasswordTxt.UseSystemPasswordChar = true;
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            myProfilePanel.Controls.Clear();
            myProfilePanel.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void savePasswordBtn_Click(object sender, EventArgs e)
        {
            BankingPEntities5 dbe = new BankingPEntities5();

            if (oldPasswordTxt.Text.Length == 0)
            {
                asterixOld.Visible = true;
                MessageBox.Show("Please enter old password.");
            }
            else if (newPasswordTxt.Text.Length == 0)
            {
                asterixNew.Visible = true;
                MessageBox.Show("Please enter new password.");
            }
            else if (confirmPasswordTxt.Text.Length == 0)
            {

                asterixConfirm.Visible = true;
                MessageBox.Show("Please enter confirm password field.");
            }

            else if (oldPasswordTxt.Text != string.Empty || newPasswordTxt.Text != string.Empty || confirmPasswordTxt.Text != string.Empty)
            {
                var user1 = dbe.Admins.FirstOrDefault(a => a.Password.Equals(oldPasswordTxt.Text));
                if (user1 != null)
                {
                    if (user1.Id.Equals(Convert.ToInt32(CoAdminLogin.instance.getAdminId.Text)) && newPasswordTxt.Text == confirmPasswordTxt.Text)
                    {
                        user1.Password = confirmPasswordTxt.Text;
                        dbe.SaveChanges();

                        ProfileSettingsPage profilepage = new ProfileSettingsPage();
                        addUserControl(profilepage);
                        MessageBox.Show("Password has been changed successfully.");
                    }
                    else
                    {
                        asterixNew.Visible = true;
                        asterixConfirm.Visible = true;
                        MessageBox.Show("The new password and confirm password fields do not match.");
                    }
                }

                else
                {
                    asterixOld.Visible = true;
                    MessageBox.Show("Incorrect password.");
                }
            }
        }
    }
}
