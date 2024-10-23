Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("_TextChanged")>
Public Class JT_TextBox
    Private _borderColor As Color = Color.Indigo
    Private _borderSize As Integer = 2
    Private _StyleUnderLine As Boolean = False
    Private _IsFocused As Boolean = False
    Private _borderColorFocus As Color = Color.DarkOrchid
    Private _radius As Integer = 0
    Private _PlaceHolderColor As Color = Color.DarkGray
    Private _TextPlaceHolder As String = ""
    Private _IsPlaceholder As Boolean = False
    Private _IsPassword As Boolean = False

    Public Sub New()
        InitializeComponent()

        ' Suscripción a los eventos del TextBox interno
        AddHandler TextBox1.TextChanged, AddressOf textBox1_TextChanged
        AddHandler TextBox1.Click, AddressOf textBox1_Click
        AddHandler TextBox1.MouseEnter, AddressOf textBox1_MouseEnter
        AddHandler TextBox1.MouseLeave, AddressOf textBox1_MouseLeave
        AddHandler TextBox1.DoubleClick, AddressOf textBox1_DoubleClick
        AddHandler TextBox1.KeyDown, AddressOf textBox1_KeyDown
        AddHandler TextBox1.KeyPress, AddressOf textBox1_KeyPress
        AddHandler TextBox1.KeyUp, AddressOf textBox1_KeyUp
        AddHandler TextBox1.MouseClick, AddressOf textBox1_MouseClick
        AddHandler TextBox1.MouseDown, AddressOf textBox1_MouseDown
        AddHandler TextBox1.MouseMove, AddressOf textBox1_MouseMove
        AddHandler TextBox1.MouseUp, AddressOf textBox1_MouseUp
        AddHandler TextBox1.GotFocus, AddressOf textBox1_GotFocus
        AddHandler TextBox1.LostFocus, AddressOf textBox1_LostFocus
        AddHandler TextBox1.Enter, AddressOf TextBox1_Enter
        AddHandler TextBox1.Leave, AddressOf TextBox1_Leave
    End Sub

    ' Eventos
    <Description("Evento que se dispara cuando el texto del cuadro de texto cambia.")>
    Public Event _TextChanged As EventHandler

#Region "Propiedades"
    ' Propiedad BorderColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el color del borde del cuadro de texto.")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad BorderSize
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Establece el grosor del borde del cuadro de texto.")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            _borderSize = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad StyleUnderLine
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Determina si el borde del cuadro de texto se dibuja como una línea en la parte inferior.")>
    Public Property StyleUnderLine As Boolean
        Get
            Return _StyleUnderLine
        End Get
        Set(value As Boolean)
            _StyleUnderLine = value
            Me.Invalidate()
        End Set
    End Property

    ' Propiedad IsPassword
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Determina si el cuadro de texto actúa como un campo de contraseña.")>
    Public Property IsPassword As Boolean
        Get
            Return _IsPassword
        End Get
        Set(value As Boolean)
            _IsPassword = value
            TextBox1.UseSystemPasswordChar = value
        End Set
    End Property

    ' Propiedad IsMultiline
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Establece si el cuadro de texto permite múltiples líneas de texto.")>
    Public Property IsMultiline As Boolean
        Get
            Return TextBox1.Multiline
        End Get
        Set(value As Boolean)
            TextBox1.Multiline = value
        End Set
    End Property

    ' Propiedad CharacterCasing
    <Browsable(True)>
    <Category("Comportamiento - Dynamic UI Controls")>
    <Description("Establece si el texto se convierte a mayúsculas, minúsculas o se mantiene como está.")>
    Public Property CharacterCasing As CharacterCasing
        Get
            Return TextBox1.CharacterCasing
        End Get
        Set(value As CharacterCasing)
            TextBox1.CharacterCasing = value

        End Set
    End Property

    ' Propiedad MyText
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Texto contenido en el cuadro de texto.")>
    Public Property MyText As String
        Get
            If _IsPlaceholder Then
                Return ""
            Else
                Return TextBox1.Text
            End If
        End Get
        Set(value As String)
            TextBox1.Text = value
            SetPlaceHolder()
        End Set
    End Property

    ' Propiedad ForeColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el color del texto en el cuadro de texto.")>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
        End Set
    End Property

    ' Propiedad BackColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Establece el color de fondo del cuadro de texto.")>
    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
        End Set
    End Property

    ' Propiedad Font
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define la fuente del texto en el cuadro de texto.")>
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            TextBox1.Font = value
            If Me.DesignMode Then
                UpdateControlHeight()
            End If
        End Set
    End Property

    ' Propiedad BorderColorFocus
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el color del borde cuando el cuadro de texto está enfocado.")>
    Public Property BorderColorFocus As Color
        Get
            Return _borderColorFocus
        End Get
        Set(value As Color)
            _borderColorFocus = value
        End Set
    End Property

    ' Propiedad Radius
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el radio de las esquinas redondeadas del cuadro de texto.")>
    Public Property Radius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _radius = value
                Me.Invalidate()
            End If
        End Set
    End Property

    ' Propiedad IsPlaceholder
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Determina si el cuadro de texto está mostrando el placeholder.")>
    Public Property IsPlaceholder As Boolean
        Get
            Return _IsPlaceholder
        End Get
        Set(value As Boolean)
            _IsPlaceholder = value
        End Set
    End Property

    ' Propiedad PlaceHolderColor
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el color del texto del placeholder.")>
    Public Property PlaceHolderColor As Color
        Get
            Return _PlaceHolderColor
        End Get
        Set(value As Color)
            _PlaceHolderColor = value
            If _IsPlaceholder Then
                TextBox1.ForeColor = value
            End If
        End Set
    End Property

    ' Propiedad TextPlaceHolder
    <Browsable(True)>
    <Category("Apariencia - Dynamic UI Controls")>
    <Description("Define el texto que se mostrará como placeholder en el cuadro de texto.")>
    Public Property TextPlaceHolder As String
        Get
            Return _TextPlaceHolder
        End Get
        Set(value As String)
            _TextPlaceHolder = value
            TextBox1.Text = ""
            SetPlaceHolder()
        End Set
    End Property
