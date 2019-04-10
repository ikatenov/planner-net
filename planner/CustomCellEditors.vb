Imports System.Drawing.Text
Imports TenTec.Windows.iGridLib

Public Class DateTimePickerCellEditor
    Inherits iGCellEditorBase

    ' The edit control itself
    Private fDateTimePicker As New DateTimePicker() With {
            .Format = DateTimePickerFormat.Custom, .CustomFormat = "MM/dd/yyyy"}

#Region "Obligatory members"

    Public Overrides ReadOnly Property EditControl As Control
        Get
            Return fDateTimePicker
        End Get
    End Property

    Public Overrides Sub SetBounds(textBounds As Rectangle, suggestedBounds As Rectangle)
        fDateTimePicker.Bounds = textBounds
    End Sub

#End Region

#Region "Editing cell value 'as is' in its native format"

    Public Overrides ReadOnly Property PassValueAsText As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Property Value As Object
        Get
            Return fDateTimePicker.Value
        End Get
        Set(value As Object)
            fDateTimePicker.Value = CDate(value)
        End Set
    End Property

#End Region

End Class

Public Class iGDropDownCalendarDateRange
    Implements IiGDropDownControl

#Region "Fields"

    ''' <summary>
    ''' The calendar control itself.
    ''' </summary>
    Private fCalendar As MonthCalendar

    ''' <summary>
    ''' The grid for which the calendar is shown.
    ''' </summary>
    Private fGrid As iGrid

#End Region

