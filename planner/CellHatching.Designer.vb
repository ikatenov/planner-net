<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CellHatching
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
        Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
        Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.iGrid2 = New TenTec.Windows.iGridLib.iGrid()
        Me.iGrid1 = New TenTec.Windows.iGridLib.iGrid()
        Me.checkBoxShowAbbreviationInPartHour = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.iGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.iGrid2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.iGrid1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(494, 317)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'iGrid2
        '
        Me.iGrid2.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
        Me.iGrid2.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
        Me.iGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.iGrid2.Header.Height = 20
        Me.iGrid2.Location = New System.Drawing.Point(6, 164)
        Me.iGrid2.Margin = New System.Windows.Forms.Padding(6)
        Me.iGrid2.Name = "iGrid2"
        Me.iGrid2.Size = New System.Drawing.Size(482, 147)
        Me.iGrid2.TabIndex = 2
        '
        'iGrid1
        '
        Me.iGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
        Me.iGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
        Me.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.iGrid1.Header.Height = 20
        Me.iGrid1.Location = New System.Drawing.Point(6, 6)
        Me.iGrid1.Margin = New System.Windows.Forms.Padding(6)
        Me.iGrid1.Name = "iGrid1"
        Me.iGrid1.Size = New System.Drawing.Size(482, 146)
        Me.iGrid1.TabIndex = 1
        '
        'checkBoxShowAbbreviationInPartHour
        '
        Me.checkBoxShowAbbreviationInPartHour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.checkBoxShowAbbreviationInPartHour.AutoSize = True
        Me.checkBoxShowAbbreviationInPartHour.Checked = True
        Me.checkBoxShowAbbreviationInPartHour.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkBoxShowAbbreviationInPartHour.Location = New System.Drawing.Point(18, 341)
        Me.checkBoxShowAbbreviationInPartHour.Name = "checkBoxShowAbbreviationInPartHour"
        Me.checkBoxShowAbbreviationInPartHour.Size = New System.Drawing.Size(174, 17)
        Me.checkBoxShowAbbreviationInPartHour.TabIndex = 2
        Me.checkBoxShowAbbreviationInPartHour.Text = "Show Abbreviation in Part Hour"
        Me.checkBoxShowAbbreviationInPartHour.UseVisualStyleBackColor = True
        '
        'CellHatching
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 370)
        Me.Controls.Add(Me.checkBoxShowAbbreviationInPartHour)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CellHatching"
        Me.Text = "Cell Hatching"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.iGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
    Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents iGrid2 As TenTec.Windows.iGridLib.iGrid
    Friend WithEvents iGrid1 As TenTec.Windows.iGridLib.iGrid
    Friend WithEvents checkBoxShowAbbreviationInPartHour As CheckBox
End Class
