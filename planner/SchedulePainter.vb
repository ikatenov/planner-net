Imports TenTec.Windows.iGridLib
Imports System.Drawing.Drawing2D ' hatch brushes

Public Enum CellTimePart
    Start15Min
    Start30Min
    Start45Min
    End15Min
    End30Min
    End45Min
    FullInterval
End Enum

Public Class SchedulePainter

    Private ReadOnly CELL_BACK_COLOR As Color = Color.White
    Private ReadOnly ACCEPTANCE_PENDING_COLOR As Color = Color.Red
    Private ReadOnly NOT_ACCEPTED_COLOR As Color = Color.LightSlateGray

    Private Const HATCH_HALF As HatchStyle = HatchStyle.NarrowVertical
    Private Const HATCH_UPWARD As HatchStyle = HatchStyle.WideUpwardDiagonal
    Private Const HATCH_DOWNWARD As HatchStyle = HatchStyle.WideDownwardDiagonal

    Private ReadOnly ACCEPTANCE_PENDING_BRUSH_SOLID_COLOR As SolidBrush = New SolidBrush(ACCEPTANCE_PENDING_COLOR)
    Private ReadOnly ACCEPTANCE_PENDING_BRUSH_HATCH_HALF As HatchBrush = New HatchBrush(HATCH_HALF, ACCEPTANCE_PENDING_COLOR, CELL_BACK_COLOR)
    Private ReadOnly ACCEPTANCE_PENDING_BRUSH_HATCH_UPWARD As HatchBrush = New HatchBrush(HATCH_UPWARD, ACCEPTANCE_PENDING_COLOR, CELL_BACK_COLOR)
    Private ReadOnly ACCEPTANCE_PENDING_BRUSH_HATCH_DOWNWARD As HatchBrush = New HatchBrush(HATCH_DOWNWARD, ACCEPTANCE_PENDING_COLOR, CELL_BACK_COLOR)

    Private ReadOnly NOT_ACCEPTED_BRUSH_HATCH_HALF As HatchBrush = New HatchBrush(HATCH_HALF, NOT_ACCEPTED_COLOR, CELL_BACK_COLOR)
    Private ReadOnly NOT_ACCEPTED_BRUSH_HATCH_UPWARD As HatchBrush = New HatchBrush(HATCH_UPWARD, NOT_ACCEPTED_COLOR, CELL_BACK_COLOR)
    Private ReadOnly NOT_ACCEPTED_BRUSH_HATCH_DOWNWARD As HatchBrush = New HatchBrush(HATCH_DOWNWARD, NOT_ACCEPTED_COLOR, CELL_BACK_COLOR)

    Private Class CellAuxInfo
        Public DeptID As Integer
        Public TimePart As CellTimePart
        Public DeptLetterBrush As Brush
        Public BackgroundBrush As Brush

        Public Sub New(ByVal deptID As Integer, ByVal timePart As CellTimePart, ByVal deptLetterBrush As Brush, ByVal backgroundBrush As Brush)
            Me.DeptID = deptID
            Me.TimePart = timePart
            Me.DeptLetterBrush = deptLetterBrush
            Me.BackgroundBrush = backgroundBrush
        End Sub
    End Class

    Private Class DeptInfo
        Public Name As String
        Public Abbreviation As String
        Public Color As Color
        Public BrushSolidColor As SolidBrush
        Public BrushHatchHalf As HatchBrush
        Public BrushHatchUpward As HatchBrush
        Public BrushHatchDownward As HatchBrush
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
            .Color = color,
            .BrushSolidColor = New SolidBrush(color),
            .BrushHatchHalf = New HatchBrush(HATCH_HALF, color, CELL_BACK_COLOR),
            .BrushHatchUpward = New HatchBrush(HATCH_UPWARD, color, CELL_BACK_COLOR),
            .BrushHatchDownward = New HatchBrush(HATCH_DOWNWARD, color, CELL_BACK_COLOR)
            }
        fDicDepts.Add(ID, di)
    End Sub

    Public Sub AddSchedule(ByVal rowIndex As Integer, ByVal startColIndex As Integer, ByVal endColIndex As Integer,
                           ByVal deptID As Integer, ByVal workAccepted As Boolean, ByVal acceptancePending As Boolean,
                           ByVal startTimePart As CellTimePart, ByVal endTimePart As CellTimePart,
                           Optional ByVal grid As iGrid = Nothing)

        Dim di As DeptInfo = fDicDepts(deptID)

        If grid Is Nothing Then
            grid = fDefaultGrid
        End If

        With grid

            .BeginUpdate()

            With .Cells(rowIndex, startColIndex)
                .Style = fCellTimeStyle
                .AuxValue = New CellAuxInfo(deptID, startTimePart, GetDeptLetterBrush(startTimePart, workAccepted, acceptancePending, di),
                    GetTimePartBrush(startTimePart, workAccepted, acceptancePending, di))
            End With

            For iCol As Integer = startColIndex + 1 To endColIndex - 1

                With .Cells(rowIndex, iCol)
                    .Style = fCellTimeStyle
                    .AuxValue = New CellAuxInfo(deptID, CellTimePart.FullInterval, GetDeptLetterBrush(CellTimePart.FullInterval, workAccepted, acceptancePending, di),
                        GetTimePartBrush(CellTimePart.FullInterval, workAccepted, acceptancePending, di))
                End With

            Next

            With .Cells(rowIndex, endColIndex)
                .Style = fCellTimeStyle
                .AuxValue = New CellAuxInfo(deptID, endTimePart, GetDeptLetterBrush(endTimePart, workAccepted, acceptancePending, di),
                    GetTimePartBrush(endTimePart, workAccepted, acceptancePending, di))
            End With

            .EndUpdate()

        End With

    End Sub

    Private Function GetDeptLetterBrush(ByVal timePart As CellTimePart, ByVal workAccepted As Boolean, ByVal acceptancePending As Boolean, ByVal di As DeptInfo) As Brush
        If (di.Color = Color.Black) AndAlso (timePart = CellTimePart.FullInterval) AndAlso (acceptancePending OrElse workAccepted) Then
            Return Brushes.White
        Else
            Return Brushes.Black
        End If
    End Function

    Private Function GetTimePartBrush(ByVal timePart As CellTimePart, ByVal workAccepted As Boolean, ByVal acceptancePending As Boolean, ByVal di As DeptInfo) As Brush

        Select Case timePart

            Case CellTimePart.FullInterval

                If acceptancePending Then
                    Return ACCEPTANCE_PENDING_BRUSH_SOLID_COLOR
                Else
                    If workAccepted Then
                        Return di.BrushSolidColor
                    Else
                        Return Nothing
                    End If
                End If

            Case CellTimePart.Start15Min, CellTimePart.End15Min

                If acceptancePending Then
                    Return ACCEPTANCE_PENDING_BRUSH_HATCH_UPWARD
                Else
                    If workAccepted Then
                        Return di.BrushHatchUpward
                    Else
                        Return NOT_ACCEPTED_BRUSH_HATCH_UPWARD
                    End If
                End If

            Case CellTimePart.Start45Min, CellTimePart.End45Min

                If acceptancePending Then
                    Return ACCEPTANCE_PENDING_BRUSH_HATCH_DOWNWARD
                Else
                    If workAccepted Then
                        Return di.BrushHatchDownward
                    Else
                        Return NOT_ACCEPTED_BRUSH_HATCH_DOWNWARD
                    End If
                End If

            Case CellTimePart.Start30Min, CellTimePart.End30Min

                If acceptancePending Then
                    Return ACCEPTANCE_PENDING_BRUSH_HATCH_HALF
                Else
                    If workAccepted Then
                        Return di.BrushHatchHalf
                    Else
                        Return NOT_ACCEPTED_BRUSH_HATCH_HALF
                    End If
                End If

        End Select

        Return Nothing ' To ignore warning BC42105 "Function doesn't return a value on all code paths"

    End Function

    Private Sub iGrid_CustomDrawCellBackground(sender As Object, e As iGCustomDrawCellEventArgs)
        Dim sourceCell As iGCell
        sourceCell = DirectCast(sender, iGrid).Cells(e.RowIndex, e.ColIndex)

        Dim cai As CellAuxInfo
        cai = DirectCast(sourceCell.AuxValue, CellAuxInfo)

        If cai Is Nothing Then
            Return
        End If

        'TODO: Use antialiasing for drawing?
        'Dim saveSmoothingMode As Drawing2D.SmoothingMode = e.Graphics.SmoothingMode
        'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim brush As Brush = cai.BackgroundBrush

        If brush IsNot Nothing Then

            Dim rcFill As Rectangle

            Select Case cai.TimePart

                Case CellTimePart.Start30Min

                    Dim halfWidth As Integer = e.Bounds.Width \ 2
                    rcFill.X = e.Bounds.X + halfWidth
                    rcFill.Width = e.Bounds.Width - halfWidth
                    rcFill.Y = e.Bounds.Y
                    rcFill.Height = e.Bounds.Height

                Case CellTimePart.End30Min

                    Dim halfWidth As Integer = e.Bounds.Width \ 2
                    rcFill.X = e.Bounds.X
                    rcFill.Width = e.Bounds.Width - halfWidth
                    rcFill.Y = e.Bounds.Y
                    rcFill.Height = e.Bounds.Height

                Case Else

                    rcFill = e.Bounds

            End Select

            e.Graphics.FillRectangle(brush, rcFill)

        End If

        'e.Graphics.SmoothingMode = saveSmoothingMode

    End Sub

    Private Sub iGrid_CustomDrawCellForeground(sender As Object, e As iGCustomDrawCellEventArgs)
        Dim sourceCell As iGCell
        sourceCell = DirectCast(sender, iGrid).Cells(e.RowIndex, e.ColIndex)

        Dim cai As CellAuxInfo
        cai = DirectCast(sourceCell.AuxValue, CellAuxInfo)

        If cai Is Nothing Then
            Return
        End If

        If (cai.TimePart = CellTimePart.FullInterval) OrElse (ShowAbbreviationInPartHour) Then
            e.Graphics.DrawString(fDicDepts(cai.DeptID).Abbreviation, fCellTimeFont, cai.DeptLetterBrush, e.Bounds, fCellTimeStrFmt)
        End If
    End Sub

End Class
