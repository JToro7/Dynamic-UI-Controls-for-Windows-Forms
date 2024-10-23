Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Class JT_Button
    Inherits Button

    ' Propiedades Personalizadas
    Private _borderSize As Integer = 0
    Private _radius As Integer = 20
    Private _borderColor As Color = Color.Aquamarine

    ' Propiedad BorderSize
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el grosor del borde del botón.")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            _borderSize = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad Radius
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Establece el radio de las esquinas redondeadas del botón.")>
    Public Property Radius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            _radius = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad BorderColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Especifica el color del borde del botón.")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad BackgroundColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Establece el color de fondo del botón.")>
    Public Property BackgroundColor As Color
        Get
            Return Me.BackColor
        End Get
        Set(value As Color)
            Me.BackColor = value
        End Set
    End Property

    ' Propiedad TextColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el color del texto del botón.")>
    Public Property TextColor As Color
        Get
            Return Me.ForeColor
        End Get
        Set(value As Color)
            Me.ForeColor = value
        End Set
    End Property

    ' Constructor
    Public Sub New()
        ' Establecer propiedades predeterminadas
        Me.FlatAppearance.BorderSize = 0
        Me.Size = New Size(100, 25)
        Me.FlatStyle = FlatStyle.Flat
        Me.BackColor = Color.Indigo
        Me.ForeColor = Color.White
        AddHandler Me.Resize, AddressOf Button_Resize
    End Sub
    Private Sub Button_Resize(sender As Object, e As EventArgs)
        If _radius > Me.Height Then
            _radius = Me.Height
        End If
    End Sub


    ' Método para obtener la ruta de la figura con esquinas redondeadas
    Private Function GetFigurePath(r As RectangleF, radius As Decimal) As GraphicsPath
        Dim Path As New GraphicsPath
        Path.StartFigure()
        Path.AddArc(r.X, r.Y, radius, radius, 180, 90)
        Path.AddArc(r.Width - radius, r.Y, radius, radius, 270, 90)
        Path.AddArc(r.Width - radius, r.Height - radius, radius, radius, 0, 90)
        Path.AddArc(r.X, r.Height - radius, radius, radius, 90, 90)
        Path.CloseFigure()
        Return Path
    End Function

    ' Sobrescribir OnPaint para dibujar el botón con esquinas redondeadas y borde
    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim rectSurface As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        Dim rectBorder As RectangleF = New RectangleF(1, 1, Me.Width - 0.8F, Me.Height - 1)

        If Radius > 2 Then
            ' Botón redondeado
            Using pathSurface As GraphicsPath = GetFigurePath(rectSurface, Radius),
                  pathBorder As GraphicsPath = GetFigurePath(rectBorder, Radius - 1.0F),
                  penSurface As New Pen(Me.Parent.BackColor, 2),
                  penBorder As New Pen(BorderColor, BorderSize)

                penBorder.Alignment = PenAlignment.Inset

                ' Superficie del botón
                Me.Region = New Region(pathSurface)

                ' Dibujar la superficie para un resultado HD
                pevent.Graphics.DrawPath(penSurface, pathSurface)

                ' Borde del botón
                If BorderSize >= 1 Then
                    pevent.Graphics.DrawPath(penBorder, pathBorder)
                End If
            End Using
        Else
            ' Botón normal
            Me.Region = New Region(rectSurface)
            If BorderSize >= 1 Then
                Using penBorder As New Pen(BorderColor, BorderSize)
                    penBorder.Alignment = PenAlignment.Inset
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Me.Width - 1, Me.Height - 1)
                End Using
            End If
        End If
    End Sub

    ' Evento para manejar cambios en el color de fondo del contenedor
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        AddHandler Me.Parent.BackColorChanged, AddressOf Container_BackColorChanged
    End Sub

    Private Sub Container_BackColorChanged(sender As Object, e As EventArgs)
        If Me.DesignMode Then
            Me.Invalidate()
        End If
    End Sub
End Class
