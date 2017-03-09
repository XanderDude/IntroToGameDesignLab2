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
    Dim bmpTree2 As Bitmap = New Bitmap("..\Images\tree2Lab2.png")
    Dim bmpTree3 As Bitmap = New Bitmap("..\Images\tree3Lab2.png")
    Dim bmpTreeResized as Bitmap

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
    Dim cshttreeType as Short

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
    
    dim rngRandom as Random

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

        rngRandom = new Random()
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
        
        ' Draw the BG
        graBGBuffer.DrawImageUnscaled(bmpBG, 0, 0)
        If boolBG = True Then
            graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If

        ' Draw the lake
        If boolLake = True Then
            graLake.DrawImageUnscaled(bmpLake, cshtLakeX, cshtLakeY)
            'graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If

        ' Draw the tree
        if boolTree
            graTree.DrawImageUnscaled(bmpTreeResized, cshtTreeX, cshtTreeY)
        End If
        
        ' Draw the sprite
        If boolSprite = True Then
            graSprite.DrawImageUnscaled(bmpSprite, cshtSpriteX, cshtSpriteY)

        End If
    End Sub

    Private Sub btnTree_Click(sender As Object, e As EventArgs) Handles btnTree.Click
        Dim cshtTreeScale as Short
        Dim bmpOrignalTree as Bitmap
        
        ' Get the inputs
        cshtTreeX = fGetInputBox("Please enter the x position of tree", "Tree Position", 0, 0, CShort(pnlLab2.Width))
        cshtTreeY = fGetInputBox("Please enter the y position of tree", "Tree Position", 0, 0, CShort(pnlLab2.Height))
        cshtTreeType = fGetInputBox("Please enter the type of tree (1-3)", "Tree Type", 1, 1, 3)
        cshtTreeScale = fGetInputBox("Please enter the scale of the tree (1-20)", "Tree Size", 1, 1, 20)
        
        ' Get the tree
        select cshtTreeType
            Case 1
                bmpOrignalTree = bmpTree1
            Case 2
                bmpOrignalTree = bmpTree2
            Case 3
                bmpOrignalTree = bmpTree3
            Case Else
                ' Otherwise throw an exception
                ' (This is needed so the compiler doesn't freak out)
                Throw new Exception("Tree type was somehow set to a number that is out of range")
        End Select
        
        ' Set the tree height and width
        cshtTreeH = CShort(bmpOrignalTree.Height) * cshtTreeScale
        cshtTreeW = CShort(bmpOrignalTree.Width) * cshtTreeScale

        ' Dispose the old tree 
        ' Yes the ? is supose to be there,
        ' it makes it so dispose doesnt get called if bmpTreeResized is null
        bmpTreeResized?.Dispose()

        ' Set the current tree bitmap
        bmpTreeResized = New Bitmap(cshtTreeW,cshtTreeH)

        ' resize the current bitmap
        for cshtOldTreeX as short = 0 to CShort(bmpOrignalTree.Width - 1)
            for cshtOldTreeY as short = 0 to CShort(bmpOrignalTree.Height - 1)
                ' Get the pixel for that position
                dim colPixelColor as Color = bmpOrignalTree.GetPixel(cshtOldTreeX, cshtOldTreeY)

                ' Set the pixel for all the required positions
                for cshtNewTreeX as short = cshtOldTreeX * cshtTreeScale to (cshtOldTreeX * cshtTreeScale) + cshtTreeScale
                    for cshtNewTreeY as short = cshtOldTreeY * cshtTreeScale to (cshtOldTreeX * cshtTreeScale) + cshtTreeScale
                        bmpTreeResized.SetPixel(cshtNewTreeX, cshtNewTreeY, colPixelColor)
                    next
                Next
            Next
        Next

        boolTree = True

        ' Update the screen
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

    private Function fGetInputBox(strMessage as string, strTitle as string, shtDefaultValue as short, shtMinValue As Short, shtMaxValue As Short) As Short
        ' Declare variables
        Dim strInput As string
        Dim shtValue as Short

        While true
            ' Get user input
            strInput = InputBox(strMessage, strTitle, Cstr(shtDefaultValue))

            ' User cancled input
            if strInput Is nothing
                Continue While
            End If

            ' User entered invalid input
            if not short.TryParse(strInput, shtValue)
                MessageBox.Show("Invalid number. Please enter a number between " & shtMinValue & " and " & shtMaxValue)
                Continue While
            end if

            ' User entered invalid input
            If shtValue < shtMinValue Or shtValue > shtMaxValue
                MessageBox.Show("Invalid number. Please enter a number between " & shtMinValue & " and " & shtMaxValue)
                Continue While
            end if

            return shtValue
        End While
    End Function
End Class

