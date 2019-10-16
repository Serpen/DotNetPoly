'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015
' Time: 12:19
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class StartField
	Inherits GameField
	
	Sub New()
		MyBase.new("Start")
	End Sub

    Public Const GIVECASHBASE As UInteger = 10

    Overrides Sub MoveOver(pPlayer As Player)
        MyBase.MoveOver(pPlayer)
        GameBoard.BANK.MoneyTransferTo(pPlayer, GIVECASHBASE)
        Console.WriteLine("Spieler {0} erhält {1}", pPlayer.Name, GIVECASHBASE)
    End Sub
	
	Public Overrides Sub MoveOn(pPlayer As Player)
		MyBase.MoveOn(pPlayer)
        GameBoard.BANK.MoneyTransferTo(pPlayer, GIVECASHBASE * 2)
        Console.WriteLine("Spieler {0} erhält {1}", pPlayer.Name, GIVECASHBASE * 2)
    End Sub
	
	
End Class
