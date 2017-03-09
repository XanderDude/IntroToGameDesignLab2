'========================================================================================
'Header:
'
'
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

    Dim cshtBGXStep As Short = -1
    Dim cshtBGYStep As Short = 0

    Dim cshtBGX As Short

    Dim mtxSprite As Matrix
    Dim mtxBG As Matrix

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
        mtxBG = New Matrix(1, 0, 0, 1, cshtBGXStep, cshtBGYStep)

        Me.KeyPreview = True
    End Sub

    Private Sub btnBackground_Click(sender As Object, e As EventArgs) Handles btnBackground.Click
        boolBG = True
        sUpdateScreen()
    End Sub

    Private Sub btnLake_Click(sender As Object, e As EventArgs) Handles btnLake.Click
        boolLake = True
        sUpdateScreen()
    End Sub
    Private Sub sUpdateScreen()
        ' Clear graphic
        graBG.Clear(Color.White)

        graBGBuffer.DrawImageUnscaled(bmpBG, 0, 0)
        If boolBG = True Then
            graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If
        If boolLake = True Then

            graLake.DrawImageUnscaled(bmpLake, cshtLakeX, cshtLakeY)
            'graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
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
        const cshtSpeed as Short = 5
        
        ' Move the character
        select e.KeyCode
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
End Class

