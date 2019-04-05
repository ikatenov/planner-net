Module GlobalFuncs

    Public Function GetStartOfWeek() As Date
        Dim curDate As Date = DateTime.Today
        Dim dayOfWeek As Integer = CInt(curDate.DayOfWeek)
        If dayOfWeek = 0 Then dayOfWeek = 7
        Return curDate.AddDays(-dayOfWeek + 1)
    End Function

    Public Function GetLocalDayOfWeek(ByVal dayOfWeek As DayOfWeek) As String
        ' There is also a way to get the local name of the day of week
        ' using the platform's System.Globalization.CultureInfo:
        ' https://stackoverflow.com/questions/12993459/vb-net-getting-weekday-name

        Select Case dayOfWeek
            Case DayOfWeek.Monday
                Return "Maandag"
            Case DayOfWeek.Tuesday
                Return "Dinsdag"
            Case DayOfWeek.Wednesday
                Return "Woensdag"
            Case DayOfWeek.Thursday
                Return "Donderdag"
            Case DayOfWeek.Friday
                Return "Veijdag"
            Case DayOfWeek.Saturday
                Return "Zaterdag"
            Case DayOfWeek.Sunday
                Return "Zondag"
            Case Else ' just to avoid the compilator waring about all code paths
                Return Nothing
        End Select
    End Function

    Public Function LoadEmployeeList() As String()
        Return {"Chanel Chan", "Yenna Klaassen", "Lieven Mertens", "Marc Van Tendeloo", "Christine Verdonck"}
    End Function

End Module
