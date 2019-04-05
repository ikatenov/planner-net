Imports TenTec.Windows.iGridLib

Public Enum CellTimePart
    Start15Min
    Start30Min
    Start45Min
    End15Min
    End30Min
    End45Min
    FullHour
End Enum

Public Class SchedulePainter

    Private ReadOnly TIME_EDGE_PEN As Pen = Pens.DarkSlateGray
    Private ReadOnly CELL_TIME_BRUSH As Brush = Brushes.Black
    Private ReadOnly ACCEPTANCE_PENDING_BRUSH As SolidBrush = New SolidBrush(Color.Red)

    Private Class CellAuxInfo
        Public DeptID As Integer
        Public TimePart As CellTimePart
        Public BackgroundBrush As SolidBrush
        Public FillWithColor As Boolean

        Public Sub New(ByVal deptID As Integer, ByVal timePart As CellTimePart, ByVal backgroundBrush As SolidBrush, ByVal fillWithColor As Boolean)
            Me.DeptID = deptID
            Me.TimePart = timePart
            Me.BackgroundBrush = backgroundBrush
            Me.FillWithColor = fillWithColor
        End Sub
    End Class

    Private Class DeptInfo
        Public Name As String
        Public Abbreviation As String
        Public Brush As SolidBrush
    End Class

    Private fDefaultGrid As iGrid
    Private fCellTimeStyle As New iGCellStyle
    Private fCellTimeStrFmt As New StringFormat
    Private fCellTimeFont As Font
    Private fDicDepts As New Dictionary(Of Integer, DeptInfo)

    Public Property ShowAbbreviationInPartHour As Boolean = True

    Public Sub New()
        With fCellTimeStyle
            .TextAlign = iGContentAlignment.MiddleCenter
            .CustomDrawFlags = iGCustomDrawFlags.Foreground Or iGCustomDrawFlags.Background
        End With

        fCellTimeFont = New Font("Tahoma", 9, FontStyle.Bold)

        With fCellTimeStrFmt
            .Alignment = StringAlignment.Center
            .LineAlignment = StringAlignment.Center
            .FormatFlags = StringFormatFlags.NoWrap
        End With
    End Sub

    Public Sub Attach(ByVal grid As iGrid)
        If fDefaultGrid Is Nothing Then
            fDefaultGrid = grid
        End If

        AddHandler grid.CustomDrawCellBackground, AddressOf iGrid_CustomDrawCellBackground
        AddHandler grid.CustomDrawCellForeground, AddressOf iGrid_CustomDrawCellForeground
    End Sub

    Public Sub DefineDepartment(ByVal ID As Integer, ByVal name As String, ByVal abbreviation As String, ByVal color As Color)
        Dim di As New DeptInfo With {
            .Name = name,
            .Abbreviation = abbreviation,
            .Brush = New SolidBrush(color)
            }
        fDicDepts.Add(ID, di)
    End Sub

    Public Sub AddSchedule(ByVal rowIndex As Integer, ByVal startColIndex As Integer, ByVal endColIndex As Integer,
                           ByVal deptID As Integer, ByVal workAccepted As Boolean, ByVal acceptancePending As Boolean,
                           ByVal startTimePart As CellTimePart, ByVal endTimePart As CellTimePart,
                           Optional ByVal grid As iGrid = Nothing)

        Dim di As DeptInfo = fDicDepts(deptID)

        Dim brush As SolidBrush
        Dim fillWithColor As Boolean

        If (Not workAccepted) And (acceptancePending) Then
            brush = ACCEPTANCE_PENDING_BRUSH
            fillWithColor = True
        Else
            brush = di.Brush
            fillWithColor = workAccepted
        End If

        If grid Is Nothing Then
            grid = fDefaultGrid
        End If

        With grid

            .BeginUpdate()

            With .Cells(rowIndex, startColIndex)
                .Style = fCellTimeStyle
                .AuxValue = New CellAuxInfo(deptID, startTimePart, brush, fillWithColor)
            End With

            For iCol As Integer = startColIndex + 1 To endColIndex - 1

                With .Cells(rowIndex, iCol)
                    .Style = fCellTimeStyle
                    .AuxValue = New CellAuxInfo(deptID, CellTimePart.FullHour, brush, fillWithColor)
                End With

            Next

            With .Cells(rowIndex, endColIndex)
                .Style = fCellTimeStyle
                .AuxValue = New CellAuxInfo(deptID, endTimePart, brush, fillWithColor)
            End With

            .EndUpdate()

        End With

    End Sub

    Private Sub iGrid_CustomDrawCellBackground(sender As Object, e As iGCustomDrawCellEventArgs)
        Dim sourceCell As iGCell
        sourceCell = DirectCast(sender, iGrid).Cells(e.RowIndex, e.ColIndex)

        Dim cti As CellAuxInfo
        cti = DirectCast(sourceCell.AuxValue, CellAuxInfo)

        If cti Is Nothing Then
            Return
        End If

        'TODO: Use antialiasing for drawing?
        'Dim saveSmoothingMode As Drawing2D.SmoothingMode = e.Graphics.SmoothingMode
        'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        If cti.FillWithColor Then

            Dim brush As SolidBrush = cti.BackgroundBrush

            Select Case cti.TimePart

                Case CellTimePart.FullHour

                    e.Graphics.FillRectangle(brush, e.Bounds)

                Case CellTimePart.Start15Min

                    e.Graphics.FillPolygon(brush, New Point() _
                        {New Point(e.Bounds.X, e.Bounds.Bottom), New Point(e.Bounds.Right, e.Bounds.Y), New Point(e.Bounds.Right, e.Bounds.Bottom)})

                Case CellTimePart.Start45Min

                    e.Graphics.FillPolygon(brush, New Point() _
                        {New Point(e.Bounds.X, e.Bounds.Y), New Point(e.Bounds.Right, e.Bounds.Y), New Point(e.Bounds.Right, e.Bounds.Bottom)})

                Case CellTimePart.End15Min

                    e.Graphics.FillPolygon(brush, New Point() _
                        {New Point(e.Bounds.X, e.Bounds.Y), New Point(e.Bounds.Right - 1, e.Bounds.Y), New Point(e.Bounds.X, e.Bounds.Bottom - 1)})

                Case CellTimePart.End45Min

                    e.Graphics.FillPolygon(brush, New Point() _
                        {New Point(e.Bounds.X, e.Bounds.Y - 1), New Point(e.Bounds.Right + 1, e.Bounds.Bottom), New Point(e.Bounds.X, e.Bounds.Bottom)})

                Case CellTimePart.Start30Min

                    Dim x As Integer = e.Bounds.X + e.Bounds.Width \ 2
                    e.Graphics.FillRectangle(brush, New Rectangle(e.Bounds.X + e.Bounds.Width \ 2, e.Bounds.Y, e.Bounds.Right - x, e.Bounds.Height))

                Case CellTimePart.End30Min

                    e.Graphics.FillRectangle(brush, New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width \ 2, e.Bounds.Height))

            End Select


        End If


        Select Case cti.TimePart

            Case CellTimePart.FullHour

                ' Nothing to draw

            Case CellTimePart.Start15Min

                e.Graphics.DrawLine(TIME_EDGE_PEN, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right - 1, e.Bounds.Y)

            Case CellTimePart.Start45Min

                e.Graphics.DrawLine(TIME_EDGE_PEN, e.Bounds.X, e.Bounds.Y, e.Bounds.Right, e.Bounds.Bottom)

            Case CellTimePart.End15Min

                e.Graphics.DrawLine(TIME_EDGE_PEN, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right - 1, e.Bounds.Y)

            Case CellTimePart.End45Min

                e.Graphics.DrawLine(TIME_EDGE_PEN, e.Bounds.X, e.Bounds.Y, e.Bounds.Right, e.Bounds.Bottom)

            Case CellTimePart.Start30Min, CellTimePart.End30Min

                Dim x As Integer = e.Bounds.X + e.Bounds.Width \ 2
                e.Graphics.DrawLine(TIME_EDGE_PEN, x, e.Bounds.Top, x, e.Bounds.Bottom)

        End Select

        'e.Graphics.SmoothingMode = saveSmoothingMode

    End Sub

    Private Sub iGrid_CustomDrawCellForeground(sender As Object, e As iGCustomDrawCellEventArgs)
        Dim sourceCell As iGCell
        sourceCell = DirectCast(sender, iGrid).Cells(e.RowIndex, e.ColIndex)

        Dim cti As CellAuxInfo
        cti = DirectCast(sourceCell.AuxValue, CellAuxInfo)

        If cti Is Nothing Then
            Return
        End If

        If (cti.TimePart = CellTimePart.FullHour) OrElse (ShowAbbreviationInPartHour) Then
            e.Graphics.DrawString(fDicDepts(cti.DeptID).Abbreviation, fCellTimeFont, CELL_TIME_BRUSH, e.Bounds, fCellTimeStrFmt)
        End If
    End Sub

End Class
