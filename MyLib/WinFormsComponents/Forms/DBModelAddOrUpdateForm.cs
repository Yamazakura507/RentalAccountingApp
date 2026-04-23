using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsComponents.Forms
{
    internal partial class DBModelAddOrUpdateForm : Form
    {
        private Type modelType;
        private object modelObject = null;

        public DBModelAddOrUpdateForm(Type modelType)
        {
            InitializeComponent();

            this.modelType = modelType;
        }

        public DBModelAddOrUpdateForm(Type modelType, object mdelObject) : this(modelType) 
        {
            this.modelObject = mdelObject;
        }
    }
}
