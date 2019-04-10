<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LaadLogboek
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LaadLogboek))
        Me.iGrid1 = New TenTec.Windows.iGridLib.iGrid()
        Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
        Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
        Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.checkBoxTreeLines = New System.Windows.Forms.CheckBox()
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
        Me.iGrid1.Size = New System.Drawing.Size(1110, 307)
        Me.iGrid1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "lock-icon.png")
        Me.ImageList1.Images.SetKeyName(1, "add-1-icon.png")
        '
        'checkBoxTreeLines
        '
        Me.checkBoxTreeLines.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.checkBoxTreeLines.AutoSize = True
        Me.checkBoxTreeLines.Location = New System.Drawing.Point(12, 332)
        Me.checkBoxTreeLines.Name = "checkBoxTreeLines"
        Me.checkBoxTreeLines.Size = New System.Drawing.Size(106, 17)
        Me.checkBoxTreeLines.TabIndex = 1
        Me.checkBoxTreeLines.Text = "Show Tree Lines"
        Me.checkBoxTreeLines.UseVisualStyleBackColor = True
        '
        'LaadLogboek
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1134, 361)
        Me.Controls.Add(Me.checkBoxTreeLines)
        Me.Controls.Add(Me.iGrid1)
        Me.Name = "LaadLogboek"
        Me.Text = "Laad Logboek"
        CType(Me.iGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents iGrid1 As TenTec.Windows.iGridLib.iGrid
    Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
    Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
    Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents checkBoxTreeLines As CheckBox
End Class
