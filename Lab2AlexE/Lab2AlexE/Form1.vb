'========================================================================================
'Created by: Alex Easter & Sarah Reilly
'Name: Lab2
'Class: CS 161 Section A
'Date started: 3/1/17
'Date due: 3/8/17
'
'Summary: 
'
'TODO:
'
'
'Bugs:
' 
'========================================================================================

Option Explicit On
Option Strict On

Imports System.Drawing.Drawing2D
Imports System.Threading

Public Class frmLab2

    Dim cshtFrames As Short = 3

    Dim graBG As Graphics
    Dim graBGBuffer As Graphics
    Dim graSprite As Graphics
    Dim graLake As Graphics
    Dim graTree As Graphics

    Dim bmpBuffer As Bitmap
    Dim bmpSprite As Bitmap = New Bitmap("..\Images\birdLab2.png")
    Dim bmpBG As Bitmap = New Bitmap("..\Images\backgroundLab2.png")
    Dim bmpLake As Bitmap = New Bitmap("..\Images\lakeLab2.png")
    Dim bmpTree1 As Bitmap = New Bitmap("..\Images\tree1Lab2.png")
    Dim bmpTree2 As Bitmap = New Bitmap("..\Images\tree1Lab2.png")
    Dim bmpTree3 As Bitmap = New Bitmap("..\Images\tree1Lab2.png")

    Dim cshtSpriteX As Short = 10
    Dim cshtSpriteY As Short = 150
    Dim cshtSpriteW As Short = CShort(bmpSprite.Width \ cshtFrames)
    Dim cshtSpriteH As Short = CShort(bmpSprite.Height)

    Dim cshtLakeX As Short
    Dim cshtLakeY As Short
    Dim cshtLakeW As Short = CShort(bmpLake.Width)
    Dim cshtLakeH As Short = CShort(bmpLake.Height)

    Dim cshtTreeX As Short
    Dim cshtTreeY As Short
    Dim cshtTreeW As Short = CShort(bmpTree1.Width)
    Dim cshtTreeH As Short = CShort(bmpTree1.Height)

    Dim cshtSpriteXStep As Short = 0
    Dim cshtSpriteYStep As Short = 0

    Dim mtxSprite As Matrix

    Dim recCurrentFrame As Rectangle
    Dim cshtFrameX As Short
    Dim cshtFrameY As Short
    Dim cshtAnimatedSpriteLength As Short = CShort(bmpSprite.Width)

    Dim boolBG As Boolean
    Dim boolLake As Boolean
    Dim boolTree As Boolean
    Dim boolSprite As Boolean

    Dim cintTreeType As Integer

    Dim cpntPoly1(5) As Point

    Private Sub frmLab2_Load(sender As Object, e As EventArgs) Handles Me.Load
        '--------------------------------------------------------------------------------
        'Description: Prepares graphics and text boxes.
        '--------------------------------------------------------------------------------

        graBG = pnlLab2.CreateGraphics
        bmpBuffer = New Bitmap(pnlLab2.Width, pnlLab2.Height, graBG)
        graBGBuffer = Graphics.FromImage(bmpBuffer)
        graSprite = pnlLab2.CreateGraphics
        bmpSprite.MakeTransparent(Color.FromArgb(255, 0, 255))
        graLake = pnlLab2.CreateGraphics
        bmpLake.MakeTransparent(Color.FromArgb(255, 0, 255))
        graTree = pnlLab2.CreateGraphics
        bmpTree1.MakeTransparent(Color.FromArgb(255, 0, 255))
        bmpTree2.MakeTransparent(Color.FromArgb(255, 0, 255))
        bmpTree3.MakeTransparent(Color.FromArgb(255, 0, 255))

        mtxSprite = New Matrix(1, 0, 0, 1, cshtSpriteXStep, cshtSpriteYStep)

        pnlLakePoints.Visible = False

        txtLakePnt1X.Text = "0"
        txtLakePnt1Y.Text = "0"
        txtLakePnt2X.Text = "0"
        txtLakePnt2Y.Text = "0"
        txtLakePnt3X.Text = "0"
        txtLakePnt3Y.Text = "0"
        txtLakePnt4X.Text = "0"
        txtLakePnt4Y.Text = "0"
        txtLakePnt5X.Text = "0"
        txtLakePnt5Y.Text = "0"
        txtLakePnt6X.Text = "0"
        txtLakePnt6Y.Text = "0"

    End Sub

    Private Sub btnBackground_Click(sender As Object, e As EventArgs) Handles btnBackground.Click
        '--------------------------------------------------------------------------------
        'Description: On click, allow sUpdateScreen to display the background.
        '--------------------------------------------------------------------------------
        boolBG = True
        sUpdateScreen()
    End Sub

    Private Sub btnLake_Click(sender As Object, e As EventArgs) Handles btnLake.Click
        '--------------------------------------------------------------------------------
        'Description: On click, make pnlLakePoints visible and allow sUpdateScreen to 
        '             display the lake.
        '--------------------------------------------------------------------------------
        boolLake = True
        pnlLakePoints.Visible = True
    End Sub
    Private Sub sUpdateScreen()
        '--------------------------------------------------------------------------------
        'Description: When called, prepares and displays graphics in pnlLab2
        '--------------------------------------------------------------------------------
        ' Clear graphic
        graBG.Clear(Color.White)

        graBGBuffer.DrawImageUnscaled(bmpBG, 0, 0)
        If boolBG = True Then
            graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If
        If boolLake = True Then
            graLake.FillPolygon(Brushes.LightBlue, cpntPoly1)
            graLake.DrawPolygon(Pens.Blue, cpntPoly1)
        End If
        If boolTree = True Then

            'graTree.DrawImageUnscaled("bmpTree" + (cintTreeType + 1).ToString, cshtTreeX, cshtTreeY)
            'graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If
        If boolSprite = True Then
            graSprite.DrawImageUnscaled(bmpSprite, cshtSpriteX, cshtSpriteY)

        End If

    End Sub

    Private Sub btnTree_Click(sender As Object, e As EventArgs) Handles btnTree.Click

        boolTree = True
        'graTree.Clear()
        If cintTreeType = 2 Then
            graTree.DrawImageUnscaled(bmpTree3, cshtTreeX, cshtTreeY)
            cintTreeType = 1
        ElseIf cintTreeType = 1 Then
            graTree.DrawImageUnscaled(bmpTree2, cshtTreeX, cshtTreeY)
            cintTreeType += 1
        ElseIf cintTreeType = 0 Then
            graTree.DrawImageUnscaled(bmpTree1, cshtTreeX, cshtTreeY)
            cintTreeType += 1
        End If
        sUpdateScreen()
    End Sub

    Private Sub btnCharacter_Click(sender As Object, e As EventArgs) Handles btnCharacter.Click
        boolSprite = True
        sUpdateScreen()
    End Sub

    Private Sub frmLab2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Constant speed the character moves at
        Const cshtSpeed As Short = 5

        ' Move the character
        Select Case e.KeyCode
            Case Keys.W
                cshtSpriteY -= cshtSpeed
            Case Keys.S
                cshtSpriteY += cshtSpeed
            Case Keys.D
                cshtSpriteX += cshtSpeed
            Case Keys.A
                cshtSpriteX -= cshtSpeed
        End Select

        ' Update the screen
        sUpdateScreen()
    End Sub
    Private Sub btnLakePntsConfirm_Click(sender As Object, e As EventArgs) Handles btnLakePntsConfirm.Click
        '--------------------------------------------------------------------------------
        'Description: On click, set the Lake's points according to txtLakePnt(s) in 
        '             pnlLakePoints.
        '             Close pnlLakePoints and call sUpdateScreen.
        '--------------------------------------------------------------------------------

        pnlLakePoints.Visible = False

        cpntPoly1(0).X = CInt(txtLakePnt1X.Text)
        cpntPoly1(0).Y = CInt(txtLakePnt1Y.Text)
        cpntPoly1(1).X = CInt(txtLakePnt2X.Text)
        cpntPoly1(1).Y = CInt(txtLakePnt2Y.Text)
        cpntPoly1(2).X = CInt(txtLakePnt3X.Text)
        cpntPoly1(2).Y = CInt(txtLakePnt3Y.Text)
        cpntPoly1(3).X = CInt(txtLakePnt4X.Text)
        cpntPoly1(3).Y = CInt(txtLakePnt4Y.Text)
        cpntPoly1(4).X = CInt(txtLakePnt5X.Text)
        cpntPoly1(4).Y = CInt(txtLakePnt5Y.Text)
        cpntPoly1(5).X = CInt(txtLakePnt6X.Text)
        cpntPoly1(5).Y = CInt(txtLakePnt6Y.Text)

        sUpdateScreen()

    End Sub

    Private Sub btnLakeReset_Click(sender As Object, e As EventArgs) Handles btnLakeReset.Click
        '--------------------------------------------------------------------------------
        'Description: On click, reset text boxes in pnlLakePoints, reset cpntPoly1(), and
        '             clear graphics.
        '             Call sUpdateScreen.
        '--------------------------------------------------------------------------------

        pnlLakePoints.Visible = False
        boolLake = False
        txtLakePnt1X.Text = "0"
        txtLakePnt1Y.Text = "0"
        txtLakePnt2X.Text = "0"
        txtLakePnt2Y.Text = "0"
        txtLakePnt3X.Text = "0"
        txtLakePnt3Y.Text = "0"
        txtLakePnt4X.Text = "0"
        txtLakePnt4Y.Text = "0"
        txtLakePnt5X.Text = "0"
        txtLakePnt5Y.Text = "0"
        txtLakePnt6X.Text = "0"
        txtLakePnt6Y.Text = "0"

        cpntPoly1(0).X = CInt(txtLakePnt1X.Text)
        cpntPoly1(0).Y = CInt(txtLakePnt1Y.Text)
        cpntPoly1(1).X = CInt(txtLakePnt2X.Text)
        cpntPoly1(1).Y = CInt(txtLakePnt2Y.Text)
        cpntPoly1(2).X = CInt(txtLakePnt3X.Text)
        cpntPoly1(2).Y = CInt(txtLakePnt3Y.Text)
        cpntPoly1(3).X = CInt(txtLakePnt4X.Text)
        cpntPoly1(3).Y = CInt(txtLakePnt4Y.Text)
        cpntPoly1(4).X = CInt(txtLakePnt5X.Text)
        cpntPoly1(4).Y = CInt(txtLakePnt5Y.Text)
        cpntPoly1(5).X = CInt(txtLakePnt6X.Text)
        cpntPoly1(5).Y = CInt(txtLakePnt6Y.Text)

        graLake.Clear(pnlLab2.BackColor)
        sUpdateScreen()


    End Sub
End Class

