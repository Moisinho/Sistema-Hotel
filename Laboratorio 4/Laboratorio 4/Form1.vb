'Moises Betancourt 20-70-7371
'Fernando Barrios  8-1002-1207
'Javier Hernandez 8-1001-178
'1LS131

Public Class Form1
    Private WithEvents dateTimePicker As New DateTimePicker()
    Private WithEvents datePickerSalida As New DateTimePicker()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim nombresColumnas As String() = {"Nombre", "Fecha de Entrada", "Fecha de Salida", "Tipo de Habitación", "Dias de Estadía", "Costo Total"}
        Dim anchosColumnas As Integer() = {100, 100, 100, 100, 100, 100}

        Dim columnaNombre As New DataGridViewTextBoxColumn()
        columnaNombre.HeaderText = nombresColumnas(0)
        columnaNombre.Name = "Nombre"
        columnaNombre.Width = anchosColumnas(0)
        dgvHotel.Columns.Add(columnaNombre)

        Dim fechaEntrada As New DataGridViewTextBoxColumn()
        fechaEntrada.HeaderText = nombresColumnas(1)
        fechaEntrada.Name = "fechaEntrada"
        fechaEntrada.Width = anchosColumnas(1)
        dgvHotel.Columns.Add(fechaEntrada)

        Dim fechaSalida As New DataGridViewTextBoxColumn()
        fechaSalida.HeaderText = nombresColumnas(2)
        fechaSalida.Name = "fechaSalida"
        fechaSalida.Width = anchosColumnas(2)
        dgvHotel.Columns.Add(fechaSalida)

        Dim tipoHabitacion As New DataGridViewComboBoxColumn()
        tipoHabitacion.HeaderText = nombresColumnas(3)
        tipoHabitacion.Name = "tipoHabitacion"
        tipoHabitacion.Width = anchosColumnas(3)
        tipoHabitacion.Items.AddRange("Individual ($50)", "Doble ($75)", "Suite ($120)")
        dgvHotel.Columns.Add(tipoHabitacion)

        Dim DiasEstadia As New DataGridViewTextBoxColumn()
        DiasEstadia.HeaderText = nombresColumnas(4)
        DiasEstadia.Name = "DiasEstadia"
        DiasEstadia.Width = anchosColumnas(4)
        dgvHotel.Columns.Add(DiasEstadia)
        DiasEstadia.ReadOnly = True

        Dim CostoTotal As New DataGridViewTextBoxColumn()
        CostoTotal.HeaderText = nombresColumnas(5)
        CostoTotal.Name = "CostoTotal"
        CostoTotal.Width = anchosColumnas(5)
        dgvHotel.Columns.Add(CostoTotal)
        CostoTotal.ReadOnly = True

        dateTimePicker.Format = DateTimePickerFormat.Short
        dateTimePicker.Visible = False
        dgvHotel.Controls.Add(dateTimePicker)

        datePickerSalida.Format = DateTimePickerFormat.Short
        datePickerSalida.Visible = False
        dgvHotel.Controls.Add(datePickerSalida)

        For Each row As DataGridViewRow In dgvHotel.Rows
            row.Cells("fechaSalida").ReadOnly = True
        Next
    End Sub

    ' Manejo del DateTimePicker para la fecha de entrada
    Private Sub dgvHotel_CellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) Handles dgvHotel.CellBeginEdit
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaEntrada" Then

            Dim rect As Rectangle = dgvHotel.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
            dateTimePicker.Size = rect.Size
            dateTimePicker.Location = rect.Location
            dateTimePicker.Visible = True
        End If
    End Sub

    Private Sub dgvHotel_CellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvHotel.CellEndEdit
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaEntrada" Then

            dgvHotel(e.ColumnIndex, e.RowIndex).Value = dateTimePicker.Value.ToShortDateString()
            dateTimePicker.Visible = False
        End If

    End Sub

    ' Manejo del DateTimePicker para la fecha de salida
    Private Sub dgvHotel_CellBeginEditSalida(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) Handles dgvHotel.CellBeginEdit
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaSalida" Then

            Dim rect As Rectangle = dgvHotel.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
            datePickerSalida.Size = rect.Size
            datePickerSalida.Location = rect.Location
            datePickerSalida.Visible = True
        End If
    End Sub

    Private Sub dgvHotel_CellEndEditSalida(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvHotel.CellEndEdit
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaSalida" Then
            ' Guardar el valor seleccionado en la celda
            dgvHotel(e.ColumnIndex, e.RowIndex).Value = datePickerSalida.Value.ToShortDateString()
            datePickerSalida.Visible = False

        End If
    End Sub


    Private Sub calcularEstadia(ByVal rowIndex As Integer)
        Dim fechaEntrada As Date
        Dim fechaSalida As Date

        If Date.TryParse(dgvHotel.Rows(rowIndex).Cells("fechaEntrada").Value, fechaEntrada) AndAlso
           Date.TryParse(dgvHotel.Rows(rowIndex).Cells("fechaSalida").Value, fechaSalida) Then


            Dim dias As Integer = (fechaSalida - fechaEntrada).Days

            If dias > 0 Then

                dgvHotel.Rows(rowIndex).Cells("DiasEstadia").Value = dias
            Else
                MessageBox.Show("Ingrese una fecha valida", "ALERTA!", MessageBoxButtons.OKCancel)
            End If

        End If

    End Sub

    Private Sub calcularCosto(ByVal rowIndex As Integer)
        Dim costoTotal As Decimal
        Dim tipoHabitacion As String

        If dgvHotel.Rows(rowIndex).Cells("tipoHabitacion").Value IsNot Nothing Then
            tipoHabitacion = dgvHotel.Rows(rowIndex).Cells("tipoHabitacion").Value.ToString()

            ' Determinar el costo por día en función del tipo de habitación seleccionada
            Select Case tipoHabitacion
                Case "Individual ($50)"
                    costoTotal = 50 * 1.1
                Case "Doble ($75)"
                    costoTotal = 75 * 1.1
                Case "Suite ($120)"
                    costoTotal = 120 * 1.1
            End Select

            Dim dias As Integer
            If Integer.TryParse(dgvHotel.Rows(rowIndex).Cells("DiasEstadia").Value.ToString(), dias) Then
                costoTotal *= dias
            Else
                costoTotal = 0
            End If

            dgvHotel.Rows(rowIndex).Cells("CostoTotal").Value = "$" & costoTotal.ToString("F2")
        End If
    End Sub

    Private Sub dgvHotel_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvHotel.CellValueChanged
        If dgvHotel.Columns(e.ColumnIndex).Name = "Nombre" Then
            'Validaciones Nombre
            If Not String.IsNullOrEmpty(dgvHotel.Rows(e.RowIndex).Cells("Nombre").Value.ToString()) Then

                dgvHotel.Rows(e.RowIndex).Cells("fechaEntrada").ReadOnly = False
                dgvHotel.Rows(e.RowIndex).Cells("fechaSalida").ReadOnly = False
                dgvHotel.Rows(e.RowIndex).Cells("tipoHabitacion").ReadOnly = False
            Else
                dgvHotel.Rows(e.RowIndex).Cells("fechaEntrada").ReadOnly = True
                dgvHotel.Rows(e.RowIndex).Cells("fechaSalida").ReadOnly = True
                dgvHotel.Rows(e.RowIndex).Cells("tipoHabitacion").ReadOnly = True
            End If

        End If

        'Validaciones fechaEntrada
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaEntrada" Then

            If Not String.IsNullOrEmpty(dgvHotel.Rows(e.RowIndex).Cells("fechaEntrada").Value.ToString()) Then

                dgvHotel.Rows(e.RowIndex).Cells("fechaSalida").ReadOnly = False
            Else

                dgvHotel.Rows(e.RowIndex).Cells("fechaSalida").ReadOnly = True
            End If
        End If

        'Llamar funcion calcularEstadia
        If dgvHotel.Columns(e.ColumnIndex).Name = "fechaSalida" Then
            calcularEstadia(e.RowIndex)
        End If

        'Llamar funcion calcularCosto
        If dgvHotel.Columns(e.ColumnIndex).Name = "tipoHabitacion" Then
            calcularCosto(e.RowIndex)
        End If


    End Sub

    Private Sub dgvHotel_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvHotel.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If dgvHotel.Columns(e.ColumnIndex).Name = "fechaEntrada" Or dgvHotel.Columns(e.ColumnIndex).Name = "fechaSalida" Or dgvHotel.Columns(e.ColumnIndex).Name = "tipoHabitacion" Then
                Dim valorNombre = dgvHotel.Rows(e.RowIndex).Cells("Nombre").Value
                If valorNombre Is Nothing OrElse String.IsNullOrEmpty(valorNombre.ToString()) Then
                    MessageBox.Show("Debe ingresar su nombre primero", "ALERTA!", MessageBoxButtons.OKCancel)
                Else

                End If
            End If
            'Validaciones Fecha de Salida
            If dgvHotel.Columns(e.ColumnIndex).Name = "fechaSalida" Then
                Dim valorEntrada = dgvHotel.Rows(e.RowIndex).Cells("fechaEntrada").Value
                If valorEntrada Is Nothing OrElse String.IsNullOrEmpty(valorEntrada.ToString()) Then
                    MessageBox.Show("Debe ingresar la fecha de entrada", "ALERTA!", MessageBoxButtons.OKCancel)
                Else

                End If
            End If
            'Validaciones Tipo de Habitacion
            If dgvHotel.Columns(e.ColumnIndex).Name = "tipoHabitacion" Then
                Dim valorSalida = dgvHotel.Rows(e.RowIndex).Cells("fechaSalida").Value
                If valorSalida Is Nothing OrElse String.IsNullOrEmpty(valorSalida.ToString()) Then
                    MessageBox.Show("Debe ingresar la fecha de salida", "ALERTA!", MessageBoxButtons.OKCancel)
                Else

                End If
            End If
        End If
    End Sub
End Class
