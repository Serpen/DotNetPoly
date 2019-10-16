'
' Created by SharpDevelop.
' User: serpe_000
' Date: 22.11.2015
' Time: 15:03
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Interface IBoardAssignment
	ReadOnly Property GameBoard As GameBoard
	Sub AssignToBoard(bBoard As GameBoard)
	
End Interface
