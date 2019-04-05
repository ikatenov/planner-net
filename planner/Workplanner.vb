Imports TenTec.Windows.iGridLib

Public Class Workplanner
    Private Sub Workplanner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' General settings for the grid
        SetupGrid()
        ' Display data for the first shop in list
        comboBoxWinkel.SelectedIndex = 0
    End Sub

    Private Sub comboBoxWinkel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxWinkel.SelectedIndexChanged
        PopulateGrid()
    End Sub

#Region " Routines to setup/populate grid"

    Private Sub SetupGrid()
        ' Use flat look to minimze space occupied by day/hour columns
        iGrid1.Header.UseXPStyles = False
        iGrid1.Header.Appearance = iGControlPaintAppearance.StyleFlat

        ' Header action options
        iGrid1.Header.AllowPress = False

        iGrid1.DefaultCol.AllowMoving = False
        iGrid1.DefaultCol.AllowSizing = False

        ' Two rows of column headers for days and work hours
        iGrid1.Header.Rows.Count = 2
    End Sub

    Private Sub PopulateGrid()
        iGrid1.BeginUpdate()

        ResetGrid()

        SetupGridColumnsAndHeader()

        AddEmployees()

        AddEmployeesData()

        iGrid1.EndUpdate()
    End Sub

    Private Sub ResetGrid()
        iGrid1.Cols.Count = 0
        iGrid1.Rows.Count = 0
    End Sub

    Private Sub SetupGridColumnsAndHeader()
        ' Find the start day of the current week
        Dim startOfWeek As Date = GetStartOfWeek()

        ' Add the Persoon column
        Dim colEmployee As iGCol = iGrid1.Cols.Add()
        colEmployee.Text = "Persoon"
        iGrid1.Header.Cells(0, 0).SpanRows = 2
        iGrid1.Header.Cells(0, 0).TextAlign = iGContentAlignment.MiddleLeft

        ' Add column headers for days
        Dim daySeparatorCellStyle As New iGCellStyle()
        daySeparatorCellStyle.BackColor = Color.Black

        For iDay As Integer = 0 To 6
            Dim dateToDisplay As Date = startOfWeek.AddDays(iDay)

            ' Find the min and max work hours
            Dim minWorkHour As Integer
            Dim maxWorkHour As Integer
            GetWorkHours(dateToDisplay, minWorkHour, maxWorkHour)

            ' Add the hours of this day
            Dim startCol As Integer = iGrid1.Cols.Count
            For iHour = minWorkHour To maxWorkHour
                Dim colHour As iGCol = iGrid1.Cols.Add()
                colHour.Text = iHour.ToString()
                colHour.Width = 21
            Next

            Dim colDaySeparator As iGCol = iGrid1.Cols.Add()
            colDaySeparator.Width = 3
            colDaySeparator.CellStyle = daySeparatorCellStyle

            Dim colHdrDay As iGColHdr = iGrid1.Header.Cells(1, startCol)
            colHdrDay.SpanCols = maxWorkHour - minWorkHour + 2
            colHdrDay.Value = GetLocalDayOfWeek(dateToDisplay.DayOfWeek) & " " & dateToDisplay.ToString("d/M/yyyy")
            colHdrDay.TextAlign = iGContentAlignment.MiddleCenter
        Next
    End Sub

    Private Sub AddEmployees()
        ' Add rows with employees
        Dim employees() As String = LoadEmployeeList()
        For Each emloyee As String In employees
            Dim rowEmployee As iGRow = iGrid1.Rows.Add()
            rowEmployee.Cells(0).Value = emloyee
        Next

        ' Freeze the first column with employees
        iGrid1.FrozenArea.ColCount = 1
        iGrid1.Cols(0).AllowSizing = True
        iGrid1.Cols(0).AutoWidth()
        iGrid1.Cols(0).AllowSizing = False
    End Sub

    Private Sub AddEmployeesData()
        With iGrid1.Cells(0, 2)
            .SpanCols = 5
            .Value = "S"
            .BackColor = Color.Pink
        End With

        With iGrid1.Cells(0, 8)
            .SpanCols = 4
            .Value = "S"
            .BackColor = Color.Pink
        End With

        With iGrid1.Cells(1, 5)
            .SpanCols = 4
            .Value = "C"
            .BackColor = Color.Coral
        End With

        With iGrid1.Cells(1, 10)
            .SpanCols = 5
            .Value = "C"
            .BackColor = Color.Coral
        End With
    End Sub

    Private Sub GetWorkHours(ByVal aDate As Date, ByRef minWorkHour As Integer, ByRef maxWorkHour As Integer)
        minWorkHour = CInt(IIf(aDate.DayOfWeek = DayOfWeek.Sunday, 7, 6))
        maxWorkHour = CInt(IIf(aDate.DayOfWeek = DayOfWeek.Sunday, 17, 19))
    End Sub

#End Region

#Region " Grid event handlers "

    Private Sub iGrid1_ColHdrStartDrag(sender As Object, e As iGColHdrStartDragEventArgs) Handles iGrid1.ColHdrStartDrag
        ' Disable any column reordering
        e.DoDefault = False
    End Sub

#End Region

End Class
