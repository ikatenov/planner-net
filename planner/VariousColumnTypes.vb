Imports System.Drawing.Text
Imports TenTec.Windows.iGridLib

Public Class VariousColumnTypes
    Private fDropDownCalendar As New iGDropDownCalendarDateRange
    Private fDropDownList As New iGDropDownList

    Private Sub VariousColumnTypes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With iGrid1
            .BeginUpdate()

            .DefaultCol.Width = 100
            .FocusRect = False

#Region " Two date picker columns "

            With .Cols.Add()
                .Text = "DatePicker" & vbCrLf & "Cell Editor"
                .CellStyle.FormatString = "{0:MM/dd/yyyy}"
                .DefaultCellValue = DateTime.Now
                .CellStyle.CustomEditor = New DateTimePickerCellEditor()
            End With

            With .Cols.Add()
                .Text = "DatePicker" & vbCrLf & "As Drop-Down"
                .CellStyle.FormatString = "{0:MM/dd/yyyy}"
                .DefaultCellValue = DateTime.Now
                .CellStyle.DropDownControl = fDropDownCalendar
                ' Force the grid to use the cell value to display the cell text 
                ' (instead of the drop down control item) and to apply the format 
                ' string to the display text.
                .CellStyle.TypeFlags = iGCellTypeFlags.ComboPreferValue Or iGCellTypeFlags.NoTextEdit
            End With

#End Region

#Region " Drop-down list column"

            ' Define the drop-don list items
            For i = 1 To 10
                fDropDownList.Items.Add(i, $"Item {i}") ' value, text
            Next

            ' Create a column with the attached drop-down list
            With .Cols.Add()
                .Text = "Drop-Down" & vbCrLf & "List"
                .CellStyle.DropDownControl = fDropDownList
                .CellStyle.TypeFlags = iGCellTypeFlags.NoTextEdit  ' comment out this line if arbitrary text is allowed
                .DefaultCellAuxValue = fDropDownList.Items(0)      ' default values for combobox cells are set this way
            End With

#End Region

#Region " Checkbox column "

            With .Cols.Add()
                .Text = "Checkbox" & vbCrLf & "Column"
                .CellStyle.Type = iGCellType.Check
                .CellStyle.SingleClickEdit = iGBool.True                ' change check state on cell click
                .CellStyle.ImageAlign = iGContentAlignment.MiddleCenter ' checkboxes are aligned using this option
            End With

#End Region

#Region " Euro sums column"

            With .Cols.Add()
                .Text = "Euro" & vbCrLf & "Sums"
                .CellStyle.TextAlign = iGContentAlignment.TopRight
                .CellStyle.FormatString = "{0:€#,##.00}"
                .DefaultCellValue = 123210
            End With

#End Region

            .DefaultRow.Height = 23
            .Rows.Count = 100

            .EndUpdate()
        End With
    End Sub
End Class