#End Region

#Region "Métodos Privados"
    ' Establece el placeholder si el cuadro de texto está vacío
    Private Sub SetPlaceHolder()
        If String.IsNullOrWhiteSpace(TextBox1.Text) And _TextPlaceHolder <> "" Then
            _IsPlaceholder = True
            TextBox1.Text = _TextPlaceHolder
            TextBox1.ForeColor = _PlaceHolderColor
            If _IsPassword Then
                TextBox1.UseSystemPasswordChar = False
            End If
        End If
    End Sub

    ' Elimina el placeholder cuando el usuario empieza a escribir
    Private Sub RemovePlaceHolder()
        If _IsPlaceholder And TextPlaceHolder <> "" Then
            _IsPlaceholder = False
            TextBox1.Text = ""
            TextBox1.ForeColor = Me.ForeColor
            If _IsPassword Then
                TextBox1.UseSystemPasswordChar = True
            End If
        End If
    End Sub

    ' Ajusta la altura del control
    Private Sub UpdateControlHeight()
        If TextBox1.Multiline = False Then
            Dim txtHeight As Integer = TextRenderer.MeasureText("Text", Me.Font).Height + 1
            TextBox1.Multiline = True
            TextBox1.MinimumSize = New Size(0, txtHeight)
            TextBox1.Multiline = False
        End If
        Me.Height = TextBox1.Height + Me.Padding.Top + Me.Padding.Bottom
    End Sub

    ' Obtiene la ruta de la figura para el borde redondeado
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
#End Region

