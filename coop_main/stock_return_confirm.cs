﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace coop_main
{
    public partial class stock_return_confirm : Form
    {
        public stock_return_confirm()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
