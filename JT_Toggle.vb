Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Class JT_Toggle
    Inherits CheckBox

    ' Propiedades Personalizadas
    Private _onBackColor As Color = Color.MediumOrchid
    Private _onToggleColor As Color = Color.WhiteSmoke
    Private _offBackColor As Color = Color.Gray
    Private _offToggleColor As Color = Color.Gainsboro
    Private _isSolidStyle As Boolean = True

    ' Propiedad Text invalidada
    <Browsable(False)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(value As String)
            ' No hacer nada para invalidar la propiedad
        End Set
    End Property

    ' Propiedad OnBackColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Color de fondo del control cuando está activado (On).")>
    Public Property OnBackColor As Color
        Get
            Return _onBackColor
        End Get
        Set(value As Color)
            _onBackColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad OnToggleColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Color del 'toggle' cuando está activado (On).")>
    Public Property OnToggleColor As Color
        Get
            Return _onToggleColor
        End Get
        Set(value As Color)
            _onToggleColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad OffBackColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Color de fondo del control cuando está desactivado (Off).")>
    Public Property OffBackColor As Color
        Get
            Return _offBackColor
        End Get
        Set(value As Color)
            _offBackColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad OffToggleColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Color del 'toggle' cuando está desactivado (Off).")>
    Public Property OffToggleColor As Color
        Get
            Return _offToggleColor
        End Get
        Set(value As Color)
            _offToggleColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad IsSolidStyle
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Determina si el fondo del control se rellena completamente o solo se dibuja el contorno.")>
    <DefaultValue(True)>
    Public Property IsSolidStyle As Boolean
        Get
            Return _isSolidStyle
        End Get
        Set(value As Boolean)
            _isSolidStyle = value
            Me.Invalidate()
        End Set
    End Property

    ' Constructor
    Public Sub New()
        Me.MinimumSize = New Size(45, 22)
        Me.AutoSize = False ' Establece AutoSize en False por defecto
    End Sub

    ' Métodos
    Private Function GetFigurePath() As GraphicsPath
        Dim arcSize As Integer = Me.Height - 1
        Dim leftArc As New Rectangle(0, 0, arcSize, arcSize)
        Dim rightArc As New Rectangle(Me.Width - arcSize - 2, 0, arcSize, arcSize)

        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(leftArc, 90, 180)
        path.AddArc(rightArc, 270, 180)
        path.CloseFigure()

        Return path
    End Function

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        Dim toggleSize As Integer = Me.Height - 5
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        pevent.Graphics.Clear(Me.Parent.BackColor)
        If Me.Checked Then
            If _isSolidStyle Then
                pevent.Graphics.FillPath(New SolidBrush(OnBackColor), GetFigurePath)
            Else
                pevent.Graphics.DrawPath(New Pen(_onBackColor), GetFigurePath())
            End If
            pevent.Graphics.FillEllipse(New SolidBrush(OnToggleColor), New Rectangle(Me.Width - Me.Height + 1, 2, toggleSize, toggleSize))
        Else
            If _isSolidStyle Then
                pevent.Graphics.FillPath(New SolidBrush(OffBackColor), GetFigurePath)
            Else
                pevent.Graphics.DrawPath(New Pen(_offBackColor), GetFigurePath())
            End If
            pevent.Graphics.FillEllipse(New SolidBrush(OffToggleColor), New Rectangle(2, 2, toggleSize, toggleSize))
        End If
    End Sub
End Class
