Imports TenTec.Windows.iGridLib

Public Class LimitDragSelect
    Private Sub LimitDragSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With iGrid1
            .BeginUpdate()

            .SelectionMode = iGSelectionMode.MultiExtended

            .Cols.Count = 15
            .Rows.Count = 100

            .EndUpdate()
        End With
    End Sub

    ' Set the cursor clip rectangle when the mouse button is pressed in a cell
    Private Sub iGrid1_CellMouseDown(sender As Object, e As iGCellMouseDownEventArgs) Handles iGrid1.CellMouseDown
        Dim rcCellBounds As Rectangle = DirectCast(sender, iGrid).RectangleToScreen(e.Bounds)

        Dim rcScreenBounds As Rectangle = Screen.FromControl(Me).Bounds

        Cursor.Clip = New Rectangle(rcScreenBounds.X, rcCellBounds.Y, rcScreenBounds.Width, rcCellBounds.Height - 1)
    End Sub

    ' Clear the cursor clip rectangle when the mous ebutton is released.
    ' Pay attention to the fact that we do this in iGrid's MouseUp event but not CellMouseUp:
    ' CellMouseUp is not triggered if the mouse pointer was released outside of the bounds of
    ' the cell in which the mouse button was pressed.
    Private Sub iGrid1_MouseUp(sender As Object, e As MouseEventArgs) Handles iGrid1.MouseUp
        Cursor.Clip = Rectangle.Empty
    End Sub
End Class