#Region "IiGDropDownControl Members"

    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Returns a drop-down control item that corresponds to the specified cell value.
    ''' </summary>
    Function GetItemByValue(ByVal value As Object, ByVal firstByOrder As Boolean) As Object Implements IiGDropDownControl.GetItemByValue
        Return value
    End Function

    ''' <summary>
    ''' Returns a drop-down control item that corresponds to the specified text.
    ''' This method is invoked when the user enters a text to a cell.
    ''' </summary>
    Function GetItemByText(ByVal text As String) As Object Implements IiGDropDownControl.GetItemByText
        Try
            Return DateTime.Parse(text)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the image index of the specified drop-down control item.
    ''' </summary>
    Function GetItemImageIndex(ByVal item As Object) As Integer Implements IiGDropDownControl.GetItemImageIndex
        Return -1
    End Function

    ''' <summary>
    ''' Gets the cell value of the specified drop-down control item.
    ''' </summary>
    Public Function GetItemValue(ByVal item As Object) As Object Implements IiGDropDownControl.GetItemValue
        Return item
    End Function

    ''' <summary>
    ''' Returns the control to be shown in the drop-down form.
    ''' </summary>
    Function GetDropDownControl(ByVal grid As iGrid, ByVal font As Font, ByVal interfaceType As Type) As Control Implements IiGDropDownControl.GetDropDownControl
        fGrid = grid
        Calendar.Font = font
        Calendar.RightToLeft = fGrid.RightToLeft
        Return Calendar
    End Function

    ''' <summary>
    ''' Is raised after the drop-down has been shown. Here it does nothing.
    ''' </summary>
    Sub OnShow() Implements IiGDropDownControl.OnShow
    End Sub

    ''' <summary>
    ''' Is raised when the drop-down is about to be shown. Here it does nothing.
    ''' </summary>
    Public Sub OnShowing() Implements IiGDropDownControl.OnShowing
    End Sub

    ''' <summary>
    ''' Is raised after the drop-down has been hidden. Here it does nothing.
    ''' </summary>
    Sub OnHide() Implements IiGDropDownControl.OnHide
    End Sub

    ''' <summary>
    ''' Gets or sets the selected date in the calendar.
    ''' This property is used before and after editing a cell.
    ''' </summary>
    Property SelectedItem() As Object Implements IiGDropDownControl.SelectedItem
        Get
            Return Calendar.SelectionStart
        End Get
        Set(ByVal value As Object)
            If value Is Nothing Then
                value = DateTime.Today.Date
            End If
            If Not value.GetType Is GetType(DateTime) Then
                Throw New ArgumentException
            End If
            Dim myValue As DateTime = DirectCast(value, DateTime).Date
            Calendar.SetSelectionRange(myValue, myValue)
        End Set
    End Property

    ''' <summary>
    ''' Returns the width of the calendar control.
    ''' It should return -1 to set width of the drop-down control by the column width.
    ''' </summary>
    ReadOnly Property Width() As Integer Implements IiGDropDownControl.Width
        Get
            Return Calendar.SingleMonthSize.Width + 2
        End Get
    End Property

    ''' <summary>
    ''' Returns the height of the calendar control
    ''' </summary>
    ReadOnly Property Height() As Integer Implements IiGDropDownControl.Height
        Get
            Return Calendar.SingleMonthSize.Height + 2
        End Get
    End Property

    ''' <summary>
    ''' This property should return the image list used by the drop-down control.
    ''' The image list will be used by the grid to display the cell image if the 
    ''' cell or column style does not contain an image list.
    ''' </summary>
    ReadOnly Property ImageList() As ImageList Implements IiGDropDownControl.ImageList
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the drop-down form has resizeable border.
    ''' </summary>
    ReadOnly Property Sizeable() As Boolean Implements IiGDropDownControl.Sizeable
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether to save the drop-down control value 
    ''' to the cell after the drop-down form is hidden owing to losing 
    ''' of the focus (mouse was clicked out of the form or the user 
    ''' switched to other application).
    ''' </summary>
    ReadOnly Property CommitOnHide() As Boolean Implements IiGDropDownControl.CommitOnHide
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Determines whether to automatically substitute the entered text
    ''' with the corresponding value from the drop-down list. 
    ''' </summary>
    ReadOnly Property AutoSubstitution() As Boolean Implements IiGDropDownControl.AutoSubstitution
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Determines whether we have the close button in the title bar.
    ''' </summary>
    Public ReadOnly Property CloseButton As Boolean Implements IiGDropDownControl.CloseButton
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Determines the text in the title bar.
    ''' </summary>
    Public ReadOnly Property Text As String Implements IiGDropDownControl.Text
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Determines whether to hide the drop-down if it is opened from a column header.
    ''' </summary>
    Public ReadOnly Property HideColHdrDropDown As Boolean Implements IiGDropDownControl.HideColHdrDropDown
        Get
            Return True
        End Get
    End Property

    Public Sub SetTextRenderingHint(textRenderingHint As TextRenderingHint) Implements IiGDropDownControl.SetTextRenderingHint
        ' Not applicable to the Calendar control
    End Sub

#End Region

#Region "Calendar Functionality"

    ''' <summary>
    ''' Returns the MonthCalendar control used as the drop-down cell editor.
    ''' If the calendar control is not created, creates it on-the-fly.
    ''' </summary>
    Private ReadOnly Property Calendar() As MonthCalendar
        Get
            If fCalendar Is Nothing Then
                fCalendar = New MonthCalendar
                AddHandler fCalendar.DateSelected, AddressOf Me.fCalendar_DateSelected
                AddHandler fCalendar.KeyDown, AddressOf Me.fCalendar_KeyDown
                fCalendar.MaxSelectionCount = 1
                fCalendar.CreateControl() ' Need to create the API handle for proper results returned from Calendar.SingleMonthSize
            End If
            Return fCalendar
        End Get
    End Property

    ''' <summary>
    ''' Saves the value to the cell when a date is selected.
    ''' </summary>
    Private Sub fCalendar_DateSelected(ByVal sender As Object, ByVal e As DateRangeEventArgs)
        fGrid.CommitEditCurCell()
    End Sub

    ''' <summary>
    ''' Saves the value to the cell when the Enter key is pressed and cancels editing 
    ''' when the Esc key is pressed.
    ''' </summary>
    Private Sub fCalendar_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                fGrid.CommitEditCurCell()
            Case Keys.Escape
                fGrid.CancelEditCurCell()
        End Select
    End Sub

    ''' <summary>
    ''' The minimum date which can be selected from the calendar.
    ''' </summary>
    Public Property MinDate() As Date
        Get
            Return Calendar.MinDate.Date
        End Get
        Set(ByVal value As Date)
            Calendar.MinDate = value.Date
        End Set
    End Property

    ''' <summary>
    ''' The maximum date which can be selected from the calendar.
    ''' </summary>
    Public Property MaxDate() As Date
        Get
            Return Calendar.MaxDate.Date
        End Get
        Set(ByVal value As Date)
            Calendar.MaxDate = value.Date
        End Set
    End Property

    ''' <summary>
    ''' Sets the minimum date to the lowest allowed value.
    ''' </summary>
    Public Sub ResetMinDate()
        Calendar.MinDate = #1/1/1753#
    End Sub

    ''' <summary>
    ''' Sets the maximum date to the biggest allowed value.
    ''' </summary>
    Public Sub ResetMaxDate()
        Calendar.MaxDate = #12/31/9998#
    End Sub

#End Region

End Class
