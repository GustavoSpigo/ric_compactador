<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.KryptonListBox1 = New Krypton.Toolkit.KryptonListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.KryptonButton1 = New Krypton.Toolkit.KryptonButton()
        Me.KryptonLabel1 = New Krypton.Toolkit.KryptonLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.KryptonButton2 = New Krypton.Toolkit.KryptonButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.KryptonLabel2 = New Krypton.Toolkit.KryptonLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.KryptonButton3 = New Krypton.Toolkit.KryptonButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(800, 450)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.KryptonListBox1)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.KryptonLabel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(792, 424)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Selecione o tipo de projeto"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'KryptonListBox1
        '
        Me.KryptonListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonListBox1.Location = New System.Drawing.Point(3, 23)
        Me.KryptonListBox1.Name = "KryptonListBox1"
        Me.KryptonListBox1.Size = New System.Drawing.Size(786, 360)
        Me.KryptonListBox1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.KryptonButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 383)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(786, 38)
        Me.Panel1.TabIndex = 4
        '
        'KryptonButton1
        '
        Me.KryptonButton1.Dock = System.Windows.Forms.DockStyle.Right
        Me.KryptonButton1.Location = New System.Drawing.Point(696, 10)
        Me.KryptonButton1.Name = "KryptonButton1"
        Me.KryptonButton1.Size = New System.Drawing.Size(90, 28)
        Me.KryptonButton1.TabIndex = 3
        Me.KryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.KryptonButton1.Values.Text = "Próximo..."
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonLabel1.Location = New System.Drawing.Point(3, 3)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Size = New System.Drawing.Size(786, 20)
        Me.KryptonLabel1.TabIndex = 1
        Me.KryptonLabel1.Values.Text = "Selecione o tipo de projeto"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Controls.Add(Me.KryptonLabel2)
        Me.TabPage2.Controls.Add(Me.KryptonButton2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(792, 424)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Selecione o arquivo e o destino"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'KryptonButton2
        '
        Me.KryptonButton2.Location = New System.Drawing.Point(22, 22)
        Me.KryptonButton2.Name = "KryptonButton2"
        Me.KryptonButton2.Size = New System.Drawing.Size(148, 25)
        Me.KryptonButton2.TabIndex = 0
        Me.KryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.KryptonButton2.Values.Text = "Selecione o arquivo..."
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(792, 424)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Conclusão"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'KryptonLabel2
        '
        Me.KryptonLabel2.Location = New System.Drawing.Point(22, 53)
        Me.KryptonLabel2.Name = "KryptonLabel2"
        Me.KryptonLabel2.Size = New System.Drawing.Size(6, 2)
        Me.KryptonLabel2.TabIndex = 1
        Me.KryptonLabel2.Values.Text = ""
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.KryptonButton3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 383)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(786, 38)
        Me.Panel2.TabIndex = 5
        '
        'KryptonButton3
        '
        Me.KryptonButton3.Dock = System.Windows.Forms.DockStyle.Right
        Me.KryptonButton3.Location = New System.Drawing.Point(696, 10)
        Me.KryptonButton3.Name = "KryptonButton3"
        Me.KryptonButton3.Size = New System.Drawing.Size(90, 28)
        Me.KryptonButton3.TabIndex = 3
        Me.KryptonButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.KryptonButton3.Values.Text = "Próximo..."
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "RIC - Compactador"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents KryptonListBox1 As Krypton.Toolkit.KryptonListBox
    Friend WithEvents KryptonLabel1 As Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonButton1 As Krypton.Toolkit.KryptonButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents KryptonButton2 As Krypton.Toolkit.KryptonButton
    Friend WithEvents KryptonLabel2 As Krypton.Toolkit.KryptonLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents KryptonButton3 As Krypton.Toolkit.KryptonButton
End Class
