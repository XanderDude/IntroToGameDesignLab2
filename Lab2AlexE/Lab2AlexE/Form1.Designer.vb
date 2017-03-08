<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnBackground = New System.Windows.Forms.Button()
        Me.btnLake = New System.Windows.Forms.Button()
        Me.btnTree = New System.Windows.Forms.Button()
        Me.btnCharacter = New System.Windows.Forms.Button()
        Me.pnlLab2 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'btnBackground
        '
        Me.btnBackground.BackColor = System.Drawing.Color.LemonChiffon
        Me.btnBackground.Location = New System.Drawing.Point(147, 387)
        Me.btnBackground.Name = "btnBackground"
        Me.btnBackground.Size = New System.Drawing.Size(75, 42)
        Me.btnBackground.TabIndex = 0
        Me.btnBackground.Text = "Background"
        Me.btnBackground.UseVisualStyleBackColor = False
        '
        'btnLake
        '
        Me.btnLake.BackColor = System.Drawing.Color.LemonChiffon
        Me.btnLake.Location = New System.Drawing.Point(229, 387)
        Me.btnLake.Name = "btnLake"
        Me.btnLake.Size = New System.Drawing.Size(75, 42)
        Me.btnLake.TabIndex = 1
        Me.btnLake.Text = "Lake"
        Me.btnLake.UseVisualStyleBackColor = False
        '
        'btnTree
        '
        Me.btnTree.BackColor = System.Drawing.Color.LemonChiffon
        Me.btnTree.Location = New System.Drawing.Point(311, 387)
        Me.btnTree.Name = "btnTree"
        Me.btnTree.Size = New System.Drawing.Size(75, 42)
        Me.btnTree.TabIndex = 2
        Me.btnTree.Text = "Tree"
        Me.btnTree.UseVisualStyleBackColor = False
        '
        'btnCharacter
        '
        Me.btnCharacter.BackColor = System.Drawing.Color.LemonChiffon
        Me.btnCharacter.Location = New System.Drawing.Point(392, 387)
        Me.btnCharacter.Name = "btnCharacter"
        Me.btnCharacter.Size = New System.Drawing.Size(75, 42)
        Me.btnCharacter.TabIndex = 3
        Me.btnCharacter.Text = "Character"
        Me.btnCharacter.UseVisualStyleBackColor = False
        '
        'pnlLab2
        '
        Me.pnlLab2.BackColor = System.Drawing.Color.White
        Me.pnlLab2.Location = New System.Drawing.Point(13, 13)
        Me.pnlLab2.Name = "pnlLab2"
        Me.pnlLab2.Size = New System.Drawing.Size(599, 368)
        Me.pnlLab2.TabIndex = 4
        '
        'frmLab2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Orange
        Me.ClientSize = New System.Drawing.Size(624, 441)
        Me.Controls.Add(Me.pnlLab2)
        Me.Controls.Add(Me.btnCharacter)
        Me.Controls.Add(Me.btnTree)
        Me.Controls.Add(Me.btnLake)
        Me.Controls.Add(Me.btnBackground)
        Me.Name = "frmLab2"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnBackground As Button
    Friend WithEvents btnLake As Button
    Friend WithEvents btnTree As Button
    Friend WithEvents btnCharacter As Button
    Friend WithEvents pnlLab2 As Panel
End Class
