'========================================================================================
'Created by: Alex Easter
'Name: Lab2
'Class: CS 161 Section A
'Date started: 3/1/17
'Date due: 3/8/17'
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
    Dim bmpTree(2) As Bitmap
    Dim bmpTree1 As Bitmap = New Bitmap("..\Images\tree1Lab2.png")
    Dim bmpTree2 As Bitmap = New Bitmap("..\Images\tree2Lab2.png")
    Dim bmpTree3 As Bitmap = New Bitmap("..\Images\tree3Lab2.png")

    Dim cshtSpriteX As Short = 10
    Dim cshtSpriteY As Short = 150
    Dim cshtSpriteW As Short = CShort(bmpSprite.Width)
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


    Private Sub frmLab2_Load(sender As Object, e As EventArgs) Handles Me.Load
        '--------------------------------------------------------------------------------
        'Description: 
        '--------------------------------------------------------------------------------

        bmpTree(0) = (bmpTree1)
        bmpTree(1) = (bmpTree2)
        bmpTree(2) = (bmpTree3)

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

    End Sub

    Private Sub btnBackground_Click(sender As Object, e As EventArgs) Handles btnBackground.Click
        boolBG = True
        sUpdateScreen()
    End Sub

    Private Sub btnLake_Click(sender As Object, e As EventArgs) Handles btnLake.Click

        boolLake = True

        pnlLakePoints.Visible = True

    End Sub
    Private Sub sUpdateScreen()

        graBGBuffer.DrawImageUnscaled(bmpBG, 0, 0)

        If boolBG = True Then
            graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If

        If boolTree = True Then
            'graTree.Clear(pnlLab2.BackColor)
            graTree.DrawImage(bmpTree(cintTreeType), cshtTreeX, cshtTreeY)

        End If

        If boolSprite = True Then
            graSprite.DrawImageUnscaled(bmpSprite, cshtSpriteX, cshtSpriteY)
        End If

    End Sub

    Private Sub btnTree_Click(sender As Object, e As EventArgs) Handles btnTree.Click

        boolTree = True

        If cintTreeType = 0 Then
            cintTreeType += 1
            graTree.DrawImage(bmpTree(cintTreeType), cshtTreeX, cshtTreeY)
            'graTree.DrawImageUnscaled(bmpTree3, cshtTreeX, cshtTreeY)
        ElseIf cintTreeType = 1 Then
            cintTreeType += 1
            graTree.DrawImage(bmpTree(cintTreeType), cshtTreeX, cshtTreeY)
            'graTree.DrawImageUnscaled(bmpTree2, cshtTreeX, cshtTreeY)

        ElseIf cintTreeType = 2 Then
            cintTreeType = 0
            graTree.DrawImage(bmpTree(cintTreeType), cshtTreeX, cshtTreeY)
            'graTree.DrawImageUnscaled(bmpTree1, cshtTreeX, cshtTreeY)

        End If
        graBGBuffer.DrawImageUnscaled(bmpBuffer, 0, 0)
    End Sub

    Private Sub btnCharacter_Click(sender As Object, e As EventArgs) Handles btnCharacter.Click
        boolSprite = True
        sUpdateScreen()
    End Sub

    Private Sub frmLab2_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim shtviewWidth As Short = CShort(pnlLab2.Width)

        If e.KeyCode = Keys.Right And cshtSpriteX < (shtviewWidth - 3 - bmpSprite.Width) Then
            cshtSpriteX += CShort(3)
        ElseIf e.KeyCode = Keys.Left And cshtSpriteX > (3) Then
            cshtSpriteX -= CShort(3)
        ElseIf e.KeyCode = Keys.Down And cshtSpriteY < (pnlLab2.Height - 3 - bmpSprite.Height) Then
            cshtSpriteY += CShort(3)
        ElseIf e.KeyCode = Keys.Up Then
            cshtSpriteY -= CShort(3)
        End If
        sUpdateScreen()
    End Sub

    Private Sub btnLakePntsConfirm_Click(sender As Object, e As EventArgs) Handles btnLakePntsConfirm.Click

        Dim pntPoly1(5) As Point

        pnlLakePoints.Visible = False

        pntPoly1(0).X = CInt(txtLakePnt1X.Text)
        pntPoly1(0).Y = CInt(txtLakePnt1Y.Text)
        pntPoly1(1).X = CInt(txtLakePnt2X.Text)
        pntPoly1(1).Y = CInt(txtLakePnt2Y.Text)
        pntPoly1(2).X = CInt(txtLakePnt3X.Text)
        pntPoly1(2).Y = CInt(txtLakePnt3Y.Text)
        pntPoly1(3).X = CInt(txtLakePnt4X.Text)
        pntPoly1(3).Y = CInt(txtLakePnt4Y.Text)
        pntPoly1(4).X = CInt(txtLakePnt5X.Text)
        pntPoly1(4).Y = CInt(txtLakePnt5Y.Text)
        pntPoly1(5).X = CInt(txtLakePnt6X.Text)
        pntPoly1(5).Y = CInt(txtLakePnt6Y.Text)

        graLake.FillPolygon(Brushes.Blue, pntPoly1)
        graLake.DrawPolygon(Pens.LightBlue, pntPoly1)

    End Sub
End Class

