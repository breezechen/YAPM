' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Public Class frmAbout

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim s As String
        s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Microsoft Sans Serif;}{\f1\fmodern\fprq1\fcharset0 Courier New;}}" & vbNewLine
        s &= "{\colortbl ;\red0\green0\blue255;}" & vbNewLine
        s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs24 Yet Another Process Monitor (YAPM)\par" & vbNewLine
        s &= "By violent_ken\fs20\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "\b Executable informations\b0\par" & vbNewLine
        s &= "Version : 1.0.0.0.0 Beta 2\par" & vbNewLine
        s &= "Release date : 01/01/2009\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "Please visit YAPM Sourceforge.net page :\par" & vbNewLine
        s &= "\cf1\ul https://sourceforge.net/projects/yaprocmon/\cf0\ulnone\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "and YAPM website :\par" & vbNewLine
        s &= "\cf1\ul http://yaprocmon.sourceforge.net/\cf0\ulnone\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "\b License\b0\par" & vbNewLine
        s &= "\f1\fs18 This program is under GNU GPL 2.0.\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "0. This License applies to any program or other work which contains a notice placed by the copyright holder saying it may be distributed under the terms of this General Public License. The " & Chr(34) & "Program" & Chr(34) & ", below, refers to any such program or work, and a " & Chr(34) & "work based on the Program" & Chr(34) & " means either the Program or any derivative work under copyright law: that is to say, a work containing the Program or a portion of it, either verbatim or with modifications and/or translated into another language. (Hereinafter, translation is included without limitation in the term " & Chr(34) & "modification" & Chr(34) & ".) Each licensee is addressed as " & Chr(34) & "you" & Chr(34) & ". \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "Activities other than copying, distribution and modification are not covered by this License; they are outside its scope. The act of running the Program is not restricted, and the output from the Program is covered only if its contents constitute a work based on the Program (independent of having been made by running the Program). Whether that is true depends on what the Program does. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "1. You may copy and distribute verbatim copies of the Program's source code as you receive it, in any medium, provided that you conspicuously and appropriately publish on each copy an appropriate copyright notice and disclaimer of warranty; keep intact all the notices that refer to this License and to the absence of any warranty; and give any other recipients of the Program a copy of this License along with the Program. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "You may charge a fee for the physical act of transferring a copy, and you may at your option offer warranty protection in exchange for a fee. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "2. You may modify your copy or copies of the Program or any portion of it, thus forming a work based on the Program, and copy and distribute such modifications or work under the terms of Section 1 above, provided that you also meet all of these conditions: \par" & vbNewLine
        s &= "a) You must cause the modified files to carry prominent notices stating that you changed the files and the date of any change. \par" & vbNewLine
        s &= "b) You must cause any work that you distribute or publish, that in whole or in part contains or is derived from the Program or any part thereof, to be licensed as a whole at no charge to all third parties under the terms of this License. \par" & vbNewLine
        s &= "c) If the modified program normally reads commands interactively when run, you must cause it, when started running for such interactive use in the most ordinary way, to print or display an announcement including an appropriate copyright notice and a notice that there is no warranty (or else, saying that you provide a warranty) and that users may redistribute the program under these conditions, and telling the user how to view a copy of this License. (Exception: if the Program itself is interactive but does not normally print such an announcement, your work based on the Program is not required to print an announcement.) \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "These requirements apply to the modified work as a whole. If identifiable sections of that work are not derived from the Program, and can be reasonably considered independent and separate works in themselves, then this License, and its terms, do not apply to those sections when you distribute them as separate works. But when you distribute the same sections as part of a whole which is a work based on the Program, the distribution of the whole must be on the terms of this License, whose permissions for other licensees extend to the entire whole, and thus to each and every part regardless of who wrote it. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "Thus, it is not the intent of this section to claim rights or contest your rights to work written entirely by you; rather, the intent is to exercise the right to control the distribution of derivative or collective works based on the Program. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "In addition, mere aggregation of another work not based on the Program with the Program (or with a work based on the Program) on a volume of a storage or distribution medium does not bring the other work under the scope of this License. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "3. You may copy and distribute the Program (or a work based on it, under Section 2) in object code or executable form under the terms of Sections 1 and 2 above provided that you also do one of the following: \par" & vbNewLine
        s &= "a) Accompany it with the complete corresponding machine-readable source code, which must be distributed under the terms of Sections 1 and 2 above on a medium customarily used for software interchange; or, \par" & vbNewLine
        s &= "b) Accompany it with a written offer, valid for at least three years, to give any third party, for a charge no more than your cost of physically performing source distribution, a complete machine-readable copy of the corresponding source code, to be distributed under the terms of Sections 1 and 2 above on a medium customarily used for software interchange; or, \par" & vbNewLine
        s &= "c) Accompany it with the information you received as to the offer to distribute corresponding source code. (This alternative is allowed only for noncommercial distribution and only if you received the program in object code or executable form with such an offer, in accord with Subsection b above.) \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "The source code for a work means the preferred form of the work for making modifications to it. For an executable work, complete source code means all the source code for all modules it contains, plus any associated interface definition files, plus the scripts used to control compilation and installation of the executable. However, as a special exception, the source code distributed need not include anything that is normally distributed (in either source or binary form) with the major components (compiler, kernel, and so on) of the operating system on which the executable runs, unless that component itself accompanies the executable. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "If distribution of executable or object code is made by offering access to copy from a designated place, then offering equivalent access to copy the source code from the same place counts as distribution of the source code, even though third parties are not compelled to copy the source along with the object code. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "4. You may not copy, modify, sublicense, or distribute the Program except as expressly provided under this License. Any attempt otherwise to copy, modify, sublicense or distribute the Program is void, and will automatically terminate your rights under this License. However, parties who have received copies, or rights, from you under this License will not have their licenses terminated so long as such parties remain in full compliance. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "5. You are not required to accept this License, since you have not signed it. However, nothing else grants you permission to modify or distribute the Program or its derivative works. These actions are prohibited by law if you do not accept this License. Therefore, by modifying or distributing the Program (or any work based on the Program), you indicate your acceptance of this License to do so, and all its terms and conditions for copying, distributing or modifying the Program or works based on it. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "6. Each time you redistribute the Program (or any work based on the Program), the recipient automatically receives a license from the original licensor to copy, distribute or modify the Program subject to these terms and conditions. You may not impose any further restrictions on the recipients' exercise of the rights granted herein. You are not responsible for enforcing compliance by third parties to this License. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "7. If, as a consequence of a court judgment or allegation of patent infringement or for any other reason (not limited to patent issues), conditions are imposed on you (whether by court order, agreement or otherwise) that contradict the conditions of this License, they do not excuse you from the conditions of this License. If you cannot distribute so as to satisfy simultaneously your obligations under this License and any other pertinent obligations, then as a consequence you may not distribute the Program at all. For example, if a patent license would not permit royalty-free redistribution of the Program by all those who receive copies directly or indirectly through you, then the only way you could satisfy both it and this License would be to refrain entirely from distribution of the Program. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "If any portion of this section is held invalid or unenforceable under any particular circumstance, the balance of the section is intended to apply and the section as a whole is intended to apply in other circumstances. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "It is not the purpose of this section to induce you to infringe any patents or other property right claims or to contest validity of any such claims; this section has the sole purpose of protecting the integrity of the free software distribution system, which is implemented by public license practices. Many people have made generous contributions to the wide range of software distributed through that system in reliance on consistent application of that system; it is up to the author/donor to decide if he or she is willing to distribute software through any other system and a licensee cannot impose that choice. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "This section is intended to make thoroughly clear what is believed to be a consequence of the rest of this License. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "8. If the distribution and/or use of the Program is restricted in certain countries either by patents or by copyrighted interfaces, the original copyright holder who places the Program under this License may add an explicit geographical distribution limitation excluding those countries, so that distribution is permitted only in or among countries not thus excluded. In such case, this License incorporates the limitation as if written in the body of this License. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "9. The Free Software Foundation may publish revised and/or new versions of the General Public License from time to time. Such new versions will be similar in spirit to the present version, but may differ in detail to address new problems or concerns. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "Each version is given a distinguishing version number. If the Program specifies a version number of this License which applies to it and " & Chr(34) & "any later version" & Chr(34) & ", you have the option of following the terms and conditions either of that version or of any later version published by the Free Software Foundation. If the Program does not specify a version number of this License, you may choose any version ever published by the Free Software Foundation. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "10. If you wish to incorporate parts of the Program into other free programs whose distribution conditions are different, write to the author to ask for permission. For software which is copyrighted by the Free Software Foundation, write to the Free Software Foundation; we sometimes make exceptions for this. Our decision will be guided by the two goals of preserving the free status of all derivatives of our free software and of promoting the sharing and reuse of software generally. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "NO WARRANTY\par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "11. BECAUSE THE PROGRAM IS LICENSED FREE OF CHARGE, THERE IS NO WARRANTY FOR THE PROGRAM, TO THE EXTENT PERMITTED BY APPLICABLE LAW. EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT HOLDERS AND/OR OTHER PARTIES PROVIDE THE PROGRAM " & Chr(34) & "AS IS" & Chr(34) & " WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION. \par" & vbNewLine
        s &= "\par" & vbNewLine
        s &= "12. IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING WILL ANY COPYRIGHT HOLDER, OR ANY OTHER PARTY WHO MAY MODIFY AND/OR REDISTRIBUTE THE PROGRAM AS PERMITTED ABOVE, BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF SUCH HOLDER OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. \par" & vbNewLine
        s &= "END OF TERMS AND CONDITIONS\par" & vbNewLine
        s &= "}"
        Me.rtb.Rtf = s
    End Sub

    Private Sub lnklblSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblSF.LinkClicked
        cFile.ShellOpenFile("http://yaprocmon.sourceforge.net/")
    End Sub
End Class