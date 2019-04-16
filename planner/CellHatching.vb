Imports TenTec.Windows.iGridLib

Public Class CellHatching

    Private fSchedulePainter As New SchedulePainter

    Private Sub CellHatching_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid(iGrid1)
        SetupGrid(iGrid2)

        With fSchedulePainter
            .DefineDepartment(1, "Kassa", "K", Color.Aquamarine)
            .DefineDepartment(2, "Butchery", "S", Color.BlanchedAlmond)
            .DefineDepartment(3, "Return", "Rt", Color.Pink)
            .DefineDepartment(9, "Black dept", "B", Color.Black)

            .Attach(iGrid1)
            .Attach(iGrid2)

            .AddSchedule(0, 6, 7, 2, True, False, CellTimePart.FullInterval, CellTimePart.FullInterval)     ' 11:00 - 13:00, accepted
            .AddSchedule(1, 2, 5, 1, True, False, CellTimePart.Start45Min, CellTimePart.End15Min)           ' 07:45 - 10:15, accepted
            .AddSchedule(1, 7, 9, 2, False, False, CellTimePart.Start30Min, CellTimePart.End30Min)          ' 12:30 - 14:30, not accepted
            .AddSchedule(2, 10, 14, 2, False, True, CellTimePart.Start30Min, CellTimePart.End30Min)         ' 15:30 - 19:30, acceptance pending
            .AddSchedule(3, 4, 8, 3, True, False, CellTimePart.Start30Min, CellTimePart.End15Min)           ' 09:30 - 13:15, accepted 
            .AddSchedule(4, 8, 12, 3, True, False, CellTimePart.Start15Min, CellTimePart.End45Min)          ' 13:15 - 17:45, accepted 

            .AddSchedule(0, 8, 12, 2, True, False, CellTimePart.Start45Min, CellTimePart.End15Min, iGrid2)  ' 13:45 - 17:15, accepted
            .AddSchedule(2, 6, 13, 9, True, False, CellTimePart.Start45Min, CellTimePart.End30Min, iGrid2)  ' 11:45 - 18:30, accepted
            .AddSchedule(3, 6, 13, 9, False, False, CellTimePart.Start45Min, CellTimePart.End30Min, iGrid2) ' 11:45 - 18:30, not accepted
            .AddSchedule(4, 1, 7, 1, False, False, CellTimePart.Start30Min, CellTimePart.End45Min, iGrid2)  ' 06:30 - 12:45, not accepted
        End With
    End Sub

    Private Sub SetupGrid(ByVal grid As iGrid)
        With grid
            .BeginUpdate()

            .ReadOnly = True
            .Header.UseXPStyles = False
            .Header.Appearance = iGControlPaintAppearance.StyleFlat
            .Header.Font = New Font("Arial", 7)
            .FocusRect = False

            .SelectionMode = iGSelectionMode.MultiExtended
            .SelCellsBackColor = Color.FromArgb(100, SystemColors.Highlight) ' semi-transparent selection

            .DefaultCol.Width = 18
            .DefaultRow.Height = 18

            .Cols.Count = 16
            For Each col As iGCol In .Cols
                If col.Index > 0 Then
                    col.Text = col.Index + 5
                End If
            Next

            ' Add rows with employees
            Dim employees() As String = LoadEmployeeList()
            .Rows.Count = employees.Length
            For iRow As Integer = 0 To employees.Length - 1
                .Cells(iRow, 0).Value = employees(iRow)
            Next
            .FrozenArea.ColCount = 1
            .Cols(0).AutoWidth()
            .Cols(0).CellStyle.BackColor = SystemColors.Control

            .EndUpdate()
        End With
    End Sub

    Private Sub checkBoxShowAbbreviationInPartHour_CheckedChanged(sender As Object, e As EventArgs) Handles checkBoxShowAbbreviationInPartHour.CheckedChanged
        fSchedulePainter.ShowAbbreviationInPartHour = checkBoxShowAbbreviationInPartHour.Checked
        iGrid1.Invalidate()
        iGrid2.Invalidate()
    End Sub
End Class