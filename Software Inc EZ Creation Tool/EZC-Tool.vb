Imports System.IO
Public Class EZC_Tool

    Private Sub ToolStripStatusLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel1.Click
        Process.Start("https://github.com/XeCuSoft/Software.Inc-EZ-Creation-Tool")
    End Sub

    Private Sub EZC_Tool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.TabPages.Remove(TabPage5)
        txtboxgamepfad.Text = My.Settings.gamepfad

        Try
            Me.Text = "Software Inc. EZ Creation Tool | v" & System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Catch ex As Exception
            Me.Text = "Software Inc. EZ Creation Tool | v" & Application.ProductVersion.ToString
        End Try

        updateapp()
    End Sub

    Private Sub gamepfadchange_Click(sender As Object, e As EventArgs) Handles gamepfadchange.Click
        installationspfadset()
    End Sub

    Private Sub installationspfadset()
        Dim FolderBrowser As New FolderBrowserDialog
        FolderBrowser.Description = "Please choose as path to Software Inc."
        FolderBrowser.ShowNewFolderButton = True
        FolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
        FolderBrowser.SelectedPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        If FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtboxgamepfad.Text = FolderBrowser.SelectedPath
            If File.Exists(FolderBrowser.SelectedPath & "\Software Inc.exe") = False Then
                MsgBox("In the specified directory Software Inc was not found." & vbCrLf &
                       "Please select the correct directory .")
                installationspfadset()
            Else
                My.Settings.gamepfad = txtboxgamepfad.Text
                My.Settings.Save()
            End If

        End If
    End Sub

    Private Sub modcreate_Click(sender As Object, e As EventArgs) Handles modcreatebutton.Click
        If txtboxgamepfad.Text = "" Then
            installationspfadset()
        End If
        If txtboxModName.Text = "" Then
            StatusLabel.Text = "Please enter a Modname"
        Else
            If Not IO.Directory.Exists(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text) Then
                Try
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text)
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Companies")
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\CompanyTypes")
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Events")
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\NameGenerators")
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Scenarios")
                    IO.Directory.CreateDirectory(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\SoftwareTypes")
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
                End Try
            End If

            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Companies\" & txtboxModName.Text & ".xml", codecompanies.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\CompanyTypes\" & txtboxModName.Text & ".xml", codecompanytype.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Events\" & txtboxModName.Text & ".xml", codeevent.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\NameGenerators\" & TextBoxNameofNameGenerator.Text & ".txt", codegenname.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Scenarios\" & txtboxModName.Text & ".xml", codescenario.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\SoftwareTypes\" & txtboxModName.Text & ".xml", codesoftwaretypes.Text)
            IO.File.WriteAllText(txtboxgamepfad.Text & "\Mods\" & txtboxModName.Text & "\Personalities.xml", codepersonalities.Text)

        End If
    End Sub

    Private Sub code_companies()
        codecompanies.Text =
            "<Company>" & vbNewLine _
            & "<Name>" & txtboxCompanyName.Text & "</Name>" & vbNewLine _
            & "<Money>" & txtboxBudget.Text & "</Money>" & vbNewLine _
            & "<Reputation>" & comboboxReputation.Text & "</Reputation>" & vbNewLine _
            & "<Founded>" & comboboxFounded.Text & "</Founded>" & vbNewLine _
            & "<Products>" & vbNewLine _
            & codeproducts.Text _
            & "</Products>" & vbNewLine _
            & "</Company>"
    End Sub

    Private Sub Companies_TextChanged(sender As Object, e As EventArgs) Handles txtboxCompanyName.TextChanged, txtboxBudget.TextChanged, comboboxReputation.SelectedIndexChanged, comboboxFounded.SelectedIndexChanged
        code_companies()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        code_products()
        code_companies()
    End Sub

    Dim productcode
    Private Sub code_products()
        codeproducts.Text +=
            "<Product>" & vbNewLine _
            & "<Name>" & TextBoxProductName.Text & "</Name>" & vbNewLine _
            & "<Release>" & ComboBoxReleaseMonth.Text & ComboBoxReleaseJear.Text & "</Release>" & vbNewLine _
            & "<Type>" & ComboBoxProductType.Text & "</Type>" & vbNewLine _
            & "<Features>" & vbNewLine _
            & "<Feature></Feature>" & vbNewLine _
            & "<Quality>" & ComboBoxQuality.Text & "</Quality>" & vbNewLine _
            & "<OpenSource>" & CheckBoxOpenSource.Checked.ToString.ToUpper & "</OpenSource>" & vbNewLine _
            & "<InHouse>" & CheckBoxInHouse.Checked.ToString.ToUpper & "</InHouse>" & vbNewLine

    End Sub

    Private Sub code_personalities()
        codepersonalities.Text =
            "<PersonalityGraph>" & vbNewLine _
            & "<Personalities>" & vbNewLine _
            & "<Personality>" & vbNewLine _
            & "<Name>" & txtboxNameofPersonality.Text & "</Name>" & vbNewLine _
            & "<Aptitude>" & comboxAptitude.Text & "</Aptitude>" & vbNewLine _
            & "<Leadership>" & comboxLeadership.Text & "</Leadership>" & vbNewLine _
            & "<Diligence>" & comboxDiligence.Text & "</Founded>" & vbNewLine _
            & "<Relationships>" & vbNewLine _
            & "<Relation Name = " & comboxRelation.Text & ">" & comboxRelation2.Text & "</Relation>" & vbNewLine _
            & "</Relationships>" & vbNewLine _
            & "</Personality>" & vbNewLine _
            & "</Personalities>" & vbNewLine _
            & "<Incompatibilities>" & vbNewLine _
            & "</Incompatibilities>" & vbNewLine _
            & "</PersonalityGraph>"
    End Sub

    Private Sub Personality_TextChanged(sender As Object, e As EventArgs) Handles txtboxNameofPersonality.TextChanged, comboxAptitude.SelectedIndexChanged, comboxLeadership.SelectedIndexChanged, comboxDiligence.SelectedIndexChanged, comboxRelation.SelectedIndexChanged, comboxRelation2.SelectedIndexChanged
        code_personalities()
    End Sub

    Private Sub code_softwaretype()
        codesoftwaretypes.Text =
            "<SoftwareType>" & vbNewLine _
            & "<Name>" & TextBoxNameOfSoftwareType.Text & "</Name>" & vbNewLine _
            & "<Description>" & SoftwareTypeDescription.Text & "</Description>" & vbNewLine _
            & "<Random>" & ComboBoxRandom.Text & "</Random>" & vbNewLine _
            & "<Popularity>" & ComboBoxPopularitySoftwareTypes.Text & "</Popularity>" & vbNewLine _
            & "<OSSpecific>" & CheckBoxOSSpecific.Checked.ToString.ToUpper & "</OSSpecific>" & vbNewLine _
            & "<OneClient>" & CheckBoxOneClient.Checked.ToString.ToUpper & "</OneClient>" & vbNewLine _
            & "<InHouse>" & CheckBoxInHouseSoftwareTypes.Checked.ToString.ToUpper & "</InHouse>" & vbNewLine _
            & "<Category>" & ComboBoxCategory.Text & "</Category>" & vbNewLine _
            & "<NameGenerator>" & TextBoxNameGenerator.Text & "</NameGenerator>" & vbNewLine _
            & "<Needs>" & vbNewLine _
            & "<Name>" & ComboBoxNeeds.Text & "</Name>" & vbNewLine _
            & "</Needs>" & vbNewLine _
            & "<Features>" & vbNewLine _
            & codefeatures.Text & vbNewLine _
            & "</Features>" & vbNewLine _
            & "</SoftwareType>"
    End Sub

    Private Sub SoftwareType_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNameOfSoftwareType.TextChanged, SoftwareTypeDescription.TextChanged, ComboBoxRandom.SelectedIndexChanged, ComboBoxPopularitySoftwareTypes.SelectedIndexChanged, CheckBoxOSSpecific.CheckedChanged, CheckBoxOneClient.CheckedChanged, CheckBoxInHouseSoftwareTypes.CheckedChanged, ComboBoxCategory.SelectedIndexChanged, TextBoxNameGenerator.TextChanged, ComboBoxNeeds.SelectedIndexChanged
        code_softwaretype()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CheckBoxForced.Checked Then
            codefeatures.Text +=
                "<Feature Forced=" & CheckBoxForced.Checked.ToString.ToUpper & ">" & vbNewLine
        ElseIf Len(TextBoxFeatureFrom.Text) > 0
            codefeatures.Text +=
                  "<Feature From=" & TextBoxFeatureFrom.Text & ">" & vbNewLine
        Else
            codefeatures.Text +=
                 "<Feature>" & vbNewLine
        End If

        code_features()
        code_softwaretype()
    End Sub

    Private Sub code_features()
        codefeatures.Text +=
            "<Name>" & TextBoxNameOfFeature.Text & "</Name>" & vbNewLine _
            & "<Description>" & DescriptionFeature.Text & "</Description>" & vbNewLine _
            & "<DevTime>" & NumericUpDownDevTime.Value & "</DevTime>" & vbNewLine _
            & "<Innovation>" & NumericUpDownInnovation.Value & "</Innovation>" & vbNewLine _
            & "<Usability>" & NumericUpDownUsability.Value & "</Usability>" & vbNewLine _
            & "<Stability>" & NumericUpDownStability.Value & "</Stability>" & vbNewLine _
            & "<CodeArt>" & ComboBoxCodeArt.Text & "</CodeArt>" & vbNewLine _
            & "<Dependencies>" & vbNewLine _
            & "<Dependency Software=" & ComboBoxDependencies.Text & ">" & DependenciesDescription.Text & "</Dependency>" & vbNewLine _
            & "</Dependencies>" & vbNewLine _
            & "</Feature>"
    End Sub

    Private Sub code_companytypecode()
        codecompanytype.Text =
            "<CompanyType> " & vbNewLine _
            & "<Specialization>" & comboxSpecialization.Text & "</Specialization>" & vbNewLine _
            & "<Chance>" & numupdownChance.Text & "</Chance>" & vbNewLine _
            & "<Types>" & vbNewLine _
            & "<Type Name=" & comboxSoftware.Text & ">" & comboxSoftware2.Text & "<Type>" & vbNewLine _
            & "</Types>" & vbNewLine _
            & "</CompanyType>"
    End Sub

    Private Sub CompanyType_TextChanged(sender As Object, e As EventArgs) Handles comboxSpecialization.SelectedIndexChanged, numupdownChance.ValueChanged, comboxSoftware.SelectedIndexChanged, comboxSoftware2.SelectedIndexChanged
        code_companytypecode()
    End Sub

    Private Sub code_event()
        codeevent.Text +=
            "<Event>" & vbNewLine _
            & "<Name>" & TextBoxNameOfEvent.Text & "</Name>" & vbNewLine _
            & "<Companies>" & vbNewLine _
            & "<Company>" & TextBoxCompany.Text & "</Company>" & vbNewLine _
            & "</Companies>" & vbNewLine _
            & "</Event>"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        code_event()
    End Sub

    Private Sub code_scenario()
        codescenario.Text =
            "<Scenario>" & vbNewLine _
            & "<Name>" & TextBoxNameOfScenario.Text & "</Name>" & vbNewLine _
            & "<Money>" & vbNewLine _
            & "<Amount>" & TextBoxMoney.Text & "</Amount>" & vbNewLine _
            & "</Money>" & vbNewLine _
            & "<Goals>" & vbNewLine _
            & "<Goal>" & TextBoxGoals.Text & "</Goal>" & vbNewLine _
            & "</Goals>" & vbNewLine _
            & "<Years>" & vbNewLine _
            & "<Year>" & TextBoxYears.Text & "</Year>" & vbNewLine _
            & "</Years>" & vbNewLine _
            & "<Events>" & vbNewLine _
            & "<Event>" & TextBoxEvents.Text & "</Event>" & vbNewLine _
            & "</Events>" & vbNewLine _
            & "<Simulation>" & CheckBoxSimulation.Checked.ToString.ToUpper & "</Simulation>" & vbNewLine _
            & "</Scenario>"
    End Sub

    Private Sub Scenario_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNameOfScenario.TextChanged, TextBoxMoney.TextChanged, TextBoxGoals.TextChanged, TextBoxYears.TextChanged, TextBoxEvents.TextChanged, CheckBoxSimulation.CheckedChanged
        code_scenario()
    End Sub

    Private Sub updateapp()
        Try
            File.Delete("update.txt")
            My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/XeCuSoft/Software.Inc-EZ-Creation-Tool/master/update.txt", "update.txt")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Update - Error")
        End Try

        If File.Exists("update.txt") Then
            Dim sr As StreamReader = New StreamReader("update.txt")

            Select Case sr.ReadLine().ToString()
                Case My.Application.Info.Version.ToString()
                    'File.Delete("update.txt")
                Case Else
                    updatebutton.Visible = True
            End Select
            sr.Close()
            File.Delete("update.txt")
        Else
            MsgBox("Error - Update server could not be found!", MsgBoxStyle.Exclamation, "Update - Error")
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        TabControl1.TabPages.Add(TabPage5)
        TabControl1.SelectedTab = TabPage5
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.TabPages.Remove(TabPage5)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click, ToolStripMenuItem3.Click
        StatusLabel.Text = "Status : Coming when it done"
    End Sub

    Private Sub updatebutton_Click(sender As Object, e As EventArgs) Handles updatebutton.Click
        Process.Start("https://github.com/XeCuSoft/Software.Inc-EZ-Creation-Tool/releases")
    End Sub
End Class