Imports TenTec.Windows.iGridLib

Public Class LaadLogboek
    Private Const WERKNEMER_LOGDE_WEBSITE As String = "Werknemer logde in op de website"
    Private Const WERKNEMER_AANVAARDDE_WERKUREN As String = "Werknemer aanvaardde geplande werkuren"

    Private Sub IconTreeGridSample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With iGrid1
            .BeginUpdate()

            .ImageList = ImageList1
            .ReadOnly = True
            .RowMode = True
            .DefaultRow.Height = 20

            .Cols.Count = 7

            .Cols(0).Text = "Datum logboek"
            .Cols(1).Text = "Type log"
            .Cols(2).Text = "Medewerker"
            .Cols(3).Text = "Aanvang prestatie"
            .Cols(4).Text = "Einde prestatie"
            .Cols(5).Text = "Afdeling"
            .Cols(6).Text = "Onderwerp"

            Const DATE_FORMAT As String = "{0:MM/dd/yyyy HH:mm}"
            .Cols(0).CellStyle.FormatString = DATE_FORMAT
            .Cols(3).CellStyle.FormatString = DATE_FORMAT
            .Cols(4).CellStyle.FormatString = DATE_FORMAT

            .TreeCol = .Cols(0)

            AddRecord(0, New Date(2019, 3, 31, 20, 41, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Liselotte Vergote")
            AddRecord(0, New Date(2019, 3, 31, 19, 18, 0), 1, WERKNEMER_AANVAARDDE_WERKUREN, "Veronique Cineger", New Date(2019, 4, 12, 12, 0, 0), New Date(2019, 4, 12, 15, 30, 0), "Uitpak+magazijn")
            AddRecord(1, New Date(2019, 3, 31, 19, 18, 0), 1, WERKNEMER_AANVAARDDE_WERKUREN, "Veronique Cineger", New Date(2019, 4, 12, 7, 0, 0), New Date(2019, 4, 12, 11, 0, 0), "Uitpak+magazijn")
            AddRecord(1, New Date(2019, 3, 31, 19, 18, 0), 1, WERKNEMER_AANVAARDDE_WERKUREN, "Veronique Cineger", New Date(2019, 4, 10, 11, 30, 0), New Date(2019, 4, 10, 16, 30, 0), "Uitpak+magazijn")
            AddRecord(1, New Date(2019, 3, 31, 19, 18, 0), 1, WERKNEMER_AANVAARDDE_WERKUREN, "Veronique Cineger", New Date(2019, 4, 10, 7, 0, 0), New Date(2019, 4, 10, 11, 0, 0), "Uitpak+magazijn")
            AddRecord(0, New Date(2019, 3, 31, 19, 18, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Veronique Cineger")
            AddRecord(0, New Date(2019, 3, 31, 19, 15, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Veronique Cineger")
            AddRecord(0, New Date(2019, 3, 31, 18, 50, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Gianni Duwayn")
            AddRecord(0, New Date(2019, 3, 31, 15, 9, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Carmen Rotsaert")
            AddRecord(0, New Date(2019, 3, 31, 14, 49, 0), 0, WERKNEMER_LOGDE_WEBSITE, "Ann Gysel")

            .Cols.AutoWidth()

            .EndUpdate()
        End With
    End Sub

    Private Sub AddRecord(ByVal pLevel As Integer, ByVal pDatumLogboek As Date, ByVal pImageIndex As Integer, ByVal pTypeLog As String,
                          ByVal pMedewerker As String, Optional ByVal pAanvangPrestatie? As Date = Nothing, Optional ByVal pEindePrestatie? As Date = Nothing,
                          Optional ByVal pAfdeling As String = Nothing, Optional pOnderwerp As String = Nothing)
        With iGrid1
            .BeginUpdate()

            Dim row As iGRow = .Rows.Add()

            row.Level = pLevel
            If pLevel = 0 Then
                row.TreeButton = iGTreeButtonState.Visible
            Else
                row.TreeButton = iGTreeButtonState.Hidden
            End If

            row.Cells(0).ImageIndex = pImageIndex
            row.Cells(0).Value = pDatumLogboek
            row.Cells(1).Value = pTypeLog
            row.Cells(2).Value = pMedewerker
            If pAanvangPrestatie.HasValue Then
                row.Cells(3).Value = pAanvangPrestatie.Value
            End If
            If pEindePrestatie.HasValue Then
                row.Cells(4).Value = pEindePrestatie.Value
            End If
            row.Cells(5).Value = pAfdeling
            row.Cells(6).Value = pOnderwerp

            .EndUpdate()
        End With
    End Sub

    Private Sub checkBoxTreeLines_CheckedChanged(sender As Object, e As EventArgs) Handles checkBoxTreeLines.CheckedChanged
        iGrid1.TreeLines.Visible = checkBoxTreeLines.Checked
    End Sub
End Class