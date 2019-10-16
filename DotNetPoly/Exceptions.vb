Public Class DotNetPolyException
    Inherits Exception

    Sub New(pMessage As String)
        MyBase.New(pMessage)
    End Sub
End Class
