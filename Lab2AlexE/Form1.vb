'========================================================================================
'Created by: Alex Easter & Sarah Reilly
'Name: Lab2
'Class: CS 161 Section A
'Date started: 3/1/17
'Date due: 3/8/17
'
'Summary: A simple program that allows you to draw graphics on the screen. The Background
'         draws the background. The Lake button gives you options for setting the points 
'         to a lake. Confirm to draw the lake, and reset to erase the lake and clear the
'         values. The Tree button lets you pick a place, type of tree, and the tree's scale.
'         The Character button draws a bird that is controlled with WASD.
'TODO:
'
'
'Bugs:
' - Giving incorrect values for the Lake's points will crash.
' - Can't exit out of the input boxes.
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
    Dim bmpTreeResized As Bitmap

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
    Dim cshtTreeType As Short

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

        ' Draw the BG
        graBGBuffer.DrawImageUnscaled(bmpBG, 0, 0)
        If boolBG = True Then
            graBG.DrawImageUnscaled(bmpBuffer, 0, 0)
        End If

        ' Draw the lake
        If boolLake = True Then
            graLake.FillPolygon(Brushes.LightBlue, cpntPoly1)
            graLake.DrawPolygon(Pens.Blue, cpntPoly1)
        End If

        ' Draw the tree
        If boolTree = True Then
            graTree.DrawImageUnscaled(bmpTreeResized, cshtTreeX, cshtTreeY)
        End If

        ' Draw the sprite
        If boolSprite = True Then
            graSprite.DrawImageUnscaled(bmpSprite, cshtSpriteX, cshtSpriteY)

        End If
    End Sub

    Private Sub btnTree_Click(sender As Object, e As EventArgs) Handles btnTree.Click
        '--------------------------------------------------------------------------------
        'Description: When clicked, the user will be asked for values used to draw a tree.
        '             A tree is then selected, scaled, and positioned. The screen is updated.
        '--------------------------------------------------------------------------------

        Dim cshtTreeScale As Short
        Dim bmpOrignalTree As Bitmap

        ' Get the inputs
        cshtTreeX = fGetInputBox("Please enter the x position of tree", "Tree Position", 0, 0,
                                                                         CShort(pnlLab2.Width))
        cshtTreeY = fGetInputBox("Please enter the y position of tree", "Tree Position", 0, 0,
                                                                        CShort(pnlLab2.Height))
        cshtTreeType = fGetInputBox("Please enter the type of tree (1-3)", "Tree Type", 1, 1, 3)
        cshtTreeScale = fGetInputBox("Please enter the scale of the tree (1-10)", "Tree Scale",
                                                                                        1, 1, 10)

        ' Get the tree
        Select Case cshtTreeType
            Case 1
                bmpOrignalTree = bmpTree1
            Case 2
                bmpOrignalTree = bmpTree2
            Case 3
                bmpOrignalTree = bmpTree3
            Case Else
                ' Otherwise throw an exception
                ' (This is needed so the compiler doesn't freak out)
                Throw New Exception("Tree type was somehow set to a number that is out of range")
        End Select

        ' Set the tree height and width
        cshtTreeW = CShort(bmpOrignalTree.Width) * cshtTreeScale
        cshtTreeH = CShort(bmpOrignalTree.Height) * cshtTreeScale

        ' Dispose the old tree 
        ' Yes the ? is supose to be there,
        ' it makes it so dispose doesnt get called if bmpTreeResized is null
        bmpTreeResized?.Dispose()

        ' Set the current tree bitmap
        bmpTreeResized = New Bitmap(cshtTreeW, cshtTreeH)

        ' resize the current bitmap
        For cshtOldTreeX As Short = 0 To CShort(bmpOrignalTree.Width - 1)
            For cshtOldTreeY As Short = 0 To CShort(bmpOrignalTree.Height - 1)
                ' Get the pixel for that position
                Dim colPixelColor As Color = bmpOrignalTree.GetPixel(cshtOldTreeX, cshtOldTreeY)

                ' Set the pixel for all the required positions
                For cshtOffsetX As Short = 0 To cshtTreeScale - 1S
                    For cshtOffsetY As Short = 0 To cshtTreeScale - 1S
                        bmpTreeResized.SetPixel((cshtOldTreeX * cshtTreeScale) + cshtOffsetX,
                                    (cshtOldTreeY * cshtTreeScale) + cshtOffsetY, colPixelColor)
                    Next
                Next
            Next
        Next

        boolTree = True

        ' Update the screen
        sUpdateScreen()
    End Sub

    Private Sub btnCharacter_Click(sender As Object, e As EventArgs) Handles btnCharacter.Click
        '--------------------------------------------------------------------------------------
        'Description: When clicked, sUpdateScreen is called allowing the character to be drawn.
        '--------------------------------------------------------------------------------------

        boolSprite = True
        sUpdateScreen()
    End Sub

    Private Sub frmLab2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        '--------------------------------------------------------------------------------------
        'Description: Allows the user to control the character sprite with WASD.
        '--------------------------------------------------------------------------------------

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
    Private Function fGetInputBox(strMessage As String, strTitle As String,
            shtDefaultValue As Short, shtMinValue As Short, shtMaxValue As Short) As Short
        '--------------------------------------------------------------------------------------
        'Description: Checks the values return from the user in fGetInputBox
        '--------------------------------------------------------------------------------------

        ' Declare variables
        Dim strInput As String
        Dim shtValue As Short


        While True
            ' Get user input
            strInput = InputBox(strMessage, strTitle, CStr(shtDefaultValue))


            ' User cancled input
            If strInput Is Nothing Then

                Continue While
            End If

            ' User entered invalid input
            If Not Short.TryParse(strInput, shtValue) Then
                MessageBox.Show("Invalid number. Please enter a number between " &
                                                shtMinValue & " and " & shtMaxValue)
                Continue While
            End If


            ' User entered invalid input
            If shtValue < shtMinValue Or shtValue > shtMaxValue Then
                MessageBox.Show("Invalid number. Please enter a number between " &
                                                shtMinValue & " and " & shtMaxValue)
                Continue While
            End If


            Return shtValue
        End While
    End Function
End Class

