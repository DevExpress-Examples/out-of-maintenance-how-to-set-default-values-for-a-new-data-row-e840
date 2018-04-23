Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace WindowsApplication1
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(635, 286)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1 })
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			Me.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(635, 286)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
		End Sub

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Public Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Function CreateTable() As DataTable
			Dim table = New DataTable("Test")
			table.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
			table.Columns.Add(New DataColumn("Name", System.Type.GetType("System.String")))
			table.Columns.Add(New DataColumn("PocketMoney", System.Type.GetType("System.Double")))
			table.Columns.Add(New DataColumn("Taxed", System.Type.GetType("System.Boolean")))
			table.Columns.Add(New DataColumn("Date", System.Type.GetType("System.DateTime")))
			table.Rows.Add(New Object() { 0, "Ann", 1.15, Nothing, DateTime.Now })
			table.Rows.Add(New Object() { 1, "Bill", 0.99, True, DateTime.Now })
			table.Rows.Add(New Object() { 2, "Dick", 3.5, False, DateTime.Now })
			table.Rows.Add(New Object() { 3, "Edward", 1.0, True, DateTime.Now })
			table.AcceptChanges()

			Return table
		End Function

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim table = CreateTable()
			Dim view = New DataView(table)
			AddHandler view.ListChanged, AddressOf view_ListChanged
			gridControl1.DataSource = view
			gridView1.Columns("ID").OptionsColumn.ReadOnly = True
		End Sub
		Private lastCount As Integer = -1
		Private Sub view_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
			Dim view = TryCast(sender, DataView)
			Dim nextID = view.Count - 1
			If e.ListChangedType = ListChangedType.ItemAdded AndAlso view.Count <> lastCount Then
				view(e.NewIndex)("ID") = nextID
				view(e.NewIndex)("Name") = String.Format("New Name {0}", nextID)
				view(e.NewIndex)("PocketMoney") = nextID * 0.5
				view(e.NewIndex)("Taxed") = True
				view(e.NewIndex)("Date") = DateTime.Now
			End If
			lastCount = view.Count
		End Sub
	End Class
End Namespace