#Region "Métodos Sobrescritos"
    ' Sobrescribir OnPaint para dibujar el borde personalizado
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim graph As Graphics = e.Graphics

        ' Dibujar borde redondeado o normal según el radio
        If _radius > 1 Then
            Dim rectBorderSmooth As Rectangle = Me.ClientRectangle
            Dim rectBorder As Rectangle = Rectangle.Inflate(rectBorderSmooth, -BorderSize, -BorderSize)
            Dim smoothSize As Integer = If(BorderSize > 0, BorderSize, 1)

            Using pathBorderSmooth As GraphicsPath = GetFigurePath(rectBorderSmooth, _radius),
                  pathBorder As GraphicsPath = GetFigurePath(rectBorder, _radius - BorderSize),
                  penBorderSmooth As New Pen(Me.Parent.BackColor, smoothSize),
                  penBorder As New Pen(If(_IsFocused, _borderColorFocus, _borderColor), BorderSize)

                Me.Region = New Region(pathBorderSmooth)
                graph.SmoothingMode = SmoothingMode.AntiAlias
                penBorder.Alignment = PenAlignment.Center

                If _StyleUnderLine Then
                    graph.DrawPath(penBorderSmooth, pathBorderSmooth)
                    graph.SmoothingMode = SmoothingMode.None
                    graph.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                Else
                    graph.DrawPath(penBorderSmooth, pathBorderSmooth)
                    graph.DrawPath(penBorder, pathBorder)
                End If
            End Using
        Else
            Using penBorder As New Pen(If(_IsFocused, _borderColorFocus, _borderColor), BorderSize)
                Me.Region = New Region(Me.ClientRectangle)
                penBorder.Alignment = PenAlignment.Inset

                If _StyleUnderLine Then
                    graph.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                Else
                    graph.DrawRectangle(penBorder, 0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                End If
            End Using
        End If
    End Sub

    ' Sobrescribir OnResize para ajustar la altura del control
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If Me.DesignMode Then
            UpdateControlHeight()
        End If
    End Sub

    ' Sobrescribir OnLoad para ajustar la altura del control al cargarse
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        UpdateControlHeight()
    End Sub
#End Region

#Region "Eventos reenviados"
    <Description("Se genera cuando el texto del cuadro de texto cambia.")>
    Private Sub textBox1_TextChanged(sender As Object, e As EventArgs)
        RaiseEvent _TextChanged(Me, e)
    End Sub

    <Description("Se genera cuando se hace clic en el cuadro de texto.")>
    Private Sub textBox1_Click(sender As Object, e As EventArgs)
        Me.OnClick(e)
    End Sub

    <Description("Se genera cuando el mouse entra en el área del cuadro de texto.")>
    Private Sub textBox1_MouseEnter(sender As Object, e As EventArgs)
        Me.OnMouseEnter(e)
    End Sub

    <Description("Se genera cuando el mouse sale del área del cuadro de texto.")>
    Private Sub textBox1_MouseLeave(sender As Object, e As EventArgs)
        Me.OnMouseLeave(e)
    End Sub

    <Description("Se genera cuando se hace doble clic en el cuadro de texto.")>
    Private Sub textBox1_DoubleClick(sender As Object, e As EventArgs)
        Me.OnDoubleClick(e)
    End Sub

    <Description("Se genera cuando se presiona una tecla mientras el cuadro de texto está enfocado.")>
    Private Sub textBox1_KeyDown(sender As Object, e As KeyEventArgs)
        Me.OnKeyDown(e)
    End Sub

    <Description("Se genera cuando se presiona una tecla de entrada en el cuadro de texto.")>
    Private Sub textBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        Me.OnKeyPress(e)
    End Sub

    <Description("Se genera cuando se suelta una tecla en el cuadro de texto.")>
    Private Sub textBox1_KeyUp(sender As Object, e As KeyEventArgs)
        Me.OnKeyUp(e)
    End Sub

    <Description("Se genera cuando se hace clic en el cuadro de texto con el botón del mouse.")>
    Private Sub textBox1_MouseClick(sender As Object, e As MouseEventArgs)
        Me.OnMouseClick(e)
    End Sub

    <Description("Se genera cuando se presiona el botón del mouse en el cuadro de texto.")>
    Private Sub textBox1_MouseDown(sender As Object, e As MouseEventArgs)
        Me.OnMouseDown(e)
    End Sub

    <Description("Se genera cuando el mouse se mueve sobre el cuadro de texto.")>
    Private Sub textBox1_MouseMove(sender As Object, e As MouseEventArgs)
        Me.OnMouseMove(e)
    End Sub

    <Description("Se genera cuando se suelta el botón del mouse sobre el cuadro de texto.")>
    Private Sub textBox1_MouseUp(sender As Object, e As MouseEventArgs)
        Me.OnMouseUp(e)
    End Sub

    <Description("Se genera cuando el cuadro de texto obtiene el foco.")>
    Private Sub textBox1_GotFocus(sender As Object, e As EventArgs)
        Me.OnGotFocus(e)
    End Sub

    <Description("Se genera cuando el cuadro de texto pierde el foco.")>
    Private Sub textBox1_LostFocus(sender As Object, e As EventArgs)
        Me.OnLostFocus(e)
    End Sub

    ' Evento cuando el TextBox obtiene el foco
    Private Sub TextBox1_Enter(sender As Object, e As EventArgs)
        _IsFocused = True
        Me.Invalidate()
        RemovePlaceHolder()
    End Sub

    ' Evento cuando el TextBox pierde el foco
    Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
        _IsFocused = False
        Me.Invalidate()
        SetPlaceHolder()
    End Sub
#End Region
End Class
