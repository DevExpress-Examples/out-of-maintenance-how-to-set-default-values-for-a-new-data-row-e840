using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication1 {
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form {
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1() {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing ) {
            if ( disposing ) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(635, 286);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 286);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.Run(new Form1());
        }

        private DataTable CreateTable() {
            var table = new DataTable("Test");
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("PocketMoney", System.Type.GetType("System.Double")));
            table.Columns.Add(new DataColumn("Taxed", System.Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("Date", System.Type.GetType("System.DateTime")));
            table.Rows.Add(new object[] { 0, "Ann", 1.15, null, DateTime.Now });
            table.Rows.Add(new object[] { 1, "Bill", 0.99, true, DateTime.Now });
            table.Rows.Add(new object[] { 2, "Dick", 3.5, false, DateTime.Now });
            table.Rows.Add(new object[] { 3, "Edward", 1.0, true, DateTime.Now });
            table.AcceptChanges();

            return table;
        }

        private void Form1_Load(object sender, EventArgs e) {
            var table = CreateTable();
            var view = new DataView(table);
            view.ListChanged += new ListChangedEventHandler(view_ListChanged);
            gridControl1.DataSource = view;
            gridView1.Columns["ID"].OptionsColumn.ReadOnly = true;
        }
        private int lastCount = -1;
        private void view_ListChanged(object sender, ListChangedEventArgs e) {
            var view = sender as DataView;
            var nextID = view.Count - 1;
            if (e.ListChangedType == ListChangedType.ItemAdded && view.Count != lastCount) {
                view[e.NewIndex]["ID"] = nextID;
                view[e.NewIndex]["Name"] = string.Format("New Name {0}", nextID);
                view[e.NewIndex]["PocketMoney"] = nextID * 0.5;
                view[e.NewIndex]["Taxed"] = true;
                view[e.NewIndex]["Date"] = DateTime.Now;
            }
            lastCount = view.Count;
        }
    }
}
