Imports System.Globalization

Public Class JT_TextBoxFecha
    Inherits JT_TextBox

    ' Sobrescribir el evento OnKeyPress para validar la entrada de caracteres
    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        FormatoFecha(TextBox1, e)
    End Sub

    ' Sobrescribir el evento OnTextChanged para validar la fecha
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        If Not IsPlaceholder Then ' Solo validar si no es un placeholder
            ValidarFechaChanged(TextBox1)
        End If
    End Sub

    ' Sobrescribir el evento OnLeave para verificar la fecha al salir del control
    Protected Overrides Sub OnLeave(e As EventArgs)
        MyBase.OnLeave(e)
        If Not IsPlaceholder Then ' Solo validar si no es un placeholder
            ValidarFechaLeave(TextBox1)
        End If
    End Sub

    ' Método para formatear la fecha mientras se ingresa
    Private Sub FormatoFecha(NombreTextBox As TextBox, e As KeyPressEventArgs)
        ' Permitir solo dígitos y control (backspace)
        If Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
            MsgBox("Este campo solo acepta caracteres numéricos.", vbCritical, "No permitido")
            Return
        End If

        ' Agregar guiones automáticamente al escribir la fecha
        If Not Char.IsControl(e.KeyChar) Then
            Dim texto As String = NombreTextBox.Text

            If texto.Length = 2 OrElse texto.Length = 5 Then
                texto += "-" & e.KeyChar
                NombreTextBox.Text = texto
                NombreTextBox.SelectionStart = NombreTextBox.Text.Length
                e.Handled = True
            ElseIf texto.Length = 10 Then
                e.Handled = True
            End If
        End If
    End Sub

    ' Método para validar la fecha mientras se está escribiendo
    Private Sub ValidarFechaChanged(NombreTextbox As TextBox)
        If NombreTextbox.Text.Length < 10 Then Return

        Dim fechaValida As Boolean = DateTime.TryParseExact(
            NombreTextbox.Text,
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            Nothing
        )

        If Not fechaValida Then
            MsgBox("Ingrese una fecha válida (dd-MM-yyyy).", vbCritical, "Formato incorrecto")
            NombreTextbox.Clear()
        End If
    End Sub

    ' Método para validar la fecha al salir del control
    Private Sub ValidarFechaLeave(NombreTextbox As TextBox)
        ' Si el campo está vacío, no realizar validación adicional
        If String.IsNullOrWhiteSpace(NombreTextbox.Text) Then Return

        ' Si el texto es menor a 10 caracteres, no es una fecha completa
        If NombreTextbox.Text.Length < 10 Then
            MsgBox("Ingrese una fecha válida.", vbCritical, "No permitido")
            NombreTextbox.Clear()
            NombreTextbox.Focus()
            Return
        End If

        ' Verificar si la fecha es válida
        Dim fechaValida As Boolean = DateTime.TryParseExact(
            NombreTextbox.Text,
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            Nothing
        )

        If Not fechaValida Then
            MsgBox("Ingrese una fecha válida (dd-MM-yyyy).", vbCritical, "Formato incorrecto")
            NombreTextbox.Clear()
            NombreTextbox.Focus()
        End If
    End Sub
End Class
