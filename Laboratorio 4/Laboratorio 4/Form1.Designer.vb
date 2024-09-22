<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvHotel = New DataGridView()
        lbTitulo = New Label()
        CType(dgvHotel, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvHotel
        ' 
        dgvHotel.BackgroundColor = SystemColors.ButtonShadow
        dgvHotel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHotel.Location = New Point(52, 82)
        dgvHotel.Name = "dgvHotel"
        dgvHotel.Size = New Size(683, 333)
        dgvHotel.TabIndex = 0
        ' 
        ' lbTitulo
        ' 
        lbTitulo.AutoSize = True
        lbTitulo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lbTitulo.Location = New Point(299, 32)
        lbTitulo.Name = "lbTitulo"
        lbTitulo.Size = New Size(185, 25)
        lbTitulo.TabIndex = 1
        lbTitulo.Text = "SISTEMA DE HOTEL"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lbTitulo)
        Controls.Add(dgvHotel)
        Name = "Form1"
        Text = "Form1"
        CType(dgvHotel, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dgvHotel As DataGridView
    Friend WithEvents lbTitulo As Label

End Class
