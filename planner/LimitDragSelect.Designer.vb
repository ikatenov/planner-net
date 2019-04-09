<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LimitDragSelect
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.iGrid1 = New TenTec.Windows.iGridLib.iGrid()
        Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
        Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
        Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
        CType(Me.iGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'iGrid1
        '
        Me.iGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.iGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
        Me.iGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
        Me.iGrid1.Header.Height = 20
        Me.iGrid1.Location = New System.Drawing.Point(12, 12)
        Me.iGrid1.Name = "iGrid1"
        Me.iGrid1.Size = New System.Drawing.Size(776, 426)
        Me.iGrid1.TabIndex = 0
        '
        'LimitDragSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.iGrid1)
        Me.Name = "LimitDragSelect"
        Me.Text = "LimitDragSelect"
        CType(Me.iGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents iGrid1 As TenTec.Windows.iGridLib.iGrid
    Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
    Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
    Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
End Class
